using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goodzila.Providers
{
    internal class Oracle : Interfaces.IProviderBase
    {
        public int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public int ExecuteReturnLastId()
        {
            throw new NotImplementedException();
        }

        public object ExecuteReturnScaler()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet Query()
        {
            throw new NotImplementedException();
        }

        public List<T> Query<T>()
        {
            throw new NotImplementedException();
        }

        public T QuerySingle<T>()
        {
            throw new NotImplementedException();
        }

        public Enum.DbProviders ProviderType
        {
            get { throw new NotImplementedException(); }
        }

        public bool TransactionStart
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Sql
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Data.CommandType commandType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Struct.ParameterCollection Parameters
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ConectionString
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool TransactionIsUserBegin
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public System.Data.ConnectionState State()
        {
            throw new NotImplementedException();
        }

        public void TransactionRollBack()
        {
            throw new NotImplementedException();
        }

        public void TransactionCommit()
        {
            throw new NotImplementedException();
        }

        public void TransactionOpen()
        {
            throw new NotImplementedException();
        }


        public dynamic QuerySingle()
        {
            throw new NotImplementedException();
        }


        public dynamic QuerySingleDynamic()
        {
            throw new NotImplementedException();
        }
    }
}
