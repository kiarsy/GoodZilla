using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using Goodzila.Struct;

namespace Goodzila.Providers
{
    internal class FakeProvider : Interfaces.IProviderBase
    {
        public Fake.FakeMap FakeMapClass
        { get; set; }

        public int ExecuteNonQuery()
        {
            return FakeMapClass.ExecuteNonQuery;
        }

        public int ExecuteReturnLastId()
        {
            return FakeMapClass.ExecuteReturnLastId;
        }

        public object ExecuteReturnScaler()
        {
            return FakeMapClass.ExecuteReturnScaler;
        }

        public DataSet Query()
        {
            return FakeMapClass.Query;
        }

        public List<T> Query<T>()
        {
            throw new NotImplementedException();
        }

        public T QuerySingle<T>()
        {
            return (T)FakeMapClass.QuerySingleT;
        }



        public Enum.DbProviders ProviderType
        {
            get;
            set;
        }

        public bool TransactionStart
        {
            get;
            set;
        }

        public string Sql
        {
            get;
            set;
        }

        public CommandType commandType
        {
            get;
            set;
        }

        public ParameterCollection Parameters
        {
            get;
            set;
        }

        public string ConectionString
        {
            get;
            set;
        }

        public bool TransactionIsUserBegin
        {
            get;
            set;
        }

        public void Open()
        {

        }

        public void Close()
        {

        }

        public ConnectionState State()
        {
            return ConnectionState.Open;
        }

        public void TransactionRollBack()
        {

        }

        public void TransactionCommit()
        {

        }

        public void TransactionOpen()
        {
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