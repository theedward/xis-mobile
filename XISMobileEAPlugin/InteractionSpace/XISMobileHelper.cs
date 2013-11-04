
using System.Windows.Forms;
using System;
namespace XISMobileEAPlugin.InteractionSpace
{
    class XISMobileHelper
    {
        public static EA.Diagram CreateDiagram(EA.Package package, string diagramName, string diagramType)
        {
            EA.Diagram diagram = package.Diagrams.AddNew(diagramName, diagramType);
            diagram.ShowDetails = 0;
            diagram.Update();
            package.Update();
            return diagram;
        }

        //public static EA.DiagramObject CreateElementLink(EA.Repository repository, EA.Element element, EA.Diagram diagram)
        //{
        //    EA.DiagramObject obj = diagram.DiagramObjects.AddNew(element.Name, element.Type);
        //    obj.Update();
        //    diagram.Update();
        //    diagram.DiagramObjects.Refresh();
        //    string query = "update t_diagramobjects set Object_ID = " + element.ElementID + " where Diagram_ID = " + diagram.DiagramID + " and Object_ID = 0"; ;
        //    repository.Execute(query);
        //    return obj;
        //}

        public static EA.DiagramObject SetPosition(EA.Repository repository, EA.Diagram diagram, EA.Element element,
            int? left = null, int? right = null, int? top = null, int? bottom = null, int sequence = 0)
        {
            //try
            //{
            diagram.DiagramObjects.Refresh();
            for (short i = 0; i < diagram.DiagramObjects.Count; i++)
            {
                EA.DiagramObject obj = diagram.DiagramObjects.GetAt(i);

                if (obj.ElementID == element.ElementID)
                {
                    if (left.HasValue && right.HasValue && top.HasValue && bottom.HasValue)
                    {
                        obj.left = left.Value;
                        obj.right = right.Value;
                        obj.top = top.Value * -1;
                        obj.bottom = top.Value * -1;
                        obj.Sequence = sequence;
                        obj.Update();
                    }
                    return obj;
                }
            }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.StackTrace);
            //}

            EA.DiagramObject diagObj = diagram.DiagramObjects.AddNew(element.Name, "Class");
            
            if (left.HasValue && right.HasValue && top.HasValue && bottom.HasValue)
            {
                diagObj.left = left.Value;
                diagObj.right = right.Value;
                diagObj.top = top.Value * -1;
                diagObj.bottom = bottom.Value * -1;
            }
            diagObj.Sequence = sequence;
            diagObj.Update();
            diagram.Update();
            diagram.DiagramObjects.Refresh();
            string query = "update t_diagramobjects set Object_ID = " + element.ElementID + " where Diagram_ID = "
                + diagram.DiagramID + " and Object_ID = 0";
            repository.Execute(query);

            return diagObj;
        }

        public static EA.Element CreateXisInteractionSpace(EA.Package package, EA.Diagram diagram,
            string name, string title, bool isMainScreen = false)
        {
            EA.Element element = package.Elements.AddNew(name, "Class");
            element.Stereotype = "XisInteractionSpace";
            element.Update();

            foreach (EA.TaggedValue tv in element.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "isMainScreen":
                        tv.Value = isMainScreen.ToString().ToLower();
                        break;
                    case "name":
                        tv.Value = name;
                        break;
                    case "title":
                        tv.Value = title;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            package.Update();

            return element;
        }

        public static void SetXisWidgetValues(EA.Element widget, int posX, int posY,
            int width, int height, string value)
        {
            foreach (EA.TaggedValue tv in widget.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "posX":
                        tv.Value = posX.ToString();
                        break;
                    case "posY":
                        tv.Value = posY.ToString();
                        break;
                    case "width":
                        tv.Value = width.ToString();
                        break;
                    case "height":
                        tv.Value = height.ToString();
                        break;
                    case "value":
                        tv.Value = value;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
        }

        public static EA.Element CreateXisCompositeWidget(EA.Package package, string name, CompositeWidgetType type, string onTap = null,
            string onLongTap = null, string searchBy = null, string orderBy = null)
        {
            EA.Element composite = package.Elements.AddNew(name, "Class");
            composite.Stereotype = "XisCompositeWidget";
            composite.Update();

            foreach (EA.TaggedValue tv in composite.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "type":
                        tv.Value = type.ToString();
                        break;
                    case "onTap":
                        tv.Value = onTap;
                        break;
                    case "onLongTap":
                        tv.Value = onLongTap;
                        break;
                    case "searchBy":
                        tv.Value = searchBy;
                        break;
                    case "orderBy":
                        tv.Value = orderBy;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            package.Update();

            return composite;
        }

        public static EA.Element CreateXisCompositeWidget(EA.Element parent, string name, CompositeWidgetType type, string onTap = null,
            string onLongTap = null, string searchBy = null, string orderBy = null)
        {
            EA.Element composite = parent.Elements.AddNew(name, "Class");
            composite.Stereotype = "XisCompositeWidget";
            composite.Update();

            foreach (EA.TaggedValue tv in composite.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "type":
                        tv.Value = type.ToString();
                        break;
                    case "onTap":
                        tv.Value = onTap;
                        break;
                    case "onLongTap":
                        tv.Value = onLongTap;
                        break;
                    case "searchBy":
                        tv.Value = searchBy;
                        break;
                    case "orderBy":
                        tv.Value = orderBy;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            parent.Update();

            return composite;
        }

        public static EA.Element CreateXisMenu(EA.Element parent, string name, MenuType type)
        {
            EA.Element menu = parent.Elements.AddNew(name, "Class");
            menu.Stereotype = "XisMenu";
            menu.Update();

            foreach (EA.TaggedValue tv in menu.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "type":
                        tv.Value = type.ToString();
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            parent.Update();

            return menu;
        }

        public static EA.Element CreateXisMenu(EA.Package package, string name, MenuType type)
        {
            EA.Element menu = package.Elements.AddNew(name, "Class");
            menu.Stereotype = "XisMenu";
            menu.Update();

            foreach (EA.TaggedValue tv in menu.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "type":
                        tv.Value = type.ToString();
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            package.Update();

            return menu;
        }

        public static EA.Element CreateXisMenuGroup(EA.Element parent, string name)
        {
            EA.Element group = parent.Elements.AddNew(name, "Class");
            group.Stereotype = "XisMenuGroup";
            group.Update();

            foreach (EA.TaggedValue tv in group.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            parent.Update();

            return group;
        }

        public static EA.Element CreateXisMenuItem(EA.Element parent, string name, string onTap)
        {
            EA.Element item = parent.Elements.AddNew(name, "Class");
            item.Stereotype = "XisMenuItem";
            item.Update();

            foreach (EA.TaggedValue tv in item.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "onTap":
                        tv.Value = onTap;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            parent.Update();

            return item;
        }

        public static EA.Element CreateXisList(EA.Element parent, string name, string searchBy = null, string orderBy = null)
        {
            EA.Element list = parent.Elements.AddNew(name, "Class");
            list.Stereotype = "XisList";
            list.Update();

            foreach (EA.TaggedValue tv in list.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "searchBy":
                        tv.Value = searchBy;
                        break;
                    case "orderBy":
                        tv.Value = orderBy;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            parent.Update();

            return list;
        }

        public static EA.Element CreateXisListItem(EA.Element parent, string name, string onTap = null, string onLongTap = null)
        {
            EA.Element item = parent.Elements.AddNew(name, "Class");
            item.Stereotype = "XisListItem";
            item.Update();

            foreach (EA.TaggedValue tv in item.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "onTap":
                        tv.Value = onTap;
                        break;
                    case "onLongTap":
                        tv.Value = onLongTap;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            parent.Update();

            return item;
        }

        public static XisWidget ProcessXisAttribute(EA.Repository repository, EA.Diagram diagram,
            XisWidget parent, EA.Attribute attribute, string entityName)
        {
            XisWidget widget = null;

            switch (attribute.Type.ToLower())
            {
                case "int":
                case "double":
                case "string":
                    widget = new XisTextBox(repository, parent, diagram, attribute.Name + "TxtBox", attribute.Name, attribute.Name);
                    break;
                case "bool":
                case "boolean":
                    // DropDown
                    widget = new XisDropdown(repository, parent, diagram, attribute.Name + "Dropdown", attribute.Name);
                    break;
                case "date":
                    widget = new XisDatePicker(repository, parent, diagram, attribute.Name + "DatePicker");
                    break;
                case "time":
                    //widget = CreateXisTimePicker(parent, attribute.Name + "TimePicker");
                    break;
                default:
                    break;
            }
            widget.SetValue(entityName + "." + attribute.Name);

            return widget;
        }

        public static EA.Element CreateXisTextBox(EA.Element parent, string name, string label, string hint, int lines = 1)
        {
            EA.Element textBox = parent.Elements.AddNew(name, "Class");
            textBox.Stereotype = "XisTextBox";
            textBox.Update();

            foreach (EA.TaggedValue tv in textBox.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "label":
                        tv.Value = label;
                        break;
                    case "hint":
                        tv.Value = hint;
                        break;
                    case "lines":
                        tv.Value = lines.ToString();
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            parent.Update();

            return textBox;
        }

        public static EA.Element CreateXisButton(EA.Element parent, string name, string onTap = null)
        {
            EA.Element button = parent.Elements.AddNew(name, "Class");
            button.Stereotype = "XisButton";
            button.Update();

            foreach (EA.TaggedValue tv in button.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "onTap":
                        tv.Value = onTap;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            parent.Update();

            return button;
        }

        public static EA.Element CreateXisDropdown(EA.Element parent, string name, string label)
        {
            EA.Element dropdown = parent.Elements.AddNew(name, "Class");
            dropdown.Stereotype = "XisDropdown";
            dropdown.Update();

            foreach (EA.TaggedValue tv in dropdown.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "label":
                        tv.Value = label;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            parent.Update();

            return dropdown;
        }

        public static EA.Element CreateXisDatePicker(EA.Element parent, string name, string timezone = null)
        {
            EA.Element datePicker = parent.Elements.AddNew(name, "Class");
            datePicker.Stereotype = "XisDatePicker";
            datePicker.Update();

            foreach (EA.TaggedValue tv in datePicker.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "timezone":
                        tv.Value = timezone;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            parent.Update();

            return datePicker;
        }

        public static EA.Element CreateXisTimePicker(EA.Element parent, string name, string timezone = null)
        {
            EA.Element timePicker = parent.Elements.AddNew(name, "Class");
            timePicker.Stereotype = "XisTimePicker";
            timePicker.Update();

            foreach (EA.TaggedValue tv in timePicker.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "name":
                        tv.Value = name;
                        break;
                    case "timezone":
                        tv.Value = timezone;
                        break;
                    default:
                        break;
                }
                tv.Update();
            }
            parent.Update();

            return timePicker;
        }

        public static EA.Method CreateXisAction(EA.Repository repository, EA.Element parent, string name, ActionType type, string navigation = null)
        {
            EA.Method action = null;

            switch (type)
	        {
		        case ActionType.Cancel:
                    action = parent.Methods.AddNew(name, "");
                    break;
                case ActionType.Save:
                    action = parent.Methods.AddNew(name, "");
                    break;
                case ActionType.Create:
                    action = parent.Methods.AddNew(name, "");
                    break;
                case ActionType.Read:
                    action = parent.Methods.AddNew(name, "");
                    break;
                case ActionType.Update:
                    action = parent.Methods.AddNew(name, "");
                    break;
                case ActionType.Delete:
                    action = parent.Methods.AddNew(name, "");
                    break;
                case ActionType.DeleteAll:
                    action = parent.Methods.AddNew(name, "");
                    break;
                case ActionType.ZoomIn:
                    break;
                case ActionType.ZoomOut:
                    break;
                case ActionType.WebService:
                    break;
                case ActionType.Custom:
                    break;
	        }

            action.Stereotype = "XisAction";
            action.StereotypeEx = "XIS-Mobile::XisAction";
            action.ClassifierID = "0";
            action.Update();

            //repository.Execute("insert into t_xref (XrefID, Name, Type, Visibility, Partition, Description, Client, Supplier) values " +
            //    "('{" + Guid.NewGuid() + "}', 'Stereotypes', 'operation property', 'Public', 0, '@STEREO;Name=XisAction;FQName=XIS-Mobile::XisAction;@ENDSTEREO;', '" + action.MethodGUID + "', '&lt;none&gt;');");
            //MessageBox.Show(Guid.NewGuid() + "");
            //foreach (EA.MethodTag tv in action.TaggedValues)
            //{
            //    switch (tv.Name)
            //    {
            //        case "name":
            //            tv.Value = name;
            //            break;
            //        case "type":
            //            tv.Value = type.ToString();
            //            break;
            //        case "navigation":
            //            tv.Value = navigation;
            //            break;
            //        default:
            //            break;
            //    }
            //    tv.Update();
            //}
            parent.Update();

            return action;
        }
    }

    enum CompositeWidgetType
    {
        Form,
        Tab
    }

    enum MenuType
    {
        OptionsMenu,
        ContextMenu
    }

    enum GestureType
    {
        Tap,
        DoubleTap,
        LongTap,
        Swipe,
        Pinch,
        Stretch
    }

    enum ActionType
    {
        OK,
        Cancel,
        Save,
        Create,
        Read,
        Update,
        Delete,
        DeleteAll,
        ZoomIn,
        ZoomOut,
        WebService,
        Custom
    }

    enum PrimitiveType
    {
        Integer,
        Double,
        String,
        Boolean,
        Date,
        Time
    }
}
