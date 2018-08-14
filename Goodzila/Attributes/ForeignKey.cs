using Goodzila.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Goodzila.Attributes
{
    public class ForeignKey : Attribute, IGoodZillaAttribute
    {
        public ForeignKey(Type ForriegnKeyClass)
        {
            this.ForriegnKeyClass = ForriegnKeyClass;
        }

        public Type ForriegnKeyClass
        { get; set; }

    }
}
