using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XISMobileEAPlugin.InteractionSpace
{
    abstract class XisSimpleWidget : XisWidget
    {
        public XisSimpleWidget(EA.Repository repository) : base(repository)
        {
            Repository = repository;
        }
    }
}
