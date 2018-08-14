using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace da
{
    public class DB
    {
        static Goodzila.GoodzilaCore _gDb;
        public static Goodzila.GoodzilaCore gDb()
        {
            if (_gDb == null)
            {
                _gDb = new Goodzila.GoodzilaCore(Goodzila.DbProviders.Microsoft_Sql_Server);
                _gDb.ConnectionString = "Data Source=.;Initial Catalog=Test;Integrated Security=True";
            }
            return _gDb;
        }
    }
}
