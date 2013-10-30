﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XISMobileEAPlugin.InteractionSpace
{
    class XisListItem : XisWidget
    {
        public List<XisWidget> Widgets { get; set; }

        public XisListItem(EA.Repository repository, EA.Diagram diagram,
            XisList parent, string name, string onTap = null, string onLongTap = null) : base(repository)
        {
            Element = XISMobileHelper.CreateXisListItem(parent.Element, name, onTap, onLongTap);
            Widgets = new List<XisWidget>();
            parent.Items.Add(this);
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

        public void SetOnLongTapAction(string onLongTap)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "onLongTap")
                {
                    tv.Value = onLongTap;
                    tv.Update();
                    return;
                }
            }
        }
    }
}
