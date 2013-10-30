using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XISMobileEAPlugin.InteractionSpace
{
    class XisMenu : XisWidget
    {
        public List<XisMenuGroup> Groups { get; set; }
        public List<XisMenuItem> Items { get; set; }

        public XisMenu(EA.Repository repository, EA.Diagram diagram, XisWidget parent, string name, MenuType type) : base(repository)
        {
            Element = XISMobileHelper.CreateXisMenu(parent.Element, name, type);
            Groups = new List<XisMenuGroup>();
            Items = new List<XisMenuItem>();

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

        public XisMenu(EA.Repository repository, EA.Diagram diagram,
            EA.Package package, string name, MenuType type)
            : base(repository)
        {
            Element = XISMobileHelper.CreateXisMenu(package, name, type);
            Items = new List<XisMenuItem>();
        }
    }
}
