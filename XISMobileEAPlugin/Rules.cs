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

        private const string rule17 = "Rule17";
        private const string rule18 = "Rule18";
        private const string rule19 = "Rule19";
        private const string rule20 = "Rule20";

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
                    return "A XisBE_EntityMasterAssociation must connect a XisBusinessEntity (source) to a XisEntity (target)";
                case rule14:
                    return "A XisBE_EntityDetailAssociation must connect a XisBusinessEntity (source) to a XisEntity (target)";
                case rule15:
                    return "A XisBE_EntityReferenceAssociation must connect a XisBusinessEntity (source) to a XisEntity (target)";
                case rule16:
                    return "XisBusinessEntities must have 1 XisBE_EntityMasterAssociation!";
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
                    default: break;
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

            if (Connector.Stereotype == "XisBE_EntityMasterAssociation"
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

            if (Connector.Stereotype == "XisBE_EntityDetailAssociation"
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

            if (Connector.Stereotype == "XisBE_EntityReferenceAssociation"
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
                    if (conn.Stereotype == "XisBE_EntityMasterAssociation")
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

        //// XisInteractionSpace composed of XisWidgets
        //private void DoRule04(EA.Repository Repository, EA.Element Element)
        //{
        //    if (Element.Type == "Class" && Element.Stereotype == "XisInteractionSpace")
        //    {
        //        if (Element.Elements.Count < 1)
        //        {
        //            EA.Project Project = Repository.GetProjectInterface();
        //            Project.PublishResult(LookupMap(rule04), EA.EnumMVErrorType.mvError, GetRuleStr(rule04A));
        //            isValid = false;
        //        }
        //        else
        //        {
        //            bool valid = true;
        //            EA.Element widget = null;

        //            for (short i = 0; i < Element.Elements.Count && valid; i++)
        //            {
        //                widget = Element.Elements.GetAt(i);

        //                switch (widget.Stereotype)
        //                {
        //                    case "XisCompositeWidget":
        //                    case "XisLabel":
        //                    case "XisTextBox":
        //                    case "XisCheckBox":
        //                    case "XisButton":
        //                    case "XisLink":
        //                    case "XisDatePicker":
        //                    case "XisTimePicker":
        //                    case "XisImage":
        //                    case "XisWebView":
        //                    case "XisMapView":
        //                        break;
        //                    default:
        //                        valid = false;
        //                        break;
        //                }
        //            }

        //            if (!valid)
        //            {
        //                EA.Project Project = Repository.GetProjectInterface();
        //                Project.PublishResult(LookupMap(rule04), EA.EnumMVErrorType.mvError, GetRuleStr(rule04B));
        //                isValid = false;
        //            }
        //        }
        //    }
        //}

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
