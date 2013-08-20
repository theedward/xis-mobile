﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XISMobileEAPlugin.InteractionSpace
{
    class XisDropdown : XisWidget
    {
        public XisDropdown(EA.Repository repository, XisWidget parent, EA.Diagram diagram,
            string name, string label) : base(repository)
        {
            Element = XISMobileHelper.CreateXisDropdown(parent.Element, name, label);

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
