using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Goodzila.Struct;

namespace Goodzila
{
    public class Command
    {
        public Command(Interfaces.IProviderBase DbProvider)
        {
            this.DbProvider = DbProvider;

            //If database Connection is not Connected
            if (DbProvider.State() == System.Data.ConnectionState.Closed)
                DbProvider.Open();

            //if User ACtive Transaction (call TransactionBegin()) And Provider Transaction is Close , Open Provider Transaction
            if (DbProvider.TransactionIsUserBegin && !DbProvider.TransactionStart)
                DbProvider.TransactionOpen();
        }

        #region Fields

        Interfaces.IProviderBase DbProvider;

        #endregion

        #region Command Method

        public void CloseConnection()
        {
            DbProvider.Close();
        }

        public Command Sql(string SqlSyntax)
        {
            DbProvider.Sql = SqlSyntax;

            return this;
        }

        public Command changeCommandType(CommandType commandType)
        {
            DbProvider.commandType = commandType;
            return this;
        }

        #endregion

        #region Parameter Method

        /// <summary>
        /// Assign value to specified Parameters.
        /// Example : SELECT * FROM table WHERE a=@FieldA AND b=@FieldB ;
        /// </summary>
        /// <param name="Name">Name of parameter</param>
        /// <param name="Value">Value of parameter</param>
        /// <returns></returns>
        public Command ParameterAdd(string Name, object Value)
        {

            Parameter P = new Parameter();
            P.Name = Name;
            P.value = Value;
            
            DbProvider.Parameters.Add(P);
            
            return this;
        }
        
        /// <summary>
        /// Assign value to Numeric serial parameters.
        /// Example : SELECT * FROM table WHERE a=@1 AND b=@2 AND c=@3 ;
        /// </summary>
        /// <param name="Value">value of parameter</param>
        /// <returns></returns>
        public Command ParameterAdd(object Value)
        {
            Parameter P = new Parameter();
            P.Name = (DbProvider.Parameters.Count + 1).ToString();
            P.value = Value;

            DbProvider.Parameters.Add(P);

            return this;
        }

        #endregion

        #region ExecuteMethods

        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <returns>Number of rows affected.</returns>
        /// 
        public int ExecuteNonQuery()
        {
            return DbProvider.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the Value of Last identity Field.
        /// </summary>
        /// <returns>SCOPE_IDENTITY</returns>
        public int ExecuteReturnLast()
        {
            return DbProvider.ExecuteReturnLastId();
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result.
        /// Additional columns or rows are ignored .
        /// </summary>
        /// <returns></returns>
        public object ExecuteReturnScaler()
        {
            return DbProvider.ExecuteReturnScaler();
        }

        /// <summary>
        /// fill a DataSet with the table(s) from your Sql command result.
        /// </summary>
        /// <returns>Dataset of selected table(s)</returns>
        public DataSet Query()
        {
            return DbProvider.Query();
        }

        /// <summary>
        /// Create a Generic List of T Class and fill it with the table(s) data of your Sql command result.
        /// Add a item for each row of result in returned generic list.
        /// </summary>
        /// <typeparam name="T">Class type of bussines layer</typeparam>
        /// <returns>Generic list of selected rows in T class format</returns>
        public List<T> Query<T>()
        {
            return DbProvider.Query<T>();
        }

        /// <summary>
        /// Create a T Class and its properties with Top Row.
        /// </summary>
        /// <typeparam name="T">Class type of bussines layer</typeparam>
        /// <returns>Return top row of selected rows in T class format</returns>
        public T QuerySingle<T>()
        {
            return DbProvider.QuerySingle<T>();
        }

        public dynamic QuerySingle()
        {
            return DbProvider.QuerySingleDynamic();
        }
        #endregion
    }
}
