using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XISMobileEAPlugin.InteractionSpace
{
    class XisMenuItem : XisWidget
    {
        public XisMenuItem(EA.Repository repository, EA.Diagram diagram, XisWidget parent, string name, string onTap = null)
            : base(repository)
        {
            Element = XISMobileHelper.CreateXisMenuItem(parent.Element, name, onTap);

            if (parent is XisMenu)
            {
                XisMenu menu = parent as XisMenu;
                menu.Items.Add(this);
            }
            else if (parent is XisMenuGroup)
            {
                XisMenuGroup group = parent as XisMenuGroup;
                group.Items.Add(this);
            }
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
    }
}
