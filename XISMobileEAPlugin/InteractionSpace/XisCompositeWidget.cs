using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XISMobileEAPlugin.InteractionSpace
{
    class XisCompositeWidget : XisWidget
    {
        public List<XisWidget> Widgets { get; set; }

        public XisCompositeWidget(EA.Repository repository, EA.Diagram diagram,
            XisWidget parent, string name, CompositeWidgetType type, string onTap = null, string onLongTap = null,
            string searchBy = null, string orderBy = null) : base(repository)
        {
            Element = XISMobileHelper.CreateXisCompositeWidget(parent.Element, name, type, onTap, onLongTap, searchBy, orderBy);
            Widgets = new List<XisWidget>();

            if (parent is XisInteractionSpace)
            {
                XisInteractionSpace it = parent as XisInteractionSpace;
                it.Widgets.Add(this);
            }
            else if (parent is XisCompositeWidget)
            {
                XisCompositeWidget comp = parent as XisCompositeWidget;
                comp.Widgets.Add(this);
            }
        }

        public XisCompositeWidget(EA.Repository repository, EA.Diagram diagram,
            EA.Package package, string name, CompositeWidgetType type, string onTap = null, string onLongTap = null,
            string searchBy = null, string orderBy = null)
            : base(repository)
        {
            Element = XISMobileHelper.CreateXisCompositeWidget(package, name, type, onTap, onLongTap, searchBy, orderBy);
            Widgets = new List<XisWidget>();
        }

        public void SetOnTapAction(string onTap)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "onTap")
                {
                    tv.Value = onTap;
                    tv.Update();
                    return;
                }
            }
        }

        public void SetSearchBy(string searchBy)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "searchBy")
                {
                    tv.Value = searchBy;
                    tv.Update();
                    return;
                }
            }
        }
    }
}
