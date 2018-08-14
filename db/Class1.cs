using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace db
{
    public class Class1
    {

        public void Commit()
        {
            C.commit();
        }

        public void rollback()
        {
            C.rollback();
        }

        public void begin()
        {
            C.begintrans();
        }


        da.Class1 C = new da.Class1();

        public System.Data.DataSet CustomerList()
        {
            return C.CustomerList();
        }


        public int Update1()
        {
            return C.CustomerUpdate("1", "hoseini1");
        }


        public int Update2()
        {
            return C.CustomerUpdate("2", "hoseini2");
        }
    }
}
