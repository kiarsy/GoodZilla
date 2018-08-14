using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Customer
    {
        private static Goodzila.GoodzilaCore _DB;
        public static Goodzila.GoodzilaCore DB()
        {
            if (_DB == null)
                //_DB = new Goodzila.GoodzilaCore(Goodzila.DbProviders.Microsoft_Sql_Server, @"Data Source=.\SQLEXPRESS;AttachDbFilename=F:\Project_lovely\Omega\Omega\App_Data\Omega.mdf;Integrated Security=True;User Instance=True");
                _DB = new Goodzila.GoodzilaCore(Goodzila.DbProviders.Microsoft_Sql_Server, @"Data Source=.;Initial Catalog=Loan_1390;Integrated Security=True");

            _DB.defaultCommandType = System.Data.CommandType.Text;
            return _DB;
        }
    }
}
