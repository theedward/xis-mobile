﻿using System;
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

        private const string rule01 = "Rule01";
        private const string rule02 = "Rule02";
        private const string rule03 = "Rule03";
        private const string rule03A = "Rule03A";
        private const string rule03B = "Rule03B";
        private const string rule04 = "Rule04";
        private const string rule04A = "Rule04A";
        private const string rule04B = "Rule04B";
        private const string rule05 = "Rule05";
        private const string rule06 = "Rule06";
        private const string rule07 = "Rule07";
        private const string rule08 = "Rule08";
        private const string rule09 = "Rule09";
        private const string rule09A = "Rule09A";
        private const string rule09B = "Rule09B";
        private const string rule09C = "Rule09C";

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
                    return "The model must be composed of 3 views!";
                case rule02:
                    return "XisEntities must have at least 1 XisEntityAttribute!";
                case rule03A:
                    return "XisEntityAssociations must connect only XisEntities!";
                case rule03B:
                    return "XisEntities must be connected only by XisEntityAssociations!";
                case rule04A:
                    return "XisInteractionSpace must contain at least 1 XisWidget!";
                case rule04B:
                    return "XisInteractionSpace must be only composed of XisWidgets!";
                case rule05:
                    return "Lists/Menus can only be composed of Groups or Items!";
                case rule06:
                    return "XisGestures must have 1 XisAction!";
                case rule07:
                    return "XisActions must be owned only by XisGestures!";
                case rule08:
                    return "All XisActions parameters must be XisArguments!";
                case rule09A:
                    return "XisActions must have a type!";
                case rule09B:
                    return "XisActions of type 'NavigateTo' must have a XisArgument named 'navigationSpace'!";
                case rule09C:
                    return "XisActions of type 'WebService' must have a XisArgument named 'url'!";
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
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule02)), rule02);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, rule03), rule03);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, rule04), rule04);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, rule05), rule05);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, rule06), rule06);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, rule07), rule07);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, rule08), rule08);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, rule09), rule09);
            // TODO: expand this list
        }

        public void RunPackageRule(EA.Repository Repository, string sRuleID, long PackageID)
        {
            EA.Package package = Repository.GetPackageByID((int)PackageID);
            if (package != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    case rule01:
                        DoRule01(Repository, PackageID);
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
                    case rule02:
                        DoRule02(Repository, Element);
                        break;
                    case rule04:
                        DoRule04(Repository, Element);
                        break;
                    case rule06:
                        DoRule06(Repository, Element);
                        break;
                    default: break;
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
                    case rule03:
                        DoRule03(Repository, Connector);
                        break;
                    default:
                        break;
                }
            }
        }

        public void RunMethodRule(EA.Repository Repository, string sRuleID, string MethodGUID, long ObjectID)
        {
            EA.Method method = (EA.Method)Repository.GetMethodByGuid(MethodGUID);
            if (method != null)
            {
                switch (LookupMapEx(sRuleID))
                {
                    case rule07:
                        DoRule07(Repository, method, ObjectID);
                        break;
                    case rule08:
                        DoRule08(Repository, method);
                        break;
                    case rule09:
                        DoRule09(Repository, method);
                        break;
                    default:
                        break;
                }
            }
        }

        // Views exist?
        private void DoRule01(EA.Repository Repository, long PackageID)
        {
            EA.Package model = (EA.Package)Repository.Models.GetAt(0);
            EA.Package view = (EA.Package)model.Packages.GetAt(0);
            
            if (model.Packages.Count != 3 && view.PackageID == PackageID)
            {
                EA.Project Project = Repository.GetProjectInterface();
                Project.PublishResult(LookupMap(rule01), EA.EnumMVErrorType.mvError, GetRuleStr(rule01));
                isValid = false;
            }
        }

        // XisEntities with XisEntityAttributes
        private void DoRule02(EA.Repository Repository, EA.Element Element)
        {
            bool valid = true;

            if (Element.Type == "Class" && Element.Stereotype == "XisEntity")
            {
                if (Element.Attributes.Count > 0)
                {
                    EA.Attribute attr = null;
                    for (int i = 0; i < Element.Attributes.Count; i++)
                    {
                        attr = Element.Attributes.GetAt(0);
                        if (attr.Stereotype != "XisEntityAttribute")
                        {
                            valid = false;
                            break;
                        }
                    }
                }
                else
                {
                    valid = false;
                }

                if (!valid)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule02), EA.EnumMVErrorType.mvError, GetRuleStr(rule02));
                    isValid = valid;
                }
            }
        }

        // XisEntityAssociations
        private void DoRule03(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisEntityAssociation")
            {
                if (client.Stereotype != "XisEntity" || supplier.Stereotype != "XisEntity")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule03), EA.EnumMVErrorType.mvError, GetRuleStr(rule03A));
                    isValid = false;
                }
            }
            else if (client.Stereotype == "XisEntity" && supplier.Stereotype == "XisEntity")
            {
                EA.Project Project = Repository.GetProjectInterface();
                Project.PublishResult(LookupMap(rule03), EA.EnumMVErrorType.mvError, GetRuleStr(rule03B));
                isValid = false;
            }
        }

        // XisInteractionSpace composed of XisWidgets
        private void DoRule04(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisInteractionSpace")
            {
                if (Element.Elements.Count < 1)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule04), EA.EnumMVErrorType.mvError, GetRuleStr(rule04A));
                    isValid = false;
                }
                else
                {
                    bool valid = true;
                    EA.Element widget = null;

                    for (short i = 0; i < Element.Elements.Count && valid; i++)
                    {
                        widget = Element.Elements.GetAt(i);

                        switch (widget.Stereotype)
                        {
                            case "XisCompositeWidget":
                            case "XisLabel":
                            case "XisTextBox":
                            case "XisCheckBox":
                            case "XisButton":
                            case "XisLink":
                            case "XisDatePicker":
                            case "XisTimePicker":
                            case "XisImage":
                            case "XisWebView":
                            case "XisMapView":
                                break;
                            default:
                                valid = false;
                                break;
                        }
                    }

                    if (!valid)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule04), EA.EnumMVErrorType.mvError, GetRuleStr(rule04B));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule05(EA.Repository Repository, EA.Element Element)
        {
            EA.Package model = (EA.Package)Repository.Models.GetAt(0);

            if (Element.Type == "Class" && Element.Stereotype == "XisCompositeWidget")
            {
                EA.TaggedValue tv = Element.TaggedValues.GetByName("type");
                if (!string.IsNullOrEmpty(tv.Name) && tv.Name == "type")
	            {
		            if (tv.Value == "Menu" || tv.Value == "List")
                    {
                        if (Element.Elements.Count > 0)
                        {
                            EA.Element el = null;
                            for (short i = 0; i < Element.Elements.Count; i++)
                            {
                                el = Element.Elements.GetAt(i);
                                if (el.Type != "Class" || (el.Stereotype != "Group" || el.Stereotype != "Item"))
	                            {
		                            isValid = false;
                                    EA.Project Project = Repository.GetProjectInterface();
                                    Project.PublishResult(LookupMap(rule05), EA.EnumMVErrorType.mvError, GetRuleStr(rule05));
                                    break;
	                            }   
                            }
                        }
                    }
	            }
            }
        }

        // XisGesture must have 1 XisAction
        private void DoRule06(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisGesture")
            {
                bool valid = true;

                if (Element.Methods.Count == 1)
                {
                    EA.Method action = Element.Methods.GetAt(0);

                    if (action.Stereotype != "XisAction")
                    {
                        valid = false;
                    }
                }
                else
                {
                    valid = false;
                }

                if (!valid)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule06), EA.EnumMVErrorType.mvError, GetRuleStr(rule06));
                    isValid = false;
                }
            }
        }

        // XisAction owned by XisGestures
        private void DoRule07(EA.Repository Repository, EA.Method method, long ObjectID)
        {
            if (method.Stereotype == "XisAction")
            {
                EA.Element element = (EA.Element)Repository.GetElementByID((int)ObjectID);

                if (element.Type != "Class" || element.Stereotype != "XisGesture")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule07), EA.EnumMVErrorType.mvError, GetRuleStr(rule07));
                    isValid = false;
                }
            }
        }

        // XisAction with XisArguments
        private void DoRule08(EA.Repository Repository, EA.Method method)
        {
            if (method.Stereotype == "XisAction" && method.Parameters.Count > 0)
            {
                EA.Parameter parameter = null;
                bool valid = true;

                for (short i = 0; i < method.Parameters.Count; i++)
                {
                    parameter = method.Parameters.GetAt(i);
                    if (parameter.Stereotype != "XisArgument")
                    {
                        valid = false;
                        break;
                    }
                }

                if (!valid)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule08), EA.EnumMVErrorType.mvError, GetRuleStr(rule08));
                    isValid = false;
                }
            }
        }

        // XisAction NavigateTo with IS
        private void DoRule09(EA.Repository Repository, EA.Method method)
        {
            if (method.Stereotype == "XisAction")
            {
                EA.MethodTag tv = method.TaggedValues.GetByName("type");

                if (tv == null || string.IsNullOrEmpty(tv.Value))
                {
                    // No value Type
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule09), EA.EnumMVErrorType.mvError, GetRuleStr(rule09A));
                    isValid = false;
                }
                else
                {
                    switch (tv.Value)
                    {
                        case "NavigateTo":
                            if (method.Parameters.Count == 1)
                            {
                                EA.Parameter p = method.Parameters.GetAt(0);
                                if (p.Stereotype != "XisArgument" || p.Name != "interactionSpace")
                                {
                                    // doesn't exist
                                    EA.Project Project = Repository.GetProjectInterface();
                                    Project.PublishResult(LookupMap(rule09), EA.EnumMVErrorType.mvError, GetRuleStr(rule09B));
                                    isValid = false;
                                }
                            }
                            else
                            {
                                // doesn't exist
                                EA.Project Project = Repository.GetProjectInterface();
                                Project.PublishResult(LookupMap(rule09), EA.EnumMVErrorType.mvError, GetRuleStr(rule09B));
                                isValid = false;
                            }
                            break;
                        case "WebService":
                            if (method.Parameters.Count == 1)
                            {
                                EA.Parameter p = method.Parameters.GetAt(0);
                                if (p.Stereotype != "XisArgument" || p.Name != "url")
                                {
                                    // doesn't exist
                                    EA.Project Project = Repository.GetProjectInterface();
                                    Project.PublishResult(LookupMap(rule09), EA.EnumMVErrorType.mvError, GetRuleStr(rule09C));
                                    isValid = false;
                                }
                            }
                            else
                            {
                                // doesn't exist
                                EA.Project Project = Repository.GetProjectInterface();
                                Project.PublishResult(LookupMap(rule09), EA.EnumMVErrorType.mvError, GetRuleStr(rule09C));
                                isValid = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}