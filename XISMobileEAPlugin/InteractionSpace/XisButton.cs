using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XISMobileEAPlugin.InteractionSpace
{
    class XisButton : XisWidget
    {
        public XisButton(EA.Repository repository, XisWidget parent, EA.Diagram diagram,
            string name, string onTap = null) : base(repository)
        {
            Element = XISMobileHelper.CreateXisButton(parent.Element, name, onTap);

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
