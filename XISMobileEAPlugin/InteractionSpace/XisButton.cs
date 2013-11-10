using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XISMobileEAPlugin.InteractionSpace
{
    class XisButton : XisSimpleWidget
    {
        public XisButton(EA.Repository repository, XisWidget parent, EA.Diagram diagram,
            string name, string onTap = null) : base(repository)
        {
            if (parent is XisInteractionSpace)
            {
                XisInteractionSpace it = parent as XisInteractionSpace;
                Element = XISMobileHelper.CreateXisButton(it.Element, name, onTap);
                it.Widgets.Add(this);
            }
            else if (parent is XisVisibilityBoundary)
            {
                XisVisibilityBoundary boundary = parent as XisVisibilityBoundary;
                Element = XISMobileHelper.CreateXisButton(boundary.Parent.Element, name, onTap);
                boundary.Widgets.Add(this);
            }
            else if (parent is XisCompositeWidget)
            {
                XisCompositeWidget comp = parent as XisCompositeWidget;
                Element = XISMobileHelper.CreateXisButton(comp.Element, name, onTap);
                comp.Widgets.Add(this);
            }
        }
    }
}
