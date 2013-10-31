using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XISMobileEAPlugin.InteractionSpace
{
    abstract class XisCompositeWidget : XisWidget
    {
        public List<XisWidget> Widgets { get; set; }

        public XisCompositeWidget(EA.Repository repository, XisWidget parent) : base(repository)
        {
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

        public XisCompositeWidget(EA.Repository repository) : base(repository)
        {
            Widgets = new List<XisWidget>();
        }
    }
}
