using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataTierGenerator
{
    public delegate void CountUpdate(object sender, CountEventArgs e);

    /// <summary>
    /// Generates C# and SQL code for accessing a database.
    /// </summary>
    public sealed class Generator
    {
        private Generator() { }

        public static event CountUpdate DatabaseCounted;
        public static event CountUpdate TableCounted;

        public static Util.Provider proveedor;
        public static string ConnectionString = "";
        /// <summary>
        /// Generates the SQL and C# code for the specified database.
        /// </summary>
        /// <param name="connectionString">The connection string to be used to connect the to the database.</param>
        /// <param name="grantLoginName">The SQL Server login name that should be granted execute rights on the generated stored procedures.</param>
        /// <param name="createMultipleFiles">A flag indicating if the generated stored procedures should be created in one file or separate files.</param>
        /// <param name="targetNamespace">The namespace that the generated C# classes should contained in.</param>
        //public static void Generate(string connectionString, string grantLoginName, string storedProcedurePrefix, bool createMultipleFiles, string targetNamespace)
        //{
        //    ArrayList tableList = new ArrayList();
        //    string databaseName;
        //    string sqlPath;
        //    string csPath;

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        databaseName = connection.Database;
        //        sqlPath = databaseName + "\\SQL\\";
        //        csPath = databaseName + "\\CS\\";

        //        connection.Open();

        //        // Get a list of the entities in the database
        //        DataTable dataTable = new DataTable();
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(Utility.GetTableQuery(connection.Database), connection);
        //        dataAdapter.Fill(dataTable);

        //        // Process each table
        //        foreach (DataRow dataRow in dataTable.Rows)
        //        {
        //            Table table = new Table();
        //            table.Name = (string)dataRow["TABLE_NAME"];
        //            table.Alias = table.Name;
        //            QueryTable(connection, table);
        //            tableList.Add(table);
        //        }
        //    }

        //    DatabaseCounted(null, new CountEventArgs(tableList.Count));

        //    // Generate the necessary SQL and C# code for each table
        //    int count = 0;
        //    if (tableList.Count > 0)
        //    {
        //        // Create the necessary directories
        //        Utility.CreateSubDirectory(sqlPath, true);
        //        Utility.CreateSubDirectory(csPath, true);

        //        // Create the necessary database logins
        //        SqlGenerator.CreateUserQueries(databaseName, grantLoginName, sqlPath, createMultipleFiles);

        //        // Create the SqlClientUtility class
        //        CsGenerator.CreateSqlClientUtilityClass(targetNamespace, csPath);
        //        VBGenerator.CreateSqlClientUtilityClassVB(targetNamespace, csPath);
        //        //Create UtilDA y UtilBO Class
        //        CsGenerator.CreateUtilDAClass(targetNamespace, csPath);
        //        CsGenerator.CreateUtilBOClass(targetNamespace, csPath);



        //        // Create everything we need
        //        foreach (Table table in tableList)
        //        {
        //            /*Genera los SPS para el MSSQL Server*/
        //            SqlGenerator.CreateInsertStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);
        //            SqlGenerator.CreateUpdateStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);
        //            SqlGenerator.CreateDeleteStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);
        //            SqlGenerator.CreateDeleteAllByStoredProcedures(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);
        //            SqlGenerator.CreateSelectStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);
        //            SqlGenerator.CreateSelectAllStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);
        //            SqlGenerator.CreateSelectAllByStoredProcedures(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);


        //            /*Genera las capas para la aplicacion*/
        //            CsGenerator.CreateDataTransferClass(table, targetNamespace, csPath);
        //            VBGenerator.CreateDataTransferClassVB(table, targetNamespace, csPath);
        //            CsGenerator.CreateDataAccessClass(table, targetNamespace, storedProcedurePrefix, csPath);
        //            VBGenerator.CreateDataAccessClass(table, targetNamespace, storedProcedurePrefix, csPath);
        //            //CsGenerator.CreateMySqlDataAccessClass(table, targetNamespace, storedProcedurePrefix, csPath);

        //            count++;
        //            TableCounted(null, new CountEventArgs(count));
        //        }
        //    }
        //}

        /// <summary>
        /// Generates the SQL and C# code for the specified Table in the database.
        /// </summary>
        /// <param name="connectionString">The connection string to be used to connect the to the database.</param>
        /// <param name="grantLoginName">The SQL Server login name that should be granted execute rights on the generated stored procedures.</param>
        /// <param name="createMultipleFiles">A flag indicating if the generated stored procedures should be created in one file or separate files.</param>
        /// <param name="targetNamespace">The namespace that the generated C# classes should contained in.</param>
        public static void GenerateforTable(DbProviderFactory dbProvider, string connectionString, string grantLoginName, string storedProcedurePrefix, bool createMultipleFiles, string targetNamespace, string strTableName, string strTableAlias, DataTable dtColumnAlias, string storedProcedurePrefixExecute)
        {
            ArrayList tableList = new ArrayList();
            string databaseName = VBGenerator.DataBaseSys;
            string sqlPath;
            string csPath;

           
                //connection.ConnectionString = connectionString;
                //connection.Open();

                /*if (Util.strProp == null || Util.strProp == string.Empty)
                    databaseName = connection.Database;
                else
                    databaseName = Util.strProp.Replace(".", "");*/

                sqlPath = databaseName + "\\SQL\\";
                csPath = databaseName + "\\CS\\";

                // Get a list of the entities in the database
                DataTable dataTable = new DataTable();
                DataTable dtQuery = new DataTable();
                IDataParameter[] parametros = new IDataParameter[0];
                //DbCommand cmd = dbProvider.CreateCommand();
                //cmd.Connection = connection;
                //cmd.CommandType = CommandType.Text;

                switch (proveedor)
                {  
                    case Util.Provider.Access:
                        //cmd.CommandText = Utility.GetTableQuery(connection.Database) + " AND TABLE_NAME='" + strTableName + "'";
                        break;
                    case Util.Provider.SqlClient:
                        SQLServerConnection sqlserver = new SQLServerConnection();
                        sqlserver.ConnectionString = connectionString;
                        dataTable = sqlserver.ExecuteDataTable(CommandType.Text, Utility.GetTableQuery(databaseName) + " AND TABLE_NAME='" + strTableName + "'", parametros);
                        sqlserver.Dispose();
                        //cmd.CommandText = Utility.GetTableQuery(connection.Database) + " AND TABLE_NAME='" + strTableName + "'";
                        break;
                    case Util.Provider.MySql:
                        MySQLConnection mysql = new MySQLConnection();
                        mysql.ConnectionString = connectionString;
                        dataTable = mysql.ExecuteDataTable(CommandType.Text, Utility.GetTableQuery(databaseName) + " AND TABLE_NAME='" + strTableName + "';", parametros);
                        mysql.Dispose();
                        //cmd.CommandText = Utility.GetTableQuery(connection.Database) + " AND TABLE_NAME='" + strTableName + "';";
                        break;
                    case Util.Provider.PostgreSQL:
                        PostgreSQLConnection pgsql = new PostgreSQLConnection();
                        pgsql.ConnectionString = connectionString;
                        dataTable = pgsql.ExecuteDataTable(CommandType.Text, "SELECT current_database()AS TABLE_CATALOG, relname AS TABLE_NAME FROM pg_class WHERE relname='" + strTableName + "'", parametros);
                        pgsql.Dispose();
                        //cmd.CommandText = Utility.GetTableQuery(connection.Database) + " AND TABLE_NAME='" + strTableName + "';";
                        break;
                    case Util.Provider.Oracle:
                        OracleConnection orcl = new OracleConnection();
                        orcl.ConnectionString = connectionString;
                        dataTable = orcl.ExecuteDataTable(CommandType.Text, "select * from user_catalog where table_name='" + strTableName + "'", parametros);
                        orcl.Dispose();
                        break;
                }

                if (storedProcedurePrefixExecute.Trim() != "") { 
                    switch (proveedor){
                        case Util.Provider.SqlClient:
                            SQLServerConnection sqlserver = new SQLServerConnection();
                            sqlserver.ConnectionString = connectionString;
                            dtQuery = sqlserver.ExecuteDataTable(CommandType.Text, storedProcedurePrefixExecute, null);
                            sqlserver.Dispose();
                            //cmd.CommandText = Utility.GetTableQuery(connection.Database) + " AND TABLE_NAME='" + strTableName + "'";
                            break;
                    }
                }
                

                /*using (DbDataAdapter dataAdapter = dbProvider.CreateDataAdapter())
                {
                    //dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(dataTable);
                }*/
                // Process each table
                foreach (DataRow dataRow in dataTable.Rows){
                    Table table = new Table();
                    table.Name = (string)dataRow["TABLE_NAME"];
                    table.Alias = (strTableAlias.Trim() == "" ? table.Name : strTableAlias);
                    QueryTable(dbProvider, null, table, dtColumnAlias);
                    tableList.Add(table);
                }
            

            DatabaseCounted(null, new CountEventArgs(tableList.Count));

            // Generate the necessary SQL and C# code for each table
            int count = 0;
            if (tableList.Count > 0){
                // Create the necessary directories
                Utility.CreateSubDirectory(sqlPath, true);
                Utility.CreateSubDirectory(csPath, true);

                // Create the necessary database logins
                SqlGenerator.CreateUserQueries(databaseName, grantLoginName, sqlPath, createMultipleFiles);

                //


                // Create the SqlClientUtility class
                CsGenerator.CreateSqlClientUtilityClass(targetNamespace, csPath);

                //Crea las clases Java para acceder a la base de datos
                CsGenerator.CreateJDBCUtilityClass(targetNamespace, csPath);

                VBGenerator.CreateSqlClientUtilityClassVB(targetNamespace, csPath);

                //Create las clases UtilDA y UtilBO
                CsGenerator.CreateUtilDAClass(targetNamespace, csPath);
                CsGenerator.CreateUtilBOClass(targetNamespace, csPath);
                VBGenerator.CreateUtilDAClass(targetNamespace, csPath);
                VBGenerator.CreateUtilBOClass(targetNamespace, csPath);

                //Create las Clases GenericsDA y GenericsBO
                CsGenerator.CreateGenericsDAClass(targetNamespace, csPath);
                CsGenerator.CreateGenericsBOClass(targetNamespace, csPath);
                VBGenerator.CreateGenericsDAClass(targetNamespace, csPath);
                VBGenerator.CreateGenericsBOClass(targetNamespace, csPath);

                //Crea la clase auditoria
                CsGenerator.CreateAuditoriaEOClass(targetNamespace, csPath);


                // Create the MySqlClientUtility class
                //CsGenerator.CreateMySqlClientUtilityClass(targetNamespace, csPath);

                SqlGenerator.proveedor = proveedor;

                // Create everything we need
                foreach (Table table in tableList)
                {
                    /*Genera los SPS para el MSSQL Server*/
                    SqlGenerator.CreateInsertStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);
                    SqlGenerator.CreateUpdateStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);
                    SqlGenerator.CreateDeleteStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);
                    //SqlGenerator.CreateDeleteAllByStoredProcedures(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);
                    SqlGenerator.CreateSelectStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);
                    SqlGenerator.CreateSelectAllStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);
                    //SqlGenerator.CreateSelectAllByStoredProcedures(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);

                    /*Genera los SPS para el MySQL Server*/
                    // MySqlGenerator.CreateInsertStoredProcedure(table, grantLoginName, storedProcedurePrefix, sqlPath, createMultipleFiles);

                    /*Genera las capas para la aplicacion*/

                    //Genera la clase para la entidad

                    CsGenerator.CreateDataTransferJavaClass(table, targetNamespace, csPath);
                    CsGenerator.CreateDataBussinessJavaClass(table, targetNamespace, csPath, storedProcedurePrefix);

                    CsGenerator.CreateDataTransferClass(table, targetNamespace, csPath);
                    VBGenerator.CreateDataTransferClassVB(table, targetNamespace, csPath);

                    CsGenerator.CreateDataAccessClass(table, targetNamespace, storedProcedurePrefix, csPath, dtQuery, storedProcedurePrefixExecute);
                    VBGenerator.CreateDataAccessClass(table, targetNamespace, storedProcedurePrefix, csPath);
                    //CsGenerator.CreateMySqlDataAccessClass(table, targetNamespace, storedProcedurePrefix, csPath);
                    /*Genera el Business Object*/
                    CsGenerator.CreateBusinessObjectsClass(table, targetNamespace, storedProcedurePrefix, csPath);
                    VBGenerator.CreateBusinessObjectsClass(table, targetNamespace, storedProcedurePrefix, csPath);
                    count++;
                    TableCounted(null, new CountEventArgs(count));
                }
            }
        }

        /// <summary>
        /// Retrieves the column, primary key, and foreign key information for the specified table.
        /// </summary>
        /// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
        /// <param name="table">The table instance that information should be retrieved for.</param>
        //private static void QueryTable(SqlConnection connection, Table table)
        //{
        //    // Get a list of the entities in the database
        //    DataTable dataTable = new DataTable();
        //    SqlDataAdapter dataAdapter = new SqlDataAdapter(Utility.GetColumnQuery(table.Name), connection);
        //    dataAdapter.Fill(dataTable);

        //    foreach (DataRow columnRow in dataTable.Rows)
        //    {
        //        Column column = new Column();
        //        column.Name = columnRow["COLUMN_NAME"].ToString();
        //        column.Alias = column.Name;
        //        column.Type = columnRow["DATA_TYPE"].ToString();
        //        column.Precision = columnRow["NUMERIC_PRECISION"].ToString();
        //        column.Scale = columnRow["NUMERIC_SCALE"].ToString();

        //        // Determine the column's length
        //        if (columnRow["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value)
        //        {
        //            column.Length = columnRow["CHARACTER_MAXIMUM_LENGTH"].ToString();
        //        }
        //        else
        //        {
        //            column.Length = columnRow["COLUMN_LENGTH"].ToString();
        //        }

        //        // Is the column a RowGuidCol column?
        //        if (columnRow["IS_ROWGUIDCOL"].ToString() == "1")
        //        {
        //            column.IsRowGuidCol = true;
        //        }

        //        // Is the column an Identity column?
        //        if (columnRow["IS_IDENTITY"].ToString() == "1")
        //        {
        //            column.IsIdentity = true;
        //        }

        //        // Is columnRow column a computed column?
        //        if (columnRow["IS_COMPUTED"].ToString() == "1")
        //        {
        //            column.IsComputed = true;
        //        }

        //        table.Columns.Add(column);
        //    }

        //    // Get the list of primary keys
        //    DataTable primaryKeyTable = Utility.GetPrimaryKeyList(connection, table.Name);
        //    foreach (DataRow primaryKeyRow in primaryKeyTable.Rows)
        //    {
        //        string primaryKeyName = primaryKeyRow["COLUMN_NAME"].ToString();

        //        foreach (Column column in table.Columns)
        //        {
        //            if (column.Name == primaryKeyName)
        //            {
        //                table.PrimaryKeys.Add(column);
        //                break;
        //            }
        //        }
        //    }

        //    // Get the list of foreign keys
        //    DataTable foreignKeyTable = Utility.GetForeignKeyList(connection, table.Name);
        //    foreach (DataRow foreignKeyRow in foreignKeyTable.Rows)
        //    {
        //        string name = foreignKeyRow["FK_NAME"].ToString();
        //        string columnName = foreignKeyRow["FKCOLUMN_NAME"].ToString();

        //        if (table.ForeignKeys.ContainsKey(name) == false)
        //        {
        //            table.ForeignKeys.Add(name, new ArrayList());
        //        }

        //        ArrayList foreignKeys = (ArrayList)table.ForeignKeys[name];

        //        foreach (Column column in table.Columns)
        //        {
        //            if (column.Name == columnName)
        //            {
        //                foreignKeys.Add(column);
        //                break;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Retrieves the column, primary key, and foreign key information for the specified table with Column Alias.
        /// </summary>
        /// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
        /// <param name="table">The table instance that information should be retrieved for.</param>
        private static void QueryTable(DbProviderFactory dbProvider, DbConnection connection, Table table, DataTable dtColumnAlias)
        {
            // Get a list of the entities in the database
            DataTable dataTable = new DataTable();
            IDataParameter[] parametros = new IDataParameter[0];
            switch (proveedor) { 
                case Util.Provider.SqlClient:
                    SQLServerConnection sqlserver = new SQLServerConnection();
                    sqlserver.ConnectionString = ConnectionString;
                    dataTable = sqlserver.ExecuteDataTable(CommandType.Text, Utility.GetColumnQuery(table.Name), parametros);
                    sqlserver.Dispose();
                    break;
                case Util.Provider.Oracle:
                    OracleConnection orcl = new OracleConnection();
                    orcl.ConnectionString = ConnectionString;
                    dataTable = orcl.ExecuteDataTable(CommandType.Text, Utility.GetColumnQuery(table.Name), parametros);
                    orcl.Dispose();
                    break;
                case Util.Provider.PostgreSQL:
                    PostgreSQLConnection pgsql = new PostgreSQLConnection();
                    pgsql.ConnectionString = ConnectionString;
                    dataTable = pgsql.ExecuteDataTable(CommandType.Text, Utility.GetColumnQuery(table.Name), parametros);
                    pgsql.Dispose();
                    break;
                case Util.Provider.MySql:
                    MySQLConnection mysql = new MySQLConnection();
                    mysql.ConnectionString = ConnectionString;
                    dataTable = mysql.ExecuteDataTable(CommandType.Text, Utility.GetColumnQuery(table.Name), parametros);
                    mysql.Dispose();
                    break;
                case Util.Provider.Db2:
                    break;
            }
            /*DbCommand cmd = dbProvider.CreateCommand();
            cmd.CommandText = Utility.GetColumnQuery(table.Name);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            DbDataAdapter dataAdapter = dbProvider.CreateDataAdapter();
            dataAdapter.SelectCommand = cmd;
            dataAdapter.Fill(dataTable);*/

            foreach (DataRow columnRow in dataTable.Rows)
            {
                Column column = new Column();
                column.Name = columnRow["COLUMN_NAME"].ToString();
                column.Type = columnRow["DATA_TYPE"].ToString();
                column.Precision = columnRow["NUMERIC_PRECISION"].ToString();
                column.Scale = columnRow["NUMERIC_SCALE"].ToString();
                column.Alias = ObtenerColumnAlias(column.Name, dtColumnAlias);
                column.IsNullable = ObtenerColumnIsNullable(column.Name, dtColumnAlias);
                column.IsPrimaryKey = ObtenerColumnIsPrimaryKey(column.Name, dtColumnAlias);
                column.IsForeignKey = ObtenerColumnIsForeignKey(column.Name, dtColumnAlias);


                // Determine the column's length
                if (columnRow["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value)
                {
                    column.Length = columnRow["CHARACTER_MAXIMUM_LENGTH"].ToString();
                }
                else
                {
                    column.Length = columnRow["COLUMN_LENGTH"]!=null?columnRow["COLUMN_LENGTH"].ToString():"0";
                }

                // Is the column a RowGuidCol column?
                if (columnRow["IS_ROWGUIDCOL"].ToString() == "1")
                {
                    column.IsRowGuidCol = true;
                }

                // Is the column an Identity column?
                if (columnRow["IS_IDENTITY"].ToString() == "1")
                {
                    column.IsIdentity = true;
                }

                // Is columnRow column a computed column?
                if (columnRow["IS_COMPUTED"].ToString() == "1")
                {
                    column.IsComputed = true;
                }

                if (ObtenerColumnVisibility(column.Name, dtColumnAlias))
                { table.Columns.Add(column); }
            }

            // Get the list of primary keys
            DataTable primaryKeyTable = Utility.GetPrimaryKeyList(dbProvider, connection, table.Name);
            foreach (DataRow primaryKeyRow in primaryKeyTable.Rows)
            {
                string primaryKeyName = primaryKeyRow["COLUMN_NAME"].ToString();

                foreach (Column column in table.Columns)
                {
                    if (column.Name == primaryKeyName)
                    {
                        if (ObtenerColumnVisibility(column.Name, dtColumnAlias))
                        { table.PrimaryKeys.Add(column); }
                        break;
                    }
                }
            }

            // Get the list of foreign keys
            DataTable foreignKeyTable = Utility.GetForeignKeyList(dbProvider, connection, table.Name);
            foreach (DataRow foreignKeyRow in foreignKeyTable.Rows)
            {
                string name = foreignKeyRow["FK_NAME"].ToString();
                string columnName = foreignKeyRow["FKCOLUMN_NAME"].ToString();

                if (table.ForeignKeys.ContainsKey(name) == false)
                {
                    if (ObtenerColumnVisibility(columnName, dtColumnAlias))
                    { table.ForeignKeys.Add(name, new ArrayList()); }
                }

                ArrayList foreignKeys = (ArrayList)table.ForeignKeys[name];

                foreach (Column column in table.Columns)
                {
                    if (column.Name == columnName)
                    {
                        if (ObtenerColumnVisibility(column.Name, dtColumnAlias))
                        { foreignKeys.Add(column); }
                        break;
                    }
                }
            }
        }
        public static string ObtenerColumnAlias(string ColumnName, DataTable dtColumnAlias)
        {
            DataRow[] rows = dtColumnAlias.Select("COLUMN_NAME='" + ColumnName + "'");
            if (rows.Length == 0)
            {
                return ColumnName;
            }
            else
            {
                return (rows[0]["COLUMN_ALIAS"].ToString().Trim() == "" ? ColumnName : rows[0]["COLUMN_ALIAS"].ToString().Trim());
            }
        }
        public static bool ObtenerColumnIsForeignKey(string ColumnName, DataTable dtColumnAlias)
        {
            DataRow[] rows = dtColumnAlias.Select("COLUMN_NAME='" + ColumnName + "'");
            if (rows.Length == 0)
            { return false; }
            else
            {
                if (rows[0]["IS_Nullable"].ToString().Trim().ToLower().Equals("yes"))
                { return true; }
                else if (rows[0]["IS_Nullable"].ToString().Trim().ToLower().Equals("no"))
                { return false; }
                else
                { return false; }
            }
        }
        public static bool ObtenerColumnIsPrimaryKey(string ColumnName, DataTable dtColumnAlias)
        {
            DataRow[] rows = dtColumnAlias.Select("COLUMN_NAME='" + ColumnName + "'");
            if (rows.Length == 0)
            { return false; }
            else
            {
                if (Convert.ToBoolean(rows[0]["IsPK"]) == true)
                { return true; }
                else if (Convert.ToBoolean(rows[0]["IsPK"]) == false)
                { return false; }
                else
                { return false; }
            }
        }
        public static bool ObtenerColumnIsNullable(string ColumnName, DataTable dtColumnAlias)
        {
            DataRow[] rows = dtColumnAlias.Select("COLUMN_NAME='" + ColumnName + "'");
            if (rows.Length == 0)
            { return false; }
            else
            {
                if (rows[0]["IS_Nullable"].ToString().ToLower().Equals("yes"))
                { return true; }
                else if (rows[0]["IS_Nullable"].ToString().ToLower().Trim().Equals("no"))
                { return false; }
                else
                { return false; }
            }
        }
        public static bool ObtenerColumnVisibility(string ColumnName, DataTable dtColumnAlias)
        {
            DataRow[] rows = dtColumnAlias.Select("COLUMN_NAME='" + ColumnName + "' AND COLUMN_VISIBLE=True");
            if (rows.Length == 0)
            { return false; }
            else
            { return true; }
        }
    }
}
