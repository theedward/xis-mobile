using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace XISMobileEAPlugin
{
    public class Rules
    {
        private string m_sCategoryID;
        private ArrayList m_RuleIDs;
        private ArrayList m_RuleIDEx;
        public bool isValid;

        // Domain View Rules
        private const string rule01 = "Rule01";
        private const string rule02 = "Rule02";
        private const string rule03 = "Rule03";
        private const string rule04 = "Rule04";
        private const string rule05 = "Rule05";
        private const string rule06 = "Rule06";
        private const string rule07 = "Rule07";
        private const string rule08 = "Rule08";
        private const string rule09 = "Rule09";
        private const string rule10 = "Rule10";
        private const string rule11 = "Rule11";
        private const string rule12 = "Rule12";
        // BusinessEntities View Rules
        private const string rule13 = "Rule13";
        private const string rule14 = "Rule14";
        private const string rule15 = "Rule15";
        private const string rule16 = "Rule16";
        // UseCases View Rules
        private const string rule17 = "Rule17";
        private const string rule18 = "Rule18";
        private const string rule19 = "Rule19";
        private const string rule20 = "Rule20";
        private const string rule21 = "Rule21";
        private const string rule22 = "Rule22";
        // Architectural View Rules
        private const string rule23 = "Rule23";
        private const string rule24 = "Rule24";
        private const string rule25 = "Rule25";
        private const string rule26 = "Rule26";
        private const string rule27 = "Rule27";
        private const string rule28 = "Rule28";
        private const string rule29 = "Rule29";
        // NavigationSpace View Rules
        private const string rule30 = "Rule30";
        // InteractionSpace View Rules
        private const string rule31 = "Rule31";
        private const string rule32 = "Rule32";
        private const string rule33 = "Rule33";
        private const string rule34 = "Rule34";
        private const string rule35 = "Rule35";
        private const string rule36 = "Rule36";
        private const string rule37 = "Rule37";
        private const string rule38 = "Rule38";

        public Rules()
        {
            m_RuleIDs = new ArrayList();
            m_RuleIDEx = new ArrayList();
            isValid = false;
        }

        private string LookupMap(string sKey)
        {
            return DoLookupMap(sKey, m_RuleIDs, m_RuleIDEx);
        }

        private string LookupMapEx(string sRule)
        {
            return DoLookupMap(sRule, m_RuleIDEx, m_RuleIDs);
        }

        private string DoLookupMap(string sKey, ArrayList arrValues, ArrayList arrKeys)
        {
            if (arrKeys.Contains(sKey))
                return arrValues[arrKeys.IndexOf(sKey)].ToString();
            else
                return "";
        }

        private void AddToMap(string sRuleID, string sKey)
        {
            m_RuleIDs.Add(sRuleID);
            m_RuleIDEx.Add(sKey);
        }

        private string GetRuleStr(string sRuleID)
        {
            switch (sRuleID)
            {
                case rule01:
                    return "The model must be composed of at least 3 views (BusinessEntities, Domain and UseCases views)!";
                case rule02:
                    return "The model must be composed of at least 4 views (Architectural, BusinessEntities, Domain and UseCases views)!";
                case rule03:
                    return "The model must be composed of at least 4 views (BusinessEntities, Domain, InteractionSpace and NavigationSpace views)!";
                case rule04:
                    return "The model must be composed of 5 views (Architectural, BusinessEntities, Domain, InteractionSpace and NavigationSpace views)!";
                case rule05:
                    return "A XisEntity must have at least 1 XisEntityAttribute!";
                case rule06:
                    return "A XisEntity must only have attributes with stereotype «XisEntityAttribute»!";
                case rule07:
                    return "XisEntities must be connected only by XisEntityAssociation or XisEntityInheritance!";
                case rule08:
                    return "A XisEntityAssociation must only connect XisEntities!";
                case rule09:
                    return "A XisEntityInheritance must only connect XisEntities!";
                case rule10:
                    return "A XisEnumeration must have at least 1 XisEnumerationValue!";
                case rule11:
                    return "A XisEnumeration must only have attributes with stereotype «XisEnumerationValue»!";
                case rule12:
                    return "A XisEntityAttribute must have a valid type!";
                case rule13:
                    return "A XisBE-EntityMasterAssociation must connect a XisBusinessEntity (source) to a XisEntity (target)!";
                case rule14:
                    return "A XisBE-EntityDetailAssociation must connect a XisBusinessEntity (source) to a XisEntity (target)!";
                case rule15:
                    return "A XisBE-EntityReferenceAssociation must connect a XisBusinessEntity (source) to a XisEntity (target)!";
                case rule16:
                    return "XisBusinessEntities must have 1 XisBE-EntityMasterAssociation!";
                case rule17:
                    return "There must be 1 starting XisUseCase!";
                case rule18:
                    return "XisEntityUseCases must be connected to XisBusinessEntities by XisEntityUC-BEAssociations!";
                case rule19:
                    return "XisServiceUseCases must be connected to XisBusinessEntities and/or XisProviders by XisServiceUC-BEAssociations and XisServiceUC-ProviderAssociations, respectively!";
                case rule20:
                    return "A XisEntityUC-BEAssociation must connect a XisEntityUseCase (source) to a XisBusinessEntity (target)!";
                case rule21:
                    return "A XisServiceUC-BEAssociation must connect a XisServiceUseCase (source) to a XisBusinessEntity (target)!";
                case rule22:
                    return "A XisServiceUC-ProviderAssociation must connect a XisServiceUseCase (source) to a XisProvider (target)!";
                case rule23:
                    return "XisMobileApp must be connected to XisServices by XisMobileApp-ServiceAssociations!";
                case rule24:
                    return "A XisMobileApp-ServiceAssociation must connect XisMobileApp (source) to a XisService (target)!";
                case rule25:
                    return "A XisService must have at least 1 XisServiceMethod!";
                case rule26:
                    return "A XisService must only have methods with stereotype «XisMethod»!";
                case rule27:
                    return "A XisInternalProvider must realize a XisInternalService!";
                case rule28:
                    return "A XisServer must realize a XisRemoteService!";
                case rule29:
                    return "A XisClientMobileApp must realize a XisRemoteService!";
                case rule30:
                    return "A XisNavigationAssociation must only connect XisInteractionSpaces!";
                case rule31:
                    return "There must be 1 XisInteractionSpace that is the main screen!";
                case rule32:
                    return "A XisInteractionSpace must have at least 1 XisWidget!";
                case rule33:
                    return "All XisInteractionSpace elements must be XisWidgets!";
                case rule34:
                    return "A XisIS-BEAssociation must connect a XisInteractionSpace (source) to a XisBusinessEntity (target)!";
                case rule35:
                    return "A XisIS-MenuAssociation must connect a XisInteractionSpace (source) to a XisMenu (target)!";
                case rule36:
                    return "A XisIS-DialogAssociation must connect a XisInteractionSpace (source) to a XisDialog (target)!";
                case rule37:
                    return "";
                case rule38:
                    return "";
                //case rule04A:
                //    // validar tipos attrs
                //    return "XisInteractionSpace must contain at least 1 XisWidget!";
                //case rule04B:
                //    return "XisInteractionSpace must be only composed of XisWidgets!";
                //case rule05:
                //    return "XisLists can only be composed of XisListGroups or XisListItems!";
                //case rule06:
                //    return "XisGestures must have 1 XisAction!";
                //case rule07:
                //    return "XisActions must be owned only by XisGestures!";
                //case rule08:
                //    return "All XisActions parameters must be XisArguments!";
                //case rule09A:
                //    return "XisActions must have a type!";
                //case rule09B:
                //    return "XisActions of type 'NavigateTo' must have a XisArgument named 'navigationSpace'!";
                //case rule09C:
                //    return "XisActions of type 'WebService' must have a XisArgument named 'url'!";
                default:
                    return "";
            }
        }

        public void ConfigureCategories(EA.Repository Repository)
        {
            m_sCategoryID = Repository.GetProjectInterface().DefineRuleCategory("XIS-Mobile Rules");
        }

        public void ConfigureRules(EA.Repository Repository)
        {
            EA.Project Project = Repository.GetProjectInterface();
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule01)), rule01);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule05)), rule05);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule06)), rule06);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule07)), rule07);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule08)), rule08);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule09)), rule09);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule10)), rule10);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule11)), rule11);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule12)), rule12);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule13)), rule13);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule14)), rule14);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule15)), rule15);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule16)), rule16);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule17)), rule17);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule18)), rule18);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule19)), rule19);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule20)), rule20);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule21)), rule21);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule22)), rule22);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule23)), rule23);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule24)), rule24);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule25)), rule25);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule26)), rule26);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule27)), rule27);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule28)), rule28);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule29)), rule29);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule30)), rule30);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule31)), rule31);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule32)), rule32);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule33)), rule33);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule34)), rule34);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule35)), rule35);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule36)), rule36);
            // TODO: expand this list
        }

        public void RunPackageRule(EA.Repository Repository, string sRuleID, long PackageID)
        {
            EA.Package Package = Repository.GetPackageByID((int)PackageID);
            if (Package != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    case rule01:
                        DoRule01_04(Repository, Package);
                        break;
                    case rule17:
                        DoRule17(Repository, Package);
                        break;
                    case rule31:
                        DoRule31(Repository, Package);
                        break;
                    default:
                        break;
                }
            }
        }

        public void RunDiagramRule(EA.Repository Repository, string sRuleID, long lDiagramID)
        {
            EA.Diagram Diagram = Repository.GetDiagramByID((int)lDiagramID);
            if (Diagram != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    //case rule05:
                    //    DoRule05(Repository, lDiagramID);
                    //    break;
                    default:
                        break;
                }
            }
        }

        public void RunElementRule(EA.Repository Repository, string sRuleID, EA.Element Element)
        {
            if (Element != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    case rule05:
                        DoRule05(Repository, Element);
                        break;
                    case rule06:
                        DoRule06(Repository, Element);
                        break;
                    case rule10:
                        DoRule10(Repository, Element);
                        break;
                    case rule11:
                        DoRule11(Repository, Element);
                        break;
                    case rule16:
                        DoRule16(Repository, Element);
                        break;
                    case rule18:
                        DoRule18(Repository, Element);
                        break;
                    case rule19:
                        DoRule19(Repository, Element);
                        break;
                    case rule23:
                        DoRule23(Repository, Element);
                        break;
                    case rule25:
                        DoRule25(Repository, Element);
                        break;
                    case rule26:
                        DoRule26(Repository, Element);
                        break;
                    case rule27:
                        DoRule27(Repository, Element);
                        break;
                    case rule28:
                        DoRule28(Repository, Element);
                        break;
                    case rule29:
                        DoRule29(Repository, Element);
                        break;
                    case rule32:
                        DoRule32(Repository, Element);
                        break;
                    case rule33:
                        DoRule33(Repository, Element);
                        break;
                    default:
                        break;
                }
            }
        }

        public void RunAttributeRule(EA.Repository Repository, string sRuleID, string AttributeGUID, long ObjectID)
        {
            EA.Attribute Attribute = Repository.GetAttributeByGuid(AttributeGUID);
            if (Attribute != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    case rule12:
                        DoRule12(Repository, Attribute);
                        break;
                    default:
                        break;
                }
            }
        }

        public void RunConnectorRule(EA.Repository Repository, string sRuleID, long lConnectorID)
        {
            EA.Connector Connector = Repository.GetConnectorByID((int)lConnectorID);
            if (Connector != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    case rule07:
                        DoRule07(Repository, Connector);
                        break;
                    case rule08:
                        DoRule08(Repository, Connector);
                        break;
                    case rule09:
                        DoRule09(Repository, Connector);
                        break;
                    case rule13:
                        DoRule13(Repository, Connector);
                        break;
                    case rule14:
                        DoRule14(Repository, Connector);
                        break;
                    case rule15:
                        DoRule15(Repository, Connector);
                        break;
                    case rule20:
                        DoRule20(Repository, Connector);
                        break;
                    case rule21:
                        DoRule21(Repository, Connector);
                        break;
                    case rule22:
                        DoRule22(Repository, Connector);
                        break;
                    case rule24:
                        DoRule24(Repository, Connector);
                        break;
                    case rule30:
                        DoRule30(Repository, Connector);
                        break;
                    case rule34:
                        DoRule34(Repository, Connector);
                        break;
                    case rule35:
                        DoRule35(Repository, Connector);
                        break;
                    case rule36:
                        DoRule36(Repository, Connector);
                        break;
                    default:
                        break;
                }
            }
        }

        public void RunMethodRule(EA.Repository Repository, string sRuleID, string MethodGUID, long ObjectID)
        {
            EA.Method method = Repository.GetMethodByGuid(MethodGUID);
            if (method != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    //case rule07:
                    //    DoRule07(Repository, method, ObjectID);
                    //    break;
                    default:
                        break;
                }
            }
        }

        // Validate number of views
        private void DoRule01_04(EA.Repository Repository, EA.Package Package)
        {
            EA.Project Project = Repository.GetProjectInterface();
            EA.Package model = (EA.Package)Repository.Models.GetAt(0);
            EA.Package view = (EA.Package)model.Packages.GetAt(0);

            if (view.PackageID == Package.PackageID)
            {
                if (view.Packages.Count < 3)
                {
                    Project.PublishResult(LookupMap(rule01), EA.EnumMVErrorType.mvError, GetRuleStr(rule01));
                    isValid = false;
                }
                else if (view.Packages.Count == 3)
                {
                    EA.Package p = null;
                    bool arch = false;
                    Dictionary<string, int> smart = new Dictionary<string, int>(3);

                    for (short i = 0; i < view.Packages.Count; i++)
                    {
                        p = view.Packages.GetAt(i);

                        if (p.StereotypeEx == "Architectural View")
                        {
                            arch = true;
                            break;
                        }
                        else if (p.StereotypeEx == "BusinessEntities View" || p.StereotypeEx == "Domain View"
                                 || p.StereotypeEx == "UseCases View")
                        {
                            if (smart.ContainsKey(p.StereotypeEx))
                            {
                                smart[p.StereotypeEx] += 1;
                            }
                            else
                            {
                                smart.Add(p.StereotypeEx, 1);
                            }
                        }
                    }

                    if (arch)
                    {
                        Project.PublishResult(LookupMap(rule01), EA.EnumMVErrorType.mvError, GetRuleStr(rule02));
                        isValid = false;
                    }
                    else
                    {
                        if (!smart.ContainsKey("BusinessEntities View") || !smart.ContainsKey("Domain View")
                            || !smart.ContainsKey("UseCases View")
                            || smart["BusinessEntities View"] != 1 || smart["Domain View"] != 1 || smart["UseCases View"] != 1)
                        {
                            Project.PublishResult(LookupMap(rule01), EA.EnumMVErrorType.mvError, GetRuleStr(rule01));
                            isValid = false;
                        }
                    }
                }
                else
                {
                    EA.Package p = null;
                    bool arch = false;
                    Dictionary<string, int> dummy = new Dictionary<string, int>(5);

                    for (short i = 0; i < view.Packages.Count; i++)
                    {
                        p = view.Packages.GetAt(i);

                        if (p.StereotypeEx == "Architectural View")
                        {
                            arch = true;
                            if (dummy.ContainsKey(p.StereotypeEx))
                            {
                                dummy[p.StereotypeEx] += 1;
                            }
                            else
                            {
                                dummy.Add(p.StereotypeEx, 1);
                            }
                        }
                        else if (p.StereotypeEx == "BusinessEntities View" || p.StereotypeEx == "Domain View"
                                 || p.StereotypeEx == "InteractionSpace View" || p.StereotypeEx == "NavigationSpace View")
                        {
                            if (dummy.ContainsKey(p.StereotypeEx))
                            {
                                dummy[p.StereotypeEx] += 1;
                            }
                            else
                            {
                                dummy.Add(p.StereotypeEx, 1);
                            }
                        }
                    }

                    if (arch && (!dummy.ContainsKey("Architectural View") || !dummy.ContainsKey("BusinessEntities View")
                        || !dummy.ContainsKey("Domain View") || !dummy.ContainsKey("InteractionSpace View")
                        || !dummy.ContainsKey("NavigationSpace View") || dummy["Architectural View"] != 1
                        || dummy["BusinessEntities View"] != 1 || dummy["Domain View"] != 1
                        || dummy["InteractionSpace View"] != 1 || dummy["NavigationSpace View"] != 1))
                    {
                        Project.PublishResult(LookupMap(rule01), EA.EnumMVErrorType.mvError, GetRuleStr(rule03));
                        isValid = false;
                    }
                    else if (!dummy.ContainsKey("BusinessEntities View") || !dummy.ContainsKey("Domain View")
                            || !dummy.ContainsKey("InteractionSpace View") || !dummy.ContainsKey("NavigationSpace View")
                            || dummy["BusinessEntities View"] != 1 || dummy["Domain View"] != 1
                            || dummy["InteractionSpace View"] != 1 || dummy["NavigationSpace View"] != 1)
                    {
                        Project.PublishResult(LookupMap(rule01), EA.EnumMVErrorType.mvError, GetRuleStr(rule04));
                        isValid = false;
                    }
                }
            }
        }

        // XisEntities with XisEntityAttributes
        private void DoRule05(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisEntity")
            {
                if (Element.Attributes.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule05), EA.EnumMVErrorType.mvError, GetRuleStr(rule05));
                    isValid = false;
                }
            }
        }

        private void DoRule06(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisEntity")
            {
                if (Element.Attributes.Count > 0)
                {
                    EA.Attribute attr = null;

                    for (short i = 0; i < Element.Attributes.Count; i++)
                    {
                        attr = Element.Attributes.GetAt(i);

                        if (attr.Stereotype != "XisEntityAttribute")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule06), EA.EnumMVErrorType.mvError, GetRuleStr(rule06));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule07(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Project Project = Repository.GetProjectInterface();
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype != "XisEntityAssociation" && Connector.Stereotype != "XisEntityInheritance"
                && client.Stereotype == "XisEntity" && supplier.Stereotype == "XisEntity")
            {
                Project.PublishResult(LookupMap(rule07), EA.EnumMVErrorType.mvError, GetRuleStr(rule07));
                isValid = false;
            }
        }

        private void DoRule08(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Project Project = Repository.GetProjectInterface();
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisEntityAssociation"
                && (client.Stereotype != "XisEntity" || supplier.Stereotype != "XisEntity"))
            {
                Project.PublishResult(LookupMap(rule08), EA.EnumMVErrorType.mvError, GetRuleStr(rule08));
                isValid = false;
            }
        }

        private void DoRule09(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Project Project = Repository.GetProjectInterface();
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisEntityInheritance"
                && (client.Stereotype != "XisEntity" || supplier.Stereotype != "XisEntity"))
            {
                Project.PublishResult(LookupMap(rule09), EA.EnumMVErrorType.mvError, GetRuleStr(rule09));
                isValid = false;
            }
        }

        private void DoRule10(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Stereotype == "XisEnumeration" && Element.Attributes.Count == 0)
            {
                EA.Project Project = Repository.GetProjectInterface();
                Project.PublishResult(LookupMap(rule10), EA.EnumMVErrorType.mvError, GetRuleStr(rule10));
                isValid = false;
            }
        }

        private void DoRule11(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Stereotype == "XisEnumeration" && Element.Attributes.Count > 0)
            {
                EA.Attribute attr = null;
                for (short i = 0; i < Element.Attributes.Count; i++)
                {
                    attr = Element.Attributes.GetAt(i);

                    if (attr.Stereotype != "XisEnumerationValue")
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule11), EA.EnumMVErrorType.mvError, GetRuleStr(rule11));
                        isValid = false;
                        break;
                    }
                }
            }
        }

        private void DoRule12(EA.Repository Repository, EA.Attribute Attribute)
        {
            if (Attribute.Stereotype == "XisEntityAttribute")
            {
                if (string.IsNullOrEmpty(Attribute.Type))
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule12), EA.EnumMVErrorType.mvError, GetRuleStr(rule12));
                    isValid = false;
                }
                else
                {
                    bool primitive = false;
                    switch (Attribute.Type.ToLower())
                    {
                        case "int":
                        case "integer":
                        case "double":
                        case "float":
                        case "long":
                        case "short":
                        case "char":
                        case "string":
                        case "bool":
                        case "boolean":
                        case "byte":
                        case "date":
                        case "time":
                        case "image":
                        case "url":
                            primitive = true;
                            break;
                        default:
                            break;
                    }

                    if (!primitive)
                    {
                        EA.Package package = Repository.GetPackageByID(Repository.GetElementByID(Attribute.ParentID).PackageID);
                        EA.Element el = null;
                        bool exists = false;

                        for (short i = 0; i < package.Elements.Count; i++)
                        {
                            el = package.Elements.GetAt(i);

                            if (el.Name == Attribute.Type && (el.Stereotype == "XisEntity" || el.Stereotype == "XisEntityInheritance"))
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule12), EA.EnumMVErrorType.mvError, GetRuleStr(rule12));
                            isValid = false;
                        }
                    }
                }
            }
        }

        private void DoRule13(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisBE-EntityMasterAssociation"
                && (client.Stereotype != "XisBusinessEntity" || supplier.Stereotype != "XisEntity"))
            {
                EA.Project Project = Repository.GetProjectInterface();
                Project.PublishResult(LookupMap(rule13), EA.EnumMVErrorType.mvError, GetRuleStr(rule13));
                isValid = false;
            }
        }

        private void DoRule14(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisBE-EntityDetailAssociation"
                && (client.Stereotype != "XisBusinessEntity" || supplier.Stereotype != "XisEntity"))
            {
                EA.Project Project = Repository.GetProjectInterface();
                Project.PublishResult(LookupMap(rule14), EA.EnumMVErrorType.mvError, GetRuleStr(rule14));
                isValid = false;
            }
        }

        private void DoRule15(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisBE-EntityReferenceAssociation"
                && (client.Stereotype != "XisBusinessEntity" || supplier.Stereotype != "XisEntity"))
            {
                EA.Project Project = Repository.GetProjectInterface();
                Project.PublishResult(LookupMap(rule15), EA.EnumMVErrorType.mvError, GetRuleStr(rule15));
                isValid = false;
            }
        }

        private void DoRule16(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Stereotype == "XisBusinessEntity")
            {
                bool hasMaster = false;
                EA.Connector conn = null;

                for (short i = 0; i < Element.Connectors.Count; i++)
                {
                    conn = Element.Connectors.GetAt(i);

                    if (conn.Stereotype == "XisBE-EntityMasterAssociation")
                    {
                        hasMaster = true;
                        break;
                    }
                }

                if (!hasMaster)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule16), EA.EnumMVErrorType.mvError, GetRuleStr(rule16));
                    isValid = false;
                }
            }
        }

        private void DoRule17(EA.Repository Repository, EA.Package Package)
        {
            if (Package.StereotypeEx == "UseCases View")
            {
                List<EA.Element> useCases = new List<EA.Element>();
                EA.Element el = null;

                for (short i = 0; i < Package.Elements.Count; i++)
                {
                    el = Package.Elements.GetAt(i);
                    if (el.Type == "UseCase" && (el.Stereotype == "XisEntityUseCase" || el.Stereotype == "XisServiceUseCase"))
                    {
                        useCases.Add(el);
                    }
                }

                if (useCases.Count > 1)
                {
                    bool isStartingUseCase = false;
                    int startingCounter = 0;

                    foreach (EA.Element uc in useCases)
                    {
                        isStartingUseCase = bool.Parse(M2MTransformer.GetTaggedValue(el.TaggedValues, "isStartingUseCase").Value);
                        if (isStartingUseCase)
                        {
                            startingCounter++;    
                        }
                    }

                    if (startingCounter != 1)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule17), EA.EnumMVErrorType.mvError, GetRuleStr(rule17));
                        isValid = false;    
                    }
                }
            }
        }

        private void DoRule18(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Stereotype == "XisEntityUseCase")
            {
                EA.Project Project = Repository.GetProjectInterface();

                if (Element.Connectors.Count > 0)
                {
                    EA.Connector conn = null;
                    EA.Element supplier = null;

                    for (short i = 0; i < Element.Connectors.Count; i++)
                    {
                        conn = Element.Connectors.GetAt(i);
                        supplier = Repository.GetElementByID(conn.SupplierID);

                        if (conn.Stereotype != "XisEntityUC-BEAssociation" && supplier.Stereotype == "XisBusinessEntity")
                        {
                            Project.PublishResult(LookupMap(rule18), EA.EnumMVErrorType.mvError, GetRuleStr(rule18));
                            isValid = false;
                        }
                    }
                }
                else
                {
                    Project.PublishResult(LookupMap(rule18), EA.EnumMVErrorType.mvError, GetRuleStr(rule18));
                    isValid = false;
                }
            }
        }

        private void DoRule19(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Stereotype == "XisServiceUseCase")
            {
                EA.Project Project = Repository.GetProjectInterface();

                if (Element.Connectors.Count > 0)
                {
                    EA.Connector conn = null;
                    EA.Element supplier = null;

                    for (short i = 0; i < Element.Connectors.Count; i++)
                    {
                        conn = Element.Connectors.GetAt(i);
                        supplier = Repository.GetElementByID(conn.SupplierID);

                        if ((conn.Stereotype != "XisServiceUC-BEAssociation" && supplier.Stereotype == "XisBusinessEntity")
                            || (conn.Stereotype != "XisServiceUC-ProviderAssociation"
                            && (supplier.Stereotype == "XisInternalProvider" || supplier.Stereotype == "XisServer"
                                || supplier.Stereotype == "XisClientMobileApp"
                            )))
                        {
                            Project.PublishResult(LookupMap(rule18), EA.EnumMVErrorType.mvError, GetRuleStr(rule18));
                            isValid = false;
                        }
                    }
                }
                else
                {
                    Project.PublishResult(LookupMap(rule19), EA.EnumMVErrorType.mvError, GetRuleStr(rule19));
                    isValid = false;
                }
            }
        }

        private void DoRule20(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisEntityUC-BEAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisEntityUseCase" || supplier.Stereotype != "XisBusinessEntity")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule20), EA.EnumMVErrorType.mvError, GetRuleStr(rule20));
                    isValid = false;
                }
            }
        }

        private void DoRule21(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisServiceUC-BEAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisServiceUseCase" || supplier.Stereotype != "XisBusinessEntity")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule21), EA.EnumMVErrorType.mvError, GetRuleStr(rule21));
                    isValid = false;
                }
            }
        }

        private void DoRule22(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisServiceUC-ProviderAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisServiceUseCase" || (supplier.Stereotype != "XisInternalProvider"
                    && supplier.Stereotype != "XisServer" && supplier.Stereotype != "XisClientMobileApp"))
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule22), EA.EnumMVErrorType.mvError, GetRuleStr(rule22));
                    isValid = false;
                }
            }
        }

        private void DoRule23(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMobileApp")
            {
                EA.Project Project = Repository.GetProjectInterface();

                if (Element.Connectors.Count > 0)
                {
                    EA.Connector conn = null;

                    for (short i = 0; i < Element.Connectors.Count; i++)
                    {
                        conn = Element.Connectors.GetAt(i);

                        if (conn.Stereotype != "XisMobileApp-ServiceAssociation")
                        {
                            Project.PublishResult(LookupMap(rule23), EA.EnumMVErrorType.mvError, GetRuleStr(rule23));
                            isValid = false;
                            break;
                        }
                    }
                }
                else
                {
                    Project.PublishResult(LookupMap(rule23), EA.EnumMVErrorType.mvError, GetRuleStr(rule23));
                    isValid = false;
                }
            }
        }

        private void DoRule24(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisMobileApp-ServiceAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisMobileApp" || supplier.Stereotype != "XisService")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule24), EA.EnumMVErrorType.mvError, GetRuleStr(rule24));
                    isValid = false;
                }
            }
        }

        private void DoRule25(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Interface" && (Element.Stereotype == "XisInternalService" || Element.Stereotype == "XisRemoteService"))
            {
                if (Element.Methods.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule25), EA.EnumMVErrorType.mvError, GetRuleStr(rule25));
                    isValid = false;
                }
            }
        }

        private void DoRule26(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Interface" && (Element.Stereotype == "XisInternalService" || Element.Stereotype == "XisRemoteService"))
            {
                if (Element.Methods.Count > 0)
                {
                    EA.Method method = null;

                    for (short i = 0; i < Element.Methods.Count; i++)
                    {
                        method = Element.Methods.GetAt(i);

                        if (method.Stereotype != "XisMethod")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule26), EA.EnumMVErrorType.mvError, GetRuleStr(rule26));
                            isValid = false;       
                        }
                    }
                }
            }
        }

        private void DoRule27(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisInternalProvider")
            {
                if (Element.Connectors.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule27), EA.EnumMVErrorType.mvError, GetRuleStr(rule27));
                    isValid = false; 
                }
            }
        }

        private void DoRule28(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisServer")
            {
                if (Element.Connectors.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule28), EA.EnumMVErrorType.mvError, GetRuleStr(rule28));
                    isValid = false;
                }
            }
        }

        private void DoRule29(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisClientMobileApp")
            {
                if (Element.Connectors.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule29), EA.EnumMVErrorType.mvError, GetRuleStr(rule29));
                    isValid = false;
                }
            }
        }

        private void DoRule30(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisNavigationAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisInteractionSpace" || supplier.Stereotype != "XisInteractionSpace")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule30), EA.EnumMVErrorType.mvError, GetRuleStr(rule30));
                    isValid = false;
                }
            }
        }

        private void DoRule31(EA.Repository Repository, EA.Package Package)
        {
            if (Package.StereotypeEx == "InteractionSpace View")
            {
                int mainScreenCounter = 0;
                EA.Element el = null;
                bool isMainScreen = false;

                for (short i = 0; i < Package.Elements.Count; i++)
                {
                    el = Package.Elements.GetAt(i);

                    if (el.Stereotype == "XisInteractionSpace")
                    {
                        isMainScreen = bool.Parse(M2MTransformer.GetTaggedValue(el.TaggedValues, "isMainScreen").Value);
                        if (isMainScreen)
                        {
                            mainScreenCounter++;
                        }
                    }
                }

                if (mainScreenCounter != 1)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule31), EA.EnumMVErrorType.mvError, GetRuleStr(rule31));
                    isValid = false;
                }
            }
        }

        private void DoRule32(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisInteractionSpace")
            {
                if (Element.Elements.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule32), EA.EnumMVErrorType.mvError, GetRuleStr(rule32));
                    isValid = false;
                }
            }
        }

        private void DoRule33(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisInteractionSpace")
            {
                if (Element.Elements.Count > 0)
                {
                    bool valid = true;
                    EA.Element widget = null;

                    for (short i = 0; i < Element.Elements.Count && valid; i++)
                    {
                        widget = Element.Elements.GetAt(i);

                        switch (widget.Stereotype)
                        {
                            case "XisLabel":
                            case "XisTextBox":
                            case "XisCheckBox":
                            case "XisButton":
                            case "XisLink":
                            case "XisImage":
                            case "XisDatePicker":
                            case "XisTimePicker":
                            case "XisWebView":
                            case "XisMapView":
                            case "XisDropDown":
                            case "XisList":
                            case "XisForm":
                            case "XisVisibilityBoundary":
                            case "XisMenu":
                                break;
                            default:
                                valid = false;
                                break;
                        }
                    }

                    if (!valid)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule33), EA.EnumMVErrorType.mvError, GetRuleStr(rule33));
                        isValid = false;
                    }  
                }
            }
        }

        private void DoRule34(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisIS-BEAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisInteractionSpace" || supplier.Stereotype != "XisBusinessEntity")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule34), EA.EnumMVErrorType.mvError, GetRuleStr(rule34));
                    isValid = false;
                }
            }
        }

        private void DoRule35(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisIS-MenuAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisInteractionSpace" || supplier.Stereotype != "XisMenu")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule35), EA.EnumMVErrorType.mvError, GetRuleStr(rule35));
                    isValid = false;
                }
            }
        }

        private void DoRule36(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisIS-DialogAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisInteractionSpace" || supplier.Stereotype != "XisDialog")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule36), EA.EnumMVErrorType.mvError, GetRuleStr(rule36));
                    isValid = false;
                }
            }
        }

        //private void DoRule05(EA.Repository Repository, EA.Element Element)
        //{
        //    EA.Package model = (EA.Package)Repository.Models.GetAt(0);

        //    if (Element.Type == "Class" && Element.Stereotype == "XisCompositeWidget")
        //    {
        //        EA.TaggedValue tv = Element.TaggedValues.GetByName("type");
        //        if (!string.IsNullOrEmpty(tv.Name) && tv.Name == "type")
        //        {
        //            if (tv.Value == "Menu" || tv.Value == "List")
        //            {
        //                if (Element.Elements.Count > 0)
        //                {
        //                    EA.Element el = null;
        //                    for (short i = 0; i < Element.Elements.Count; i++)
        //                    {
        //                        el = Element.Elements.GetAt(i);
        //                        if (el.Type != "Class" || (el.Stereotype != "Group" || el.Stereotype != "Item"))
        //                        {
        //                            isValid = false;
        //                            EA.Project Project = Repository.GetProjectInterface();
        //                            Project.PublishResult(LookupMap(rule05), EA.EnumMVErrorType.mvError, GetRuleStr(rule05));
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //// XisGesture must have 1 XisAction
        //private void DoRule06(EA.Repository Repository, EA.Element Element)
        //{
        //    if (Element.Type == "Class" && Element.Stereotype == "XisGesture")
        //    {
        //        bool valid = true;

        //        if (Element.Methods.Count == 1)
        //        {
        //            EA.Method action = Element.Methods.GetAt(0);

        //            if (action.Stereotype != "XisAction")
        //            {
        //                valid = false;
        //            }
        //        }
        //        else
        //        {
        //            valid = false;
        //        }

        //        if (!valid)
        //        {
        //            EA.Project Project = Repository.GetProjectInterface();
        //            Project.PublishResult(LookupMap(rule06), EA.EnumMVErrorType.mvError, GetRuleStr(rule06));
        //            isValid = false;
        //        }
        //    }
        //}

        //// XisAction owned by XisGestures
        //private void DoRule07(EA.Repository Repository, EA.Method method, long ObjectID)
        //{
        //    if (method.Stereotype == "XisAction")
        //    {
        //        EA.Element element = (EA.Element)Repository.GetElementByID((int)ObjectID);

        //        if (element.Type != "Class" || element.Stereotype != "XisGesture")
        //        {
        //            EA.Project Project = Repository.GetProjectInterface();
        //            Project.PublishResult(LookupMap(rule07), EA.EnumMVErrorType.mvError, GetRuleStr(rule07));
        //            isValid = false;
        //        }
        //    }
        //}

        //// XisAction with XisArguments
        //private void DoRule08(EA.Repository Repository, EA.Method method)
        //{
        //    if (method.Stereotype == "XisAction" && method.Parameters.Count > 0)
        //    {
        //        EA.Parameter parameter = null;
        //        bool valid = true;

        //        for (short i = 0; i < method.Parameters.Count; i++)
        //        {
        //            parameter = method.Parameters.GetAt(i);
        //            if (parameter.Stereotype != "XisArgument")
        //            {
        //                valid = false;
        //                break;
        //            }
        //        }

        //        if (!valid)
        //        {
        //            EA.Project Project = Repository.GetProjectInterface();
        //            Project.PublishResult(LookupMap(rule08), EA.EnumMVErrorType.mvError, GetRuleStr(rule08));
        //            isValid = false;
        //        }
        //    }
        //}

        // XisAction NavigateTo with IS
        //private void DoRule09(EA.Repository Repository, EA.Method method)
        //{
        //    if (method.Stereotype == "XisAction")
        //    {
        //        EA.MethodTag tv = method.TaggedValues.GetByName("type");

        //        if (tv == null || string.IsNullOrEmpty(tv.Value))
        //        {
        //            // No value Type
        //            EA.Project Project = Repository.GetProjectInterface();
        //            Project.PublishResult(LookupMap(rule09), EA.EnumMVErrorType.mvError, GetRuleStr(rule09A));
        //            isValid = false;
        //        }
        //        else
        //        {
        //            switch (tv.Value)
        //            {
        //                case "NavigateTo":
        //                    if (method.Parameters.Count == 1)
        //                    {
        //                        EA.Parameter p = method.Parameters.GetAt(0);
        //                        if (p.Stereotype != "XisArgument" || p.Name != "interactionSpace")
        //                        {
        //                            // doesn't exist
        //                            EA.Project Project = Repository.GetProjectInterface();
        //                            Project.PublishResult(LookupMap(rule09), EA.EnumMVErrorType.mvError, GetRuleStr(rule09B));
        //                            isValid = false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // doesn't exist
        //                        EA.Project Project = Repository.GetProjectInterface();
        //                        Project.PublishResult(LookupMap(rule09), EA.EnumMVErrorType.mvError, GetRuleStr(rule09B));
        //                        isValid = false;
        //                    }
        //                    break;
        //                case "WebService":
        //                    if (method.Parameters.Count == 1)
        //                    {
        //                        EA.Parameter p = method.Parameters.GetAt(0);
        //                        if (p.Stereotype != "XisArgument" || p.Name != "url")
        //                        {
        //                            // doesn't exist
        //                            EA.Project Project = Repository.GetProjectInterface();
        //                            Project.PublishResult(LookupMap(rule09), EA.EnumMVErrorType.mvError, GetRuleStr(rule09C));
        //                            isValid = false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // doesn't exist
        //                        EA.Project Project = Repository.GetProjectInterface();
        //                        Project.PublishResult(LookupMap(rule09), EA.EnumMVErrorType.mvError, GetRuleStr(rule09C));
        //                        isValid = false;
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //}
    }
}
