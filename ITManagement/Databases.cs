using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data.SqlClient;

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
    /// 

    public class ExcoODBC
    {
        internal OdbcConnection connection;
        internal OdbcCommand command;
        internal string connectionString;
        internal Database database = Database.DECADE_MARKHAM;
        internal static ExcoODBC instance;
        internal string dbName;
        private string user = "jamie";
        private string password = "jamie";

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

        // Robin externally added
        public void Set_Credentials(string user2, string password2, string databases2)
        {
            if (user2.Length > 0 && password2.Length > 0 && databases2.Length > 5)
            {
                this.user = user2;
                this.password = password2;
                this.database = Get_Database(databases2);
            }
        }

        public Database Get_Database(string db_Name)
        {
            if (db_Name == "CMSDAT") return Database.CMSDAT;
            else if (db_Name == "PRODTEST") return Database.PRODTEST;
            else if (db_Name == "DECADE_MARKHAM") return Database.DECADE_MARKHAM;
            else if (db_Name == "DECADE_MICHIGAN") return Database.DECADE_MICHIGAN;
            else if (db_Name == "DECADE_TEXAS") return Database.DECADE_TEXAS;
            else if (db_Name == "DECADE_COLOMBIA") return Database.DECADE_COLOMBIA;
            else return Database.NULL;
        }

        // be able to open and swith database connections
        public void Open()
        {

            // open db when designating with another arguments
            if (true)//this.database != database)
            {
                //this.database = database;
                switch (database)
                {
                    case Database.CMSDAT:
                        //user = "JXU";//user = "ZWANG";
                        //password = "qwpo555";//password = "ZWANG";
                        connectionString = "Driver={iSeries Access ODBC Driver};Name=cms1;System=10.0.0.35;Uid=" + user + ";Pwd=" + password + ";";
                        dbName = "cmsdat";
                        break;
                    case Database.PRODTEST:
                        //user = "JXU";//user = "ZWANG";
                        //password = "qwpo555";//password = "ZWANG";
                        connectionString = "Driver={iSeries Access ODBC Driver};Name=cms1;System=10.0.0.35;Uid=" + user + ";Pwd=" + password + ";";
                        dbName = "prodtest";
                        break;
                    case Database.DECADE_MARKHAM:
                        //user = "jamie";
                        //password = "jamie";
                        connectionString = "Driver={SQL Server};Server=10.0.0.8;Uid=" + user + ";Pwd=" + password + ";";
                        dbName = "dbo";
                        break;
                    case Database.DECADE_MICHIGAN:
                        //user = "jamie";
                        //password = "jamie";
                        connectionString = "Driver={SQL Server};Server=192.168.1.7;Uid=" + user + ";Pwd=" + password + ";";
                        dbName = "dbo";
                        break;
                    case Database.DECADE_TEXAS:
                        //user = "jamie";
                        //password = "jamie";
                        connectionString = "Driver={SQL Server};Server=192.168.12.7;Uid=" + user + ";Pwd=" + password + ";";
                        dbName = "dbo";
                        break;
                    case Database.DECADE_COLOMBIA:
                        //user = "jamie";
                        //password = "jamie";
                        connectionString = "Driver={SQL Server};Server=192.168.101.7;Uid=" + user + ";Pwd=" + password + ";";
                        dbName = "dbo";
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
                try
                {
                    connection.Open();
                }
                catch
                {
                }
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

    public class ExcoSQL
    {
        internal static ExcoSQL instance;

        public static ExcoSQL Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new ExcoSQL();
                }
                return instance;
            }
        }
    }
}
