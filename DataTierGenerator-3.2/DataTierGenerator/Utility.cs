using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Data.Common;
using System.Text;

namespace DataTierGenerator
{
    /// <summary>
    /// Provides utility functions for the data tier generator.
    /// </summary>
    public sealed class Utility
    {
        public static String DataBaseSys = "";
        public static String ConnectionString = "";
        private Utility() { }

        public static Util.Provider proveedor;

        /// <summary>
        /// Creates the specified sub-directory, if it doesn't exist.
        /// </summary>
        /// <param name="name">The name of the sub-directory to be created.</param>
        public static void CreateSubDirectory(string name)
        {
            if (Directory.Exists(name) == false)
            {
                Directory.CreateDirectory(name);
            }
        }

        /// <summary>
        /// Creates the specified sub-directory, if it doesn't exist.
        /// </summary>
        /// <param name="name">The name of the sub-directory to be created.</param>
        /// <param name="deleteIfExists">Indicates if the directory should be deleted if it exists.</param>
        public static void CreateSubDirectory(string name, bool deleteIfExists){
            if (Directory.Exists(name)){
                Directory.Delete(name, true);
            }
            Directory.CreateDirectory(name);
        }

        /// <summary>
        /// Retrieves the specified manifest resource stream from the executing assembly as a string.
        /// </summary>
        /// <param name="name">Name of the resource to retrieve.</param>
        /// <returns>The value of the specified manifest resource.</returns>
        public static string GetResource(string name)
        {
            using (StreamReader streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(name)))
            {
                return streamReader.ReadToEnd();
            }
        }

        /// <summary>
        /// Retrieves the specified manifest resource stream from the executing assembly as a string, replacing the specified old value with the specified new value.
        /// </summary>
        /// <param name="name">Name of the resource to retrieve.</param>
        /// <param name="oldValue">A string to be replaced.</param>
        /// <param name="newValue">A string to replace all occurrences of oldValue.</param>
        /// <returns>The value of the specified manifest resource, with all instances of oldValue replaced with newValue.</returns>
        public static string GetResource(string name, string oldValue, string newValue)
        {
            string returnValue = GetResource(name);
            return returnValue.Replace(oldValue, newValue);
        }

        public static string GetResource(string name, string oldValue1, string newValue1, string oldValue2, string newValue2, string oldValue3, string newValue3, string oldValue4, string newValue4)
        {
            string returnValue = GetResource(name);
            //returnValue.Replace(oldValue1, newValue1);
            return returnValue.Replace(oldValue2, newValue2).Replace(oldValue1, newValue1).Replace(oldValue3, newValue3).Replace(oldValue4, newValue4);
        }

        /// <summary>
        /// Returns the query that should be used for retrieving the list of tables for the specified database.
        /// </summary>
        /// <param name="databaseName">The database to be queried for.</param>
        /// <returns>The query that should be used for retrieving the list of tables for the specified database.</returns>
        public static string GetTableQuery(string databaseName, string param)
        {

            string strVal = string.Empty;
            switch (proveedor)
            {
                case Util.Provider.Access:
                    return GetResource("DataTierGenerator.TableQuery.sql", "#DatabaseName#", databaseName);
                case Util.Provider.SqlClient:
                    return GetResource("DataTierGenerator.TableQuery.sql", "#DatabaseName#", databaseName);
                case Util.Provider.Oracle:
                    strVal = GetResource("DataTierGenerator.TableQueryOracle.sql", "#OWNER#", databaseName);
                    break;
                case Util.Provider.Db2:
                    break;
                case Util.Provider.MySql:
                    break;
                default:
                    strVal = string.Empty;
                    break;
            }
            return strVal;
        }

        /// <summary>
        /// Returns the query that should be used for retrieving the list of tables for the specified database.
        /// </summary>
        /// <param name="databaseName">The database to be queried for.</param>
        /// <returns>The query that should be used for retrieving the list of tables for the specified database.</returns>
        public static string GetTableQuery(string databaseName)
        {
            string strVal = string.Empty;
            switch (proveedor)
            {
                case Util.Provider.Access:
                    strVal = GetResource("DataTierGenerator.TableQuery.sql", "#DatabaseName#", databaseName);
                    break;
                case Util.Provider.SqlClient:
                    strVal = GetResource("DataTierGenerator.TableQuery.sql", "#DatabaseName#", databaseName);
                    break;
                case Util.Provider.Oracle:
                    strVal = GetResource("DataTierGenerator.TableQueryOracle.sql", "#OWNER#", Util.strProp.Replace(".", ""));
                    break;
                case Util.Provider.Db2:
                    break;
                case Util.Provider.MySql:
                    strVal = GetResource("DataTierGenerator.TableQueryMySQL.sql", "#DatabaseName#", databaseName);
                    break;
                case Util.Provider.PostgreSQL:
                    strVal = GetResource("DataTierGenerator.TableQueryPostgreSQL.sql", "#DatabaseName#", databaseName);
                    break;
                default:
                    strVal = string.Empty;
                    break;
            }
            return strVal;
        }

        /// <summary>
        /// Returns the query that should be used for retrieving the list of columns for the specified table.
        /// </summary>
        /// <param name="databaseName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving the list of columns for the specified table.</returns>
        public static string GetColumnQuery(string tableName)
        {
            string strVal = string.Empty;
            switch (proveedor)
            {
                case Util.Provider.Access:
                    strVal = GetResource("DataTierGenerator.ColumnQuery.sql", "#TableName#", tableName);
                    break;
                case Util.Provider.SqlClient:
                    strVal = GetResource("DataTierGenerator.ColumnQuery.sql", "#TableName#", tableName);
                    break;
                case Util.Provider.Oracle:
                    strVal = GetResource("DataTierGenerator.ColumnQueryOracle.sql", "#TableName#", tableName);
                    //strVal = strVal.Replace("#OWNER#", Util.strProp.Replace(".", ""));
                    break;
                case Util.Provider.Db2:
                    strVal = "";
                    break;
                case Util.Provider.MySql:
                    strVal = GetResource("DataTierGenerator.ColumnQueryMySQL.sql", "#TableName#", tableName);
                    strVal = strVal + " AND INFORMATION_SCHEMA.COLUMNS.TABLE_SCHEMA = '" + DataBaseSys + "' ORDER BY INFORMATION_SCHEMA.COLUMNS.ORDINAL_POSITION";
                    break;
                case Util.Provider.PostgreSQL:
                    strVal = GetResource("DataTierGenerator.ColumnQueryPostgreSQL.sql", "#TableName#", tableName);
                    break;
                default:
                    break;
            }
            return strVal;
        }

        /// <summary>
        /// Retrieves the specified manifest resource stream from the executing assembly as a string, replacing the specified old value with the specified new value.
        /// </summary>
        /// <param name="name">Name of the resource to retrieve.</param>
        /// <param name="databaseName">The name of the database to be used.</param>
        /// <param name="grantLoginName">The name of the user to be used.</param>
        /// <returns>The queries that should be used to create the specified database login.</returns>
        public static string GetUserQueries(string databaseName, string grantLoginName)
        {
            string returnValue = GetResource("DataTierGenerator.User.sql");
            returnValue = returnValue.Replace("#DatabaseName#", databaseName);
            returnValue = returnValue.Replace("#UserName#", grantLoginName);
            return returnValue;
        }

        /// <summary>
        /// Retrieves the specified manifest resource stream from the executing assembly as a string, replacing the specified old value with the specified new value.
        /// </summary>
        /// <param name="name">Name of the resource to retrieve.</param>
        /// <param name="databaseName">The name of the database to be used.</param>
        /// <param name="grantLoginName">The name of the user to be used.</param>
        /// <returns>The queries that should be used to create the specified database login.</returns>
        public static string GetMySqlUserQueries(string databaseName, string grantLoginName)
        {
            string returnValue = GetResource("DataTierGenerator.MySqlUser.sql");
            returnValue = returnValue.Replace("#DatabaseName#", databaseName);
            returnValue = returnValue.Replace("#UserName#", grantLoginName);
            return returnValue;
        }

        /// <summary>
        /// Returns the contents of the Utilitis.Database class.
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetSqlClientUtilityClass(string targetNamespace)
        {
            return GetResource("DataTierGenerator.SqlHelper.txt", "#Namespace#", targetNamespace);
        }

        public static string GetDBMSConnectionClass(string Driver, string Protocolo, string Puerto, string targetNamespace)
        {
            //GetResource("DataTierGenerator.DBMSConection.txt", "#Driver#", Driver);
            return GetResource("DataTierGenerator.DBMSConection.txt", "#Driver#", Driver, "#Protocolo#", Protocolo, "#Puerto#", Puerto, "#Namespace#", targetNamespace);
        }

        public static string GetJDBCCommandTypeClass(string targetNamespace)
        {
            return GetResource("DataTierGenerator.CommandType.txt", "#Namespace#", targetNamespace);
        }

        public static string GetJDBCDirectionClass(string targetNamespace)
        {
            return GetResource("DataTierGenerator.Direction.txt", "#Namespace#", targetNamespace);
        }

        public static string GetJDBCParameterClass(string targetNamespace)
        {
            return GetResource("DataTierGenerator.Parameter.txt", "#Namespace#", targetNamespace);
        }

        public static string GetJDBCConnectionClass(string targetNamespace)
        {
            return GetResource("DataTierGenerator.Connection.txt", "#Namespace#", targetNamespace);
        }
        /// <summary>
        /// Returns the contents of the UtilDA class.
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetUtilDAClass(string targetNamespace)
        {
            return GetResource("DataTierGenerator.UtilDA.txt", "#Namespace#", targetNamespace);
        }
        /// <summary>
        /// Returns the contents of the GenericsBO class.
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetGenericsBOClass(string targetNamespace)
        {
            return GetResource("DataTierGenerator.GenericsBO.txt", "#Namespace#", targetNamespace);
        }
        /// <summary>
        /// Returns the contents of the GenericsDA class.
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetGenericsDAClass(string targetNamespace)
        {
            return GetResource("DataTierGenerator.GenericsDA.txt", "#Namespace#", targetNamespace);
        }

        /// <summary>
        /// Returns the contents of the UtilBO class.
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetUtilBOClass(string targetNamespace)
        {
            //return GetResource("DataTierGenerator.SqlClientUtility.txt", "#Namespace#", targetNamespace);
            return GetResource("DataTierGenerator.UtilBO.txt", "#Namespace#", targetNamespace);
        }
        /// <summary>
        /// Returns the contents of the UtilDA class.
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetUtilDAVBClass(string targetNamespace)
        {
            //return GetResource("DataTierGenerator.SqlClientUtility.txt", "#Namespace#", targetNamespace);
            return GetResource("DataTierGenerator.UtilDAVB.txt", "#Namespace#", targetNamespace);
        }

        /// <summary>
        /// Returns the contents of the UtilBO class.
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetUtilBOVBClass(string targetNamespace)
        {
            //return GetResource("DataTierGenerator.SqlClientUtility.txt", "#Namespace#", targetNamespace);
            return GetResource("DataTierGenerator.UtilBOVB.txt", "#Namespace#", targetNamespace);
        }
        /// <summary>
        /// Returns the contents of the Generics class.
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetGenericsBOVBClass(string targetNamespace)
        {
            return GetResource("DataTierGenerator.GenericsBOVB.txt", "#Namespace#", targetNamespace);
        }

        /// <summary>
        /// Returns the contents of the Generics class.
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetAuditoriaEOClass(string targetNamespace)
        {
            return GetResource("DataTierGenerator.AuditarEO.txt", "#Namespace#", targetNamespace);
        }

        /// <summary>
        /// Returns the contents of the Generics class.
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetAuditoriaEOVBClass(string targetNamespace)
        {
            return GetResource("DataTierGenerator.AuditarEOVB.txt", "#Namespace#", targetNamespace);
        }

        /// <summary>
        /// Returns the contents of the GenericsDA class.
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetGenericsDAVBClass(string targetNamespace)
        {
            //return GetResource("DataTierGenerator.SqlClientUtility.txt", "#Namespace#", targetNamespace);
            return GetResource("DataTierGenerator.GenericsDAVB.txt", "#Namespace#", targetNamespace);
        }

        /// <summary>
        /// Returns the contents of the Utilitis.Database class. VB
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetSqlClientUtilityClassVB(string targetNamespace)
        {
            return GetResource("DataTierGenerator.SqlHelperVB.txt", "#Namespace#", targetNamespace);
        }

        /// <summary>
        /// Returns the contents of the Utilitis.Database class.
        /// </summary>
        /// <param name="tableName">The table to be queried for.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        public static string GetMySqlClientUtilityClass(string targetNamespace)
        {
            return GetResource("DataTierGenerator.MySqlClientUtility.txt", "#Namespace#", targetNamespace);
        }

        /// <summary>
        /// Returns the query that should be used for retrieving the list of tables for the specified database.
        /// </summary>
        /// <param name="databaseName">The database to be queried for.</param>
        /// <returns>The query that should be used for retrieving the list of tables for the specified database.</returns>
        public static string Get(string databaseName)
        {
            return GetResource("DataTierGenerator.TableQuery.sql", "#DatabaseName#", databaseName);
        }

        public static string GetKeysOracleQuery(string nombretable)
        {
            string returnValue = GetResource("DataTierGenerator.SP_KEYS_ORACLE.sql");
            returnValue = returnValue.Replace("#OWNER#", Util.strProp.Replace(".", ""));
            returnValue = returnValue.Replace("#TableName#", nombretable);
            return returnValue;
        }

        public static string GetFKeysOracleQuery(string nombretable)
        {
            string returnValue = GetResource("DataTierGenerator.SP_FKEYS_ORACLE.sql");
            returnValue = returnValue.Replace("#OWNER#", Util.strProp.Replace(".", ""));
            returnValue = returnValue.Replace("#TableName#", nombretable);
            return returnValue;
        }

        /// <summary>
        /// Retrieves the foreign key information for the specified table.
        /// </summary>
        /// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
        /// <param name="tableName">Name of the table that foreign keys should be checked for.</param>
        /// <returns>DataReader containing the foreign key information for the specified table.</returns>
        public static DataTable GetForeignKeyList(DbProviderFactory dbProvider, DbConnection connection, string tableName)
        {

            DbParameter parameter;
            DataTable dataTable = new DataTable();
            IDataParameter[] parametros;
            switch (proveedor) { 
                case Util.Provider.MySql:
                    break;
                case Util.Provider.Oracle:
                    break;
                case Util.Provider.PostgreSQL:
                    PostgreSQLConnection pgsql = new PostgreSQLConnection();
                    pgsql.ConnectionString = ConnectionString;
                    parametros = new IDataParameter[0];
                    dataTable = pgsql.ExecuteDataTable(CommandType.Text, "SELECT a.attname as FKCOLUMN_NAME, ss.conname as FK_NAME FROM pg_attribute a, (SELECT r.oid AS roid, r.relname, nc.nspname AS nc_nspname,nr.nspname AS nr_nspname,c.oid AS coid, c.conname, c.contype, c.confkey, c.confrelid,_pg_expandarray(c.conkey) AS x FROM pg_namespace nr, pg_class r, pg_namespace nc,pg_constraint c WHERE nr.oid = r.relnamespace AND r.oid = c.conrelid AND nc.oid = c.connamespace AND c.contype IN ('u', 'f') AND r.relkind = 'r' AND (NOT pg_is_other_temp_schema(nr.oid)) AND (pg_has_role(r.relowner, 'USAGE') OR has_table_privilege(r.oid, 'SELECT') OR has_table_privilege(r.oid, 'INSERT') OR has_table_privilege(r.oid, 'UPDATE') OR has_table_privilege(r.oid, 'REFERENCES')) ) AS ss WHERE ss.roid = a.attrelid AND relname='" + tableName + "' AND a.attnum = (ss.x).x AND NOT a.attisdropped ", parametros);
                    pgsql.Dispose();
                    break;
                case Util.Provider.SqlClient:
                    SQLServerConnection sqlserver = new SQLServerConnection();
                    parametros = new IDataParameter[6];
                    IDataParameter pktable_name, pktable_owner, pktable_qualifier, fktable_name, fktable_owner, fktable_qualifier;
                    sqlserver.ConnectionString = ConnectionString;
                    pktable_name = new SqlParameter("@pktable_name", SqlDbType.VarChar, 100);
                    pktable_owner = new SqlParameter("@pktable_owner", SqlDbType.VarChar, 100);
                    pktable_qualifier = new SqlParameter("@pktable_qualifier", SqlDbType.VarChar, 100);
                    fktable_name = new SqlParameter("@fktable_name", SqlDbType.VarChar, 100);
                    fktable_owner = new SqlParameter("@fktable_owner", SqlDbType.VarChar, 100);
                    fktable_qualifier = new SqlParameter("@fktable_qualifier", SqlDbType.VarChar, 100);

                    pktable_name.Value = DBNull.Value;
                    pktable_owner.Value = DBNull.Value;
                    pktable_qualifier.Value = DBNull.Value;
                    fktable_name.Value = tableName;
                    fktable_owner.Value = DBNull.Value;
                    fktable_qualifier.Value = DBNull.Value;
                    parametros[0] = pktable_name;
                    parametros[1] = pktable_owner;
                    parametros[2] = pktable_qualifier;
                    parametros[3] = fktable_name;
                    parametros[4] = fktable_owner;
                    parametros[5] = fktable_qualifier;
                    dataTable = sqlserver.ExecuteDataTable(CommandType.StoredProcedure, "sp_fkeys", parametros);
                    sqlserver.Dispose();
                    break;
                case Util.Provider.Db2:
                    break;
            }
            /*if (connection.State == ConnectionState.Closed)
                connection.Open();
            DbCommand command = connection.CreateCommand();

            if (proveedor == Util.Provider.Oracle)
            {
                command.CommandType = CommandType.Text;
                command.CommandText = GetFKeysOracleQuery(tableName);
            }
            else if(proveedor == Util.Provider.Access)
            {

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_fkeys";

                parameter = CreaParametro(dbProvider, "@pktable_name", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
                parameter = CreaParametro(dbProvider, "@pktable_owner", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
                parameter = CreaParametro(dbProvider, "@pktable_qualifier", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
                parameter = CreaParametro(dbProvider, "@fktable_name", tableName, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
                parameter = CreaParametro(dbProvider, "@fktable_owner", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
                parameter = CreaParametro(dbProvider, "@fktable_qualifier", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
            }
            else if (proveedor == Util.Provider.SqlClient)
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_fkeys";

                parameter = CreaParametro(dbProvider, "@pktable_name", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
                parameter = CreaParametro(dbProvider, "@pktable_owner", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
                parameter = CreaParametro(dbProvider, "@pktable_qualifier", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
                parameter = CreaParametro(dbProvider, "@fktable_name", tableName, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
                parameter = CreaParametro(dbProvider, "@fktable_owner", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
                parameter = CreaParametro(dbProvider, "@fktable_qualifier", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
            }
            else if (proveedor == Util.Provider.MySql)
            {
                return dataTable;
            }
            DbDataAdapter dataAdapter = dbProvider.CreateDataAdapter();
            dataAdapter.SelectCommand = command;
            
            dataAdapter.Fill(dataTable);*/

            return dataTable;

        }

        /// <summary>
        /// Retrieves the primary key information for the specified table.
        /// </summary>
        /// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
        /// <param name="tableName">Name of the table that primary keys should be checked for.</param>
        /// <returns>DataReader containing the primary key information for the specified table.</returns>
        public static DataTable GetPrimaryKeyList(DbProviderFactory dbProvider, DbConnection connection, string tableName)
        {
            DbParameter parameter;
            DataTable dataTable = new DataTable();
            IDataParameter[] parametros;
            switch (proveedor) { 
                case Util.Provider.MySql:
                    MySQLConnection mysql = new MySQLConnection();
                    mysql.ConnectionString = ConnectionString;
                    parametros = new IDataParameter[0];
                    dataTable = mysql.ExecuteDataTable(CommandType.Text, "SELECT INFORMATION_SCHEMA.COLUMNS.* FROM INFORMATION_SCHEMA.COLUMNS WHERE INFORMATION_SCHEMA.COLUMNS.TABLE_SCHEMA = '" + DataBaseSys + "' AND INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + tableName + "' and INFORMATION_SCHEMA.COLUMNS.column_key = 'pri'", parametros);
                    mysql.Dispose();
                    break;
                case Util.Provider.Oracle:
                    OracleConnection orcl = new OracleConnection();
                    orcl.ConnectionString = ConnectionString;
                    parametros = new IDataParameter[0];
                    dataTable = orcl.ExecuteDataTable(CommandType.Text, "select  column_name, table_name, position, owner from user_cons_columns where table_name='" + tableName + "' and position is not null and constraint_name like 'SYS%'", parametros);
                    orcl.Dispose();
                    break;
                case Util.Provider.PostgreSQL:
                    PostgreSQLConnection pgsql = new PostgreSQLConnection();
                    pgsql.ConnectionString = ConnectionString;
                    parametros = new IDataParameter[0];
                    dataTable = pgsql.ExecuteDataTable(CommandType.Text, "SELECT  at.attname as column_name FROM pg_namespace nr, pg_class r, pg_attribute at, pg_namespace nc, pg_constraint c WHERE nr.oid = r.relnamespace AND r.oid = at.attrelid AND nc.oid = c.connamespace AND r.relname='" + tableName + "' AND (CASE WHEN c.contype = 'f' THEN r.oid = c.confrelid AND at.attnum = ANY (c.confkey) ELSE r.oid = c.conrelid AND at.attnum = ANY (c.conkey) END) AND NOT at.attisdropped AND c.contype IN ('p') AND r.relkind = 'r' ", parametros);
                    pgsql.Dispose();
                    break;
                case Util.Provider.SqlClient:
                    SQLServerConnection sqlserver = new SQLServerConnection();
                    IDataParameter table_name, table_owner, table_qualifier;
                    parametros = new IDataParameter[3];
                    sqlserver.ConnectionString = ConnectionString;
                    table_name = new SqlParameter("@table_name", SqlDbType.VarChar, 100);
                    table_owner = new SqlParameter("@table_owner", SqlDbType.VarChar, 100);
                    table_qualifier = new SqlParameter("@table_qualifier", SqlDbType.VarChar, 100);
                    table_name.Value = tableName;
                    table_owner.Value = DBNull.Value;
                    table_qualifier.Value = DBNull.Value;
                    parametros[0] = table_name;
                    parametros[1] = table_owner;
                    parametros[2] = table_qualifier;
                    dataTable = sqlserver.ExecuteDataTable(CommandType.StoredProcedure, "sp_pkeys", parametros);
                    sqlserver.Dispose();
                    break;
            }
            /*if (connection.State == ConnectionState.Closed)
                connection.Open();
            DbCommand command = connection.CreateCommand();
            if (proveedor == Util.Provider.Oracle)
            {
                command.CommandText = GetKeysOracleQuery(tableName);
                command.CommandType = CommandType.Text;
            }
            else if (proveedor== Util.Provider.Access)
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_pkeys";

                parameter = CreaParametro(dbProvider, "@table_name", tableName, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);

                parameter = CreaParametro(dbProvider, "@table_owner", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);

                parameter = CreaParametro(dbProvider, "@table_qualifier", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
            }
            else if (proveedor == Util.Provider.SqlClient) 
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_pkeys";

                parameter = CreaParametro(dbProvider, "@table_name", tableName, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);

                parameter = CreaParametro(dbProvider, "@table_owner", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);

                parameter = CreaParametro(dbProvider, "@table_qualifier", DBNull.Value, ParameterDirection.Input, DbType.String, 128);
                command.Parameters.Add(parameter);
            }
            else if (proveedor == Util.Provider.MySql)
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT INFORMATION_SCHEMA.COLUMNS.* FROM INFORMATION_SCHEMA.COLUMNS WHERE INFORMATION_SCHEMA.COLUMNS.TABLE_SCHEMA = '" + DataBaseSys + "' AND INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + tableName + "' and INFORMATION_SCHEMA.COLUMNS.column_key = 'pri'";
                
            }
            else if (proveedor == Util.Provider.PostgreSQL)
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT  at.attname FROM pg_namespace nr, pg_class r, pg_attribute at, pg_namespace nc, pg_constraint c WHERE nr.oid = r.relnamespace AND r.oid = at.attrelid AND nc.oid = c.connamespace AND r.relname='" + tableName + "' AND (CASE WHEN c.contype = 'f' THEN r.oid = c.confrelid AND at.attnum = ANY (c.confkey) ELSE r.oid = c.conrelid AND at.attnum = ANY (c.conkey) END) AND NOT at.attisdropped AND c.contype IN ('p') AND r.relkind = 'r'  ";

            }
            DbDataAdapter dataAdapter = dbProvider.CreateDataAdapter();
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);*/

            return dataTable;

        }

        public static DbParameter CreaParametro(DbProviderFactory dbProvider, string sPrm, object oVle, ParameterDirection direct, DbType type, Int32 Size)
        {
            DbParameter dbPrm = dbProvider.CreateParameter();
            dbPrm.ParameterName = sPrm;
            dbPrm.Direction = direct;
            dbPrm.DbType = type;
            dbPrm.Value = oVle;
            if (Size != 0)
            {
                dbPrm.Size = Size;
            }
            return dbPrm;
        }

        /// <summary>
        /// Creates a string containing the parameter declaration for a stored procedure based on the parameters passed in.
        /// </summary>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <param name="checkForOutputParameter">Indicates if the created parameter should be checked to see if it should be created as an output parameter.</param>
        /// <returns>String containing parameter information of the specified column for a stored procedure.</returns>
        public static string CreateParameterString(Column column, bool checkForOutputParameter,int maxsize){
            string parameter;
            char k = ' ';
            switch (column.Type.ToLower()){
                case "binary":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "bigint":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "bit":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "char":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "datetime":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "date":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "decimal":
                    if (column.Scale.Length == 0)
                        parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + ")";
                    else
                        parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + ", " + column.Scale + ")";
                    break;
                case "float":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + ")";
                    break;
                case "image":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "int":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "money":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "nchar":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "ntext":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "nvarchar":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "numeric":
                    if (column.Scale.Length == 0)
                        parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + ")";
                    else
                        parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + ", " + column.Scale + ")";
                    break;
                case "real":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "smalldatetime":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "smallint":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "smallmoney":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "sql_variant":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "sysname":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "text":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "timestamp":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "tinyint":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "varbinary":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "varchar":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "uniqueidentifier":
                    parameter = "@" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                default:  // Unknow data type
                    throw (new Exception("CreateParameterString: Invalid SQL Server data type specified: " + column.Type));
            }

            // Return the new parameter string
            if (checkForOutputParameter && (column.IsIdentity || column.IsRowGuidCol)){
                return parameter + " OUTPUT";
            }else{
                return parameter;
            }
        }

        public static string CreateParameterPostgreSQLString(Column column, bool checkForOutputParameter, int maxsize)
        {
            string parameter;
            char k = ' ';
            switch (column.Type.ToLower())
            {
                case "bit":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "tinyint":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "boolean":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "smallint":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "mediumint":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "int":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "integer":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "bigint":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "float":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + ")";
                    break;
                case "double":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "decimal":
                    if (column.Scale.Length == 0)
                        parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + ")";
                    else
                        parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + "," + column.Scale + ")";
                    break;
                case "numeric":
                    if (column.Scale.Length == 0)
                        parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + ")";
                    else
                        parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + "," + column.Scale + ")";
                    break;
                case "date":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "datetime":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "timestamp":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "timestamp without time zone":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "timestamp with time zone":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "time":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "char":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "character":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "character varying":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "varchar":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "binary":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "varbinary":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "blob":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "mediumblob":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "tinyblob":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "longblob":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "text":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "tinytext":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "longtext":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "mediumtext":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                default:  // Unknow data type
                    throw (new Exception("CreateParameterPostgreSQLString - Invalid PostgreSQL data type specified: " + column.Type));
            }

            // Return the new parameter string
            if (checkForOutputParameter && (column.IsIdentity || column.IsRowGuidCol))
            {
                return parameter + " OUT";
            }
            else
            {
                return parameter;
            }
        }
        public static string CreateParameterMySQLString(Column column, bool checkForOutputParameter, int maxsize)
        {
            string parameter;
            char k=' ';
            switch (column.Type.ToLower())
            {
                case "bit":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "tinyint":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "boolean":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "smallint":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "mediumint":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "int":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "integer":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "bigint":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "float":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + ")";
                    break;
                case "double":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "decimal":
                    if (column.Scale.Length == 0)
                        parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + ")";
                    else
                        parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Precision + "," + column.Scale + ")";
                    break;
                case "date":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "datetime":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "timestamp":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "time":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "char":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "varchar":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "binary":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "varbinary":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type + "(" + column.Length + ")";
                    break;
                case "blob":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "mediumblob":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "tinyblob":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "longblob":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "text":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "tinytext":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "longtext":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                case "mediumtext":
                    parameter = "p" + column.Name.PadRight(maxsize, k) + " " + column.Type;
                    break;
                default:  // Unknow data type
                    throw (new Exception("Invalid MySQL data type specified: " + column.Type));
            }

            // Return the new parameter string
            if (checkForOutputParameter && (column.IsIdentity || column.IsRowGuidCol))
            {
                return parameter + " OUT";
            }
            else
            {
                return parameter;
            }
        }

        public static string CreaOracleParametroStore(Column column, bool checkForOutputParameter, int maxsize)
        {
            string parameter="";
            char k = ' ';
            switch (column.Type.ToLower())
            {
                case "number":
                    if (!column.Precision.Equals("")) {
                        if (Double.Parse(column.Precision) > 0)
                        {
                            parameter = "p" + column.Name.ToUpper().PadRight(maxsize, k) + " IN " + column.Type.ToUpper() + "";
                        }
                    }
                    else {
                        parameter = "p" + column.Name.ToUpper().PadRight(maxsize, k) + " IN " + column.Type.ToUpper();
                    }
                    break;
                case "char":
                    parameter = "p" + column.Name.ToUpper().PadRight(maxsize, k) + " IN " + column.Type.ToUpper() + ""; ;
                    break;
                case "varchar2":
                    //parameter = "@" + column.Name + " " + column.Type + "(" + column.Length + ")";
                    parameter = "p" + column.Name.ToUpper().PadRight(maxsize, k) + " IN " + column.Type.ToUpper() + ""; 
                    break;
                case "date":
                    parameter = "p" + column.Name.ToUpper().PadRight(maxsize, k) + " IN " + column.Type.ToUpper(); ;
                    break;
                default:  // Unknow data type
                    throw (new Exception("CreaOracleParametroStore: Invalid ORACLE Server data type specified: " + column.Type));
            }

            // Return the new parameter string
            if (checkForOutputParameter && (column.IsIdentity || column.IsRowGuidCol))
            {
                return parameter + " OUTPUT";
            }
            else
            {
                return parameter;
            }
        }



        /// <summary>
        /// Creates a string containing the parameter declaration for a stored procedure based on the parameters passed in.
        /// </summary>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <param name="checkForOutputParameter">Indicates if the created parameter should be checked to see if it should be created as an output parameter.</param>
        /// <returns>String containing parameter information of the specified column for a stored procedure.</returns>
        public static string CreateOracleParameterString(Column column, bool checkForOutputParameter)
        {
            string parameter;

            switch (column.Type.ToLower())
            {
                case "binary":
                    parameter = "pi_" + column.Name + " IN " + "NUMBER";
                    break;
                case "bigint":
                    parameter = "pi_" + column.Name + " IN " + "NUMBER";
                    break;
                case "bit":
                    parameter = "pi_" + column.Name + " IN " + "CHAR";
                    break;
                case "char":
                    parameter = "pi_" + column.Name + " IN " + "CHAR";
                    break;
                case "datetime":
                    parameter = "pi_" + column.Name + " IN " + "DATE";
                    break;
                case "decimal":
                    //if (column.Scale.Length == 0)
                    parameter = "pi_" + column.Name + " IN " + "NUMBER";
                    //else
                    //    parameter = "@" + column.Name + " " + column.Type + "(" + column.Precision + ", " + column.Scale + ")";
                    break;
                case "float":
                    parameter = "pi_" + column.Name + " IN " + "NUMBER";
                    break;
                case "image":
                    parameter = "pi_" + column.Name + " IN " + "VARCHAR2";
                    break;
                case "int":
                    parameter = "pi_" + column.Name + " IN " + "NUMBER";
                    break;
                case "money":
                    parameter = "pi_" + column.Name + " IN " + "NUMBER";
                    break;
                case "nchar":
                    parameter = "pi_" + column.Name + " IN " + "VARCHAR2";
                    break;
                case "ntext":
                    parameter = "pi_" + column.Name + " IN " + "VARCHAR2";
                    break;
                case "nvarchar":
                    parameter = "pi_" + column.Name + " IN " + "VARCHAR2";
                    break;
                case "numeric":
                    //if (column.Scale.Length == 0)
                    parameter = "pi_" + column.Name + " IN " + "NUMBER";
                    //else
                    //    parameter = "@" + column.Name + " " + column.Type + "(" + column.Precision + ", " + column.Scale + ")";
                    break;
                case "real":
                    parameter = "pi_" + column.Name + " IN " + "NUMBER";
                    break;
                case "smalldatetime":
                    parameter = "pi_" + column.Name + " IN " + "DATE";
                    break;
                case "smallint":
                    parameter = "pi_" + column.Name + " IN " + "NUMBER";
                    break;
                case "smallmoney":
                    parameter = "pi_" + column.Name + " IN " + "NUMBER";
                    break;
                case "sql_variant":
                    parameter = "pi_" + column.Name + " IN " + "VARCHAR2";
                    break;
                case "sysname":
                    parameter = "pi_" + column.Name + " IN " + "VARCHAR2";
                    break;
                case "text":
                    parameter = "pi_" + column.Name + " IN " + "VARCHAR2";
                    break;
                case "timestamp":
                    parameter = "pi_" + column.Name + " IN " + "DATE";
                    break;
                case "tinyint":
                    parameter = "pi_" + column.Name + " IN " + "NUMBER";
                    break;
                case "varbinary":
                    parameter = "pi_" + column.Name + " IN " + "VARCHAR2";
                    break;
                case "varchar":
                    parameter = "pi_" + column.Name + " IN " + "VARCHAR2";
                    break;
                case "uniqueidentifier":
                    parameter = "pi_" + column.Name + " IN " + "NUMBER";
                    break;
                default:  // Unknow data type
                    throw (new Exception("Invalid ORACLE Server data type specified: " + column.Type));
            }

            // Return the new parameter string
            if (checkForOutputParameter && (column.IsIdentity || column.IsRowGuidCol))
            {
                return parameter + " OUTPUT";
            }
            else
            {
                return parameter;
            }
        }

        /// <summary>
        /// Creates a string containing the parameter declaration for a stored procedure based on the parameters passed in.
        /// </summary>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <param name="checkForOutputParameter">Indicates if the created parameter should be checked to see if it should be created as an output parameter.</param>
        /// <returns>String containing parameter information of the specified column for a stored procedure.</returns>
        public static string CreateMySqlParameterString(Column column, bool checkForOutputParameter)
        {
            string parameter;

            switch (column.Type.ToLower())
            {
                case "binary":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower()) + "(" + column.Length + ")";
                    break;
                case "bigint":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "bit":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "char":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower()) + "(" + column.Length + ")";
                    break;
                case "datetime":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "decimal":
                    if (column.Scale.Length == 0)
                        parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower()) + "(" + column.Precision + ")";
                    else
                        parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower()) + "(" + column.Precision + ", " + column.Scale + ")";
                    break;
                case "float":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower()) + "(" + column.Precision + ")";
                    break;
                case "image":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "int":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "money":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "nchar":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower()) + "(" + column.Length + ")";
                    break;
                case "ntext":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "nvarchar":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower()) + "(" + column.Length + ")";
                    break;
                case "numeric":
                    if (column.Scale.Length == 0)
                        parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower()) + "(" + column.Precision + ")";
                    else
                        parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower()) + "(" + column.Precision + ", " + column.Scale + ")";
                    break;
                case "real":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "smalldatetime":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "smallint":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "smallmoney":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "sql_variant":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "sysname":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "text":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "timestamp":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "tinyint":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                case "varbinary":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower()) + "(" + column.Length + ")";
                    break;
                case "varchar":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower()) + "(" + column.Length + ")";
                    break;
                case "uniqueidentifier":
                    parameter = "_" + column.Name + " " + GetMySqlDbType(column.Type.ToLower());
                    break;
                default:  // Unknow data type
                    throw (new Exception("Invalid MySQL data type specified: " + column.Type));
            }

            // Return the new parameter string
            if (checkForOutputParameter && (column.IsIdentity || column.IsRowGuidCol))
            {
                return "OUT " + parameter;
            }
            else
            {
                return "IN " + parameter;
            }
        }

        /// <summary>
        /// Creates a string for a method parameter representing the specified column.
        /// </summary>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <returns>String containing parameter information of the specified column for a method call.</returns>
        public static string CreateMethodParameter(Column column)
        {
            string parameter;
            string columnName;

            // Format the column name
            columnName = "_" + column.Alias;

            switch (column.Type.ToLower())
            {
                case "binary":
                    parameter = "byte[] " + columnName;
                    break;
                case "bigint":
                    parameter = "" + (column.IsNullable ? "Nullable<Int64> " : "Int64 ") + columnName;
                    break;
                case "bit":
                    parameter = "bool" + (column.IsNullable ? " " : " ") + columnName;
                    break;
                case "char":
                    parameter = "string" + " " + columnName;
                    break;
                case "character":
                    parameter = "string" + " " + columnName;
                    break;
                case "character varying":
                    parameter = "string" + " " + columnName;
                    break;
                case "datetime":
                    parameter = "DateTime" + (column.IsNullable ? " " : " ") + columnName;
                    break;
                case "date":
                    parameter = "DateTime" + (column.IsNullable ? " " : " ") + columnName;
                    break;
                case "decimal":
                    parameter = "" + (column.IsNullable ? "Nullable<decimal> " : "decimal ") + columnName;
                    break;
                case "float":
                    parameter = "" + (column.IsNullable ? "Nullable<double> " : "double ") + columnName;
                    break;
                case "image":
                    parameter = "byte[] " + columnName;
                    break;
                case "int":
                    parameter = "" + (column.IsNullable ? "Nullable<int> " : "int ") + columnName;
                    break;
                case "integer":
                    parameter = "" + (column.IsNullable ? "Nullable<int> " : "int ") + columnName;
                    break;
                case "money":
                    parameter = "" + (column.IsNullable ? "Nullable<decimal> " : "decimal ") + columnName;
                    break;
                case "nchar":
                    parameter = "string " + columnName;
                    break;
                case "ntext":
                    parameter = "string " + columnName;
                    break;
                case "nvarchar":
                    parameter = "string " + columnName;
                    break;
                case "numeric":
                    parameter = "" + (column.IsNullable ? "Nullable<decimal> " : "decimal ") + columnName;
                    break;
                case "real":
                    parameter = "" + (column.IsNullable ? "Nullable<float> " : "float ") + columnName;
                    break;
                case "smalldatetime":
                    parameter = "DateTime" + (column.IsNullable ? " " : " ") + columnName;
                    break;
                case "smallint":
                    parameter = "" + (column.IsNullable ? "Nullable<short> " : "short ") + columnName;
                    break;
                case "smallmoney":
                    parameter = "" + (column.IsNullable ? "Nullable<decimal> " : "decimal ") + columnName;
                    break;
                case "sql_variant":
                    parameter = "object " + columnName;
                    break;
                case "sysname":
                    parameter = "string " + columnName;
                    break;
                case "text":
                    parameter = "string " + columnName;
                    break;
                case "timestamp":
                    parameter = "DateTime" + (column.IsNullable ? " " : " ") + columnName;
                    break;
                case "time":
                    parameter = "DateTime" + (column.IsNullable ? " " : " ") + columnName;
                    break;
                case "timestamp without time zone":
                    parameter = "DateTime" + (column.IsNullable ? " " : " ") + columnName;
                    break;
                case "timestamp with time zone":
                    parameter = "DateTime" + (column.IsNullable ? " " : " ") + columnName;
                    break;
                case "tinyint":
                    parameter = "byte" + (column.IsNullable ? " " : " ") + columnName;
                    break;
                case "varbinary":
                    parameter = "byte[] " + columnName;
                    break;
                case "varchar":
                    parameter = "string " + columnName;
                    break;
                case "uniqueidentifier":
                    parameter = "Guid " + columnName;
                    break;
                case "longtext":
                    parameter = "String " + columnName;
                    break;
                case "blob":
                    parameter = "byte[] " + columnName;
                    break;
                case "tinyblob":
                    parameter = "byte[] " + columnName;
                    break;
                case "mediumblob":
                    parameter = "byte[] " + columnName;
                    break;
                case "longblob":
                    parameter = "byte[] " + columnName;
                    break;
                case "tinytext":
                    parameter = "String " + columnName;
                    break;
                case "mediumtext":
                    parameter = "String " + columnName;
                    break;
                default:  // Unknow data type
                    throw (new Exception("Invalid SQL Server data type specified: " + column.Type));
            }

            // Return the new parameter string
            return parameter;
        }

        public static string CreateMethodJavaTypeParameter(Column column)
        {
            string parameter;
            string columnName = "Types.";
            switch (column.Type.ToLower())
            {
                case "binary":
                    parameter = columnName + "LONGVARBINARY";
                    break;
                case "bigint":
                    parameter = columnName + "BIGINT";
                    break;
                case "bit":
                    parameter = columnName + "BIT";
                    break;
                case "char":
                    parameter = columnName + "CHAR";
                    break;
                case "character":
                    parameter = columnName + "CHAR";
                    break;
                case "character varying":
                    parameter = columnName + "VARCHAR";
                    break;
                case "date":
                    parameter = columnName + "DATE";
                    break;
                case "datetime":
                    parameter = columnName + "DATE";
                    break;
                case "decimal":
                    parameter = columnName + "DECIMAL";
                    break;
                case "float":
                    parameter = columnName + "FLOAT";
                    break;
                case "image":
                    parameter = columnName + "LONGVARBINARY";
                    break;
                case "int":
                    parameter = columnName + "INTEGER";
                    break;
                case "integer":
                    parameter = columnName + "INTEGER";
                    break;
                case "money":
                    parameter = columnName + "DECIMAL";
                    break;
                case "nchar":
                    parameter = columnName + "VARCHAR";
                    break;
                case "ntext":
                    parameter = columnName + "VARCHAR";
                    break;
                case "nvarchar":
                    parameter = columnName + "VARCHAR";
                    break;
                case "numeric":
                    parameter = columnName + "NUMERIC";
                    break;
                case "number":
                    if (!column.Precision.Equals(""))
                    {
                        if (Double.Parse(column.Precision) > 0)
                        {
                            parameter = columnName + "DECIMAL";
                        }
                        else {
                            parameter = columnName + "INTEGER";
                        }
                    }
                    else {
                        parameter = columnName + "INTEGER";
                    }
                    
                    break;
                case "real":
                    parameter = columnName + "FLOAT";
                    break;
                case "smalldatetime":
                    parameter = columnName + "DATE";
                    break;
                case "smallint":
                    parameter = columnName + "SMALLINT";
                    break;
                case "smallmoney":
                    parameter = columnName + "DECIMAL";
                    break;
                case "sql_variant":
                    parameter = columnName + "JAVA_OBJECT";
                    break;
                case "sysname":
                    parameter = columnName + "VARCHAR";
                    break;
                case "text":
                    parameter = columnName + "VARCHAR";
                    break;
                case "timestamp":
                    parameter = columnName + "TIMESTAMP";
                    break;
                case "timestamp without time zone":
                    parameter = columnName + "TIMESTAMP";
                    break;
                case "timestamp with time zone":
                    parameter = columnName + "TIMESTAMP";
                    break;
                case "time":
                    parameter = columnName + "TIMESTAMP";
                    break;
                case "tinyint":
                    parameter = columnName + "TINYINT";
                    break;
                case "varbinary":
                    parameter = columnName + "VARBINARY";
                    break;
                case "varchar":
                    parameter = columnName + "VARCHAR";
                    break;
                case "varchar2":
                    parameter = columnName + "VARCHAR";
                    break;
                case "uniqueidentifier":
                    parameter = columnName + "JAVA_OBJECT";
                    break;
                case "longtext":
                    parameter = columnName + "VARCHAR";
                    break;
                case "blob":
                    parameter = columnName + "VARBINARY";
                    break;
                case "tinyblob":
                    parameter = columnName + "VARBINARY";
                    break;
                case "mediumblob":
                    parameter = columnName + "VARBINARY";
                    break;
                case "longblob":
                    parameter = columnName + "VARBINARY";
                    break;
                case "tinytext":
                    parameter = columnName + "VARCHAR";
                    break;
                case "mediumtext":
                    parameter = columnName + "VARCHAR";
                    break;
                default:  // Unknow data type
                    throw (new Exception("Invalid SQL Java data type specified: " + column.Type));
            }
            return parameter;
        }

        public static string CreateMethodJavaDataParameter(Column column)
        {
            string parameter;
            string columnName = "Resultados.";
            switch (column.Type.ToLower())
            {
                case "binary":
                    parameter = columnName + "getBytes";
                    break;
                case "bigint":
                    parameter = columnName + "getLong";
                    break;
                case "bit":
                    parameter = columnName + "getBoolean";
                    break;
                case "char":
                    parameter = columnName + "getString";
                    break;
                case "character":
                    parameter = columnName + "getString";
                    break;
                case "character varying":
                    parameter = columnName + "getString";
                    break;
                case "date":
                    parameter = columnName + "getDate";
                    break;
                case "datetime":
                    parameter = columnName + "getDate";
                    break;
                case "decimal":
                    parameter = columnName + "getDouble";
                    break;
                case "float":
                    parameter = columnName + "getFloat";
                    break;
                case "image":
                    parameter = columnName + "getBytes";
                    break;
                case "int":
                    parameter = columnName + "getInt";
                    break;
                case "integer":
                    parameter = columnName + "getInt";
                    break;
                case "money":
                    parameter = columnName + "getDouble";
                    break;
                case "nchar":
                    parameter = columnName + "getString";
                    break;
                case "ntext":
                    parameter = columnName + "getString";
                    break;
                case "nvarchar":
                    parameter = columnName + "getString";
                    break;
                case "numeric":
                    parameter = columnName + "getDouble";
                    break;
                case "number":
                    if (!column.Precision.Equals(""))
                    {
                        if (Double.Parse(column.Precision) > 0)
                        {
                            parameter = columnName + "getDouble";
                        }
                        else {
                            parameter = columnName + "getInt";
                        }
                    }
                    else {
                        parameter = columnName + "getInt";
                    }
                    
                    break;
                case "real":
                    parameter = columnName + "getFloat";
                    break;
                case "smalldatetime":
                    parameter = columnName + "getDate";
                    break;
                case "smallint":
                    parameter = columnName + "getInt";
                    break;
                case "smallmoney":
                    parameter = columnName + "getDouble";
                    break;
                case "sql_variant":
                    parameter = columnName + "getObject";
                    break;
                case "sysname":
                    parameter = columnName + "getString";
                    break;
                case "text":
                    parameter = columnName + "getString";
                    break;
                case "timestamp":
                    parameter = columnName + "getTimestamp";
                    break;
                case "time":
                    parameter = columnName + "getTimestamp";
                    break;
                case "timestamp without time zone":
                    parameter = columnName + "getTimestamp";
                    break;
                case "timestamp with time zone":
                    parameter = columnName + "getTimestamp";
                    break;
                case "tinyint":
                    parameter = columnName + "getInt";
                    break;
                case "varbinary":
                    parameter = columnName + "getBytes";
                    break;
                case "varchar":
                    parameter = columnName + "getString";
                    break;
                case "varchar2":
                    parameter = columnName + "getString";
                    break;
                case "uniqueidentifier":
                    parameter = columnName + "getObject";
                    break;
                default:  // Unknow data type
                    throw (new Exception("Invalid SQL Server data type specified: " + column.Type));
            }
            return parameter;
        }

        public static string InicializarJavaParameter(Column column)
        {
            string parameter = "";
            string columnName = "";
            
            // Format the column name
            columnName = "" + column.Alias;

            switch (column.Type.ToLower())
            {
                case "binary":
                    parameter = columnName + " = null;";
                    break;
                case "bigint":
                    parameter = columnName + " = 0;";
                    break;
                case "bit":
                    parameter = columnName + " = false;";
                    break;
                case "char":
                    parameter = columnName + " = \"\";";
                    break;
                case "character":
                    parameter = columnName + " = \"\";";
                    break;
                case "character varying":
                    parameter = columnName + " = \"\";";
                    break;
                case "date":
                    parameter = columnName + " = null; ";
                    break;
                case "datetime":
                    parameter = columnName + " = null; ";
                    break;
                case "decimal":
                    parameter = columnName + " = 0.0;";
                    break;
                case "float":
                    parameter = columnName + " = 0.0;";
                    break;
                case "image":
                    parameter = columnName + " = null;";
                    break;
                case "int":
                    parameter = columnName + " = 0;";
                    break;
                case "integer":
                    parameter = columnName + " = 0;";
                    break;
                case "money":
                    parameter = columnName + " = 0.0;";
                    break;
                case "nchar":
                    parameter = columnName + " = \"\";";
                    break;
                case "ntext":
                    parameter = columnName + " = \"\";";
                    break;
                case "nvarchar":
                    parameter = columnName + " = \"\";";
                    break;
                case "numeric":
                    parameter = columnName + " = 0.0;";
                    break;
                case "number":
                    if (!column.Precision.Equals(""))
                    {
                        if (Double.Parse(column.Precision) > 0)
                        {
                            parameter = columnName + " = 0.0;";
                        }
                        else
                        {
                            parameter = columnName + " = 0;";
                        }
                    }
                    else
                    {
                        parameter = columnName + " = 0;";
                    }
                    break;
                case "real":
                    parameter = columnName + " = 0.0;";
                    break;
                case "smalldatetime":
                    parameter = columnName + " = null; ";
                    break;
                case "smallint":
                    parameter = columnName + " = 0;";
                    break;
                case "smallmoney":
                    parameter = columnName + " = 0;";
                    break;
                case "sql_variant":
                    parameter = columnName + " = new Object;";
                    break;
                case "sysname":
                    parameter = columnName + " = \"\";";
                    break;
                case "text":
                    parameter = columnName + " = \"\";";
                    break;
                case "timestamp":
                    parameter = columnName + " = null; ";
                    break;
                case "time":
                    parameter = columnName + " = null; ";
                    break;
                case "timestamp without time zone":
                    parameter = columnName + " = null; ";
                    break;
                case "timestamp with time zone":
                    parameter = columnName + " = null; ";
                    break;
                case "tinyint":
                    parameter = columnName + " = 0;";
                    break;
                case "varbinary":
                    parameter = columnName + " = null;";
                    break;
                case "varchar":
                    parameter = columnName + " = \"\";";
                    break;
                case "varchar2":
                    parameter = columnName + " = \"\";";
                    break;
                case "uniqueidentifier":
                    parameter = columnName + " = new Object;";
                    break;
                case "longtext":
                    parameter = "String " + columnName;
                    break;
                case "blob":
                    parameter = "byte[] " + columnName;
                    break;
                case "tinyblob":
                    parameter = "byte[] " + columnName;
                    break;
                case "mediumblob":
                    parameter = "byte[] " + columnName;
                    break;
                case "longblob":
                    parameter = "byte[] " + columnName;
                    break;
                case "tinytext":
                    parameter = "String " + columnName;
                    break;
                case "mediumtext":
                    parameter = "String " + columnName;
                    break;
                default:  // Unknow data type
                    throw (new Exception("Invalid SQL data type specified: " + column.Type));
            }

            // Return the new parameter string
            return parameter;
        }
        public static string CreateMethodJavaParameter(Column column)
        {
            string parameter;
            string columnName;

            // Format the column name
            columnName = "" + column.Alias;

            switch (column.Type.ToLower())
            {
                case "binary":
                    parameter = "byte[] " + columnName;
                    break;
                case "bigint":
                    parameter = "long " + columnName;
                    break;
                case "bit":
                    parameter = "boolean " + columnName;
                    break;
                case "char":
                    parameter = "String " + columnName;
                    break;
                case "date":
                    parameter = "java.sql.Date " + columnName;
                    break;
                case "datetime":
                    parameter = "java.sql.Date " + columnName;
                    break;
                case "decimal":
                    parameter = "double " + columnName;
                    break;
                case "float":
                    parameter = "float " + columnName;
                    break;
                case "image":
                    parameter = "byte[] " + columnName;
                    break;
                case "int":
                    parameter = "int " + columnName;
                    break;
                case "money":
                    parameter = "double " + columnName;
                    break;
                case "nchar":
                    parameter = "String " + columnName;
                    break;
                case "ntext":
                    parameter = "String " + columnName;
                    break;
                case "nvarchar":
                    parameter = "String " + columnName;
                    break;
                case "numeric":
                    parameter = "double " + columnName;
                    break;
                case "number":
                    if (!column.Precision.Equals(""))
                    {
                        if (Double.Parse(column.Precision) > 0)
                        {
                            parameter = "double " + columnName;
                        }
                        else {
                            parameter = "int " + columnName;
                        }
                    }
                    else {
                        parameter = "int " + columnName;
                    }
                    
                    break;
                case "real":
                    parameter = "float " + columnName;
                    break;
                case "smalldatetime":
                    parameter = "java.sql.Date " + columnName;
                    break;
                case "smallint":
                    parameter = "int " + columnName;
                    break;
                case "smallmoney":
                    parameter = "double " + columnName;
                    break;
                case "sql_variant":
                    parameter = "Object " + columnName;
                    break;
                case "sysname":
                    parameter = "String " + columnName;
                    break;
                case "text":
                    parameter = "String " + columnName;
                    break;
                case "timestamp":
                    parameter = "java.sql.TimeStamp " + columnName;
                    break;
                case "time":
                    parameter = "java.sql.TimeStamp " + columnName;
                    break;
                case "timestamp without time zone":
                    parameter = "java.sql.TimeStamp " + columnName;
                    break;
                case "timestamp with time zone":
                    parameter = "java.sql.TimeStamp " + columnName;
                    break;
                case "tinyint":
                    parameter = "int " + columnName;
                    break;
                case "varbinary":
                    parameter = "byte[] " + columnName;
                    break;
                case "varchar":
                    parameter = "String " + columnName;
                    break;
                case "varchar2":
                    parameter = "String " + columnName;
                    break;
                case "uniqueidentifier":
                    parameter = "Object " + columnName;
                    break;
                case "longtext":
                    parameter = "String " + columnName;
                    break;
                case "blob":
                    parameter = "byte[] " + columnName;
                    break;
                case "tinyblob":
                    parameter = "byte[] " + columnName;
                    break;
                case "mediumblob":
                    parameter = "byte[] " + columnName;
                    break;
                case "longblob":
                    parameter = "byte[] " + columnName;
                    break;
                case "tinytext":
                    parameter = "String " + columnName;
                    break;
                case "mediumtext":
                    parameter = "String " + columnName;
                    break;
                case "character varying":
                    parameter = "String " + columnName;
                    break;
                case "character":
                    parameter = "String " + columnName;
                    break;
                case "integer":
                    parameter = "int " + columnName;
                    break;
                default:  // Unknow data type
                    throw (new Exception("CreateMethodJavaParameter: Invalid SQL data type specified: " + column.Type));
            }

            // Return the new parameter string
            return parameter;
        }

        public static string CreateMethodParameterOracle(Column column)
        {
            string parameter;
            string columnName;

            // Format the column name
            columnName = "_" + column.Alias;

            switch (column.Type.ToLower())
            {
                case "number":
                    parameter = "int" + (column.IsNullable ? "? " : " ") + columnName;
                    break;
                case "varchar2":
                    parameter = "String " + columnName;
                    break;
                case "char":
                    parameter = "String " + columnName;
                    break;
                case "date":
                    parameter = "DateTime" + (column.IsNullable ? "? " : " ") + columnName;
                    break;
                default:  // Unknow data type
                    throw (new Exception("CreateMethodParameterOracle: Invalid SQL Server data type specified: " + column.Type));
            }

            // Return the new parameter string
            return parameter;
        }

        //public static string GetNullableType(string type)
        //{
        //    switch (type)
        //    {
        //        case "string": return "";
        //        case "object": return "";
        //        case "byte[]": return "";
        //        case "DateTime": return "";
        //        default: return "?";
        //    }        
        //}
        /// <summary>
        /// Creates a string for a method parameter representing the specified column.
        /// </summary>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <returns>String containing parameter information of the specified column for a method call.</returns>
        public static string CreateMethodParameterVB(Column column)
        {
            string parameter;
            string columnName;

            // Format the column name
            columnName = "_" + FormatCamel(column.Alias);

            switch (column.Type.ToLower())
            {
                case "binary":
                    parameter = columnName + " As Byte() ";
                    break;
                case "bigint":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Int64) " : "Int64 ");
                    break;
                case "bit":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Boolean) " : "Boolean ");
                    break;
                case "char":
                    parameter = columnName + " As String ";
                    break;
                case "character":
                    parameter = columnName + " As String ";
                    break;
                case "character varying":
                    parameter = columnName + " As String ";
                    break;
                case "datetime":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of DateTime) " : "DateTime ");
                    break;
                case "date":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of DateTime) " : "DateTime ");
                    break;
                case "decimal":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Decimal) " : "Decimal ");
                    break;
                case "float":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Double) " : "Double ");
                    break;
                case "image":
                    parameter = columnName + " As Byte() ";
                    break;
                case "int":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Integer) " : "Integer ");
                    break;
                case "integer":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Integer) " : "Integer ");
                    break;
                case "money":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Decimal) " : "Decimal ");
                    break;
                case "nchar":
                    parameter = columnName + " As String ";
                    break;
                case "ntext":
                    parameter = columnName + " As String ";
                    break;
                case "nvarchar":
                    parameter = columnName + " As String ";
                    break;
                case "numeric":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Decimal) " : "Decimal ");
                    break;
                case "real":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Double) " : "Double ");
                    break;
                case "smalldatetime":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of DateTime) " : "DateTime ");
                    break;
                case "smallint":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Short) " : "Short ");
                    break;
                case "smallmoney":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Decimal) " : "Decimal ");
                    break;
                case "sql_variant":
                    parameter = columnName + " As Object ";
                    break;
                case "sysname":
                    parameter = columnName + " As String ";
                    break;
                case "text":
                    parameter = columnName + " As String ";
                    break;
                case "timestamp":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of DateTime) " : "DateTime ");
                    break;
                case "time":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of DateTime) " : "DateTime ");
                    break;
                case "timestamp without time zone":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of DateTime) " : "DateTime ");
                    break;
                case "timestamp with time zone":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of DateTime) " : "DateTime ");
                    break;
                case "tinyint":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Byte) " : "Byte ");
                    break;
                case "varbinary":
                    parameter = columnName + " As Byte() ";
                    break;
                case "varchar":
                    parameter = columnName + " As String ";
                    break;
                case "uniqueidentifier":
                    parameter = columnName + " As Guid ";
                    break;
                case "longtext":
                    parameter = columnName + " As String ";
                    break;
                case "blob":
                    parameter = columnName + " As byte() ";
                    break;
                case "tinyblob":
                    parameter = columnName + " As byte() ";
                    break;
                case "mediumblob":
                    parameter = columnName + " As byte() ";
                    break;
                case "longblob":
                    parameter = columnName + " As byte() ";
                    break;
                case "tinytext":
                    parameter = columnName + " As String ";
                    break;
                case "mediumtext":
                    parameter = columnName + " As String ";
                    break;
                default:  // Unknow data type
                    throw (new Exception("Invalid SQL Server data type specified: " + column.Type));
            }

            // Return the new parameter string
            return parameter;
        }

        public static string CreateMethodParameterVBOracle(Column column)
        {
            string parameter;
            string columnName;

            // Format the column name
            columnName = "_" + FormatCamel(column.Alias);

            switch (column.Type.ToLower())
            {
                case "number":
                    parameter = columnName + " As " + (column.IsNullable ? "Nullable(Of Integer) " : "Integer ");
                    break;
                case "char":
                    parameter = columnName + " As String ";
                    break;
                case "varchar2":
                    parameter = columnName + " As String ";
                    break;
                case "date":
                    parameter = columnName + " As DateTime ";
                    break;
                default:  // Unknow data type
                    throw (new Exception("Invalid SQL Server data type specified: " + column.Type));
            }

            // Return the new parameter string
            return parameter;
        }

        /// <summary>
        /// Creates the name of the method to call on a SqlDataReader for the specified column.
        /// </summary>
        /// <param name="column">The column to retrieve data for.</param>
        /// <returns>The name of the method to call on a SqlDataReader for the specified column.</returns>
        public static string CreateGetXxxMethod(Column column)
        {
            switch (column.Type.ToLower())
            {
                case "binary":
                    return "GetBytes";
                case "bigint":
                    return "GetInt64";
                case "bit":
                    return "GetBoolean";
                case "char":
                    return "GetString";
                case "datetime":
                    return "GetDateTime";
                case "decimal":
                    return "GetDecimal";
                case "float":
                    return "GetFloat";
                case "image":
                    return "GetBytes";
                case "int":
                    return "GetInt32";
                case "money":
                    return "GetDecimal";
                case "nchar":
                    return "GetString";
                case "ntext":
                    return "GetString";
                case "nvarchar":
                    return "GetString";
                case "numeric":
                    return "GetDecimal";
                case "real":
                    return "GetDecimal";
                case "smalldatetime":
                    return "GetDateTime";
                case "smallint":
                    return "GetInt16";
                case "smallmoney":
                    return "GetFloat";
                case "sql_variant":
                    return "GetBytes";
                case "sysname":
                    return "GetString";
                case "text":
                    return "GetString";
                case "timestamp":
                    return "GetDateTime";
                case "tinyint":
                    return "GetByte";
                case "varbinary":
                    return "GetBytes";
                case "varchar":
                    return "GetString";
                case "uniqueidentifier":
                    return "GetGuid";
                default:  // Unknow data type
                    throw (new Exception("Invalid SQL Server data type specified: " + column.Type));
            }
        }

        public static string CreateGetTableTypeMethod(string columnName){
            //switch (column.DataType.Name.ToLower()){
            switch (columnName.ToLower()){
                case "bit":
                    return "boolean";
                case "boolean":
                    return "boolean";
                case "char":
                    return "string";
                case "datetime":
                    return "date";
                case "date":
                    return "date";
                case "decimal":
                    return "decimal";
                case "float":
                    return "decimal";
                case "int32":
                    return "int";
                case "int":
                    return "int";
                case "money":
                    return "decimal";
                case "nchar":
                    return "string";
                case "ntext":
                    return "string";
                case "nvarchar":
                    return "string";
                case "numeric":
                    return "decimal";
                case "real":
                    return "decimal";
                case "smalldatetime":
                    return "date";
                case "smallint":
                    return "int";
                case "smallmoney":
                    return "decimal";
                case "string":
                    return "string";
                case "text":
                    return "string";
                case "varchar":
                    return "string";
                case "varbinary":
                    return "string";
                default:  // Unknow data type
                    throw (new Exception("Invalid SQL Server data type specified: " + columnName));
            }
        }
        /// <summary>
        /// Creates the name of the method to call on a SqlDataReader for the specified column.
        /// </summary>
        /// <param name="column">The column to retrieve data for.</param>
        /// <returns>The name of the method to call on a SqlDataReader for the specified column.</returns>
        public static string CreateConvertXxxMethod(Column column)
        {
            switch (column.Type.ToLower())
            {
                case "binary":
                    return "UtilDA.ReturnByte";
                case "bigint":
                    return "UtilDA.ReturnInt64";
                case "bit":
                    return "UtilDA.ReturnBoolean";
                case "char":
                    return "UtilDA.ReturnString";
                case "character":
                    return "UtilDA.ReturnString";
                case "character varying":
                    return "UtilDA.ReturnString";
                case "datetime":
                    return "UtilDA.ReturnDatetime";
                case "decimal":
                    return "UtilDA.ReturnDecimal";
                case "float":
                    return "UtilDA.ReturnDouble";
                case "image":
                    return "UtilDA.ReturnBytes";
                case "int":
                    return "UtilDA.ReturnInt";
                case "integer":
                    return "UtilDA.ReturnInt";
                case "money":
                    return "UtilDA.ReturnDouble";
                case "nchar":
                    return "UtilDA.ReturnString";
                case "ntext":
                    return "UtilDA.ReturnString";
                case "nvarchar":
                    return "UtilDA.ReturnString";
                case "numeric":
                    return "UtilDA.ReturnDouble";
                case "real":
                    return "UtilDA.ReturnDouble";
                case "smalldatetime":
                    return "UtilDA.ReturnDatetime";
                case "smallint":
                    return "UtilDA.ReturnInt16";
                case "smallmoney":
                    return "UtilDA.ReturnDouble";
                case "sql_variant":
                    return "UtilDA.ReturnByte";
                case "sysname":
                    return "UtilDA.ReturnString";
                case "text":
                    return "UtilDA.ReturnString";
                case "timestamp":
                    return "UtilDA.ReturnDatetime";
                case "time":
                    return "UtilDA.ReturnDatetime";
                case "timestamp without time zone":
                    return "UtilDA.ReturnDatetime";
                case "timestamp with time zone":
                    return "UtilDA.ReturnDatetime";
                case "tinyint":
                    return "UtilDA.ReturnByte";
                case "varbinary":
                    return "UtilDA.ReturnByte";
                case "varchar":
                    return "UtilDA.ReturnString";
                case "uniqueidentifier":
                    return "UtilDA.ReturnInt";
                case "number":
                    return "UtilDA.ReturnInt";
                case "varchar2":
                    return "UtilDA.ReturnString";
                case "date":
                    return "UtilDA.ReturnDatetime";
                case "longtext":
                    return "UtilDA.ReturnString";
                case "blob":
                    return "UtilDA.ReturnByte";
                case "tinyblob":
                    return "UtilDA.ReturnByte";
                case "mediumblob":
                    return "UtilDA.ReturnByte";
                case "longblob":
                    return "UtilDA.ReturnByte";
                case "tinytext":
                    return "UtilDA.ReturnString";
                case "mediumtext":
                    return "UtilDA.ReturnString";
                default:  // Unknow data type
                    throw (new Exception("Invalid SQL Server data type specified: " + column.Type));
            }
        }

        /// <summary>
        /// Formats a string in Camel case (the first letter is in lower case).
        /// </summary>
        /// <param name="sqlDbType">A string to be formatted.</param>
        /// <returns>A string in Camel case.</returns>
        public static string FormatCamel(string original){
            if (original.Length > 0){
                return original.Substring(0, 1).ToLower() + original.Substring(1);
            }else{
                return String.Empty;
            }
        }

        /// <summary>
        /// Formats a string in Pascal case (the first letter is in upper case).
        /// </summary>
        /// <param name="sqlDbType">A string to be formatted.</param>
        /// <returns>A string in Pascal case.</returns>
        public static string FormatPascal(string original)
        {
            if (original.Length > 0)
            {
                return original.Substring(0, 1).ToUpper() + original.Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Matches a SQL Server data type to a SqlClient.SqlDbType.
        /// </summary>
        /// <param name="sqlDbType">A string representing a SQL Server data type.</param>
        /// <returns>A string representing a SqlClient.SqlDbType.</returns>
        public static string GetSqlDbType(string sqlDbType){
            switch (sqlDbType.ToLower()){
                case "binary":
                    return "Binary";
                case "bigint":
                    return "BigInt";
                case "bit":
                    return "Bit";
                case "char":
                    return "String";
                case "datetime":
                    return "DateTime";
                case "date":
                    return "Date";
                case "decimal":
                    return "Decimal";
                case "float":
                    return "Float";
                case "image":
                    return "Image";
                case "int":
                    return "Int32";
                case "money":
                    return "Money";
                case "nchar":
                    return "NChar";
                case "ntext":
                    return "NText";
                case "nvarchar":
                    return "NVarChar";
                case "numeric":
                    return "Decimal";
                case "real":
                    return "Real";
                case "smalldatetime":
                    return "SmallDateTime";
                case "smallint":
                    return "SmallInt";
                case "smallmoney":
                    return "SmallMoney";
                case "sql_variant":
                    return "Variant";
                case "sysname":
                    return "VarChar";
                case "text":
                    return "Text";
                case "timestamp":
                    return "Timestamp";
                case "tinyint":
                    return "TinyInt";
                case "varbinary":
                    return "VarBinary";
                case "varchar":
                    return "String";
                case "uniqueidentifier":
                    return "UniqueIdentifier";
                default:  // Unknow data type
                    throw (new Exception("Invalid SQL Server data type specified: " + sqlDbType));
            }
        }

        public static string GetSqlDbTypeIsNull(string sqlDbType)
        {
            switch (sqlDbType.ToLower())
            {
                case "binary":
                    return "Binary";
                case "bigint":
                    return "BigInt";
                case "bit":
                    return "Bit";
                case "char":
                    return "''";
                case "datetime":
                    return "DateTime";
                case "decimal":
                    return "Decimal";
                case "float":
                    return "0";
                case "image":
                    return "Image";
                case "int":
                    return "0";
                case "money":
                    return "Money";
                case "nchar":
                    return "''";
                case "ntext":
                    return "NText";
                case "nvarchar":
                    return "NVarChar";
                case "numeric":
                    return "Decimal";
                case "real":
                    return "Real";
                case "smalldatetime":
                    return "SmallDateTime";
                case "smallint":
                    return "SmallInt";
                case "smallmoney":
                    return "SmallMoney";
                case "sql_variant":
                    return "Variant";
                case "sysname":
                    return "VarChar";
                case "text":
                    return "Text";
                case "timestamp":
                    return "Timestamp";
                case "tinyint":
                    return "TinyInt";
                case "varbinary":
                    return "VarBinary";
                case "varchar":
                    return "String";
                case "uniqueidentifier":
                    return "UniqueIdentifier";
                default:  // Unknow data type
                    throw (new Exception("Invalid SQL Server data type specified: " + sqlDbType));
            }
        }

        /// <summary>
        /// Matches a MySQL data type to a MySqlClieny.SqlDbType.
        /// </summary>
        /// <param name="sqlDbType">A string representing a SQL Server data type.</param>
        /// <returns>A string representing a SqlClient.SqlDbType.</returns>
        public static string GetMySqlDbType(string sqlDbType)
        {
            switch (sqlDbType.ToLower())
            {
                case "binary":
                    return "Blob";
                case "bigint":
                    return "Int64";
                case "bit":
                    return "Bit";
                case "char":
                    return "Byte";
                case "datetime":
                    return "Datetime";
                case "decimal":
                    return "Decimal";
                case "float":
                    return "Float";
                case "image":
                    return "Blob";
                case "int":
                    return "Int32";
                case "money":
                    return "Double";
                case "nchar":
                    return "VarChar";
                case "ntext":
                    return "VarChar";
                case "nvarchar":
                    return "VarChar";
                case "numeric":
                    return "Decimal";
                case "real":
                    return "Double";
                case "smalldatetime":
                    return "Datetime";
                case "smallint":
                    return "Int16";
                case "smallmoney":
                    return "Double";
                case "sql_variant":
                    return "VarChar";
                case "sysname":
                    return "VarChar";
                case "text":
                    return "VarChar";
                case "timestamp":
                    return "Timestamp";
                case "tinyint":
                    return "Int32";
                case "varbinary":
                    return "Blob";
                case "varchar":
                    return "VarChar";
                case "uniqueidentifier":
                    return "UniqueIdentifier";
                default:  // Unknow data type
                    throw (new Exception("Invalid MySQL Server data type specified: " + sqlDbType));
            }
        }
        /// <summary>
        /// Creates a string for a SqlParameter representing the specified column.
        /// </summary>
        /// <param name="table">Object that stores the information for the table that the column belongs to.</param>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <param name="passValue">Indicates if the parameter value should be passed to the database, otherwise DBNull.Value will be passed.</param>
        /// <returns>String containing SqlParameter information of the specified column for a method call.</returns>
        public static string CreateSqlParameter(Table table, Column column, bool passValue){
            byte bytePrecision;
            byte byteScale;
            string parameter;
            // Get an array of data types and variable names
            if (table == null){
                parameter = FormatCamel(column.Alias);
            }else{
                parameter = FormatCamel(table.Alias) + "." + FormatPascal(column.Alias);
            }

            // Convert the precision value
            if (column.Precision.Length > 0){
                bytePrecision = byte.Parse(column.Precision);
            }else{
                bytePrecision = 0;
            }

            // Convert the scale value
            if (column.Scale.Length > 0){
                byteScale = byte.Parse(column.Scale);
            }else{
                byteScale = 0;
            }

            // Is the parameter used for input or output
            if (column.IsIdentity || column.IsRowGuidCol){
                if (passValue){
                    return "new SqlParameter(\"@" + column.Name + "\", SqlDbType." + GetSqlDbType(column.Type) + ", " + column.Length + ", ParameterDirection.Input, false, " + bytePrecision + ", " + byteScale + ", \"" + column.Name + "\", DataRowVersion.Proposed, " + parameter + ")";
                }else{
                    return "new SqlParameter(\"@" + column.Name + "\", SqlDbType." + GetSqlDbType(column.Type) + ", " + column.Length + ", ParameterDirection.Output, false, " + bytePrecision + ", " + byteScale + ", \"" + column.Name + "\", DataRowVersion.Proposed, null)";
                }
            }else{
                return "new SqlParameter(\"@" + column.Name + "\", SqlDbType." + GetSqlDbType(column.Type) + ", " + column.Length + ", ParameterDirection.Input, false, " + bytePrecision + ", " + byteScale + ", \"" + column.Name + "\", DataRowVersion.Proposed, " + parameter + ")";
            }
        }

        // <summary>
        /// Crea una cadena con los parámetros para C#.
        /// </summary>
        /// <param name="table">Object that stores the information for the table that the column belongs to.</param>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <param name="passValue">Indicates if the parameter value should be passed to the database, otherwise DBNull.Value will be passed.</param>
        /// <returns>String containing SqlParameter information of the specified column for a method call.</returns>
        public static string CreateSqlParameterCSharp(Table table, Column column, bool passValue){
            //byte bytePrecision;
            //byte byteScale;
            string parameter;
            string columnLength;
            // Get an array of data types and variable names
            if (table == null){
                parameter = FormatCamel(column.Alias);
            }else{
                parameter = FormatCamel(table.Alias) + "." + column.Alias;
            }
            switch (column.Type.ToLower()){
                case "datetime":
                    columnLength = "";
                    break;
                case "decimal":
                    columnLength = "";
                    break;
                case "numeric":
                    columnLength = "";
                    break;
                default:
                    columnLength = ", " + column.Length;
                    break;
            }
            // Is the parameter used for input or output
            if (column.IsIdentity || column.IsRowGuidCol){
                if (passValue){
                    return "sql_comando.Parameters.Add(\"@" + column.Name + "\", SqlDbType." + GetSqlDbType(column.Type) + columnLength + ").Value = " + parameter + ";";
                }else{
                    return "sql_comando.Parameters.Add(\"@" + column.Name + "\", SqlDbType." + GetSqlDbType(column.Type) + columnLength + ").Direction = ParameterDirection.Output;";
                }
            }else{
                return "sql_comando.Parameters.Add(\"@" + column.Name + "\", SqlDbType." + GetSqlDbType(column.Type) + columnLength + ").Value = " + parameter + ";";
            }
        }

        // <summary>
        /// Crea una cadena con los parámetros OutPut para C#. Se obtiene los datos de los OutPut
        /// </summary>
        /// <param name="table">Object that stores the information for the table that the column belongs to.</param>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <param name="passValue">Indicates if the parameter value should be passed to the database, otherwise DBNull.Value will be passed.</param>
        /// <returns>String containing SqlParameter information of the specified column for a method call.</returns>
        public static string CreateSqlParameterCSharpOutPut(Table table, Column column){
            string parameter;
            string valueOutPut = "";
            string type = CreateMethodParameter(column).Split(' ')[0];
            //parameter = Utility.CreateMethodParameter(column);
            // Get an array of data types and variable names
            if (table == null){
                parameter = FormatCamel(column.Alias);
            }else{
                parameter = FormatCamel(table.Alias) + "." + column.Alias;
            }
            
            // Is the parameter used for input or output
            if (column.IsIdentity || column.IsRowGuidCol){
                valueOutPut = "" + parameter + " = (" + type + ")sql_comando.Parameters[\"@" + column.Name + "\"].Value;";
            }
            return valueOutPut;
        }

        /// <summary>
        /// Creates a string for a SqlParameter in VB representing the specified column.
        /// </summary>
        /// <param name="table">Object that stores the information for the table that the column belongs to.</param>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <param name="passValue">Indicates if the parameter value should be passed to the database, otherwise DBNull.Value will be passed.</param>
        /// <returns>String containing SqlParameter information of the specified column for a method call.</returns>
        public static string CreateSqlParameterVB(Table table, Column column, bool passValue)
        {
            byte bytePrecision;
            byte byteScale;
            string parameter;

            // Get an array of data types and variable names
            if (table == null)
            {
                parameter = "_" + FormatCamel(column.Alias);
            }
            else
            {
                parameter = "_" + FormatCamel(table.Alias) + "." + FormatPascal(column.Alias);
            }

            // Convert the precision value
            if (column.Precision.Length > 0)
            {
                bytePrecision = byte.Parse(column.Precision);
            }
            else
            {
                bytePrecision = 0;
            }

            // Convert the scale value
            if (column.Scale.Length > 0)
            {
                byteScale = byte.Parse(column.Scale);
            }
            else
            {
                byteScale = 0;
            }

            // Is the parameter used for input or output
            if (column.IsIdentity || column.IsRowGuidCol)
            {
                if (passValue)
                {
                    return "New SqlParameter(\"@" + column.Name + "\", SqlDbType." + GetSqlDbType(column.Type) + ", " + column.Length + ", ParameterDirection.Input, false, " + bytePrecision + ", " + byteScale + ", \"" + column.Name + "\", DataRowVersion.Proposed, " + parameter + ")";
                }
                else
                {
                    return "New SqlParameter(\"@" + column.Name + "\", SqlDbType." + GetSqlDbType(column.Type) + ", " + column.Length + ", ParameterDirection.Output, false, " + bytePrecision + ", " + byteScale + ", \"" + column.Name + "\", DataRowVersion.Proposed, Nothing)";
                }
            }
            else
            {
                return "New SqlParameter(\"@" + column.Name + "\", SqlDbType." + GetSqlDbType(column.Type) + ", " + column.Length + ", ParameterDirection.Input, false, " + bytePrecision + ", " + byteScale + ", \"" + column.Name + "\", DataRowVersion.Proposed, " + parameter + ")";
            }
        }
        /// <summary>
        /// Creates a string for a MySqlParameter representing the specified column.
        /// </summary>
        /// <param name="table">Object that stores the information for the table that the column belongs to.</param>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <param name="passValue">Indicates if the parameter value should be passed to the database, otherwise DBNull.Value will be passed.</param>
        /// <returns>String containing SqlParameter information of the specified column for a method call.</returns>
        public static string CreateMySqlParameter(Table table, Column column, bool passValue){
            byte bytePrecision;
            byte byteScale;
            string parameter;
            // Get an array of data types and variable names
            if (table == null){
                parameter = FormatCamel(column.Alias);
            }else{
                parameter = FormatCamel(table.Alias) + "." + FormatPascal(column.Alias);
            }

            // Convert the precision value
            if (column.Precision.Length > 0){
                bytePrecision = byte.Parse(column.Precision);
            }else{
                bytePrecision = 0;
            }

            // Convert the scale value
            if (column.Scale.Length > 0){
                byteScale = byte.Parse(column.Scale);
            }else{
                byteScale = 0;
            }

            // Is the parameter used for input or output
            if (column.IsIdentity || column.IsRowGuidCol){
                if (passValue){
                    return "new MySqlParameter(\"@" + column.Name + "\", MySqlDbType." + GetMySqlDbType(column.Type) + ", " + column.Length + ", ParameterDirection.Input, false, " + bytePrecision + ", " + byteScale + ", \"" + column.Name + "\", DataRowVersion.Proposed, " + parameter + ")";
                }else{
                    return "new MySqlParameter(\"@" + column.Name + "\", MySqlDbType." + GetMySqlDbType(column.Type) + ", " + column.Length + ", ParameterDirection.Output, false, " + bytePrecision + ", " + byteScale + ", \"" + column.Name + "\", DataRowVersion.Proposed, null)";
                }
            }else{
                return "new MySqlParameter(\"@" + column.Name + "\", MySqlDbType." + GetMySqlDbType(column.Type) + ", " + column.Length + ", ParameterDirection.Input, false, " + bytePrecision + ", " + byteScale + ", \"" + column.Name + "\", DataRowVersion.Proposed, " + parameter + ")";
            }
        }

        public static string F_Log(Exception ex){
            StringBuilder strError = new StringBuilder();
            string strDescripcion, strDetalle, strSource, strPila;
            string strHora;
            string strFecha;
            strFecha = DateTime.Now.ToString("yyyy-MM-dd");
            strHora = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');
            strDescripcion = ex.Message;
            if (ex.InnerException == null) {
                strDetalle = ex.Message;
            }else{
                strDetalle = ex.Message + " InnerException= " + ex.InnerException.Message + " StackTrace InnerException=" + ex.InnerException.StackTrace;
            }
            strSource = ex.Source;
            strPila = ex.StackTrace;
            strError.AppendLine("<Error " + "Fecha='" + strFecha + "'" + " Hora= '" + strHora + "' " + ">");
            strError.AppendLine("<Descripcion> " + strDescripcion);
	        strError.AppendLine("</Descripcion>");
	        strError.AppendLine("<Detalle> " + strDetalle);
	        strError.AppendLine("</Detalle>");
	        strError.AppendLine("<Origen> " + strSource);
	        strError.AppendLine("</Origen>");
	        strError.AppendLine("<Seguimiento> " + strPila);
	        strError.AppendLine("</Seguimiento>");
	        strError.AppendLine("</Error>");
            return strError.ToString();
        }

        public static void P_Generalog(Exception ex) { 
            string strDestinoFile;
	        string strNombreFile;
	        string strFecha;
            string strBody;
            strDestinoFile = "LogError\\";
            strFecha = DateTime.Now.ToString("yyyyMMdd");
            strNombreFile = "Error_" + strFecha + ".log";
            //CreateSubDirectory(strDestinoFile, true);
            CreateSubDirectory(strDestinoFile);
            using (StreamWriter stwFile = File.AppendText(strDestinoFile + strNombreFile)){
                strBody = F_Log(ex);
                stwFile.WriteLine();
                stwFile.Write(strBody);
                stwFile.WriteLine();
                stwFile.Flush();
                stwFile.Close();
            }
        }
    }
}
