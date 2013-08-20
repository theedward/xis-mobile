using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XISMobileEAPlugin.InteractionSpace
{
    class XisWidget
    {
        public EA.Element Element { get; set; }
        public EA.Repository Repository { get; set; }

        public XisWidget(EA.Repository repository)
        {
            Repository = repository;
        }

        public EA.DiagramObject SetPosition(EA.Diagram diagram, int? left = null, int? right = null, int? top = null, int? bottom = null,
            int sequence = 0)
        {
            EA.DiagramObject diagObj = GetDiagramObject(diagram);
            diagram.DiagramObjects.Refresh();

            if (diagObj != null)
            {
                if (left.HasValue && right.HasValue && top.HasValue && bottom.HasValue)
                {
                    diagObj.left = left.Value;
                    diagObj.right = right.Value;
                    diagObj.top = -top.Value;
                    diagObj.bottom = -bottom.Value;
                    diagObj.Sequence = sequence;
                    SetPositionTaggedValues(left.Value, right.Value, top.Value, bottom.Value);
                    diagObj.Update();
                    //diagram.Update();
                    //Repository.ReloadDiagram(diagram.DiagramID);
                }
            }
            else
            {
                diagObj = diagram.DiagramObjects.AddNew(Element.Name, "Class");

                if (left.HasValue && right.HasValue && top.HasValue && bottom.HasValue)
                {
                    diagObj.left = left.Value;
                    diagObj.right = right.Value;
                    diagObj.top = -top.Value;
                    diagObj.bottom = -bottom.Value;
                    SetPositionTaggedValues(left.Value, right.Value, top.Value, bottom.Value);
                }

                diagObj.Sequence = sequence;
                diagObj.Update();
                diagram.Update();
                //Repository.ReloadDiagram(diagram.DiagramID);
                string query = "update t_diagramobjects set Object_ID = " + Element.ElementID + " where Diagram_ID = "
                    + diagram.DiagramID + " and Object_ID = 0";
                Repository.Execute(query);
            }
            return diagObj;
        }

        public EA.DiagramObject GetDiagramObject(EA.Diagram diagram)
        {
            diagram.DiagramObjects.Refresh();
            foreach (EA.DiagramObject obj in diagram.DiagramObjects)
	        {
                if (obj.ElementID == Element.ElementID)
                {
                    return obj;
                }
	        }
            return null;
        }

        public void SetValue(string value)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "value")
                {
                    tv.Value = value;
                    tv.Update();
                    return;
                }
            }
        }

        private void SetPositionTaggedValues(int left, int right, int top, int bottom)
        {
            int value = 0;
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                switch (tv.Name)
                {
                    case "posX":
                        value = (left + right) / 2;
                        tv.Value = value.ToString();
                        tv.Update();
                        break;
                    case "posY":
                        value = (top + bottom) / 2;
                        tv.Value = value.ToString();
                        tv.Update();
                        break;
                    case "width":
                        value = right - left;
                        tv.Value = value.ToString();
                        tv.Update();
                        break;
                    case "height":
                        value = bottom - top;
                        tv.Value = value.ToString();
                        tv.Update();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
