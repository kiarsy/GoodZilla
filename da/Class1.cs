using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace da
{
    public class Class1 : Class2
    {
        public System.Data.DataSet CustomerList()
        {
            return DB.gDb().CreateCommnad().Sql("select * from Customer").Query();
        }

        public int CustomerUpdate(string c, string s)
        {
            return DB.gDb().CreateCommnad().Sql("UPDATE Customer SET name='" + s + "' where code=" + c).ExecuteNonQuery();
        }

        public int CustomerUpdate2(string c, string s)
        {
            return DB.gDb().CreateCommnad().Sql("UPDATE Customer SET name='" + s + "' where code=" + c).ExecuteNonQuery();
        }
    }
}
