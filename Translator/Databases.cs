using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;

namespace Databases
{
    public enum Database
    {
        NULL,
        CMSDAT,
        PRODTEST,
        DECADE_MARKHAM,
        DECADE_MICHIGAN,
        DECADE_TEXAS,
        DECADE_COLOMBIA
    }

    /// <summary>
    /// ExcoODBC intends to manage all database connections via ODBC.
    /// Using singleton pattern.
    /// </summary>

    public class ExcoODBC
    {
        internal OdbcConnection connection;
        internal OdbcCommand command;
        internal string connectionString;
        internal Database database;
        internal static ExcoODBC instance;
        internal string dbName;
        internal string user = string.Empty;
        internal string password = string.Empty;
        internal string connection_str = "Driver={SQL Server};Server=10.0.0.8";
        internal string database_name = "dbo";

        public void _SET_CREDENTIALS(string _login, string _password, string _database_name, string _connection_str)
        {
            this.user = _login;
            this.password = _password;
            this.connection_str = _connection_str;
            this.database_name = _database_name;
        }

        public string DbName
        {
            get
            {
                return dbName;
            }
        }

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }

        public static ExcoODBC Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new ExcoODBC();
                }
                return instance;
            }
        }

        internal ExcoODBC() { }

        // be able to open and swith database connections
        public void Open(Database database)
        {
            // open db when designating with another arguments
            if (this.database != database)
            {
                this.database = database;
                switch (database)
                {
                    case Database.DECADE_MARKHAM:
                        connectionString = connection_str + ";Uid=" + user + ";Pwd=" + password + ";";
                        dbName = database_name;
                        break;
                    default:
                        throw new Exception("Unhandled database!");
                }

                if (null == connection)
                {
                    connection = new OdbcConnection(connectionString);
                }
                else
                {
                    connection.Close();
                    connection.ConnectionString = connectionString;
                }
                //connection.ConnectionTimeout = int.MaxValue;
                connection.Open();
                if (null == command)
                {
                    command = new OdbcCommand();
                }
                command.Connection = connection;
                command.CommandTimeout = 0;
            }
        }

        public OdbcDataReader RunQuery(string query, int depreciated_count=10)
        {
            try
            {
                // adjust database for testing 
                if (Database.PRODTEST == database)
                {
                    query = query.Replace("cmsdat", "prodtest");
                }
                command = new OdbcCommand();
                command.Connection = connection;
                command.CommandTimeout = 0;
                command.CommandText = query;
                return command.ExecuteReader();
            }
            catch (Exception exception)
            {
                if (depreciated_count > 0)
                {
                    return RunQuery(query, depreciated_count - 1);
                }
                else
                {
                    throw exception;
                }
                //throw exception;
            }
        }

        public int RunQueryWithoutReader(string query)
        {
            try
            {
                // adjust database for testing
                if (Database.PRODTEST == database)
                {
                    query = query.Replace("cmsdat", "prodtest");
                }
                command = new OdbcCommand();
                command.Connection = connection;
                command.CommandTimeout = 0;
                command.CommandText = query;
                return command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
