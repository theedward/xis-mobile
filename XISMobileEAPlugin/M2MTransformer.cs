using System;
using System.Collections.Generic;
using System.Linq;
using XISMobileEAPlugin.InteractionSpace;
using System.Windows.Forms;

namespace XISMobileEAPlugin
{
    static class M2MTransformer
    {
        private static XisInteractionSpace homeIS;
        private static EA.Diagram homeDiagram;
        private static EA.Diagram nsDiagram;
        private static EA.Repository repository;

        public static void ProcessUseCase(EA.Repository rep, EA.Package navigationPackage, EA.Package interactionPackage,
            List<EA.Element> useCases, string patternType = null)
        {
            nsDiagram = XISMobileHelper.CreateDiagram(navigationPackage, "Navigation Space",
                "XIS-Mobile_Diagrams::NavigationSpaceViewModel");
            repository = rep;
            bool isStartingUC = true;

            if (patternType != null)
            {
                homeDiagram = XISMobileHelper.CreateDiagram(interactionPackage, "HomeIS Diagram",
                    "XIS-Mobile_Diagrams::InteractionSpaceViewModel");
                homeIS = new XisInteractionSpace(repository, interactionPackage, homeDiagram, "HomeIS", "Home", true);
            }

            foreach (EA.Element useCase in useCases)
            {
                foreach (EA.Connector connector in useCase.Connectors)
                {
                    if (connector.Stereotype == "XisOperatesOnAssociation")
                    {
                        EA.Element be = repository.GetElementByID(connector.SupplierID);
                        XisEntity master = null;
                        List<XisEntity> details = new List<XisEntity>();
                        List<XisEntity> references = new List<XisEntity>();

                        #region [Get Entities (Master, Details and References)]
                        foreach (EA.Connector beConn in be.Connectors)
                        {
                            switch (beConn.Stereotype)
                            {
                                case "XisMasterAssociation":
                                    master = new XisEntity(repository.GetElementByID(beConn.SupplierID),
                                        GetConnectorTag(beConn.TaggedValues, "filter").Value);
                                    break;
                                case "XisDetailAssociation":
                                    details.Add(new XisEntity(repository.GetElementByID(beConn.SupplierID),
                                        GetConnectorTag(beConn.TaggedValues, "filter").Value));
                                    break;
                                case "XisReferenceAssociation":
                                    references.Add(new XisEntity(repository.GetElementByID(beConn.SupplierID),
                                        GetConnectorTag(beConn.TaggedValues, "filter").Value));
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion

                        if (master != null)
                        {
                            //MessageBox.Show(master.Element.Name);

                            #region [Add cardinality to Entities]
                            if (details.Count > 0 || references.Count > 0)
                            {
                                foreach (EA.Connector conn in master.Element.Connectors)
                                {
                                    foreach (XisEntity detail in details)
                                    {
                                        if (conn.ClientID == detail.Element.ElementID)
                                        {
                                            detail.Cardinality = string.IsNullOrEmpty(conn.ClientEnd.Cardinality) ? "1" : conn.ClientEnd.Cardinality;
                                            detail.BeCardinality = string.IsNullOrEmpty(conn.SupplierEnd.Cardinality) ? "1" : conn.SupplierEnd.Cardinality;
                                            //MessageBox.Show("client: " + detail.Element.Name);
                                        }
                                        else if (conn.SupplierID == detail.Element.ElementID)
                                        {
                                            detail.Cardinality = string.IsNullOrEmpty(conn.SupplierEnd.Cardinality) ? "1" : conn.SupplierEnd.Cardinality;
                                            detail.BeCardinality = string.IsNullOrEmpty(conn.ClientEnd.Cardinality) ? "1" : conn.ClientEnd.Cardinality;
                                            //MessageBox.Show("supplier: " + detail.Element.Name);
                                        }
                                    }

                                    foreach (XisEntity reference in references)
                                    {
                                        if (conn.ClientID == reference.Element.ElementID)
                                        {
                                            reference.Cardinality = string.IsNullOrEmpty(conn.ClientEnd.Cardinality) ? "1" : conn.ClientEnd.Cardinality;
                                            reference.BeCardinality = string.IsNullOrEmpty(conn.SupplierEnd.Cardinality) ? "1" : conn.SupplierEnd.Cardinality;
                                            //MessageBox.Show("client: " + reference.Element.Name);
                                        }
                                        else if (conn.SupplierID == reference.Element.ElementID)
                                        {
                                            reference.Cardinality = string.IsNullOrEmpty(conn.SupplierEnd.Cardinality) ? "1" : conn.SupplierEnd.Cardinality;
                                            reference.BeCardinality = string.IsNullOrEmpty(conn.ClientEnd.Cardinality) ? "1" : conn.ClientEnd.Cardinality;
                                            //MessageBox.Show("supplier: " + reference.Element.Name);
                                        }
                                    }
                                }
                            }
                            #endregion

                            master.Details = details;
                            master.References = references;

                            EA.TaggedValue ucType = GetTaggedValue(useCase.TaggedValues, "type");

                            if (ucType != null)
                            {
                                if (ucType.Value == "Manager")
                                {
                                    if (isStartingUC && useCases.Count > 1)
                                    {
                                        ProcessManagerUseCase(interactionPackage, master, useCase, be, isStartingUC,
                                            useCases.GetRange(1, useCases.Count-1), patternType);
                                    }
                                    else
                                    {
                                        ProcessManagerUseCase(interactionPackage, master, useCase, be, isStartingUC, null, patternType);
                                    }
                                }
                                else if (ucType.Value == "Detail")
                                {
                                    if (isStartingUC && useCases.Count > 1)
                                    {
                                        ProcessDetailUseCase(interactionPackage, master, useCase, be, isStartingUC,
                                            useCases.GetRange(1, useCases.Count - 1), patternType);
                                    }
                                    else
                                    {
                                        ProcessDetailUseCase(interactionPackage, master, useCase, be, isStartingUC, null, patternType);
                                    }
                                }
                            }
                        }
                    }
                }
                isStartingUC = false;
            }
            if (patternType != null)
            {
                ComputePositions(homeIS, homeDiagram);
            }
        }

        private static void AddToHomeISByPattern(EA.Element useCase, XisInteractionSpace targetIS, string patternType) {
            String actionName = "goTo" + targetIS.Element.Name;

            switch (patternType)
            {
                case "Springboard":
                    XisButton b = new XisButton(repository, homeIS, homeDiagram, useCase.Name, actionName);
                    XISMobileHelper.CreateXisAction(repository, b.Element, actionName, ActionType.Read, targetIS.Element.Name);
                    CreateXisNavigationAssociation(actionName, homeIS, targetIS);
                    break;
                case "List Menu":
                    XisList list = null;
                    if (homeIS.Widgets.Count > 0)
                    {
                        list = homeIS.Widgets.First() as XisList;
                    }
                    else
                    {
                        list = new XisList(repository, homeDiagram, homeIS, homeIS.Element.Name + "List");
                    }
                    XisListItem item = new XisListItem(repository, homeDiagram, list, useCase.Name, actionName);
                    XISMobileHelper.CreateXisAction(repository, item.Element, actionName, ActionType.Read, targetIS.Element.Name);
                    CreateXisNavigationAssociation(actionName, homeIS, targetIS);
                    break;
                case "Tab Menu":
                    // TODO: Implement Tab
                    break;
                default:
                    break;
            }
        }

        public static void ProcessManagerUseCase(EA.Package package, XisEntity master,
            EA.Element useCase, EA.Element be, bool isStartingUC, List<EA.Element> useCases = null, String patternType = null)
        {
            // Create IS Diagram
            EA.Diagram listDiagram = XISMobileHelper.CreateDiagram(package, master.Element.Name + "ListIS Diagram",
                "XIS-Mobile_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace listIS = new XisInteractionSpace(repository, package, listDiagram,
                master.Element.Name + "ListIS", "Manage " + master.Element.Name + "s", isStartingUC, !isStartingUC);

            if (isStartingUC && patternType == null)
            {
                homeIS = listIS;
            }

            // List Creation
            XisList list = new XisList(repository, listDiagram, listIS, master.Element.Name + "List");
            list.SetValue(master.Element.Name);

            //if (ContainsSearch(operations))
            //{
            //    string searchBy = "";
            //    string tv = null;
                
            //    foreach (EA.Attribute attr in master.Element.Attributes)
            //    {
            //        tv = GetAttributeTag(attr.TaggedValues, "isKey").Value;
            //        if (!string.IsNullOrEmpty(tv) && tv.ToLower() == "true")
            //        {
            //            searchBy += master.Element.Name + "." + attr.Name + ";";
            //        }
            //    }

            //    if (string.IsNullOrEmpty(searchBy))
            //    {
            //        list.SetSearchBy(master.Element.Name + ".Id");
            //    }
            //    else
            //    {
            //        list.SetSearchBy(searchBy.Substring(0, searchBy.Length - 1));
            //    }
            //}

            XisListItem item = new XisListItem(repository, listDiagram, list, list.Element.Name + "Item");
            if (ContainsUpdateMaster(useCase))
            {
                string actionName = "edit" + master.Element.Name;
                item.SetOnTapAction(actionName);
            }
            else if (ContainsReadMaster(useCase))
            {
                string actionName = "view" + master.Element.Name;
                item.SetOnTapAction(actionName);
            }

            if (master.Element.Attributes.Count > 1)
            {
                EA.Attribute first = master.Element.Attributes.GetAt(0);
                EA.Attribute second = master.Element.Attributes.GetAt(1);
                XisLabel lbl1 = new XisLabel(repository, item, listDiagram, first.Name + "Lbl");
                lbl1.SetValue(master.Element.Name + "." + first.Name);
                XisLabel lbl2 = new XisLabel(repository, item, listDiagram, second.Name + "Lbl");
                lbl2.SetValue(master.Element.Name + "." + second.Name);
            }
            else if (master.Element.Attributes.Count == 1)
            {
                EA.Attribute attr = master.Element.Attributes.GetAt(0);
                item.SetValue(master.Element.Name + "." + attr.Name);
            }

            // Read, Update, Create
            Dictionary<ActionType, XisMenuItem> detailModes = new Dictionary<ActionType, XisMenuItem>(3);

            #region Create Context Menu
            if (ContainsReadMaster(useCase) || ContainsUpdateMaster(useCase) || ContainsDeleteMaster(useCase))
            {
                XisMenu context = new XisMenu(repository, listDiagram, package, list.Element.Name + "ContextMenu", MenuType.ContextMenu);

                if (ContainsReadMaster(useCase))
                {
                    string actionName = "view" + master.Element.Name;
                    XisMenuItem contextItem = new XisMenuItem(repository, listDiagram, context,
                        "View" + master.Element.Name + "Item", actionName);
                    detailModes.Add(ActionType.Read, contextItem);
                }
                
                if (ContainsUpdateMaster(useCase))
                {
                    string actionName = "edit" + master.Element.Name;
                    XisMenuItem contextItem = new XisMenuItem(repository, listDiagram, context,
                        "Edit" + master.Element.Name + "Item", actionName);
                    detailModes.Add(ActionType.Update, contextItem);
                }

                if (ContainsDeleteMaster(useCase))
                {
                    string actionName = "delete" + master.Element.Name;
                    XisMenuItem contextItem = new XisMenuItem(repository, listDiagram, context,
                        "Delete" + master.Element.Name + "Item", actionName);
                    XISMobileHelper.CreateXisAction(repository, contextItem.Element, actionName, ActionType.Delete);
                }
                listIS.ContextMenu = context;
            }
            #endregion

            // Navigation between home UC and the others
            if (patternType != null)
            {
                AddToHomeISByPattern(useCase, listIS, patternType);
            }
            else if (isStartingUC)
            {
                if (useCases != null)
                {
                    AssociateFirstSubSpaces(listDiagram, useCases, listIS, be.ElementID, master.Element.Name);
                }
            }

            #region Create Options Menu
            if (ContainsCreateMaster(useCase) || ContainsDeleteMaster(useCase))
            {
                XisMenu menu = new XisMenu(repository, listDiagram, listIS, listIS.Element.Name + "Menu", MenuType.OptionsMenu);

                if (ContainsCreateMaster(useCase))
                {
                    string actionName = "create" + master.Element.Name;
                    XisMenuItem menuItem = new XisMenuItem(repository, listDiagram, menu,
                        "Create" + master.Element.Name + "Item", actionName);
                    detailModes.Add(ActionType.Create, menuItem);
                }

                if (ContainsDeleteMaster(useCase))
                {
                    string actionName = "deleteAll" + master.Element.Name + "s";
                    XisMenuItem menuItem = new XisMenuItem(repository, listDiagram, menu,
                        "DeleteAll" + master.Element.Name + "Item", actionName);
                    XISMobileHelper.CreateXisAction(repository, menuItem.Element, actionName, ActionType.DeleteAll);
                }
                listIS.Menu = menu;
            } 
            #endregion

            if (detailModes.Count > 0 || item.GetOnTapAction() != null)
            {
                XisInteractionSpace detailIS = CreateMasterEditorIS(package, master, listIS, useCase, be);
                foreach (ActionType key in detailModes.Keys)
                {
                    XisMenuItem mItem = detailModes[key];
                    XISMobileHelper.CreateXisAction(repository, mItem.Element, mItem.GetOnTapAction(),
                        key, detailIS.Element.Name);
                    CreateXisNavigationAssociation(mItem.GetOnTapAction(), listIS, detailIS);
                }

                if (item.GetOnTapAction() != null)
                {
                    XISMobileHelper.CreateXisAction(repository, item.Element, item.GetOnTapAction(),
                        ActionType.Update, detailIS.Element.Name);
                    CreateXisNavigationAssociation(item.GetOnTapAction(), listIS, detailIS);
                }
            }

            ComputePositions(listIS, listDiagram);

            if (listIS.ContextMenu != null)
            {
                EA.DiagramObject obj = listIS.GetDiagramObject(listDiagram);
                int center = (obj.top + obj.bottom) / -2;
                listIS.ContextMenu.SetPosition(listDiagram, obj.right + 50, obj.right + 330, -obj.top, -obj.top + 70);
                ComputePositions(listIS.ContextMenu, listDiagram);
            }

            // Associate BE
            AssociateBEtoIS(listDiagram, listIS, be);

            if (!isStartingUC)
            {
                // TODO: Link subspaces
                //CreateXisNavigationAssociation(repository, "goTo" + listIS.Element.Name, homeIS, listIS);
            }
        }

        public static void ProcessDetailUseCase(EA.Package package, XisEntity master,
            EA.Element useCase, EA.Element be, bool isStartingUC, List<EA.Element> useCases = null, String patternType = null)
        {
            EA.Diagram detailDiagram = XISMobileHelper.CreateDiagram(package, master.Element.Name + "EditorIS Diagram",
                "XIS-Mobile_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace detailIS = new XisInteractionSpace(repository, package, detailDiagram,
                master.Element.Name + "EditorIS", null, isStartingUC, !isStartingUC);

            if (isStartingUC && patternType == null)
            {
                homeIS = detailIS;
            }

            #region Process Master attributes
            if (!string.IsNullOrEmpty(master.Filter))
            {
                List<EA.Attribute> filtered = GetFilteredAttributeList(master);
                foreach (EA.Attribute attr in filtered)
                {
                    XISMobileHelper.ProcessXisAttribute(repository, detailDiagram, detailIS, attr, master.Element.Name);
                }
            }
            else
            {
                foreach (EA.Attribute attr in master.Element.Attributes)
                {
                    XISMobileHelper.ProcessXisAttribute(repository, detailDiagram, detailIS, attr, master.Element.Name);
                }
            }
            #endregion

            #region Process Details info
            foreach (XisEntity d in master.Details)
            {
                if (d.Cardinality == "*")
                {
                    // Needs Manager screen
                    string actionName = "goTo" + d.Element.Name + "ManagerIS";
                    XisButton btn = new XisButton(repository, detailIS, detailDiagram, d.Element.Name + "ManagerButton", actionName);
                    btn.SetValue("Manage " + d.Element.Name);
                    XisInteractionSpace managerIS = CreateDetailOrRefManagerIS(package, d, detailIS, useCase, true, be);
                    XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read, managerIS.Element.Name);
                    CreateXisNavigationAssociation(actionName, detailIS, managerIS);
                }
                else
                {
                    List<EA.Attribute> filtered = new List<EA.Attribute>();

                    if (!string.IsNullOrEmpty(d.Filter))
                    {
                        filtered = GetFilteredAttributeList(d);
                    }

                    if (filtered.Count > 0)
                    {
                        if (filtered.Count > 3)
                        {
                            string actionName = "goTo" + d.Element.Name + "EditorIS";
                            XisButton btn = new XisButton(repository, detailIS, detailDiagram, d.Element.Name + "EditorButton", actionName);
                            btn.SetValue(d.Element.Name);
                            XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, d, detailIS, useCase, true, be);
                            XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read, editorIS.Element.Name);
                            CreateXisNavigationAssociation(actionName, detailIS, editorIS);
                        }
                        else
                        {
                            foreach (EA.Attribute attr in filtered)
                            {
                                XISMobileHelper.ProcessXisAttribute(repository, detailDiagram, detailIS, attr, d.Element.Name);
                            }

                            if (ContainsReadDetail(useCase))
                            {
                                if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                                {
                                    XisVisibilityBoundary b = new XisVisibilityBoundary(repository, detailDiagram, detailIS,
                                        "Save" + d.Element.Name, ContainsCreateDetail(useCase), false, ContainsUpdateDetail(useCase));
                                    string actionName = "save" + d.Element.Name;
                                    XisButton btn = new XisButton(repository, b, detailDiagram, d.Element.Name + "SaveButton", actionName);
                                    btn.SetValue("Save " + d.Element.Name);
                                    XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                                }
                            }
                            else if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                            {
                                string actionName = "save" + d.Element.Name;
                                XisButton btn = new XisButton(repository, detailIS, detailDiagram, d.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + d.Element.Name);
                                XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                    }
                    else if (d.Element.Attributes.Count > 3)
                    {
                        string actionName = "goTo" + d.Element.Name + "EditorIS";
                        XisButton btn = new XisButton(repository, detailIS, detailDiagram, d.Element.Name + "EditorButton", actionName);
                        btn.SetValue(d.Element.Name);
                        XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, d, detailIS, useCase, true, be);
                        XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read, editorIS.Element.Name);
                        CreateXisNavigationAssociation(actionName, detailIS, editorIS);
                    }
                    else
                    {
                        foreach (EA.Attribute attr in d.Element.Attributes)
                        {
                            XISMobileHelper.ProcessXisAttribute(repository, detailDiagram, detailIS, attr, d.Element.Name);
                        }

                        if (ContainsReadDetail(useCase))
                        {
                            if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                            {
                                XisVisibilityBoundary b = new XisVisibilityBoundary(repository, detailDiagram, detailIS,
                                    "Save" + d.Element.Name, ContainsCreateDetail(useCase), false, ContainsUpdateDetail(useCase));
                                string actionName = "save" + d.Element.Name;
                                XisButton btn = new XisButton(repository, b, detailDiagram, d.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + d.Element.Name);
                                XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                        else if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                        {
                            string actionName = "save" + d.Element.Name;
                            XisButton btn = new XisButton(repository, detailIS, detailDiagram, d.Element.Name + "SaveButton", actionName);
                            btn.SetValue("Save " + d.Element.Name);
                            XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                        }
                    }
                }
            }
            #endregion

            #region Process References info
            foreach (XisEntity r in master.References)
            {
                if (r.Cardinality == "*")
                {
                    // Needs Manager screen
                    string actionName = "goTo" + r.Element.Name + "ManagerIS";
                    XisButton btn = new XisButton(repository, detailIS, detailDiagram, r.Element.Name + "ManagerButton", actionName);
                    btn.SetValue("Manage " + r.Element.Name);
                    XisInteractionSpace viewIS = CreateDetailOrRefManagerIS(package, r, detailIS, useCase, false, be);
                    XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read, viewIS.Element.Name);
                    CreateXisNavigationAssociation(actionName, detailIS, viewIS);
                }
                else
                {
                    List<EA.Attribute> filtered = new List<EA.Attribute>();

                    if (!string.IsNullOrEmpty(r.Filter))
                    {
                        filtered = GetFilteredAttributeList(r);
                    }

                    if (filtered.Count > 0)
                    {
                        if (filtered.Count > 3)
                        {
                            string actionName = "goTo" + r.Element.Name + "EditorIS";
                            XisButton btn = new XisButton(repository, detailIS, detailDiagram, r.Element.Name + "EditorButton", actionName);
                            btn.SetValue(r.Element.Name);
                            XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, r, detailIS, useCase, false, be);
                            XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read, editorIS.Element.Name);
                            CreateXisNavigationAssociation(actionName, detailIS, editorIS);
                        }
                        else
                        {
                            foreach (EA.Attribute attr in filtered)
                            {
                                XISMobileHelper.ProcessXisAttribute(repository, detailDiagram, detailIS, attr, r.Element.Name);
                            }

                            if (ContainsReadReference(useCase))
                            {
                                if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                                {
                                    XisVisibilityBoundary b = new XisVisibilityBoundary(repository, detailDiagram, detailIS,
                                        "Save" + r.Element.Name, ContainsCreateReference(useCase), false, ContainsUpdateReference(useCase));
                                    string actionName = "save" + r.Element.Name;
                                    XisButton btn = new XisButton(repository, b, detailDiagram, r.Element.Name + "SaveButton", actionName);
                                    btn.SetValue("Save " + r.Element.Name);
                                    XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                                }
                            }
                            else if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                            {
                                string actionName = "save" + r.Element.Name;
                                XisButton btn = new XisButton(repository, detailIS, detailDiagram, r.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + r.Element.Name);
                                XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                    }
                    else if (r.Element.Attributes.Count > 3)
                    {
                        string actionName = "goTo" + r.Element.Name + "EditorIS";
                        XisButton btn = new XisButton(repository, detailIS, detailDiagram, r.Element.Name + "EditorButton", actionName);
                        btn.SetValue(r.Element.Name);
                        XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, r, detailIS, useCase, false, be);
                        XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read, editorIS.Element.Name);
                        CreateXisNavigationAssociation(actionName, detailIS, editorIS);
                    }
                    else
                    {
                        foreach (EA.Attribute attr in r.Element.Attributes)
                        {
                            XISMobileHelper.ProcessXisAttribute(repository, detailDiagram, detailIS, attr, r.Element.Name);
                        }

                        if (ContainsReadReference(useCase))
                        {
                            if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                            {
                                XisVisibilityBoundary b = new XisVisibilityBoundary(repository, detailDiagram, detailIS,
                                    "Save" + r.Element.Name, ContainsCreateReference(useCase), false, ContainsUpdateReference(useCase));
                                string actionName = "save" + r.Element.Name;
                                XisButton btn = new XisButton(repository, b, detailDiagram, r.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + r.Element.Name);
                                XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                        else if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                        {
                            string actionName = "save" + r.Element.Name;
                            XisButton btn = new XisButton(repository, detailIS, detailDiagram, r.Element.Name + "SaveButton", actionName);
                            btn.SetValue("Save " + r.Element.Name);
                            XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                        }
                    }
                }
            }
            #endregion

            // Navigation between main UC and the others
            if (patternType != null)
            {
                AddToHomeISByPattern(useCase, detailIS, patternType);
            }
            else if (isStartingUC)
            {
                if (useCases != null)
                {
                    AssociateFirstSubSpaces(detailDiagram, useCases, detailIS, be.ElementID, master.Element.Name);
                }
            }

            if (ContainsCreateMaster(useCase) || ContainsUpdateMaster(useCase))
            {
                XisWidget parent = detailIS;

                if (ContainsReadMaster(useCase))
                {
                    parent = new XisVisibilityBoundary(repository, detailDiagram, detailIS, "Save" + master.Element.Name + "Boundary",
                        ContainsCreateMaster(useCase), false, ContainsUpdateMaster(useCase));
                }

                XisMenu menu = new XisMenu(repository, detailDiagram, parent, detailIS.Element.Name + "Menu", MenuType.OptionsMenu);

                string actionName = "save" + master.Element.Name;
                XisMenuItem menuItem = new XisMenuItem(repository, detailDiagram, menu, "Save" + master.Element.Name, actionName);
                XISMobileHelper.CreateXisAction(repository, menuItem.Element, actionName, ActionType.Save); 
            }

            ComputePositions(detailIS, detailDiagram);
            // Associate BE
            AssociateBEtoIS(detailDiagram, detailIS, be);

            if (detailIS.GetDiagramObject(nsDiagram) == null && isStartingUC)
            {
                detailIS.SetPosition(nsDiagram, 355, 445, 10, 80);
            }

            if (!isStartingUC)
            {
                // TODO: Link subspaces
                CreateXisNavigationAssociation("goTo" + detailIS.Element.Name, homeIS, detailIS);
            }
        }

        private static XisInteractionSpace CreateMasterEditorIS(EA.Package package, XisEntity master,
            XisInteractionSpace previousIS, EA.Element useCase, EA.Element be)
        {
            EA.Diagram diagram = XISMobileHelper.CreateDiagram(package, master.Element.Name + "EditorIS Diagram",
                "XIS-Mobile_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace detailIS = new XisInteractionSpace(repository, package, diagram, master.Element.Name + "EditorIS",
                master.Element.Name + " Editor", false, true);

            #region Process Master attributes
            if (!string.IsNullOrEmpty(master.Filter))
            {
                List<EA.Attribute> filtered = GetFilteredAttributeList(master);
                foreach (EA.Attribute attr in filtered)
                {
                    XISMobileHelper.ProcessXisAttribute(repository, diagram, detailIS, attr, master.Element.Name);
                }
            }
            else
            {
                foreach (EA.Attribute attr in master.Element.Attributes)
                {
                    XISMobileHelper.ProcessXisAttribute(repository, diagram, detailIS, attr, master.Element.Name);
                }
            }
            #endregion

            #region Write Details info
            foreach (XisEntity d in master.Details)
            {
                if (d.Cardinality == "*")
                {
                    // Needs Manager screen
                    string actionName = "goTo" + d.Element.Name + "ManagerIS";
                    XisButton btn = new XisButton(repository, detailIS, diagram, d.Element.Name + "ManagerButton", actionName);
                    btn.SetValue("Manage " + d.Element.Name);
                    XisInteractionSpace managerIS = CreateDetailOrRefManagerIS(package, d, detailIS, useCase, true, be);
                    XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read, managerIS.Element.Name);
                    CreateXisNavigationAssociation(actionName, detailIS, managerIS);
                }
                else
                {
                    List<EA.Attribute> filtered = new List<EA.Attribute>();

                    if (!string.IsNullOrEmpty(d.Filter))
                    {
                        filtered = GetFilteredAttributeList(d);
                    }

                    if (filtered.Count > 0)
                    {
                        if (filtered.Count > 3)
                        {
                            string actionName = "goTo" + d.Element.Name + "EditorIS";
                            XisButton btn = new XisButton(repository, detailIS, diagram, d.Element.Name + "EditorButton", actionName);
                            btn.SetValue(d.Element.Name);
                            XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, d, detailIS, useCase, true, be);
                            XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read, editorIS.Element.Name);
                            CreateXisNavigationAssociation(actionName, detailIS, editorIS);
                        }
                        else
                        {
                            foreach (EA.Attribute attr in filtered)
                            {
                                XISMobileHelper.ProcessXisAttribute(repository, diagram, detailIS, attr, d.Element.Name);
                            }

                            if (ContainsReadDetail(useCase))
                            {
                                if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                                {
                                    XisVisibilityBoundary b = new XisVisibilityBoundary(repository, diagram, detailIS,
                                        "Save" + d.Element.Name, ContainsCreateDetail(useCase), false, ContainsUpdateDetail(useCase));
                                    string actionName = "save" + d.Element.Name;
                                    XisButton btn = new XisButton(repository, b, diagram, d.Element.Name + "SaveButton", actionName);
                                    btn.SetValue("Save " + d.Element.Name);
                                    XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                                }
                            }
                            else if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                            {
                                string actionName = "save" + d.Element.Name;
                                XisButton btn = new XisButton(repository, detailIS, diagram, d.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + d.Element.Name);
                                XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                    }
                    else if (d.Element.Attributes.Count > 3)
                    {
                        string actionName = "goTo" + d.Element.Name + "EditorIS";
                        XisButton btn = new XisButton(repository, detailIS, diagram, d.Element.Name + "EditorButton", actionName);
                        btn.SetValue(d.Element.Name);
                        XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, d, detailIS, useCase, true, be);
                        XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read, editorIS.Element.Name);
                        CreateXisNavigationAssociation(actionName, detailIS, editorIS);
                    }
                    else
                    {
                        foreach (EA.Attribute attr in d.Element.Attributes)
                        {
                            XISMobileHelper.ProcessXisAttribute(repository, diagram, detailIS, attr, d.Element.Name);
                        }

                        if (ContainsReadDetail(useCase))
                        {
                            if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                            {
                                XisVisibilityBoundary b = new XisVisibilityBoundary(repository, diagram, detailIS,
                                    "Save" + d.Element.Name, ContainsCreateDetail(useCase), false, ContainsUpdateDetail(useCase));
                                string actionName = "save" + d.Element.Name;
                                XisButton btn = new XisButton(repository, b, diagram, d.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + d.Element.Name);
                                XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                        else if (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase))
                        {
                            string actionName = "save" + d.Element.Name;
                            XisButton btn = new XisButton(repository, detailIS, diagram, d.Element.Name + "SaveButton", actionName);
                            btn.SetValue("Save " + d.Element.Name);
                            XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                        }
                    }
                }
            }
            #endregion

            #region Write References info
            foreach (XisEntity r in master.References)
            {
                if (r.Cardinality == "*")
                {
                    // Needs Manager screen
                    string actionName = "goTo" + r.Element.Name + "ManagerIS";
                    XisButton btn = new XisButton(repository, detailIS, diagram, r.Element.Name + "ManagerButton", actionName);
                    btn.SetValue("Manage " + r.Element.Name);
                    XisInteractionSpace viewIS = CreateDetailOrRefManagerIS(package, r, detailIS, useCase, false, be);
                    XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read, viewIS.Element.Name);
                    CreateXisNavigationAssociation(actionName, detailIS, viewIS);
                }
                else
                {
                    List<EA.Attribute> filtered = new List<EA.Attribute>();

                    if (!string.IsNullOrEmpty(r.Filter))
                    {
                        filtered = GetFilteredAttributeList(r);
                    }

                    if (filtered.Count > 0)
                    {
                        if (filtered.Count > 3)
                        {
                            string actionName = "goTo" + r.Element.Name + "EditorIS";
                            XisButton btn = new XisButton(repository, detailIS, diagram, r.Element.Name + "EditorButton", actionName);
                            btn.SetValue(r.Element.Name);
                            XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, r, detailIS, useCase, false, be);
                            XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read, editorIS.Element.Name);
                            CreateXisNavigationAssociation(actionName, detailIS, editorIS);
                        }
                        else
                        {
                            foreach (EA.Attribute attr in filtered)
                            {
                                XISMobileHelper.ProcessXisAttribute(repository, diagram, detailIS, attr, r.Element.Name);
                            }

                            if (ContainsReadReference(useCase))
                            {
                                if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                                {
                                    XisVisibilityBoundary b = new XisVisibilityBoundary(repository, diagram, detailIS,
                                        "Save" + r.Element.Name, ContainsCreateReference(useCase), false, ContainsUpdateReference(useCase));
                                    string actionName = "save" + r.Element.Name;
                                    XisButton btn = new XisButton(repository, b, diagram, r.Element.Name + "SaveButton", actionName);
                                    btn.SetValue("Save " + r.Element.Name);
                                    XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                                }
                            }
                            else if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                            {
                                string actionName = "save" + r.Element.Name;
                                XisButton btn = new XisButton(repository, detailIS, diagram, r.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + r.Element.Name);
                                XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                    }
                    else if (r.Element.Attributes.Count > 3)
                    {
                        string actionName = "goTo" + r.Element.Name + "EditorIS";
                        XisButton btn = new XisButton(repository, detailIS, diagram, r.Element.Name + "EditorButton", actionName);
                        btn.SetValue(r.Element.Name);
                        XisInteractionSpace editorIS = CreateDetailOrRefEditorIS(package, r, detailIS, useCase, false, be);
                        XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read, editorIS.Element.Name);
                        CreateXisNavigationAssociation(actionName, detailIS, editorIS);
                    }
                    else
                    {
                        foreach (EA.Attribute attr in r.Element.Attributes)
                        {
                            XISMobileHelper.ProcessXisAttribute(repository, diagram, detailIS, attr, r.Element.Name);
                        }

                        if (ContainsReadReference(useCase))
                        {
                            if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                            {
                                XisVisibilityBoundary b = new XisVisibilityBoundary(repository, diagram, detailIS,
                                    "Save" + r.Element.Name, ContainsCreateReference(useCase), false, ContainsUpdateReference(useCase));
                                string actionName = "save" + r.Element.Name;
                                XisButton btn = new XisButton(repository, b, diagram, r.Element.Name + "SaveButton", actionName);
                                btn.SetValue("Save " + r.Element.Name);
                                XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                            }
                        }
                        else if (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))
                        {
                            string actionName = "save" + r.Element.Name;
                            XisButton btn = new XisButton(repository, detailIS, diagram, r.Element.Name + "SaveButton", actionName);
                            btn.SetValue("Save " + r.Element.Name);
                            XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.OK);
                        }
                    }
                }
            }
            #endregion

            XisMenu menu = new XisMenu(repository, diagram, detailIS, detailIS.Element.Name + "Menu", MenuType.OptionsMenu);

            if (ContainsCreateMaster(useCase) || ContainsUpdateMaster(useCase))
            {
                string actionName = "save" + master.Element.Name;
                XisWidget parent = menu;

                if (ContainsReadMaster(useCase))
                {
                    parent = new XisVisibilityBoundary(repository, diagram, menu, "Save" + master.Element.Name + "Boundary",
                        ContainsCreateMaster(useCase), false, ContainsUpdateMaster(useCase));
                }

                XisMenuItem menuItem = new XisMenuItem(repository, diagram, parent, "Save" + master.Element.Name, actionName);
                XISMobileHelper.CreateXisAction(repository, menuItem.Element, actionName, ActionType.Save, previousIS.Element.Name);
                CreateXisNavigationAssociation(actionName, detailIS, previousIS);
            }

            string cancelAction = "cancel" + master.Element.Name;
            XisMenuItem cancelItem = new XisMenuItem(repository, diagram, menu, "Cancel" + master.Element.Name, cancelAction);
            XISMobileHelper.CreateXisAction(repository, cancelItem.Element, cancelAction, ActionType.Cancel, previousIS.Element.Name);
            CreateXisNavigationAssociation(cancelAction, detailIS, previousIS);

            ComputePositions(detailIS, diagram);

            // Associate BE
            AssociateBEtoIS(diagram, detailIS, be);

            return detailIS;
        }

        public static XisInteractionSpace CreateDetailOrRefEditorIS(EA.Package package, XisEntity entity,
            XisInteractionSpace previousIS, EA.Element useCase, bool isDetail, EA.Element be)
        {
            EA.Diagram diagram = XISMobileHelper.CreateDiagram(package, entity.Element.Name + "EditorIS Diagram",
                "XIS-Mobile_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace detailIS = new XisInteractionSpace(repository, package, diagram, entity.Element.Name + "EditorIS", null);

            if (!string.IsNullOrEmpty(entity.Filter))
            {
                List<EA.Attribute> filtered = GetFilteredAttributeList(entity);
                foreach (EA.Attribute attr in filtered)
                {
                    XISMobileHelper.ProcessXisAttribute(repository, diagram, detailIS, attr, entity.Element.Name);
                }
            }
            else
            {
                foreach (EA.Attribute attr in entity.Element.Attributes)
                {
                    XISMobileHelper.ProcessXisAttribute(repository, diagram, detailIS, attr, entity.Element.Name);
                }
            }

            XisMenu menu = new XisMenu(repository, diagram, detailIS, entity.Element.Name + "Menu", MenuType.OptionsMenu);
            
            if ((isDetail && (ContainsCreateDetail(useCase) || ContainsUpdateDetail(useCase)))
                || (!isDetail && (ContainsCreateReference(useCase) || ContainsUpdateReference(useCase))))
            {
                string actionName = "save" + entity.Element.Name;
                XisMenuItem menuItem = new XisMenuItem(repository, diagram, menu, "Save" + entity.Element.Name, actionName);
                XISMobileHelper.CreateXisAction(repository, menuItem.Element, actionName, ActionType.Save, previousIS.Element.Name);
                CreateXisNavigationAssociation(actionName, detailIS, previousIS);
            }

            string cancelAction = "cancel" + entity.Element.Name;
            XisMenuItem cancelItem = new XisMenuItem(repository, diagram, menu, "Cancel" + entity.Element.Name, cancelAction);
            XISMobileHelper.CreateXisAction(repository, cancelItem.Element, cancelAction, ActionType.Cancel);
            CreateXisNavigationAssociation("cancel" + entity.Element.Name, detailIS, previousIS);

            ComputePositions(detailIS, diagram);
            // Associate BE
            AssociateBEtoIS(diagram, detailIS, be);

            return detailIS;
        }

        public static XisInteractionSpace CreateDetailOrRefManagerIS(EA.Package package, XisEntity entity,
            XisInteractionSpace previousIS, EA.Element useCase, bool isDetail, EA.Element be)
        {
            EA.Diagram diagram = XISMobileHelper.CreateDiagram(package, entity.Element.Name + "ManagerIS Diagram",
                "XIS-Mobile_Diagrams::InteractionSpaceViewModel");
            bool isFirstSubScreen = previousIS.IsMainScreen ? true : false;
            XisInteractionSpace managerIS = new XisInteractionSpace(repository, package, diagram, entity.Element.Name + "ManagerIS",
                "Manage " + entity.Element.Name, false, isFirstSubScreen);

            XisList list = new XisList(repository, diagram, managerIS, entity.Element.Name + "List");
            list.SetValue(entity.Element.Name);

            XisListItem item = new XisListItem(repository, diagram, list, list.Element.Name + "Item");
            if (entity.Element.Attributes.Count > 1)
            {
                EA.Attribute first = entity.Element.Attributes.GetAt(0);
                EA.Attribute second = entity.Element.Attributes.GetAt(1);
                XisLabel lbl1 = new XisLabel(repository, item, diagram, first.Name + "Lbl");
                lbl1.SetValue(entity.Element.Name + "." + first.Name);
                XisLabel lbl2 = new XisLabel(repository, item, diagram, second.Name + "Lbl");
                lbl2.SetValue(entity.Element.Name + "." + second.Name);
            }
            else if (entity.Element.Attributes.Count == 1)
            {
                EA.Attribute attr = entity.Element.Attributes.GetAt(0);
                item.SetValue(entity.Element.Name + "." + attr.Name);
            }

            if ((isDetail && ContainsUpdateDetail(useCase))
                    || (!isDetail && ContainsUpdateReference(useCase)))
            {
                string actionName = "edit" + entity.Element.Name;
                item.SetOnTapAction(actionName);
            }
            if ((isDetail && ContainsReadDetail(useCase))
                || (!isDetail && ContainsReadReference(useCase)))
            {
                string actionName = "view" + entity.Element.Name;
                item.SetOnTapAction(actionName);
            }

            Dictionary<ActionType, XisMenuItem> detailModes = new Dictionary<ActionType, XisMenuItem>();

            #region Create Context Menu
            if (isDetail && (ContainsReadDetail(useCase) || ContainsUpdateDetail(useCase) || ContainsDeleteDetail(useCase))
                || !isDetail && (ContainsReadReference(useCase) || ContainsUpdateReference(useCase) || ContainsDeleteReference(useCase)))
            {
                XisMenu context = new XisMenu(repository, diagram, package, list.Element.Name + "ContextMenu", MenuType.ContextMenu);

                if (isDetail && ContainsReadDetail(useCase)
                    || !isDetail && ContainsReadReference(useCase))
                {
                    string actionName = "view" + entity.Element.Name;
                    XisMenuItem contextItem = new XisMenuItem(repository, diagram, context,
                        "View" + entity.Element.Name + "Item", actionName);
                    detailModes.Add(ActionType.Read, contextItem);
                }

                if (isDetail && ContainsUpdateDetail(useCase)
                    || !isDetail && ContainsUpdateReference(useCase))
                {
                    string actionName = "edit" + entity.Element.Name;
                    XisMenuItem contextItem = new XisMenuItem(repository, diagram, context,
                        "Edit" + entity.Element.Name + "Item", actionName);
                    detailModes.Add(ActionType.Update, contextItem);
                }

                if (isDetail && ContainsUpdateDetail(useCase)
                    || !isDetail && ContainsUpdateReference(useCase))
                {
                    string actionName = "delete" + entity.Element.Name;
                    XisMenuItem contextItem = new XisMenuItem(repository, diagram, context,
                        "Delete" + entity.Element.Name + "Item", actionName);
                    XISMobileHelper.CreateXisAction(repository, contextItem.Element, actionName, ActionType.Delete);
                }
                managerIS.ContextMenu = context;
            }
            #endregion

            #region Create Options Menu
            XisMenu menu = new XisMenu(repository, diagram, managerIS, managerIS.Element.Name + "Menu", MenuType.OptionsMenu);

            if ((isDetail && (ContainsCreateDetail(useCase) || ContainsDeleteDetail(useCase)))
                || (!isDetail && (ContainsCreateReference(useCase) || ContainsDeleteReference(useCase))))
            {
                string actionName = "create" + entity.Element.Name;
                XisMenuItem menuItem = new XisMenuItem(repository, diagram, menu,
                    "Create" + entity.Element.Name + "Item", actionName);
                detailModes.Add(ActionType.Create, menuItem);
            }

            if (isDetail && (ContainsDeleteDetail(useCase))
                || !isDetail && (ContainsDeleteReference(useCase)))
            {
                string actionName = "deleteAll" + entity.Element.Name + "s";
                XisMenuItem menuItem = new XisMenuItem(repository, diagram, menu,
                    "DeleteAll" + entity.Element.Name + "Item", actionName);
                XISMobileHelper.CreateXisAction(repository, menuItem.Element, actionName, ActionType.DeleteAll);
            }

            string actionBack = "back" + entity.Element.Name + "s";
            XisMenuItem backMenuItem = new XisMenuItem(repository, diagram, menu,
                "Back" + entity.Element.Name + "Item", actionBack);
            XISMobileHelper.CreateXisAction(repository, backMenuItem.Element, actionBack, ActionType.Cancel);
            CreateXisNavigationAssociation(actionBack, managerIS, previousIS);

            managerIS.Menu = menu;
            #endregion

            if (detailModes.Count > 0 || item.GetOnTapAction() != null)
            {
                XisInteractionSpace detailIS = CreateDetailOrRefEditorIS(package, entity, managerIS, useCase, isDetail, be);
                foreach (ActionType key in detailModes.Keys)
                {
                    XisMenuItem mItem = detailModes[key];
                    XISMobileHelper.CreateXisAction(repository, mItem.Element, mItem.GetOnTapAction(),
                        key, detailIS.Element.Name);
                    CreateXisNavigationAssociation(mItem.GetOnTapAction(), managerIS, detailIS);
                }

                if (item.GetOnTapAction() != null)
                {
                    XISMobileHelper.CreateXisAction(repository, item.Element, item.GetOnTapAction(),
                        ActionType.Update, detailIS.Element.Name);
                    CreateXisNavigationAssociation(item.GetOnTapAction(), managerIS, detailIS);
                }
            }

            ComputePositions(managerIS, diagram);

            if (managerIS.ContextMenu != null)
            {
                EA.DiagramObject obj = managerIS.GetDiagramObject(diagram);
                int center = (obj.top + obj.bottom) / -2;
                managerIS.ContextMenu.SetPosition(diagram, obj.right + 50, obj.right + 330, -obj.top, -obj.top + 70);
                ComputePositions(managerIS.ContextMenu, diagram);
            }

            // Associate BE
            AssociateBEtoIS(diagram, managerIS, be);

            return managerIS;
        }

        private static void ComputePositions(XisInteractionSpace space, EA.Diagram diagram)
        {
            if (space.Widgets.Count > 0)
            {
                EA.DiagramObject spaceObj = space.GetDiagramObject(diagram);
                ComputePositions(space.Widgets.First(), diagram, spaceObj, null);
                EA.DiagramObject obj = space.Widgets.First().GetDiagramObject(diagram);

                for (int i = 1; i < space.Widgets.Count; i++)
                {
                    ComputePositions(space.Widgets[i], diagram, null, obj);
                    obj = space.Widgets[i].GetDiagramObject(diagram);
                }
                
                space.SetPosition(diagram, spaceObj.left, spaceObj.right, -spaceObj.top, -obj.bottom + 10, spaceObj.Sequence);
            }
        }

        private static void ComputePositions(XisWidget widget, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            if (widget is XisMenu)
            {
                //MessageBox.Show("MENU " + widget.Element.Name);
                ComputePositions(widget as XisMenu, diagram, parent, sibling);
            }
            else if (widget is XisList)
            {
                //MessageBox.Show("LIST " + widget.Element.Name);
                ComputePositions(widget as XisList, diagram, parent, sibling);
            }
            else if (widget is XisSimpleWidget)
            {
                //MessageBox.Show("SIMPLE " + widget.Element.Name);
                ComputePositions(widget as XisSimpleWidget, diagram, parent, sibling);
            }
            else if (widget is XisCompositeWidget)
            {
                //MessageBox.Show("COMPOSITE " + widget.Element.Name);
                ComputePositions(widget as XisCompositeWidget, diagram, parent, sibling);
            }
        }

        // Use on Context Menus
        private static void ComputePositions(XisMenu menu, EA.Diagram diagram)
        {
            if (menu.Items.Count > 0)
            {
                EA.DiagramObject menuObj = menu.GetDiagramObject(diagram);
                ComputePositions(menu.Items.First(), diagram, menuObj, null);
                EA.DiagramObject obj = menu.Items.First().GetDiagramObject(diagram);

                for (int i = 1; i < menu.Items.Count; i++)
                {
                    ComputePositions(menu.Items[i], diagram, null, obj);
                    obj = menu.Items[i].GetDiagramObject(diagram);
                }

                menu.SetPosition(diagram, menuObj.left, menuObj.right, -menuObj.top, -obj.bottom + 10, menuObj.Sequence);
            }
        }

        private static void ComputePositions(XisList list, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            EA.DiagramObject obj = null;

            if (parent != null)
            {
                list.Element.Methods.Refresh();
                obj = list.SetPosition(diagram,
                    parent.left + 10, parent.right - 10, -parent.top + 40, -parent.top + 90 + 30 * list.Element.Methods.Count,
                    parent.Sequence - 1);
            }
            else if (sibling != null)
            {
                list.Element.Methods.Refresh();
                obj = list.SetPosition(diagram,
                    sibling.left, sibling.right, -sibling.bottom + 10, -sibling.bottom + 60 + 30 * list.Element.Methods.Count,
                    sibling.Sequence);
            }

            if (obj != null)
            {
                if (list.Items.Count > 0)
                {
                    ComputePositions(list.Items.First(), diagram, obj, null);
                    EA.DiagramObject aux = list.Items.First().GetDiagramObject(diagram);

                    for (int i = 1; i < list.Items.Count; i++)
                    {
                        ComputePositions(list.Items[i], diagram, null, aux);
                        aux = list.Items[i].GetDiagramObject(diagram);
                    }

                    list.SetPosition(diagram, obj.left, obj.right, -obj.top, -aux.bottom + 10, obj.Sequence);
                }
            }
        }

        private static void ComputePositions(XisListItem item, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            EA.DiagramObject obj = null;

            if (parent != null)
	        {
                item.Element.Methods.Refresh();
                obj = item.SetPosition(diagram,
                    parent.left + 10, parent.right - 10, -parent.top + 40, -parent.top + 90 + 30 * item.Element.Methods.Count,
                    parent.Sequence - 1);
	        }
            else if (sibling != null)
	        {
                item.Element.Methods.Refresh();
                obj = item.SetPosition(diagram,
                    sibling.left, sibling.right, -sibling.bottom + 10, -sibling.bottom + 60 + 30 * item.Element.Methods.Count,
                    sibling.Sequence);
	        }

            if (obj != null)
	        {
                if (item.Widgets.Count > 0)
                {
                    ComputePositions(item.Widgets.First() as XisSimpleWidget, diagram, obj, null);
                    EA.DiagramObject aux = item.Widgets.First().GetDiagramObject(diagram);

                    for (int i = 1; i < item.Widgets.Count; i++)
                    {
                        ComputePositions(item.Widgets[i] as XisSimpleWidget, diagram, null, aux);
                        aux = item.Widgets[i].GetDiagramObject(diagram);
                    }
                                
                    item.SetPosition(diagram, obj.left, obj.right, -obj.top, -aux.bottom + 10, obj.Sequence);
                }
	        }
        }

        private static void ComputePositions(XisMenu menu, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            EA.DiagramObject obj = null;

            if (parent != null)
            {
                menu.Element.Methods.Refresh();
                obj = menu.SetPosition(diagram,
                    parent.left + 10, parent.right - 10, -parent.top + 40, -parent.top + 90 + 30 * menu.Element.Methods.Count,
                    parent.Sequence - 1);
            }
            else if (sibling != null)
            {
                menu.Element.Methods.Refresh();
                obj = menu.SetPosition(diagram,
                    sibling.left, sibling.right, -sibling.bottom + 10, -sibling.bottom + 60 + 30 * menu.Element.Methods.Count,
                    sibling.Sequence);
            }

            if (obj != null)
            {
                if (menu.Widgets.Count > 0)
                {
                    ComputePositions(menu.Widgets.First(), diagram, obj, null);
                    EA.DiagramObject aux = menu.Widgets.First().GetDiagramObject(diagram);

                    for (int i = 1; i < menu.Widgets.Count; i++)
                    {
                        ComputePositions(menu.Widgets[i], diagram, null, aux);
                        aux = menu.Widgets[i].GetDiagramObject(diagram);
                    }

                    menu.SetPosition(diagram, obj.left, obj.right, -obj.top, -aux.bottom + 10, obj.Sequence);
                }
            }
        }

        private static void ComputePositions(XisCompositeWidget comp, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            EA.DiagramObject obj = null;

            if (parent != null)
            {
                comp.Element.Methods.Refresh();
                obj = comp.SetPosition(diagram,
                    parent.left + 10, parent.right - 10, -parent.top + 40, -parent.top + 90 + 30 * comp.Element.Methods.Count,
                    parent.Sequence - 1);
            }
            else if (sibling != null)
            {
                comp.Element.Methods.Refresh();
                obj = comp.SetPosition(diagram,
                    sibling.left, sibling.right, -sibling.bottom + 10, -sibling.bottom + 60 + 30 * comp.Element.Methods.Count,
                    sibling.Sequence);
            }

            if (obj != null)
            {
                if (comp.Widgets.Count > 0)
                {
                    ComputePositions(comp.Widgets.First(), diagram, obj, null);
                    EA.DiagramObject aux = comp.Widgets.First().GetDiagramObject(diagram);

                    for (int i = 1; i < comp.Widgets.Count; i++)
                    {
                        ComputePositions(comp.Widgets[i], diagram, null, aux);
                        aux = comp.Widgets[i].GetDiagramObject(diagram);
                    }

                    comp.SetPosition(diagram, obj.left, obj.right, -obj.top, -aux.bottom + 10, obj.Sequence);
                }
            }
        }

        private static void ComputePositions(XisSimpleWidget widget, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            if (parent != null)
            {
                widget.Element.Methods.Refresh();
                EA.Element pElem = repository.GetElementByID(parent.ElementID);
                int pMethodDist = pElem.Methods.Count > 0 ? 15 + pElem.Methods.Count * 20 : 0;
                EA.DiagramObject obj = widget.SetPosition(diagram,
                    parent.left + 10, parent.right - 10, -parent.top + 40 + pMethodDist,
                    -parent.top + 90 + 30 * widget.Element.Methods.Count + pMethodDist, parent.Sequence - 1);
            }
            else if (sibling != null)
            {
                widget.Element.Methods.Refresh();
                EA.DiagramObject obj = widget.SetPosition(diagram,
                    sibling.left, sibling.right, -sibling.bottom + 10, -sibling.bottom + 60 + 30 * widget.Element.Methods.Count,
                    sibling.Sequence);
            }
        }

        private static void CreateXisNavigationAssociation(string actionName, XisInteractionSpace source, XisInteractionSpace target)
        {
            int across = 260;
            int down = 180;
            nsDiagram.DiagramObjects.Refresh();

            #region Create Diagram Objects
            if (nsDiagram.DiagramObjects.Count > 0)
            {
                short index = Convert.ToInt16(nsDiagram.DiagramObjects.Count - 1);
                EA.DiagramObject last = null;

                if (source.GetDiagramObject(nsDiagram) == null)
                {
                    if (source.IsMainScreen)
                    {
                        source.SetPosition(nsDiagram, 355, 445, 10, 80);
                    }
                    else
                    {
                        last = nsDiagram.DiagramObjects.GetAt(index);
                        EA.DiagramObject obj = null;

                        if ((last.right + across) > 800)
                        {
                            obj = source.SetPosition(nsDiagram,
                                last.left - across * 3, last.right - across * 3, -last.top + down, -last.bottom + down);
                        }
                        else
                        {
                            obj = source.SetPosition(nsDiagram,
                                last.left + across, last.right + across, -last.top, -last.bottom);
                        }

                        if (target.GetDiagramObject(nsDiagram) == null)
                        {
                            target.SetPosition(nsDiagram,
                                obj.left + across, obj.right + across, -obj.top, -obj.bottom);
                        } 
                    }
                }
                else if (target.GetDiagramObject(nsDiagram) == null)
                {
                    if (target.IsMainScreen)
                    {
                        target.SetPosition(nsDiagram, 355, 445, 10, 80);
                    }
                    else
                    {
                        last = nsDiagram.DiagramObjects.GetAt(index);

                        if ((last.right + across) > 800)
                        {
                            target.SetPosition(nsDiagram,
                                last.left - across * 3, last.right - across * 3, -last.top + down, -last.bottom + down);
                        }
                        else
                        {
                            target.SetPosition(nsDiagram,
                                last.left + across, last.right + across, -last.top, -last.bottom);
                        } 
                    }
                }
            }
            else
            {
                if (source.IsMainScreen)
                {
                    EA.DiagramObject obj = source.SetPosition(nsDiagram, 355, 445, 10, 80);
                    target.SetPosition(nsDiagram, obj.left - across, obj.right - across, -obj.top + down, -obj.bottom + down);
                }
                else if (target.IsMainScreen)
                {
                    EA.DiagramObject obj = target.SetPosition(nsDiagram, 355, 445, 10, 80);
                    source.SetPosition(nsDiagram, obj.left - across, obj.right - across, -obj.top + down, -obj.bottom + down);
                }
                else if (source.IsFirstSubScreen)
                {
                    //MessageBox.Show(source.Element.Name + "->" + target.Element.Name);
                    EA.DiagramObject obj = source.SetPosition(nsDiagram, 95, 185, 190, 260);
                    target.SetPosition(nsDiagram, obj.left + across, obj.right + across, -obj.top, -obj.bottom);
                }
                else if (target.IsFirstSubScreen)
                {
                    EA.DiagramObject obj = target.SetPosition(nsDiagram, 95, 185, 190, 260);
                    source.SetPosition(nsDiagram, obj.left + across, obj.right + across, -obj.top, -obj.bottom);
                }
                else
                {
                    EA.DiagramObject obj = source.SetPosition(nsDiagram, 355, 445, 190, 260);
                    target.SetPosition(nsDiagram, obj.left + across, obj.right + across, -obj.top, -obj.bottom);
                }
            } 
            #endregion

            EA.Connector c = source.Element.Connectors.AddNew(actionName, "Association");
            c.ClientID = source.Element.ElementID;
            c.SupplierID = target.Element.ElementID;
            c.Direction = "Source -> Destination";
            c.Stereotype = "XisNavigationAssociation";
            c.Update();
            source.Element.Update();
            source.Element.Connectors.Refresh();
        }

        private static void AssociateFirstSubSpaces(EA.Diagram diagram, List<EA.Element> useCases,
            XisInteractionSpace space, int beID, string master)
        {
            XisButton btn = null;
            EA.Element auxBE = null;
            string entityName = null;
            string ucType = null;

            foreach (EA.Element uc in useCases)
            {
                foreach (EA.Connector c in uc.Connectors)
                {
                    if (c.Stereotype == "XisOperatesOnAssociation")
                    {
                        if (c.SupplierID != beID)
                        {
                            auxBE = repository.GetElementByID(c.SupplierID);
                            foreach (EA.Connector conn in auxBE.Connectors)
                            {
                                if (conn.Stereotype == "XisMasterAssociation")
                                {
                                    entityName = repository.GetElementByID(conn.SupplierID).Name;
                                }
                            }
                        }
                        else
                        {
                            entityName = master;
                        }
                        break;
                    }
                }
                ucType = GetTaggedValue(uc.TaggedValues, "type").Value;
                string spaceName = null;

                if (ucType == "Manager")
                {
                    spaceName = entityName + "ListIS";
                }
                else if (ucType == "Detail")
                {
                    spaceName = entityName + "DetailIS";
                }

                string actionName = "goTo" + spaceName;
                btn = new XisButton(repository, space, diagram, actionName + "Button", actionName);
                btn.SetValue(entityName);
                XISMobileHelper.CreateXisAction(repository, btn.Element, actionName, ActionType.Read,
                    spaceName);
            }
        }

        private static void AssociateBEtoIS(EA.Diagram diagram, XisInteractionSpace source, EA.Element be)
        {
            EA.DiagramObject sourceObj = source.GetDiagramObject(diagram);
            int center = (sourceObj.top + sourceObj.bottom) / -2;

            XISMobileHelper.SetPosition(repository, diagram, be, 10, 100, center - 35, center + 35);
            EA.Connector c = source.Element.Connectors.AddNew("", "Association");
            c.ClientID = source.Element.ElementID;
            c.SupplierID = be.ElementID;
            c.Direction = "Source -> Destination";
            c.Stereotype = "XisDomainAssociation";
            c.Update();
            source.Element.Update();
            source.Element.Connectors.Refresh();
        }

        private static List<EA.Attribute> GetFilteredAttributeList(XisEntity entity)
        {
            List<EA.Attribute> lst = new List<EA.Attribute>();

            if (entity.Filter.Contains(';'))
            {
                string[] attrs = entity.Filter.Split(';');
                foreach (string s in attrs)
                {
                    foreach (EA.Attribute attr in entity.Element.Attributes)
                    {
                        if (attr.Name.ToLower() == s.ToLower())
                        {
                            lst.Add(attr);
                        }
                    }
                }
            }
            else
            {
                foreach (EA.Attribute attr in entity.Element.Attributes)
                {
                    if (attr.Name.ToLower() == entity.Filter.ToLower())
                    {
                        lst.Add(attr);
                        break;
                    }
                }
            }
            return lst;
        }

        public static EA.TaggedValue GetTaggedValue(EA.Collection taggedValues, string name)
        {
            foreach (EA.TaggedValue tv in taggedValues)
            {
                if (tv.Name == name)
                {
                    return tv;
                }
            }
            return null;
        }

        private static EA.ConnectorTag GetConnectorTag(EA.Collection taggedValues, string name)
        {
            foreach (EA.ConnectorTag tv in taggedValues)
            {
                if (tv.Name == name)
                {
                    return tv;
                }
            }
            return null;
        }

        private static EA.AttributeTag GetAttributeTag(EA.Collection taggedValues, string name)
        {
            foreach (EA.AttributeTag tv in taggedValues)
            {
                if (tv.Name == name)
                {
                    return tv;
                }
            }
            return null;
        }

        private static bool ContainsCreateMaster(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "CreateMaster").Value);
        }

        private static bool ContainsReadMaster(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "ReadMaster").Value);
        }

        private static bool ContainsUpdateMaster(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "UpdateMaster").Value);
        }

        private static bool ContainsDeleteMaster(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "DeleteMaster").Value);
        }

        private static bool ContainsCreateDetail(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "CreateDetail").Value);
        }

        private static bool ContainsReadDetail(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "ReadDetail").Value);
        }

        private static bool ContainsUpdateDetail(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "UpdateDetail").Value);
        }

        private static bool ContainsDeleteDetail(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "DeleteDetail").Value);
        }

        private static bool ContainsCreateReference(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "CreateReference").Value);
        }

        private static bool ContainsReadReference(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "ReadReference").Value);
        }

        private static bool ContainsUpdateReference(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "UpdateReference").Value);
        }

        private static bool ContainsDeleteReference(EA.Element useCase)
        {
            return Boolean.Parse(GetTaggedValue(useCase.TaggedValues, "DeleteReference").Value);
        }

        //private static bool ContainsSearch(string[] operations)
        //{
        //    if (operations != null)
        //    {
        //        foreach (string op in operations)
        //        {
        //            if (op.ToLower() == "s" || op.ToLower() == "search")
        //            {
        //                return true;
        //            }
        //        } 
        //    }
        //    return false;
        //}

        public enum Mode
        {
            Create,
            View,
            Edit
        }
    }
}
