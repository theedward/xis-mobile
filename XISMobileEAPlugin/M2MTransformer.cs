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
        private static EA.Diagram nsDiagram;

        public static void ProcessUseCase(EA.Repository repository, EA.Package rootPackage, List<EA.Element> useCases)
        {
            nsDiagram = XISMobileHelper.CreateDiagram(rootPackage, "Navigation Space",
                "XIS-Mobile_Diagrams::NavigationSpaceViewModel");
            bool isStartingUC = true;

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
                            EA.TaggedValue operationsTv = GetTaggedValue(useCase.TaggedValues, "operations");
                            string[] operations = null;

                            if (operationsTv != null && !string.IsNullOrEmpty(operationsTv.Value))
                            {
                                if (!operationsTv.Value.Contains(';'))
                                {
                                    operations = new string[] { operationsTv.Value };
                                    //MessageBox.Show(operationsTv.Value);
                                }
                                else
                                {
                                    operations = operationsTv.Value.Split(';');
                                    //foreach (string s in operations)
                                    //{
                                    //    MessageBox.Show(s);
                                    //}
                                }
                            }

                            EA.TaggedValue ucType = GetTaggedValue(useCase.TaggedValues, "type");

                            if (ucType != null)
                            {
                                if (ucType.Value == "Manager")
                                {
                                    if (isStartingUC && useCases.Count > 1)
                                    {
                                        ProcessManagerUseCase(repository, rootPackage, master, operations, be, isStartingUC,
                                            useCases.GetRange(1, useCases.Count-1));
                                    }
                                    else
                                    {
                                        ProcessManagerUseCase(repository, rootPackage, master, operations, be, isStartingUC);
                                    }
                                }
                                else if (ucType.Value == "Detail")
                                {
                                    if (isStartingUC && useCases.Count > 1)
                                    {
                                        ProcessDetailUseCase(repository, rootPackage, master, operations, be, isStartingUC,
                                            useCases.GetRange(1, useCases.Count - 1));
                                    }
                                    else
                                    {
                                        ProcessDetailUseCase(repository, rootPackage, master, operations, be, isStartingUC);
                                    }
                                }
                            }
                        }
                    }
                }
                isStartingUC = false;
            }
        }

        public static void ProcessManagerUseCase(EA.Repository repository, EA.Package package, XisEntity master,
            string[] operations, EA.Element be, bool isStartingUC, List<EA.Element> useCases = null)
        {
            // Create IS Diagram
            EA.Diagram listDiagram = XISMobileHelper.CreateDiagram(package, master.Element.Name + "ListIS Diagram",
                "XIS-Mobile_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace listIS = new XisInteractionSpace(repository, package, listDiagram,
                master.Element.Name + "ListIS", isStartingUC, !isStartingUC);

            if (isStartingUC)
            {
                homeIS = listIS;
            }

            // List Creation
            XisCompositeWidget list = new XisCompositeWidget(repository, listDiagram, listIS, master.Element.Name + "List",
                CompositeWidgetType.List);

            if (ContainsSearch(operations))
            {
                string searchBy = "";
                string tv = null;
                
                foreach (EA.Attribute attr in master.Element.Attributes)
                {
                    tv = GetAttributeTag(attr.TaggedValues, "isKey").Value;
                    if (!string.IsNullOrEmpty(tv) && tv.ToLower() == "true")
                    {
                        searchBy += master.Element.Name + "." + attr.Name + ";";
                    }
                }

                if (string.IsNullOrEmpty(searchBy))
                {
                    list.SetSearchBy(master.Element.Name + ".Id");
                }
                else
                {
                    list.SetSearchBy(searchBy.Substring(0, searchBy.Length - 1));
                }
            }

            XisCompositeWidget item = new XisCompositeWidget(repository, listDiagram, list, list.Element.Name + "Item",
                CompositeWidgetType.Item);

            #region Create Context Menu
            if (ContainsRead(operations) || ContainsUpdate(operations) || ContainsDelete(operations))
            {
                XisCompositeWidget context = new XisCompositeWidget(repository, listDiagram, package, list.Element.Name + "ContextMenu",
                    CompositeWidgetType.ContextMenu);

                if (ContainsRead(operations))
                {
                    string actionName = "view" + list.Element.Name;
                    XisCompositeWidget contextItem = new XisCompositeWidget(repository, listDiagram, context, "View" + list.Element.Name,
                        CompositeWidgetType.Item, actionName);
                    XisInteractionSpace detailIS = CreateMasterDetailIS(repository, package, master, listIS, Mode.View, be);
                    XISMobileHelper.CreateXisAction(contextItem.Element, actionName, ActionType.Read, detailIS.Element.Name);
                    CreateXisNavigationAssociation(repository, actionName, listIS, detailIS);
                    
                    if (!ContainsUpdate(operations))
                    {
                        item.SetOnTapAction(actionName);   
                    }
                }
                
                if (ContainsUpdate(operations))
                {
                    string actionName = "edit" + list.Element.Name;
                    XisCompositeWidget contextItem = new XisCompositeWidget(repository, listDiagram, context, "Edit" + list.Element.Name,
                        CompositeWidgetType.Item, actionName);
                    XisInteractionSpace detailIS = CreateMasterDetailIS(repository, package, master, listIS, Mode.Edit, be);
                    XISMobileHelper.CreateXisAction(contextItem.Element, actionName, ActionType.Update, detailIS.Element.Name);
                    CreateXisNavigationAssociation(repository, actionName, listIS, detailIS);
                    item.SetOnTapAction(actionName);
                }

                if (ContainsDelete(operations))
                {
                    string actionName = "delete" + list.Element.Name;
                    XisCompositeWidget contextItem = new XisCompositeWidget(repository, listDiagram, context, "Delete" + list.Element.Name,
                        CompositeWidgetType.Item, actionName);
                    XISMobileHelper.CreateXisAction(contextItem.Element, actionName, ActionType.Delete);
                }
                listIS.ContextMenu = context;
            }
            #endregion

            // Navigation between main UC and the others
            if (isStartingUC)
            {
                if (useCases != null)
                {
                    AssociateFirstSubSpaces(repository, listDiagram, useCases, listIS, be.ElementID, master.Element.Name);
                }
            }

            #region Create Options Menu
            if (ContainsCreate(operations) || ContainsDelete(operations))
            {
                XisCompositeWidget menu = new XisCompositeWidget(repository, listDiagram, listIS, list.Element.Name + "Menu",
                    CompositeWidgetType.Menu);

                if (ContainsCreate(operations))
                {
                    string actionName = "create" + list.Element.Name;
                    XisCompositeWidget menuItem = new XisCompositeWidget(repository, listDiagram, menu,
                        "Create" + list.Element.Name, CompositeWidgetType.Item, actionName);
                    XisInteractionSpace detailIS = CreateMasterDetailIS(repository, package, master, listIS, Mode.Create, be);
                    XISMobileHelper.CreateXisAction(menuItem.Element, actionName, ActionType.Create, detailIS.Element.Name);
                    CreateXisNavigationAssociation(repository, actionName, listIS, detailIS);
                }

                if (ContainsDelete(operations))
                {
                    string actionName = "deleteAll" + list.Element.Name;
                    XisCompositeWidget menuItem = new XisCompositeWidget(repository, listDiagram, menu,
                        "DeleteAll" + list.Element.Name, CompositeWidgetType.Item, actionName);
                    XISMobileHelper.CreateXisAction(menuItem.Element, actionName, ActionType.DeleteAll);
                }
                listIS.Menu = menu;
            } 
            #endregion

            ComputePositions(listIS, listDiagram);

            if (listIS.ContextMenu != null)
            {
                EA.DiagramObject obj = listIS.GetDiagramObject(listDiagram);
                int center = (obj.top + obj.bottom) / -2;
                listIS.ContextMenu.SetPosition(listDiagram, obj.right + 50, obj.right + 330, -obj.top, -obj.top + 70);
                ComputePositions(listIS.ContextMenu, listDiagram);
            }

            // Associate BE
            AssociateBEtoIS(repository, listDiagram, listIS, be);

            if (!isStartingUC)
            {
                // TODO: Link subspaces
                CreateXisNavigationAssociation(repository, "goTo" + listIS.Element.Name, homeIS, listIS);
            }
        }

        public static void ProcessDetailUseCase(EA.Repository repository, EA.Package package, XisEntity master,
            string[] operations, EA.Element be, bool isStartingUC, List<EA.Element> useCases = null)
        {
            EA.Diagram detailDiagram = XISMobileHelper.CreateDiagram(package, master.Element.Name + "DetailIS Diagram",
                "XIS-Mobile_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace detailIS = new XisInteractionSpace(repository, package, detailDiagram,
                master.Element.Name + "DetailIS", isStartingUC, !isStartingUC);

            if (isStartingUC)
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
                if (d.Cardinality != "*" && d.Element.Attributes.Count < 4)
                {
                    if (!string.IsNullOrEmpty(d.Filter))
                    {
                        List<EA.Attribute> filtered = GetFilteredAttributeList(d);
                        foreach (EA.Attribute attr in filtered)
                        {
                            XISMobileHelper.ProcessXisAttribute(repository, detailDiagram, detailIS, attr, d.Element.Name);
                        }
                    }
                    else
                    {
                        foreach (EA.Attribute attr in d.Element.Attributes)
                        {
                            XISMobileHelper.ProcessXisAttribute(repository, detailDiagram, detailIS, attr, d.Element.Name);
                        }
                    }
                }
                else
                {
                    // TODO: Create Button for detail
                    string actionName = "view" + d.Element.Name;
                    XisButton btn = new XisButton(repository, detailIS, detailDiagram, d.Element.Name + "DetailButton");
                    XisInteractionSpace viewIS = CreateDetailOrRefIS(repository, package, d, detailIS, Mode.View, be);
                    XISMobileHelper.CreateXisAction(btn.Element, actionName, ActionType.Read, detailIS.Element.Name);
                    CreateXisNavigationAssociation(repository, actionName, detailIS, viewIS);
                }
            }
            #endregion

            #region Process References info
            foreach (XisEntity r in master.References)
            {
                if (r.Cardinality != "*" && r.Element.Attributes.Count < 4)
                {
                    if (!string.IsNullOrEmpty(r.Filter))
                    {
                        List<EA.Attribute> filtered = GetFilteredAttributeList(r);
                        foreach (EA.Attribute attr in filtered)
                        {
                            XISMobileHelper.ProcessXisAttribute(repository, detailDiagram, detailIS, attr, r.Element.Name);
                        }
                    }
                    else
                    {
                        foreach (EA.Attribute attr in r.Element.Attributes)
                        {
                            XISMobileHelper.ProcessXisAttribute(repository, detailDiagram, detailIS, attr, r.Element.Name);
                        }
                    }
                }
                else
                {
                    // TODO: Create Button for reference
                    string actionName = "view" + r.Element.Name;
                    XisButton btn = new XisButton(repository, detailIS, detailDiagram, r.Element.Name + "DetailButton");
                    XisInteractionSpace viewIS = CreateDetailOrRefIS(repository, package, r, detailIS, Mode.View, be);
                    XISMobileHelper.CreateXisAction(btn.Element, actionName, ActionType.Read, detailIS.Element.Name);
                    CreateXisNavigationAssociation(repository, actionName, detailIS, viewIS);
                }
            }
            #endregion

            // Navigation between main UC and the others
            if (isStartingUC)
            {
                if (useCases != null)
                {
                    AssociateFirstSubSpaces(repository, detailDiagram, useCases, detailIS, be.ElementID, master.Element.Name);
                }
            }

            if (ContainsUpdate(operations))
            {
                XisCompositeWidget menu = new XisCompositeWidget(repository, detailDiagram, detailIS, detailIS.Element.Name + "Menu",
                    CompositeWidgetType.Menu);

                string actionName = "save" + master.Element.Name;
                XisCompositeWidget menuItem = new XisCompositeWidget(repository, detailDiagram, menu, "Save" + master.Element.Name,
                    CompositeWidgetType.Item);
                XISMobileHelper.CreateXisAction(menuItem.Element, actionName, ActionType.Save); 
            }

            ComputePositions(detailIS, detailDiagram);
            // Associate BE
            AssociateBEtoIS(repository, detailDiagram, detailIS, be);

            if (detailIS.GetDiagramObject(nsDiagram) == null && isStartingUC)
            {
                detailIS.SetPosition(nsDiagram, 355, 445, 10, 80);
            }

            if (!isStartingUC)
            {
                // TODO: Link subspaces
                CreateXisNavigationAssociation(repository, "goTo" + detailIS.Element.Name, homeIS, detailIS);
            }
        }

        private static XisInteractionSpace CreateMasterDetailIS(EA.Repository repository, EA.Package package, XisEntity master,
            XisInteractionSpace previousIS, Mode mode, EA.Element be)
        {
            EA.Diagram diagram = XISMobileHelper.CreateDiagram(package, mode + master.Element.Name + "IS Diagram",
                "XIS-Mobile_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace detailIS = new XisInteractionSpace(repository, package, diagram, mode + master.Element.Name + "IS");

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
                if (d.Cardinality != "*" && d.Element.Attributes.Count < 4)
                {
                    if (!string.IsNullOrEmpty(d.Filter))
                    {
                        List<EA.Attribute> filtered = GetFilteredAttributeList(d);
                        foreach (EA.Attribute attr in filtered)
                        {
                            XISMobileHelper.ProcessXisAttribute(repository, diagram, detailIS, attr, d.Element.Name);
                        }
                    }
                    else
                    {
                        foreach (EA.Attribute attr in d.Element.Attributes)
                        {
                            XISMobileHelper.ProcessXisAttribute(repository, diagram, detailIS, attr, d.Element.Name);
                        }
                    }
                }
                else
                {
                    // TODO: Create Button for detail
                    string actionName = "view" + d.Element.Name;
                    XisButton btn = new XisButton(repository, detailIS, diagram, d.Element.Name + "DetailButton");
                    XisInteractionSpace viewIS = CreateDetailOrRefIS(repository, package, d, detailIS, Mode.View, be);
                    XISMobileHelper.CreateXisAction(btn.Element, actionName, ActionType.Read, detailIS.Element.Name);
                    CreateXisNavigationAssociation(repository, actionName, detailIS, viewIS);
                }
            }
            #endregion

            #region Write References info
            foreach (XisEntity r in master.References)
            {
                if (r.Cardinality != "*" && r.Element.Attributes.Count < 4)
                {
                    if (!string.IsNullOrEmpty(r.Filter))
                    {
                        List<EA.Attribute> filtered = GetFilteredAttributeList(r);
                        foreach (EA.Attribute attr in filtered)
                        {
                            XISMobileHelper.ProcessXisAttribute(repository, diagram, detailIS, attr, r.Element.Name);
                        }
                    }
                    else
                    {
                        foreach (EA.Attribute attr in r.Element.Attributes)
                        {
                            XISMobileHelper.ProcessXisAttribute(repository, diagram, detailIS, attr, r.Element.Name);
                        }
                    }
                }
                else
                {
                    // TODO: Create Button for reference
                    string actionName = "view" + r.Element.Name;
                    XisButton btn = new XisButton(repository, detailIS, diagram, r.Element.Name + "DetailButton");
                    XisInteractionSpace viewIS = CreateDetailOrRefIS(repository, package, r, detailIS, Mode.View, be);
                    XISMobileHelper.CreateXisAction(btn.Element, actionName, ActionType.Read, detailIS.Element.Name);
                    CreateXisNavigationAssociation(repository, actionName, detailIS, viewIS);
                }
            }
            #endregion

            XisCompositeWidget menu = new XisCompositeWidget(repository, diagram, detailIS, detailIS.Element.Name + "Menu",
                CompositeWidgetType.Menu);

            if (mode == Mode.Create || mode == Mode.Edit)
            {
                string actionName = "save" + master.Element.Name;
                XisCompositeWidget menuItem = new XisCompositeWidget(repository, diagram, menu, "Save" + master.Element.Name,
                    CompositeWidgetType.Item, actionName);
                XISMobileHelper.CreateXisAction(menuItem.Element, actionName, ActionType.Save, previousIS.Element.Name);
                CreateXisNavigationAssociation(repository, actionName, detailIS, previousIS);
            }

            string cancelAction = "cancel" + master.Element.Name;
            XisCompositeWidget cancelItem = new XisCompositeWidget(repository, diagram, menu, "Cancel" + master.Element.Name,
                CompositeWidgetType.Item, cancelAction);
            XISMobileHelper.CreateXisAction(cancelItem.Element, cancelAction, ActionType.Cancel, previousIS.Element.Name);
            CreateXisNavigationAssociation(repository, cancelAction, detailIS, previousIS);

            ComputePositions(detailIS, diagram);
            // Associate BE
            AssociateBEtoIS(repository, diagram, detailIS, be);

            return detailIS;
        }

        public static XisInteractionSpace CreateDetailOrRefIS(EA.Repository repository, EA.Package package, XisEntity entity,
            XisInteractionSpace previousIS, Mode mode, EA.Element be)
        {
            EA.Diagram diagram = XISMobileHelper.CreateDiagram(package, entity.Element.Name + mode + "IS Diagram",
                "XIS-Mobile_Diagrams::InteractionSpaceViewModel");
            XisInteractionSpace detailIS = new XisInteractionSpace(repository, package, diagram, mode + entity.Element.Name + "IS");

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

            XisCompositeWidget menu = new XisCompositeWidget(repository, diagram, detailIS, entity.Element.Name + mode + "Menu",
                CompositeWidgetType.Menu);

            if (mode == Mode.Edit)
            {
                string actionName = "save" + entity.Element.Name;
                XisCompositeWidget menuItem = new XisCompositeWidget(repository, diagram, menu, "Save" + entity.Element.Name,
                    CompositeWidgetType.Item, actionName);
                XISMobileHelper.CreateXisAction(menuItem.Element, actionName, ActionType.Save, previousIS.Element.Name);
                CreateXisNavigationAssociation(repository, actionName, detailIS, previousIS);
            }

            string cancelAction = "cancel" + entity.Element.Name;
            XisCompositeWidget cancelItem = new XisCompositeWidget(repository, diagram, menu, "Cancel" + entity.Element.Name,
                CompositeWidgetType.Item, cancelAction);
            XISMobileHelper.CreateXisAction(cancelItem.Element, cancelAction, ActionType.Cancel, previousIS.Element.Name);
            CreateXisNavigationAssociation(repository, "cancel" + entity.Element.Name, detailIS, previousIS);

            ComputePositions(detailIS, diagram);
            // Associate BE
            AssociateBEtoIS(repository, diagram, detailIS, be);

            return detailIS;
        }

        private static void ComputePositions(XisInteractionSpace space, EA.Diagram diagram)
        {
            if (space.Widgets.Count > 0)
            {
                EA.DiagramObject obj = space.GetDiagramObject(diagram);
                ComputePositions(space.Widgets.First(), diagram, obj, null);
                obj = space.Widgets.First().GetDiagramObject(diagram);

                for (int i = 1; i < space.Widgets.Count; i++)
                {
                    ComputePositions(space.Widgets[i], diagram, null, obj);
                    obj = space.Widgets[i].GetDiagramObject(diagram);
                }
                EA.DiagramObject spaceObj = space.GetDiagramObject(diagram);
                space.SetPosition(diagram, spaceObj.left, spaceObj.right, -spaceObj.top, -obj.bottom + 10, spaceObj.Sequence + 1);
            }
        }

        private static void ComputePositions(XisCompositeWidget comp, EA.Diagram diagram)
        {
            if (comp.Widgets.Count > 0)
            {
                EA.DiagramObject obj = comp.GetDiagramObject(diagram);
                ComputePositions(comp.Widgets.First(), diagram, obj, null);
                obj = comp.Widgets.First().GetDiagramObject(diagram);

                for (int i = 1; i < comp.Widgets.Count; i++)
                {
                    ComputePositions(comp.Widgets[i], diagram, null, obj);
                    obj = comp.Widgets[i].GetDiagramObject(diagram);
                }
                EA.DiagramObject spaceObj = comp.GetDiagramObject(diagram);
                comp.SetPosition(diagram, spaceObj.left, spaceObj.right, -spaceObj.top, -obj.bottom + 10, spaceObj.Sequence + 1);
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
                    sibling.left, sibling.right, -sibling.bottom + 10, -sibling.top + 60 + 30 * comp.Element.Methods.Count,
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
                    
                    aux = comp.Widgets.Last().GetDiagramObject(diagram);
                    comp.SetPosition(diagram, obj.left, obj.right, -obj.top, -aux.bottom + 10, obj.Sequence);
                }
	        }
        }

        private static void ComputePositions(XisWidget widget, EA.Diagram diagram, EA.DiagramObject parent, EA.DiagramObject sibling)
        {
            if (widget is XisCompositeWidget && ((XisCompositeWidget)widget).Widgets.Count > 0)
            {
                XisCompositeWidget comp = widget as XisCompositeWidget;

                if (parent != null)
                {
                    ComputePositions(comp, diagram, parent, null);
                }
                else if (sibling != null)
                {
                    ComputePositions(comp, diagram, null, sibling);
                }
            }
            else
            {
                if (parent != null)
                {
                    widget.Element.Methods.Refresh();
                    widget.SetPosition(diagram,
                        parent.left + 10, parent.right - 10, -parent.top + 40, -parent.top + 90 + 30 * widget.Element.Methods.Count,
                        parent.Sequence - 1);
                }
                else if (sibling != null)
                {
                    widget.Element.Methods.Refresh();
                    widget.SetPosition(diagram,
                        sibling.left, sibling.right, -sibling.bottom + 10, -sibling.bottom + 60 + 30 * widget.Element.Methods.Count,
                        sibling.Sequence);
                }
            }
        }

        private static void CreateXisNavigationAssociation(EA.Repository repository, string actionName,
            XisInteractionSpace source, XisInteractionSpace target)
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
                else if (target.GetDiagramObject(nsDiagram) == null)
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
            else
            {
                if (source.IsMainScreen || source.IsFirstSubScreen)
                {
                    EA.DiagramObject obj = source.SetPosition(nsDiagram, 355, 445, 10, 80);
                    target.SetPosition(nsDiagram, obj.left - across, obj.right - across, -obj.top + down, -obj.bottom + down);
                }
                else if (target.IsMainScreen || target.IsFirstSubScreen)
                {
                    EA.DiagramObject obj = target.SetPosition(nsDiagram, 355, 445, 10, 80);
                    source.SetPosition(nsDiagram, obj.left - across, obj.right - across, -obj.top + down, -obj.bottom + down);
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

        private static void AssociateFirstSubSpaces(EA.Repository repository, EA.Diagram diagram, List<EA.Element> useCases,
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
                btn = new XisButton(repository, space, diagram, actionName + "Button",
                    actionName);
                XISMobileHelper.CreateXisAction(btn.Element, actionName, ActionType.Read,
                    spaceName);
            }
        }

        private static void AssociateBEtoIS(EA.Repository repository, EA.Diagram diagram, XisInteractionSpace source, EA.Element be)
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

        private static bool ContainsCreate(string[] operations)
        {
            if (operations != null)
            {
                foreach (string op in operations)
                {
                    if (op.ToLower() == "c" || op.ToLower() == "create")
                    {
                        return true;
                    }
                } 
            }
            return false;
        }

        private static bool ContainsRead(string[] operations)
        {
            if (operations != null)
            {
                foreach (string op in operations)
                {
                    if (op.ToLower() == "r" || op.ToLower() == "read")
                    {
                        return true;
                    }
                } 
            }
            return false;
        }

        private static bool ContainsUpdate(string[] operations)
        {

            if (operations != null)
            {
                foreach (string op in operations)
                {
                    if (op.ToLower() == "u" || op.ToLower() == "update")
                    {
                        return true;
                    }
                } 
            }
            return false;
        }

        private static bool ContainsDelete(string[] operations)
        {
            if (operations != null)
            {
                foreach (string op in operations)
                {
                    if (op.ToLower() == "d" || op.ToLower() == "delete")
                    {
                        return true;
                    }
                } 
            }
            return false;
        }

        private static bool ContainsSearch(string[] operations)
        {
            if (operations != null)
            {
                foreach (string op in operations)
                {
                    if (op.ToLower() == "s" || op.ToLower() == "search")
                    {
                        return true;
                    }
                } 
            }
            return false;
        }

        public enum Mode
        {
            Create,
            View,
            Edit
        }
    }
}
