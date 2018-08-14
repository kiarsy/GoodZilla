using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Goodzila.Struct;

namespace Goodzila.Interfaces
{
    public interface IProviderBase
    {

        #region ExecuteMethods

        /// <summary>
        /// This method execute sql;
        /// </summary>
        /// <returns>Number of rows affected</returns>
        int ExecuteNonQuery();

        /// <summary>
        /// this method work as ExecuteNonQuery().
        /// </summary>
        /// <returns>SCOPE_IDENTITY</returns>
        int ExecuteReturnLastId();

        /// <summary>
        /// this method work as ExecuteNonQuery().
        /// </summary>
        /// <returns>Return one value</returns>
        object ExecuteReturnScaler();


        /// <summary>
        /// Execute sql query
        /// </summary>
        /// <returns>Dataset of selected tables and rows</returns>
        DataSet Query();

        /// <summary>
        /// this method work as Query();
        /// </summary>
        /// <typeparam name="T">Class type of bussines layer</typeparam>
        /// <returns>Generic list of selected rows in T class format</returns>
        List<T> Query<T>();

        /// <summary>
        /// this method work as Query();
        /// </summary>
        /// <typeparam name="T">Class type of bussines layer</typeparam>
        /// <returns>Return top row of selected rows in T class format</returns>
        T QuerySingle<T>();

        dynamic QuerySingleDynamic();

        #endregion

        #region Properties
        Enum.DbProviders ProviderType
        {
            get;
        }

        bool TransactionStart
        {
            set;
            get;
        }

        string Sql
        {
            get;
            set;
        }

        CommandType commandType
        {
            set;
            get;
        }

        ParameterCollection Parameters
        {
            get;
            set;
        }

        string ConectionString
        {
            set;
            get;
        }

        bool TransactionIsUserBegin
        {
            get;
            set;
        }
        #endregion

        #region Methods
        void Open();
        void Close();
        ConnectionState State();
        #endregion

        #region transactionMethod
        void TransactionRollBack();
        void TransactionCommit();
        void TransactionOpen();
        #endregion


    }
}