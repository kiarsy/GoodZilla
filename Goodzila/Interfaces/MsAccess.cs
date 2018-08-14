using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Reflection;
using Goodzila.Struct;

namespace Goodzila.Providers
{
    internal class MsAccess : Interfaces.IProviderBase
    {
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



        public MsAccess()
        {
            _Connection = new OleDbConnection();
            if (ConectionString == null)
            {
                //////////////////////////////////////////////////////////////////////////////////
                _Connection.ConnectionString = ConectionString;
                //////////////////////////////////////////////////////////////////////////////////
            }
            else
                _Connection.ConnectionString = ConectionString;
        }



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

        public System.Data.ConnectionState State()
        {
            return _Connection.State;
        }



        public void TransactionRollBack()
        {
            _Transaction.Rollback();
            TransactionStart = false;
            _Transaction = null;
        }

        public void TransactionCommit()
        {
            _Transaction.Commit();
            TransactionStart = false;
            _Transaction = null;
        }

        public void TransactionOpen()
        {
            _Transaction = _Connection.BeginTransaction();
            TransactionStart = true;
        }


        public int ExecuteNonQuery()
        {
            OleDbCommand Command = new OleDbCommand(Sql, (OleDbConnection)_Connection);
            if (_Transaction != null)
                Command.Transaction = (OleDbTransaction)_Transaction;



            foreach (Parameter P in Parameters)
                Command.Parameters.Add(new OleDbParameter(P.Name, P.value));

            return Command.ExecuteNonQuery();
        }

        public int ExecuteReturnLastId()
        {
            return ExecuteNonQuery();
        }

        public object ExecuteReturnScaler()
        {
            OleDbCommand Command = new OleDbCommand(Sql, (OleDbConnection)_Connection);
            if (_Transaction != null)
                Command.Transaction = (OleDbTransaction)_Transaction;


            foreach (Parameter P in Parameters)
                Command.Parameters.Add(new OleDbParameter(P.Name, P.value));

            return Command.ExecuteScalar();
        }


        public DataSet Query()
        {
            OleDbCommand Command = new OleDbCommand(Sql, (OleDbConnection)_Connection);
            if (_Transaction != null)
                Command.Transaction = (OleDbTransaction)_Transaction;


            foreach (Parameter P in Parameters)
                Command.Parameters.Add(new OleDbParameter(P.Name, P.value));


            OleDbDataAdapter DataAdap = new OleDbDataAdapter();
            DataSet DS = new DataSet();
            DataAdap.Fill(DS);
            return DS;
        }

        public List<T> Query<T>()
        {
            OleDbCommand Command = new OleDbCommand(Sql, (OleDbConnection)_Connection);
            if (_Transaction != null)
                Command.Transaction = (OleDbTransaction)_Transaction;


            foreach (Parameter P in Parameters)
                Command.Parameters.Add(new OleDbParameter(P.Name, P.value));

            List<string> properties = new List<string>();

            foreach (System.Reflection.PropertyInfo P in typeof(T).GetProperties())
                properties.Add(P.Name);

            List<T> Records = new List<T>();

            OleDbDataReader Dr = Command.ExecuteReader();
            object obj = Activator.CreateInstance(typeof(T));

            var columns = new List<string>();

            for (int i = 0; i < Dr.FieldCount; i++)
            {
                columns.Add(Dr.GetName(i));
            }

            var classProperties = typeof(T).GetProperties()
                .Where(Pro => Pro.PropertyType.UnderlyingSystemType.Namespace.ToLower() == typeof(T).Namespace.ToLower())
                .Select(itm => new Goodzila.Struct.Property() { ClassObject = Activator.CreateInstance(itm.PropertyType.UnderlyingSystemType), PropertyName = itm.Name });

            while (Dr.Read())
            {
                obj = Activator.CreateInstance(typeof(T));

                List<Goodzila.Struct.Property> Property = new List<Goodzila.Struct.Property>();
                Property.AddRange(classProperties);

                foreach (string column in columns)
                {
                    object value = Dr[column];
                    if (value == DBNull.Value)
                        value = null;

                    if (column.IndexOf('.') != -1)
                        goto ClassProperty;
                    PropertyInfo propertyInfo = typeof(T).GetProperty(column);
                    if (propertyInfo != null)
                        if (value != null)
                            propertyInfo.SetValue(obj, Convert.ChangeType(value, propertyInfo.PropertyType.UnderlyingSystemType), null);

                ClassProperty:
                    if (column.IndexOf('.') == -1)
                        goto END;

                    string columnN = column.Split('.')[1];
                    foreach (var item in Property)
                    {
                        object Cls = item.ClassObject;
                        PropertyInfo property = Cls.GetType().GetProperty(columnN);
                        if (property != null)
                            if (value != null)
                                property.SetValue(Cls, Convert.ChangeType(value, property.PropertyType.UnderlyingSystemType), null);

                        item.ClassObject = Cls;
                    }
                END:
                    continue;
                }
                foreach (var item in Property)
                {
                    PropertyInfo propertyInfo = typeof(T).GetProperty(item.PropertyName.ToString());
                    propertyInfo.SetValue(obj, item.ClassObject, null);
                }

                Records.Add((T)obj);
            }


            //while (Dr.Read())
            //{
            //    object obj = Activator.CreateInstance(typeof(T));
            //    foreach (string Prop in properties)
            //    {
            //        PropertyInfo numberPropertyInfo = typeof(T).GetProperty(Prop);
            //        try
            //        {
            //            numberPropertyInfo.SetValue(obj, Dr[Prop], null);
            //        }
            //        catch { }
            //    }
            //    Records.Add((T)obj);
            //}
            Dr.Close();
            return Records;
        }

        public T QuerySingle<T>()
        {
            OleDbCommand Command = new OleDbCommand(Sql, (OleDbConnection)_Connection);
            if (_Transaction != null)
                Command.Transaction = (OleDbTransaction)_Transaction;


            foreach (Parameter P in Parameters)
                Command.Parameters.Add(new OleDbParameter(P.Name, P.value));

            List<string> properties = new List<string>();

            foreach (System.Reflection.PropertyInfo P in typeof(T).GetProperties())
                properties.Add(P.Name);

            OleDbDataReader Dr = Command.ExecuteReader();
            object obj = Activator.CreateInstance(typeof(T));
            
            var columns = new List<string>();

            for (int i = 0; i < Dr.FieldCount; i++)
            {
                columns.Add(Dr.GetName(i));
            }

            var classProperties = typeof(T).GetProperties()
                .Where(Pro => Pro.PropertyType.UnderlyingSystemType.Namespace.ToLower() == typeof(T).Namespace.ToLower())
                .Select(itm => new Goodzila.Struct.Property() { ClassObject = Activator.CreateInstance(itm.PropertyType.UnderlyingSystemType), PropertyName = itm.Name });
            List<Goodzila.Struct.Property> Property = new List<Goodzila.Struct.Property>();
            Property.AddRange(classProperties);

            if (Dr.Read())
            {
                foreach (string column in columns)
                {
                    object value = Dr[column];
                    if (value == DBNull.Value)
                        value = null;

                    if (column.IndexOf('.') != -1)
                        goto ClassProperty;
                    PropertyInfo propertyInfo = typeof(T).GetProperty(column);
                    if (propertyInfo != null)
                        if (value != null)
                            propertyInfo.SetValue(obj, Convert.ChangeType(value, propertyInfo.PropertyType.UnderlyingSystemType), null);

                ClassProperty:
                    if (column.IndexOf('.') == -1)
                        goto END;

                    string columnN = column.Split('.')[1];
                    foreach (var item in Property)
                    {
                        object Cls = item.ClassObject;
                        PropertyInfo property = Cls.GetType().GetProperty(columnN);
                        if (property != null)
                            if (value != null)
                                property.SetValue(Cls, Convert.ChangeType(value, property.PropertyType.UnderlyingSystemType), null);

                        item.ClassObject = Cls;
                    }
                END:
                    continue;
                }
            }
            foreach (var item in Property)
            {
                PropertyInfo propertyInfo = typeof(T).GetProperty(item.PropertyName.ToString());
                propertyInfo.SetValue(obj, item.ClassObject, null);
            }


            //while (Dr.Read())
            //{

            //    foreach (string Prop in properties)
            //    {
            //        PropertyInfo numberPropertyInfo = typeof(T).GetProperty(Prop);
            //        numberPropertyInfo.SetValue(obj, Dr[Prop], null);
            //    }
            //    break;

            //}
            Dr.Close();
            return ((T)obj);

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