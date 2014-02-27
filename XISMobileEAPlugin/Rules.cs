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
        private const string rule39 = "Rule39";
        private const string rule40 = "Rule40";
        private const string rule41 = "Rule41";
        private const string rule42 = "Rule42";
        private const string rule43 = "Rule43";
        private const string rule44 = "Rule44";
        private const string rule45 = "Rule45";
        private const string rule46 = "Rule46";
        private const string rule47 = "Rule47";
        private const string rule48 = "Rule48";
        private const string rule49 = "Rule49";
        private const string rule50 = "Rule50";
        private const string rule51 = "Rule51";
        private const string rule52 = "Rule52";
        private const string rule53 = "Rule53";
        private const string rule54 = "Rule54";
        private const string rule55 = "Rule55";
        private const string rule56 = "Rule56";
        private const string rule57 = "Rule57";
        private const string rule58 = "Rule58";
        private const string rule59 = "Rule59";
        private const string rule60 = "Rule60";
        private const string rule61 = "Rule61";
        private const string rule62 = "Rule62";
        private const string rule63 = "Rule63";
        private const string rule64 = "Rule64";
        private const string rule65 = "Rule65";
        private const string rule66 = "Rule66";
        private const string rule67 = "Rule67";
        private const string rule68 = "Rule68";
        private const string rule69 = "Rule69";
        private const string rule70 = "Rule70";
        private const string rule71 = "Rule71";
        private const string rule72 = "Rule72";
        private const string rule73 = "Rule73";
        private const string rule74 = "Rule74";
        private const string rule75 = "Rule75";

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
                    return "A XisBusinessEntity must be connected to a XisEntity by a XisBE-EntityMasterAssociation, XisBE-EntityDetailAssociation or XisBE-EntityReferenceAssociation!";
                case rule14:
                    return "A XisBE-EntityMasterAssociation must connect a XisBusinessEntity (source) to a XisEntity (target)!";
                case rule15:
                    return "A XisBE-EntityDetailAssociation must connect a XisBusinessEntity (source) to a XisEntity (target)!";
                case rule16:
                    return "A XisBE-EntityReferenceAssociation must connect a XisBusinessEntity (source) to a XisEntity (target)!";
                case rule17:
                    return "XisBusinessEntities must have 1 XisBE-EntityMasterAssociation!";
                case rule18:
                    return "There must be 1 starting XisUseCase!";
                case rule19:
                    return "XisEntityUseCases must be connected to XisBusinessEntities by XisEntityUC-BEAssociations!";
                case rule20:
                    return "XisServiceUseCases must be connected to XisBusinessEntities and/or XisProviders by XisServiceUC-BEAssociations and XisServiceUC-ProviderAssociations, respectively!";
                case rule21:
                    return "A XisEntityUC-BEAssociation must connect a XisEntityUseCase (source) to a XisBusinessEntity (target)!";
                case rule22:
                    return "A XisServiceUC-BEAssociation must connect a XisServiceUseCase (source) to a XisBusinessEntity (target)!";
                case rule23:
                    return "A XisServiceUC-ProviderAssociation must connect a XisServiceUseCase (source) to a XisProvider (target)!";
                case rule24:
                    return "XisMobileApp must be connected to XisServices by XisMobileApp-ServiceAssociations!";
                case rule25:
                    return "A XisMobileApp-ServiceAssociation must connect XisMobileApp (source) to a XisService (target)!";
                case rule26:
                    return "A XisService must have at least 1 XisServiceMethod!";
                case rule27:
                    return "A XisService must only have methods with stereotype «XisMethod»!";
                case rule28:
                    return "A XisInternalProvider must realize a XisInternalService!";
                case rule29:
                    return "A XisServer must realize a XisRemoteService!";
                case rule30:
                    return "A XisClientMobileApp must realize a XisRemoteService!";
                case rule31:
                    return "A XisNavigationAssociation must only connect XisInteractionSpaces!";
                case rule32:
                    return "There must be 1 XisInteractionSpace that is the main screen!";
                case rule33:
                    return "A XisInteractionSpace must have at least 1 XisWidget!";
                case rule34:
                    return "All XisInteractionSpace elements must be XisWidgets!";
                case rule35:
                    return "A XisIS-BEAssociation must connect a XisInteractionSpace (source) to a XisBusinessEntity (target)!";
                case rule36:
                    return "A XisIS-MenuAssociation must connect a XisInteractionSpace (source) to a XisMenu (target)!";
                case rule37:
                    return "A XisIS-DialogAssociation must connect a XisInteractionSpace (source) to a XisDialog (target)!";
                case rule38:
                    return "A XisGesture must have 1 XisAction!";
                case rule39:
                    return "A XisWidget-GestureAssociation must connect a XisWidget (source) to a XisGesture (target)!";
                case rule40:
                    return "A XisList must contain 1 XisListItem or XisListGroup!";
                case rule41:
                    return "A XisList can only contain XisListItems or XisListGroups!";
                case rule42:
                    return "A XisListGroup must contain 1 XisListItem!";
                case rule43:
                    return "A XisListGroup can only contain 1 XisListItem!";
                case rule44:
                    return "A XisListItem can only contain XisWidgets!";
                case rule45:
                    return "A XisMenu must contain at least 1 XisMenuItem or XisMenuGroup!";
                case rule46:
                    return "A XisMenu can only contain XisMenuItems or XisMenuGroups!";
                case rule47:
                    return "A XisMenuGroup must contain at least 1 XisMenuItem!";
                case rule48:
                    return "A XisMenuGroup can only contain XisMenuItems!";
                case rule49:
                    return "A XisMenuItem cannot contain other elements!";
                case rule50:
                    return "A XisMenu of type 'OptionsMenu' must be associated to a XisInteractionSpace!";
                case rule51:
                    return "A XisButton with a XisAction must have a corresponding 'onTap' value!";
                case rule52:
                    return "A XisButton with 'onTap' value filled must have a corresponding XisAction!";
                case rule53:
                    return "A XisButton can only have 1 XisAction!";
                case rule54:
                    return "A XisLink with a XisAction must have a corresponding 'onTap' value!";
                case rule55:
                    return "A XisLink with 'onTap' value filled must have a corresponding XisAction!";
                case rule56:
                    return "A XisLink can only have 1 XisAction!";
                case rule57:
                    return "A XisMenuItem with a XisAction must have a corresponding 'onTap' value!";
                case rule58:
                    return "A XisMenuItem with 'onTap' value filled must have a corresponding XisAction!";
                case rule59:
                    return "A XisMenuItem can only have at most 1 XisAction!";
                case rule60:
                    return "A XisListItem with a XisAction must have a corresponding 'onTap' or 'onLongTap' value!";
                case rule61:
                    return "A XisListItem with 'onTap' value filled must have a corresponding XisAction!";
                case rule62:
                    return "A XisListItem with 'onLongTap' value filled must have a corresponding XisAction!";
                case rule63:
                    return "A XisListItem can only have at most 2 XisActions!";
                case rule64:
                    return "A XisDialog can only contain XisButtons (up to 3)!";
                case rule65:
                    return "A XisMapView can only contain XisMapMarkers!";
                case rule66:
                    return "A XisLabel cannot contain other elements!";
                case rule67:
                    return "A XisTextBox cannot contain other elements!";
                case rule68:
                    return "A XisCheckBox cannot contain other elements!";
                case rule69:
                    return "A XisButton cannot contain other elements!";
                case rule70:
                    return "A XisLink cannot contain other elements!";
                case rule71:
                    return "A XisImage cannot contain other elements!";
                case rule72:
                    return "A XisDatePicker cannot contain other elements!";
                case rule73:
                    return "A XisTimePicker cannot contain other elements!";
                case rule74:
                    return "A XisWebView cannot contain other elements!";
                case rule75:
                    return "A XisDropdown cannot contain other elements!";
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
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule37)), rule37);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule38)), rule38);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule39)), rule39);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule40)), rule40);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule41)), rule41);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule42)), rule42);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule43)), rule43);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule44)), rule44);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule45)), rule45);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule46)), rule46);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule47)), rule47);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule48)), rule48);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule49)), rule49);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule50)), rule50);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule51)), rule51);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule52)), rule52);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule53)), rule53);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule54)), rule54);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule55)), rule55);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule56)), rule56);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule57)), rule57);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule58)), rule58);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule59)), rule59);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule60)), rule60);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule61)), rule61);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule62)), rule62);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule63)), rule63);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule64)), rule64);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule65)), rule65);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule66)), rule66);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule67)), rule67);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule68)), rule68);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule69)), rule69);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule70)), rule70);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule71)), rule71);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule72)), rule72);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule73)), rule73);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule74)), rule74);
            AddToMap(Project.DefineRule(m_sCategoryID, EA.EnumMVErrorType.mvError, GetRuleStr(rule75)), rule75);
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
                        DoRule01_to_04(Repository, Package);
                        break;
                    case rule18:
                        DoRule18(Repository, Package);
                        break;
                    case rule32:
                        DoRule32(Repository, Package);
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
                    case rule13:
                        DoRule13(Repository, Element);
                        break;
                    case rule17:
                        DoRule17(Repository, Element);
                        break;
                    case rule19:
                        DoRule19(Repository, Element);
                        break;
                    case rule20:
                        DoRule20(Repository, Element);
                        break;
                    case rule24:
                        DoRule24(Repository, Element);
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
                    case rule33:
                        DoRule33(Repository, Element);
                        break;
                    case rule34:
                        DoRule34(Repository, Element);
                        break;
                    case rule38:
                        DoRule38(Repository, Element);
                        break;
                    case rule40:
                        DoRule40(Repository, Element);
                        break;
                    case rule41:
                        DoRule41(Repository, Element);
                        break;
                    case rule42:
                        DoRule42(Repository, Element);
                        break;
                    case rule43:
                        DoRule43(Repository, Element);
                        break;
                    case rule44:
                        DoRule44(Repository, Element);
                        break;
                    case rule45:
                        DoRule45(Repository, Element);
                        break;
                    case rule46:
                        DoRule46(Repository, Element);
                        break;
                    case rule47:
                        DoRule47(Repository, Element);
                        break;
                    case rule48:
                        DoRule48(Repository, Element);
                        break;
                    case rule49:
                        DoRule49(Repository, Element);
                        break;
                    case rule50:
                        DoRule50(Repository, Element);
                        break;
                    case rule51:
                        DoRule51_54_57(Repository, Element, "XisButton");
                        break;
                    case rule52:
                        DoRule52_55_58(Repository, Element, "XisButton");
                        break;
                    case rule53:
                        DoRule53_56_59(Repository, Element, "XisButton");
                        break;
                    case rule54:
                        DoRule51_54_57(Repository, Element, "XisLink");
                        break;
                    case rule55:
                        DoRule52_55_58(Repository, Element, "XisLink");
                        break;
                    case rule56:
                        DoRule53_56_59(Repository, Element, "XisLink");
                        break;
                    case rule57:
                        DoRule51_54_57(Repository, Element, "XisMenuItem");
                        break;
                    case rule58:
                        DoRule52_55_58(Repository, Element, "XisMenuItem");
                        break;
                    case rule59:
                        DoRule53_56_59(Repository, Element, "XisMenuItem");
                        break;
                    case rule60:
                        DoRule60(Repository, Element);
                        break;
                    case rule61:
                        DoRule61(Repository, Element);
                        break;
                    case rule62:
                        DoRule62(Repository, Element);
                        break;
                    case rule63:
                        DoRule63(Repository, Element);
                        break;
                    case rule64:
                        DoRule64(Repository, Element);
                        break;
                    case rule65:
                        DoRule65(Repository, Element);
                        break;
                    case rule66:
                        DoRule66_to_75(Repository, Element, "XisLabel");
                        break;
                    case rule67:
                        DoRule66_to_75(Repository, Element, "XisTextBox");
                        break;
                    case rule68:
                        DoRule66_to_75(Repository, Element, "XisTextBox");
                        break;
                    case rule69:
                        DoRule66_to_75(Repository, Element, "XisButton");
                        break;
                    case rule70:
                        DoRule66_to_75(Repository, Element, "XisLink");
                        break;
                    case rule71:
                        DoRule66_to_75(Repository, Element, "XisImage");
                        break;
                    case rule72:
                        DoRule66_to_75(Repository, Element, "XisDatePicker");
                        break;
                    case rule73:
                        DoRule66_to_75(Repository, Element, "XisTimePicker");
                        break;
                    case rule74:
                        DoRule66_to_75(Repository, Element, "XisWebView");
                        break;
                    case rule75:
                        DoRule66_to_75(Repository, Element, "XisDropdown");
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
                    case rule15:
                        DoRule15(Repository, Connector);
                        break;
                    case rule16:
                        DoRule16(Repository, Connector);
                        break;
                    case rule21:
                        DoRule21(Repository, Connector);
                        break;
                    case rule22:
                        DoRule22(Repository, Connector);
                        break;
                    case rule23:
                        DoRule23(Repository, Connector);
                        break;
                    case rule25:
                        DoRule25(Repository, Connector);
                        break;
                    case rule31:
                        DoRule31(Repository, Connector);
                        break;
                    case rule35:
                        DoRule35(Repository, Connector);
                        break;
                    case rule36:
                        DoRule36(Repository, Connector);
                        break;
                    case rule37:
                        DoRule37(Repository, Connector);
                        break;
                    case rule39:
                        DoRule39(Repository, Connector);
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
        private void DoRule01_to_04(EA.Repository Repository, EA.Package Package)
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

                            if (el.Name == Attribute.Type && (el.Stereotype == "XisEntity" || el.Stereotype == "XisEnumeration"))
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

        private void DoRule13(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisBusinessEntity")
            {
                if (Element.Connectors.Count > 0)
                {
                    bool hasAssociation = false;
                    EA.Connector conn = null;
                    EA.Element supplier = null;

                    for (short i = 0; i < Element.Connectors.Count; i++)
                    {
                        conn = Element.Connectors.GetAt(i);
                        supplier = Repository.GetElementByID(conn.SupplierID);

                        if ((conn.Stereotype == "XisBE-EntityMasterAssociation"
                            || conn.Stereotype == "XisBE-EntityDetailAssociation"
                            || conn.Stereotype == "XisBE-EntityReferenceAssociation")
                            && supplier.Stereotype == "XisEntity")
                        {
                            hasAssociation = true;
                            break;
                        }
                    }

                    if (!hasAssociation)
	                {
		                EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule13), EA.EnumMVErrorType.mvError, GetRuleStr(rule13));
                        isValid = false;
	                }
                }
                else
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule13), EA.EnumMVErrorType.mvError, GetRuleStr(rule13));
                    isValid = false;
                }
            }
        }

        private void DoRule14(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisBE-EntityMasterAssociation"
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

            if (Connector.Stereotype == "XisBE-EntityDetailAssociation"
                && (client.Stereotype != "XisBusinessEntity" || supplier.Stereotype != "XisEntity"))
            {
                EA.Project Project = Repository.GetProjectInterface();
                Project.PublishResult(LookupMap(rule15), EA.EnumMVErrorType.mvError, GetRuleStr(rule15));
                isValid = false;
            }
        }

        private void DoRule16(EA.Repository Repository, EA.Connector Connector)
        {
            EA.Element client = Repository.GetElementByID(Connector.ClientID);
            EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

            if (Connector.Stereotype == "XisBE-EntityReferenceAssociation"
                && (client.Stereotype != "XisBusinessEntity" || supplier.Stereotype != "XisEntity"))
            {
                EA.Project Project = Repository.GetProjectInterface();
                Project.PublishResult(LookupMap(rule16), EA.EnumMVErrorType.mvError, GetRuleStr(rule16));
                isValid = false;
            }
        }

        private void DoRule17(EA.Repository Repository, EA.Element Element)
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
                    Project.PublishResult(LookupMap(rule17), EA.EnumMVErrorType.mvError, GetRuleStr(rule17));
                    isValid = false;
                }
            }
        }

        private void DoRule18(EA.Repository Repository, EA.Package Package)
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
                        Project.PublishResult(LookupMap(rule18), EA.EnumMVErrorType.mvError, GetRuleStr(rule18));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule19(EA.Repository Repository, EA.Element Element)
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
                            Project.PublishResult(LookupMap(rule19), EA.EnumMVErrorType.mvError, GetRuleStr(rule19));
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

        private void DoRule20(EA.Repository Repository, EA.Element Element)
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
                            Project.PublishResult(LookupMap(rule19), EA.EnumMVErrorType.mvError, GetRuleStr(rule19));
                            isValid = false;
                        }
                    }
                }
                else
                {
                    Project.PublishResult(LookupMap(rule20), EA.EnumMVErrorType.mvError, GetRuleStr(rule20));
                    isValid = false;
                }
            }
        }

        private void DoRule21(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisEntityUC-BEAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisEntityUseCase" || supplier.Stereotype != "XisBusinessEntity")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule21), EA.EnumMVErrorType.mvError, GetRuleStr(rule21));
                    isValid = false;
                }
            }
        }

        private void DoRule22(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisServiceUC-BEAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisServiceUseCase" || supplier.Stereotype != "XisBusinessEntity")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule22), EA.EnumMVErrorType.mvError, GetRuleStr(rule22));
                    isValid = false;
                }
            }
        }

        private void DoRule23(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisServiceUC-ProviderAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisServiceUseCase" || (supplier.Stereotype != "XisInternalProvider"
                    && supplier.Stereotype != "XisServer" && supplier.Stereotype != "XisClientMobileApp"))
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule23), EA.EnumMVErrorType.mvError, GetRuleStr(rule23));
                    isValid = false;
                }
            }
        }

        private void DoRule24(EA.Repository Repository, EA.Element Element)
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
                            Project.PublishResult(LookupMap(rule24), EA.EnumMVErrorType.mvError, GetRuleStr(rule24));
                            isValid = false;
                            break;
                        }
                    }
                }
                else
                {
                    Project.PublishResult(LookupMap(rule24), EA.EnumMVErrorType.mvError, GetRuleStr(rule24));
                    isValid = false;
                }
            }
        }

        private void DoRule25(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisMobileApp-ServiceAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisMobileApp" || supplier.Stereotype != "XisService")
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
                if (Element.Methods.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule26), EA.EnumMVErrorType.mvError, GetRuleStr(rule26));
                    isValid = false;
                }
            }
        }

        private void DoRule27(EA.Repository Repository, EA.Element Element)
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
                            Project.PublishResult(LookupMap(rule27), EA.EnumMVErrorType.mvError, GetRuleStr(rule27));
                            isValid = false;
                        }
                    }
                }
            }
        }

        private void DoRule28(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisInternalProvider")
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
            if (Element.Type == "Class" && Element.Stereotype == "XisServer")
            {
                if (Element.Connectors.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule29), EA.EnumMVErrorType.mvError, GetRuleStr(rule29));
                    isValid = false;
                }
            }
        }

        private void DoRule30(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisClientMobileApp")
            {
                if (Element.Connectors.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule30), EA.EnumMVErrorType.mvError, GetRuleStr(rule30));
                    isValid = false;
                }
            }
        }

        private void DoRule31(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisNavigationAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisInteractionSpace" || supplier.Stereotype != "XisInteractionSpace")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule31), EA.EnumMVErrorType.mvError, GetRuleStr(rule31));
                    isValid = false;
                }
            }
        }

        private void DoRule32(EA.Repository Repository, EA.Package Package)
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
                    Project.PublishResult(LookupMap(rule32), EA.EnumMVErrorType.mvError, GetRuleStr(rule32));
                    isValid = false;
                }
            }
        }

        private void DoRule33(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisInteractionSpace")
            {
                if (Element.Elements.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule33), EA.EnumMVErrorType.mvError, GetRuleStr(rule33));
                    isValid = false;
                }
            }
        }

        private void DoRule34(EA.Repository Repository, EA.Element Element)
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
                        Project.PublishResult(LookupMap(rule34), EA.EnumMVErrorType.mvError, GetRuleStr(rule34));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule35(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisIS-BEAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisInteractionSpace" || supplier.Stereotype != "XisBusinessEntity")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule35), EA.EnumMVErrorType.mvError, GetRuleStr(rule35));
                    isValid = false;
                }
            }
        }

        private void DoRule36(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisIS-MenuAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisInteractionSpace" || supplier.Stereotype != "XisMenu")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule36), EA.EnumMVErrorType.mvError, GetRuleStr(rule36));
                    isValid = false;
                }
            }
        }

        private void DoRule37(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisIS-DialogAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (client.Stereotype != "XisInteractionSpace" || supplier.Stereotype != "XisDialog")
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule37), EA.EnumMVErrorType.mvError, GetRuleStr(rule37));
                    isValid = false;
                }
            }
        }

        private void DoRule38(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisGesture")
            {
                if (Element.Methods.Count == 1)
                {
                    EA.Method method = Element.Methods.GetAt(0);

                    if (method.Stereotype != "XisAction")
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule38), EA.EnumMVErrorType.mvError, GetRuleStr(rule38));
                        isValid = false;
                    }
                }
                else
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule38), EA.EnumMVErrorType.mvError, GetRuleStr(rule38));
                    isValid = false;
                }
            }
        }

        private void DoRule39(EA.Repository Repository, EA.Connector Connector)
        {
            if (Connector.Stereotype == "XisWidget-GestureAssociation")
            {
                EA.Element client = Repository.GetElementByID(Connector.ClientID);
                EA.Element supplier = Repository.GetElementByID(Connector.SupplierID);

                if (supplier.Stereotype != "XisGesture" ||
                    (client.Stereotype != "XisLabel" && client.Stereotype != "XisTextBox" && client.Stereotype != "XisCheckBox"
                     && client.Stereotype != "Button" && client.Stereotype != "Link" && client.Stereotype != "XisImage"
                     && client.Stereotype != "XisDatePicker" && client.Stereotype != "XisTimePicker" && client.Stereotype != "XisDropdown"
                     && client.Stereotype != "XisListItem" && client.Stereotype != "XisMenuItem"))
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule39), EA.EnumMVErrorType.mvError, GetRuleStr(rule39));
                    isValid = false;
                }
            }
        }

        private void DoRule40(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisList")
            {
                if (Element.Elements.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule40), EA.EnumMVErrorType.mvError, GetRuleStr(rule40));
                    isValid = false;
                }
            }
        }

        private void DoRule41(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisList")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        if (el.Stereotype != "XisListItem" && el.Stereotype != "XisListGroup")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule41), EA.EnumMVErrorType.mvError, GetRuleStr(rule41));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule42(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListGroup")
            {
                if (Element.Elements.Count > 0)
                {
                    bool hasItem = false;
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        if (el.Stereotype == "XisListItem")
                        {
                            hasItem = true;
                            break;
                        }
                    }

                    if (!hasItem)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule42), EA.EnumMVErrorType.mvError, GetRuleStr(rule42));
                        isValid = false;
                    }
                }
                else
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule42), EA.EnumMVErrorType.mvError, GetRuleStr(rule42));
                    isValid = false;
                }
            }
        }

        private void DoRule43(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListGroup")
            {
                if (Element.Elements.Count > 0)
                {
                    int itemCounter = 0;
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        if (el.Stereotype == "XisListItem")
                        {
                            itemCounter++;
                        }

                        if (itemCounter > 1)
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule43), EA.EnumMVErrorType.mvError, GetRuleStr(rule43));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule44(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListItem")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype != "XisLabel" && el.Stereotype != "XisTextBox" && el.Stereotype != "XisCheckBox"
                            && el.Stereotype != "Button" && el.Stereotype != "Link" && el.Stereotype != "XisImage"
                            && el.Stereotype != "XisDatePicker" && el.Stereotype != "XisTimePicker" && el.Stereotype != "XisWebView"
                            && el.Stereotype != "XisMapView" && el.Stereotype != "XisDropdown")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule44), EA.EnumMVErrorType.mvError, GetRuleStr(rule44));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule45(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMenu")
            {
                if (Element.Elements.Count == 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule45), EA.EnumMVErrorType.mvError, GetRuleStr(rule45));
                    isValid = false;
                }
            }
        }

        private void DoRule46(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMenu")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype != "XisMenuItem" && el.Stereotype != "XisMenuGroup")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule46), EA.EnumMVErrorType.mvError, GetRuleStr(rule46));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule47(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMenuGroup")
            {
                if (Element.Elements.Count > 0)
                {
                    bool hasItem = false;
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype == "XisMenuItem")
                        {
                            hasItem = true;
                            break;
                        }
                    }

                    if (!hasItem)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule47), EA.EnumMVErrorType.mvError, GetRuleStr(rule47));
                        isValid = false;
                    }
                }
                else
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule47), EA.EnumMVErrorType.mvError, GetRuleStr(rule47));
                    isValid = false;
                }
            }
        }

        private void DoRule48(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMenuGroup")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype != "XisListItem")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule48), EA.EnumMVErrorType.mvError, GetRuleStr(rule48));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule49(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMenuItem")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    Project.PublishResult(LookupMap(rule49), EA.EnumMVErrorType.mvError, GetRuleStr(rule49));
                    isValid = false;
                }
            }
        }

        private void DoRule50(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisMenu")
            {
                EA.TaggedValue menuType = M2MTransformer.GetTaggedValue(Element.TaggedValues, "type");

                if (menuType != null && menuType.Value == "OptionsMenu")
                {
                    if (Element.ParentID > 0)
                    {
                        EA.Element parent = Repository.GetElementByID(Element.ParentID);

                        if (parent.Stereotype != "XisInteractionSpace")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule50), EA.EnumMVErrorType.mvError, GetRuleStr(rule50));
                            isValid = false;
                        }
                    }
                    else if (Element.Connectors.Count > 0)
                    {
                        EA.Connector conn = null;
                        EA.Element end = null;

                        for (short i = 0; i < Element.Connectors.Count; i++)
                        {
                            conn = Element.Connectors.GetAt(i);

                            if (conn.ClientID != Element.ElementID)
                            {
                                end = Repository.GetElementByID(conn.ClientID);
                            }
                            else
                            {
                                end = Repository.GetElementByID(conn.SupplierID);
                            }

                            if ((conn.Stereotype == "XisIS-MenuAssociation" && end.Stereotype != "XisInteractionSpace")
                                || (conn.Stereotype != "XisIS-MenuAssociation" && end.Stereotype == "XisInteractionSpace"))
                            {
                                EA.Project Project = Repository.GetProjectInterface();
                                Project.PublishResult(LookupMap(rule50), EA.EnumMVErrorType.mvError, GetRuleStr(rule50));
                                isValid = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule50), EA.EnumMVErrorType.mvError, GetRuleStr(rule50));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule51_54_57(EA.Repository Repository, EA.Element Element, String stereotype)
        {
            if (Element.Type == "Class" && Element.Stereotype == stereotype)
            {
                String onTap = M2MTransformer.GetTaggedValue(Element.TaggedValues, "onTap").Value;

                if (Element.Methods.Count > 0)
                {
                    EA.Method method = null;
                    bool noOnTap = false;

                    for (short i = 0; i < Element.Methods.Count; i++)
                    {
                        method = Element.Methods.GetAt(i);

                        if (method.Stereotype == "XisAction" && method.Name != onTap)
                        {
                            noOnTap = true;
                            break;
                        }
                    }

                    if (noOnTap)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        switch (stereotype)
                        {
                            case "XisButton":
                                Project.PublishResult(LookupMap(rule51), EA.EnumMVErrorType.mvError, GetRuleStr(rule51));
                                break;
                            case "XisLink":
                                Project.PublishResult(LookupMap(rule54), EA.EnumMVErrorType.mvError, GetRuleStr(rule54));
                                break;
                            case "XisMenuItem":
                                Project.PublishResult(LookupMap(rule57), EA.EnumMVErrorType.mvError, GetRuleStr(rule57));
                                break;
                        }
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule52_55_58(EA.Repository Repository, EA.Element Element, String stereotype)
        {
            if (Element.Type == "Class" && Element.Stereotype == stereotype)
            {
                String onTap = M2MTransformer.GetTaggedValue(Element.TaggedValues, "onTap").Value;

                if (!string.IsNullOrEmpty(onTap))
                {
                    if (Element.Methods.Count > 0)
                    {
                        EA.Method method = null;
                        bool exists = false;

                        for (short i = 0; i < Element.Methods.Count; i++)
                        {
                            method = Element.Methods.GetAt(i);

                            if (method.Stereotype == "XisAction" && method.Name == onTap)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            switch (stereotype)
                            {
                                case "XisButton":
                                    Project.PublishResult(LookupMap(rule52), EA.EnumMVErrorType.mvError, GetRuleStr(rule52));
                                    break;
                                case "XisLink":
                                    Project.PublishResult(LookupMap(rule55), EA.EnumMVErrorType.mvError, GetRuleStr(rule55));
                                    break;
                                case "XisMenuItem":
                                    Project.PublishResult(LookupMap(rule58), EA.EnumMVErrorType.mvError, GetRuleStr(rule58));
                                    break;
                            }
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        switch (stereotype)
                        {
                            case "XisButton":
                                Project.PublishResult(LookupMap(rule52), EA.EnumMVErrorType.mvError, GetRuleStr(rule52));
                                break;
                            case "XisLink":
                                Project.PublishResult(LookupMap(rule55), EA.EnumMVErrorType.mvError, GetRuleStr(rule55));
                                break;
                            case "XisMenuItem":
                                Project.PublishResult(LookupMap(rule58), EA.EnumMVErrorType.mvError, GetRuleStr(rule58));
                                break;
                        }
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule53_56_59(EA.Repository Repository, EA.Element Element, String stereotype)
        {
            if (Element.Type == "Class" && Element.Stereotype == stereotype)
            {
                if (Element.Methods.Count > 0)
                {
                    EA.Method method = null;
                    int actionCounter = 0;

                    for (short i = 0; i < Element.Methods.Count; i++)
                    {
                        method = Element.Methods.GetAt(i);

                        if (method.Stereotype == "XisAction")
                        {
                            actionCounter++;
                        }
                    }

                    if (actionCounter > 1)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        switch (stereotype)
                        {
                            case "XisButton":
                                Project.PublishResult(LookupMap(rule53), EA.EnumMVErrorType.mvError, GetRuleStr(rule53));
                                break;
                            case "XisLink":
                                Project.PublishResult(LookupMap(rule56), EA.EnumMVErrorType.mvError, GetRuleStr(rule56));
                                break;
                            case "XisMenuItem":
                                Project.PublishResult(LookupMap(rule59), EA.EnumMVErrorType.mvError, GetRuleStr(rule59));
                                break;
                        }
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule60(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListItem")
            {
                String onTap = M2MTransformer.GetTaggedValue(Element.TaggedValues, "onTap").Value;
                String onLongTap = M2MTransformer.GetTaggedValue(Element.TaggedValues, "onLongTap").Value;

                if (Element.Methods.Count > 0)
                {
                    EA.Method method = null;
                    bool noDefaultGestures = false;

                    for (short i = 0; i < Element.Methods.Count; i++)
                    {
                        method = Element.Methods.GetAt(i);

                        if (method.Stereotype == "XisAction" && method.Name != onTap && method.Name != onLongTap)
                        {
                            noDefaultGestures = true;
                            break;
                        }
                    }

                    if (!noDefaultGestures)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule60), EA.EnumMVErrorType.mvError, GetRuleStr(rule60));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule61(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListItem")
            {
                String onTap = M2MTransformer.GetTaggedValue(Element.TaggedValues, "onTap").Value;

                if (!string.IsNullOrEmpty(onTap))
                {
                    if (Element.Methods.Count > 0)
                    {
                        EA.Method method = null;
                        bool exists = false;

                        for (short i = 0; i < Element.Methods.Count; i++)
                        {
                            method = Element.Methods.GetAt(i);

                            if (method.Stereotype == "XisAction" && method.Name == onTap)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule61), EA.EnumMVErrorType.mvError, GetRuleStr(rule61));
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule61), EA.EnumMVErrorType.mvError, GetRuleStr(rule61));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule62(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListItem")
            {
                String onLongTap = M2MTransformer.GetTaggedValue(Element.TaggedValues, "onLongTap").Value;

                if (!string.IsNullOrEmpty(onLongTap))
                {
                    if (Element.Methods.Count > 0)
                    {
                        EA.Method method = null;
                        bool exists = false;

                        for (short i = 0; i < Element.Methods.Count; i++)
                        {
                            method = Element.Methods.GetAt(i);

                            if (method.Stereotype == "XisAction" && method.Name == onLongTap)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule62), EA.EnumMVErrorType.mvError, GetRuleStr(rule62));
                            isValid = false;
                        }
                    }
                    else
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule62), EA.EnumMVErrorType.mvError, GetRuleStr(rule62));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule63(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisListItem")
            {
                if (Element.Methods.Count > 0)
                {
                    EA.Method method = null;
                    int actionCounter = 0;

                    for (short i = 0; i < Element.Methods.Count; i++)
                    {
                        method = Element.Methods.GetAt(i);

                        if (method.Stereotype == "XisAction")
                        {
                            actionCounter++;
                        }
                    }

                    if (actionCounter > 2)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule63), EA.EnumMVErrorType.mvError, GetRuleStr(rule63));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule64(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisDialog")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Element el = null;
                    int buttonCounter = 0;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype == "Class" && el.Stereotype == "XisButton")
                        {
                            buttonCounter++;
                        }
                        else
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule64), EA.EnumMVErrorType.mvError, GetRuleStr(rule64));
                            isValid = false;
                            break;
                        }
                    }

                    if (buttonCounter > 3)
                    {
                        EA.Project Project = Repository.GetProjectInterface();
                        Project.PublishResult(LookupMap(rule64), EA.EnumMVErrorType.mvError, GetRuleStr(rule64));
                        isValid = false;
                    }
                }
            }
        }

        private void DoRule65(EA.Repository Repository, EA.Element Element)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisDialog")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Element el = null;

                    for (short i = 0; i < Element.Elements.Count; i++)
                    {
                        el = Element.Elements.GetAt(i);

                        if (el.Stereotype != "Class" || el.Stereotype != "XisMapMarker")
                        {
                            EA.Project Project = Repository.GetProjectInterface();
                            Project.PublishResult(LookupMap(rule65), EA.EnumMVErrorType.mvError, GetRuleStr(rule65));
                            isValid = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DoRule66_to_75(EA.Repository Repository, EA.Element Element, string stereotype)
        {
            if (Element.Type == "Class" && Element.Stereotype == "XisDialog")
            {
                if (Element.Elements.Count > 0)
                {
                    EA.Project Project = Repository.GetProjectInterface();
                    switch (stereotype)
                    {
                        case "XisLabel":
                            Project.PublishResult(LookupMap(rule66), EA.EnumMVErrorType.mvError, GetRuleStr(rule66));
                            break;
                        case "XisTextBox":
                            Project.PublishResult(LookupMap(rule67), EA.EnumMVErrorType.mvError, GetRuleStr(rule67));
                            break;
                        case "XisCheckBox":
                            Project.PublishResult(LookupMap(rule68), EA.EnumMVErrorType.mvError, GetRuleStr(rule68));
                            break;
                        case "XisButton":
                            Project.PublishResult(LookupMap(rule69), EA.EnumMVErrorType.mvError, GetRuleStr(rule69));
                            break;
                        case "XisLink":
                            Project.PublishResult(LookupMap(rule70), EA.EnumMVErrorType.mvError, GetRuleStr(rule70));
                            break;
                        case "XisImage":
                            Project.PublishResult(LookupMap(rule71), EA.EnumMVErrorType.mvError, GetRuleStr(rule71));
                            break;
                        case "XisDatePicker":
                            Project.PublishResult(LookupMap(rule72), EA.EnumMVErrorType.mvError, GetRuleStr(rule72));
                            break;
                        case "XisTimePicker":
                            Project.PublishResult(LookupMap(rule73), EA.EnumMVErrorType.mvError, GetRuleStr(rule73));
                            break;
                        case "XisWebView":
                            Project.PublishResult(LookupMap(rule74), EA.EnumMVErrorType.mvError, GetRuleStr(rule74));
                            break;
                        case "XisDropdown":
                            Project.PublishResult(LookupMap(rule75), EA.EnumMVErrorType.mvError, GetRuleStr(rule75));
                            break;
                    }
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
