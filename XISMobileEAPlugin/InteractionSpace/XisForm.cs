using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XISMobileEAPlugin.InteractionSpace
{
    class XisForm : XisCompositeWidget
    {
        public XisWidget Parent { get; set; }

        public XisForm(EA.Repository repository, EA.Diagram diagram, XisWidget parent, string name, string entityName)
            : base(repository)
        {
            Element = XISMobileHelper.CreateXisForm(parent.Element, name, entityName);
            Parent = parent;

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
    }
}
