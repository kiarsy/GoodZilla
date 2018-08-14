using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Goodzila.Struct
{
    public class Property
    {
        public object ClassObject
        {
            get;
            set;
        }
        public string PropertyName
        {
            get;
            set;
        }

        public string ColumnName
        {
            get
            {
                PropertyInfo Property = ClassObject.GetType().GetProperties().Where(itm => itm.GetCustomAttributes(typeof(Goodzila.Attributes.PrimaryKey), false).Length > 0).FirstOrDefault();

                if (Property == null)
                    return "";
                else
                    return Property.Name;

            }
        }
    }
}
