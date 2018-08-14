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
using System.Dynamic;
using Microsoft.CSharp.RuntimeBinder;

namespace Goodzila.Providers
{
    internal class MsSqlServer : Interfaces.IProviderBase
    {
        public MsSqlServer()
        {
            _Connection = new SqlConnection();
            return;
            if (ConectionString == null)
            {
                //////////////////////////////////////////////////////////////////////////////////
                _Connection.ConnectionString = ConectionString;
                //////////////////////////////////////////////////////////////////////////////////
            }
            else
                _Connection.ConnectionString = ConectionString;
        }

        #region Properties

        public IDbConnection _Connection;
        public IDbTransaction _Transaction;

        public string Sql
        {
            get;
            set;
        }

        public bool TransactionStart
        {
            get;
            set;
        }

        public Enum.DbProviders ProviderType
        {
            get { return Enum.DbProviders.Microsoft_Sql_Server; }
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
            get { return _Connection.ConnectionString; }
            set { _Connection.ConnectionString = value; }
        }

        public bool TransactionIsUserBegin
        {
            get;
            set;
        }

        #endregion

        #region Connection methods and Property

        public void Open()
        {
            _Connection.Open();
            if (TransactionIsUserBegin)
            {
                _Transaction = _Connection.BeginTransaction();
                TransactionStart = true;
            }
        }

        public void Close()
        {
            _Connection.Close();
        }

        void SureConnectionAlive()
        {
            if (State() != ConnectionState.Open)
                Open();
        }

        public System.Data.ConnectionState State()
        {
            return _Connection.State;
        }

        #endregion

        #region Transaction
        public void TransactionRollBack()
        {
            _Transaction.Rollback();
            TransactionStart = false;
            TransactionIsUserBegin = false;
            _Transaction = null;
        }

        public void TransactionCommit()
        {
            _Transaction.Commit();
            TransactionStart = false;
            TransactionIsUserBegin = false;
            _Transaction = null;
        }

        public void TransactionOpen()
        {
            _Transaction = _Connection.BeginTransaction();
            TransactionStart = true;
        }

        #endregion

        #region DataBase Command Methods

        public int ExecuteNonQuery()
        {
            SqlCommand Command = new SqlCommand(Sql, (SqlConnection)_Connection);
            if (_Transaction != null)
                Command.Transaction = (SqlTransaction)_Transaction;

            Command.CommandType = commandType;

            foreach (Parameter P in Parameters)
                Command.Parameters.Add(new SqlParameter(P.Name, (P.value == null ? DBNull.Value : P.value)));

            SureConnectionAlive();
            return Command.ExecuteNonQuery();
        }

        public int ExecuteReturnLastId()
        {
            SureConnectionAlive();
            if (commandType == CommandType.StoredProcedure)
                return ExecuteNonQuery();
            try
            {
                SqlCommand Command = new SqlCommand(Sql + ";SELECT SCOPE_IDENTITY()", (SqlConnection)_Connection);
                if (_Transaction != null)
                    Command.Transaction = (SqlTransaction)_Transaction;


                Command.CommandType = commandType;

                foreach (Parameter P in Parameters)
                    Command.Parameters.Add(new SqlParameter(P.Name, (P.value == null ? DBNull.Value : P.value)));

                SureConnectionAlive();
                object O = Command.ExecuteScalar();
                int o = (O == DBNull.Value) ? 0 : int.Parse(O.ToString());
                return o;
            }
            catch (SqlException S)
            {
                throw new Exception(S.Message, S);
            }
        }

        public object ExecuteReturnScaler()
        {
            SqlCommand Command = new SqlCommand(Sql, (SqlConnection)_Connection);
            if (_Transaction != null)
                Command.Transaction = (SqlTransaction)_Transaction;



            Command.CommandType = commandType;

            foreach (Parameter P in Parameters)
                Command.Parameters.Add(new SqlParameter(P.Name, (P.value == null ? DBNull.Value : P.value)));

            SureConnectionAlive();
            return Command.ExecuteScalar();
        }

        public DataSet Query()
        {
            SqlCommand Command = new SqlCommand(Sql, (SqlConnection)_Connection);
            if (_Transaction != null)
                Command.Transaction = (SqlTransaction)_Transaction;

            Command.CommandType = commandType;

            foreach (Parameter P in Parameters)
                Command.Parameters.Add(new SqlParameter(P.Name, (P.value == null ? DBNull.Value : P.value)));

            SqlDataAdapter DataAdap = new SqlDataAdapter(Command);
            DataSet DS = new DataSet();
            SureConnectionAlive();
            DataAdap.Fill(DS);
            return DS;
        }

        public List<T> Query<T>()
        {
            SqlCommand Command = new SqlCommand(Sql, (SqlConnection)_Connection);
            if (_Transaction != null)
                Command.Transaction = (SqlTransaction)_Transaction;

            //init Command to execute
            Command.CommandType = commandType;
            foreach (Parameter P in Parameters)
                Command.Parameters.Add(new SqlParameter(P.Name, (P.value == null ? DBNull.Value : P.value)));


            List<T> OutPutRecords = new List<T>();

            SqlDataReader Dr = Command.ExecuteReader();

            //-----
            SureConnectionAlive();

            Utilities.ClassValue objClassValue = new Utilities.ClassValue();

            //----Start Read Data
            while (Dr.Read())
            {
                T ClassRecord = objClassValue.SetClassValues<T>(Dr);

                //Add To OutPut Records
                OutPutRecords.Add((T)ClassRecord);
            }

            Dr.Close();
            return OutPutRecords;
        }

        public T QuerySingle<T>()
        {
            SqlCommand Command = new SqlCommand(Sql, (SqlConnection)_Connection);
            if (_Transaction != null)
                Command.Transaction = (SqlTransaction)_Transaction;

            //init Command to execute
            Command.CommandType = commandType;
            foreach (Parameter P in Parameters)
                Command.Parameters.Add(new SqlParameter(P.Name, (P.value == null ? DBNull.Value : P.value)));

            T OutPutRecord = (T)Activator.CreateInstance(typeof(T));

            SqlDataReader Dr = Command.ExecuteReader();

            //-----
            SureConnectionAlive();

            Utilities.ClassValue objClassValue = new Utilities.ClassValue();

            //----Start Read Data
            while (Dr.Read())
            {

                OutPutRecord = objClassValue.SetClassValues<T>(Dr);
            }

            Dr.Close();
            return OutPutRecord;
        }


        #endregion





        public dynamic QuerySingleDynamic()
        {
            SqlCommand Command = new SqlCommand(Sql, (SqlConnection)_Connection);
            if (_Transaction != null)
                Command.Transaction = (SqlTransaction)_Transaction;

            //init Command to execute
            Command.CommandType = commandType;
            foreach (Parameter P in Parameters)
                Command.Parameters.Add(new SqlParameter(P.Name, (P.value == null ? DBNull.Value : P.value)));

            SqlDataReader Dr = Command.ExecuteReader();

            //-----
            SureConnectionAlive();

            Utilities.ClassValue objClassValue = new Utilities.ClassValue();

            //----Start Read Data
            while (Dr.Read())
            {

                dynamic dynamicOutPut = new ExpandoObject();
                var dictionary = (IDictionary<string, object>)dynamicOutPut;
                for (int i = 0; i < Dr.FieldCount; i++)
                {
                    dictionary.Add(Dr.GetName(i), Dr[i]);
                }
                Dr.Close();
                return dynamicOutPut;

            }
            Dr.Close();
            return null;
        }
    }
}