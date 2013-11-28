using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using XISMobileEAPlugin.InteractionSpace;
using System.Reflection;
using System.IO;

namespace XISMobileEAPlugin
{
    [ComVisible(true)]
    public class CodeGenerator
    {
        // define menu constants
        private const string menuHeader = "-&XIS-Mobile Plugin";
        private const string menuValidateModel = "&Validate Model";
        private const string menuGenerateModels = "&Generate Models";
        private const string menuGenerateCode = "&Generate Code";
        public Rules rules;

        public CodeGenerator()
        {
            rules = new Rules();
        }

        ///
        /// Called Before EA starts to check Add-In Exists
        /// Nothing is done here.
        /// This operation needs to exists for the addin to work
        ///
        /// <param name="Repository" />the EA repository
        /// a string
        public String EA_Connect(EA.Repository Repository)
        {
            //No special processing required.
            return "XISMobileEAPlugin.CodeGenerator connected";
        }
 
        ///
        /// Called when user Clicks Add-Ins Menu item from within EA.
        /// Populates the Menu with our desired selections.
        /// Location can be "TreeView" "MainMenu" or "Diagram".
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        ///
        public object EA_GetMenuItems(EA.Repository Repository, string Location, string MenuName)
        {
            switch (MenuName)
            {
                // defines the top level menu option
                case "":
                    return menuHeader;
                // defines the submenu options
                case menuHeader:
                    return new string[] {
                        menuValidateModel,
                        menuGenerateModels,
                        menuGenerateCode
                    };
                default:
                    return "";
            }
        }
 
        ///
        /// returns true if a project is currently opened
        ///
        /// <param name="Repository" />the repository
        /// true if a project is opened in EA
        bool IsProjectOpen(EA.Repository Repository)
        {
            try
            {
                return Repository.Models != null;
            }
            catch
            {
                return false;
            }
        }
 
        ///
        /// Called once Menu has been opened to see what menu items should active.
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        /// <param name="ItemName" />the name of the menu item
        /// <param name="IsEnabled" />boolean indicating whethe the menu item is enabled
        /// <param name="IsChecked" />boolean indicating whether the menu is checked
        public void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            if (IsProjectOpen(Repository))
            {
                switch (ItemName)
                {
                    case menuValidateModel:
                        IsEnabled = true;
                        break;
                    case menuGenerateModels:
                        IsEnabled = true;
                        break;
                    case menuGenerateCode:
                        IsEnabled = true;
                        break;
                    default:
                        IsEnabled = false;
                        break;
                }
            }
            else
            {
                // If no open project, disable all menu options
                IsEnabled = false;
            }
        }
 
        ///
        /// Called when user makes a selection in the menu.
        /// This is your main exit point to the rest of your Add-in
        ///
        /// <param name="Repository" />the repository
        /// <param name="Location" />the location of the menu
        /// <param name="MenuName" />the name of the menu
        /// <param name="ItemName" />the name of the selected menu item
        public void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                // user has clicked the 'Generate Code' menu option
                case menuValidateModel:
                    this.ValidateModel(Repository);
                    break;
                case menuGenerateModels:
                    this.GenerateModels(Repository);
                    break;
                case menuGenerateCode:
                    this.GenerateCode(Repository);
                    break;
                default:
                    break;
            }
        }

        #region Rules Region
        
        public void EA_OnInitializeUserRules(EA.Repository Repository)
        {
            if (Repository != null)
            {
                rules.ConfigureCategories(Repository);
                rules.ConfigureRules(Repository);
                rules.isValid = true;
            }
        }

        public void EA_OnRunPackageRule(EA.Repository Repository, string RuleID, long PackageID)
        {
            rules.RunPackageRule(Repository, RuleID, PackageID);
        }

        public void EA_OnRunDiagramRule(EA.Repository Repository, string RuleID, long lDiagramID)
        {
            rules.RunDiagramRule(Repository, RuleID, lDiagramID);
        }

        public void EA_OnRunElementRule(EA.Repository Repository, string RuleID, EA.Element element)
        {
            rules.RunElementRule(Repository, RuleID, element);
        }

        public void EA_OnRunAttributeRule(EA.Repository Repository, string RuleID, string AttGUID, long lObjectID)
        {
            // DO NOTHING
        }

        public void EA_OnRunConnectorRule(EA.Repository Repository, string RuleID, long lConnectorID)
        {
            rules.RunConnectorRule(Repository, RuleID, lConnectorID);
        }

        public void EA_OnRunMethodRule(EA.Repository Repository, string RuleID, string MethodGUID, long ObjectID)
        {
            rules.RunMethodRule(Repository, RuleID, MethodGUID, ObjectID);
        }

        public void EA_OnEndValidation(EA.Repository Repository, object Args)
        {
            if (!rules.isValid)
            {
                MessageBox.Show("Validation ended with errors");
            }
            else
            {
                MessageBox.Show("Validation ended successfully");
            }
        }

        #endregion

        public String EA_OnInitializeTechnologies(EA.Repository Repository)
        {
            string technology = "";
            string mdgPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            mdgPath += "\\XIS-Mobile MDG\\XIS-Mobile Technology.xml";
            Assembly assem = this.GetType().Assembly;

            using (Stream stream = File.OpenRead(mdgPath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        technology = reader.ReadToEnd();
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error initializing XIS-Mobile MDG Technology " + e.Message);
                }
            }
            return technology;
        }

        private void ValidateModel(EA.Repository Repository)
        {
            EA.Package package = (EA.Package)Repository.Models.GetAt(0);
            EA.Project project = Repository.GetProjectInterface();
            project.ValidatePackage(package.PackageGUID);
        }

        private void GenerateModels(EA.Repository Repository)
        {
            EA.Package rootModel = (EA.Package)Repository.Models.GetAt(0);
            EA.Package rootPackage = (EA.Package)rootModel.Packages.GetAt(0);
            EA.Package useCaseView = null;
            EA.Package navigationView = null;
            EA.Package interactionView = null;

            foreach (EA.Package package in rootPackage.Packages)
            {
                if (package.StereotypeEx == "InteractionSpace View")
                {
                    interactionView = package;
                }
                else if (package.StereotypeEx == "NavigationSpace View")
                {
                    navigationView = package;
                }
                else if (package.StereotypeEx == "UseCases View")
                {
                    useCaseView = package;
                }
            }

            if (useCaseView != null)
            {
                EA.Element startingUC = null;
                List<EA.Element> useCases = new List<EA.Element>();
                // Process all Use Cases
                foreach (EA.Element element in useCaseView.Elements)
                {
                    if (element.Type == "UseCase")
                    {
                        bool isStartingUseCase = bool.Parse(M2MTransformer.GetTaggedValue(element.TaggedValues, "isStartingUseCase").Value);

                        if (isStartingUseCase)
                        {
                            startingUC = element;
                        }
                        else
                        {
                            useCases.Add(element);
                        }
                    }
                }

                if (startingUC != null)
                {
                    useCases.Insert(0, startingUC);
                    
                    if (useCases.Count > 0)
                    {
                        if (useCases.Count < 2)
                        {
                            M2MTransformer.ProcessUseCase(Repository, navigationView, interactionView, useCases);
                        }
                        else
                        {
                            new ModelGenerationForm(Repository, navigationView, interactionView, useCases).Show();
                        }
                    }
                }
            }
        }

        private void GenerateCode(EA.Repository Repository)
        {
            new CodeGenerationForm(Repository).Show();
        }
 
        ///
        /// EA calls this operation when it exits. Can be used to do some cleanup work.
        ///
        public void EA_Disconnect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
 
    }
}
