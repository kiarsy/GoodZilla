using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Goodzila.Fake
{
    public  class FakeMap
    {
        public int ExecuteNonQuery
        {
            get;
            set;
        }

        public int ExecuteReturnLastId
        {
            get;
            set;
        }

        public object ExecuteReturnScaler
        {
            get;
            set;
        }

        public DataSet Query
        {
            get;
            set;
        }


        public List<object> QueryT
        {
            get;
            set;
        }


        public object QuerySingleT
        {
            get;
            set;
        }


    }
}