using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XISMobileEAPlugin.InteractionSpace
{
    class XisMenuGroup : XisWidget
    {
        public List<XisMenuItem> Items { get; set; }

        public XisMenuGroup(EA.Repository repository, EA.Diagram diagram, XisMenu parent, string name) : base(repository)
        {
            Element = XISMobileHelper.CreateXisMenuGroup(parent.Element, name);
            Items = new List<XisMenuItem>();
            parent.Groups.Add(this);
        }
    }
}
