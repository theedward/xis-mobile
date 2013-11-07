﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XISMobileEAPlugin.InteractionSpace
{
    class XisVisibilityBoundary : XisCompositeWidget
    {
        public XisWidget Parent { get; set; }

        public XisVisibilityBoundary(EA.Repository repository, EA.Diagram diagram, XisWidget parent, string name,
            bool create = false, bool edit = false, bool view = false)
            : base(repository)
        {
            Element = XISMobileHelper.CreateXisVisibilityBoundary(parent.Element, name, create, edit, view);
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

        public void SetCreate(bool create)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "create")
                {
                    tv.Value = create.ToString();
                    tv.Update();
                    return;
                }
            }
        }

        public void SetEdit(bool edit)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "edit")
                {
                    tv.Value = edit.ToString();
                    tv.Update();
                    return;
                }
            }
        }

        public void SetView(bool view)
        {
            foreach (EA.TaggedValue tv in Element.TaggedValues)
            {
                if (tv.Name == "view")
                {
                    tv.Value = view.ToString();
                    tv.Update();
                    return;
                }
            }
        }
    }
}
