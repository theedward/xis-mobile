﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XISMobileEAPlugin.InteractionSpace
{
    class XisList : XisCompositeWidget
    {
        public List<XisListItem> Items { get; set; }

        public XisList(EA.Repository repository, EA.Diagram diagram,
            XisInteractionSpace parent, string name, string searchBy = null, string orderBy = null)
            : base(repository, parent)
        {
            Element = XISMobileHelper.CreateXisList(parent.Element, name, searchBy, orderBy);
            Items = new List<XisListItem>();
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

        public void SetOrderBy(string orderBy)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "orderBy")
                {
                    tv.Value = orderBy;
                    tv.Update();
                    return;
                }
            }
        }
    }
}
