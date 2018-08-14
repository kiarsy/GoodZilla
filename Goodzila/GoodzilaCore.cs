using Goodzila.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Goodzila
{
    public class GoodzilaCore:IDisposable
    {
        public GoodzilaCore(Enum.DbProviders DbProvider)
        {
            this.dbProviderType = DbProvider;

            AssignProviderToInterface();
        }

        public GoodzilaCore(Enum.DbProviders DbProviders, string ConnectionString)
        {
            this.dbProviderType = DbProviders;

            AssignProviderToInterface();

            this.ConnectionString = ConnectionString;
        }

        #region Fields

        Interfaces.IProviderBase dbProviderInterface;
        Enum.DbProviders dbProviderType;
        string connectionString;

        #endregion

        #region Properties

        //FakeMapClass must use for unit Testing
        public Fake.FakeMap FakeMapClass
        {
            get;
            set;
        }

        [DefaultValue(System.Data.CommandType.Text)]
        public System.Data.CommandType defaultCommandType
        {
            get;
            set;
        }

        public string ConnectionString
        {
            get { return dbProviderInterface.ConectionString; }
            set
            {
                dbProviderInterface.ConectionString = value;
                connectionString = value;
            }
        }

        #endregion

        #region Transaction Methods

        public void TransactionBegin()
        {
            dbProviderInterface.TransactionIsUserBegin = true;
        }

        public void TransactionCommit()
        {
            if (dbProviderInterface.TransactionIsUserBegin)
                dbProviderInterface.TransactionCommit();
        }

        public void TransactionRollBack()
        {
            if (dbProviderInterface.TransactionIsUserBegin)
                dbProviderInterface.TransactionRollBack();
        }

        #endregion


        void AssignProviderToInterface()
        {
            switch (dbProviderType)
            {
                case Enum.DbProviders.Microsoft_Sql_Server:
                    dbProviderInterface = new Providers.MsSqlServer();
                    break;
                case Enum.DbProviders.Microsoft_Access:
                    dbProviderInterface = new Providers.MsAccess();
                    break;
                case Enum.DbProviders.Oracle:
                    dbProviderInterface = new Providers.Oracle();
                    break;
                case Enum.DbProviders.Microsoft_Sql_CE:
                    dbProviderInterface = new Providers.MsSqlServerCE();
                    break;
            }
        }

        void FakeMapConfig()
        {
            if (FakeMapClass != null)
            {//If FakeMap Active

                //Change CurrentProvider To FakeProvider
                dbProviderInterface = new Providers.FakeProvider();
                ((Providers.FakeProvider)dbProviderInterface).FakeMapClass = FakeMapClass;

            }
            else if (dbProviderInterface.GetType() == typeof(Providers.FakeProvider))
            {//if FakeProvider Map class Set to Null , AND Current Provider is FakeProvider

                //ChangeBack CurrentProvider to Real Provider
                AssignProviderToInterface();
                ConnectionString = connectionString;
            }
        }

        public Command CreateCommnad()
        {
            //check if FakeMap is Active , Use FakeProvider
            FakeMapConfig();

            //Initial dbProvider
            dbProviderInterface.Parameters = new ParameterCollection();
            dbProviderInterface.Sql = "";
            dbProviderInterface.commandType = defaultCommandType;

            var command = new Command(dbProviderInterface);
            AssignedCommands.Add(command);
            return command;
        }

        List<Command> AssignedCommands = new List<Command>();



        public void Dispose()
        {
            foreach (Command com in AssignedCommands)
                com.CloseConnection();
        }
    }
}