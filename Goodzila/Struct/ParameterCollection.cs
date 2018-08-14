using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goodzila.Struct
{
    public class ParameterCollection : List<Struct.Parameter>
    {
        public Struct.Parameter this[int index]
        {
            get
            {
                return base[index];
            }
        }

        public Struct.Parameter this[string name]
        {
            get
            {
                return this.SingleOrDefault(x => x.Name == name);
            }
        }
    }
}
