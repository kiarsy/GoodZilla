using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace db
{
    public class Class2
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


        da.cls2 C = new da.cls2();

        public System.Data.DataSet CustomerList()
        {
            return C.CustomerList();
        }


        public int Update1()
        {
            return C.CustomerUpdate("1", "hoseini11");
        }


        public int Update2()
        {
            return C.CustomerUpdate("2", "hoseini22");
        }
    }
}
