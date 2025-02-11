using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace DataTierGenerator
{
    /// <summary>
    /// Generates SQL Server stored procedures for a database.
    /// </summary>
    public sealed class SqlGenerator
    {
        private SqlGenerator() { }

        public static Util.Provider proveedor;
        public static String DataBaseSys = "";
        /// <summary>
        /// Creates the SQL script that is responsible for granting the specified login access to the specified database.
        /// </summary>
        /// <param name="databaseName">The name of the database that the login will be created for.</param>
        /// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
        /// <param name="path">Path where the script should be created.</param>
        /// <param name="createMultipleFiles">Indicates the script should be created in its own file.</param>
        public static void CreateUserQueries(string databaseName, string grantLoginName, string path, bool createMultipleFiles)
        {
            if (grantLoginName.Length > 0)
            {
                string fileName;

                // Determine the file name to be used
                if (createMultipleFiles)
                {
                    fileName = path + "GrantUserPermissions.sql";
                }
                else
                {
                    fileName = path + "StoredProcedures.sql";
                }

                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.Write(Utility.GetUserQueries(databaseName, grantLoginName));
                }
            }
        }

        /// <summary>
        /// Creates an insert stored procedure SQL script for the specified table
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
        /// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
        /// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
        /// <param name="path">Path where the stored procedure script should be created.</param>
        /// <param name="createMultipleFiles">Indicates the procedure(s) generated should be created in its own file.</param>
        public static void CreateInsertStoredProcedure(Table table, string grantLoginName, string storedProcedurePrefix, string path, bool createMultipleFiles)
        {
            // Create the stored procedure name
            string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0,3) + "_" + "SP" + table.Name + "_Insertar";
            string fileName;
            char k = ' ';
            string DataBase = "";
            int maxsize = 0;
            // Determine the file name to be used
            if (createMultipleFiles){
                fileName = path + procedureName + ".sql";
            }else{
                fileName = path + "StoredProcedures.sql";
            }

            using (StreamWriter writer = new StreamWriter(fileName, true)){
                // Create the seperator
                if (createMultipleFiles == false){
                    writer.WriteLine();
                    writer.WriteLine("/******************************************************************************");
                    writer.WriteLine("******************************************************************************/");
                }
                switch (proveedor){ 
                    case Util.Provider.SqlClient:
                        writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                        writer.WriteLine("\tdrop procedure [dbo].[" + procedureName + "]");
                        writer.WriteLine("GO");
                        DataBase = "SQL Server";
                        break;
                    case Util.Provider.MySql:
                        writer.WriteLine("DELIMITER $$");
                        writer.WriteLine("DROP PROCEDURE IF EXISTS " + procedureName + " $$");
                        DataBase = "MySQL";
                        break;
                    case Util.Provider.Oracle:
                        DataBase = "Oracle";
                        break;
                    case Util.Provider.Db2:
                        DataBase = "DB2";
                        break;
                    case Util.Provider.PostgreSQL:
                        DataBase = "PostgreSQL";
                        break;
                }
                // Create the drop statment
                
                writer.WriteLine();
                writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                writer.WriteLine("/*----- Empresa          :                                                  -----*/");
                writer.WriteLine("/*----- Sistema          :                                                  -----*/");
                writer.WriteLine("/*----- Modulo           :                                                  -----*/");
                writer.WriteLine("/*----- Programa         :                                                  -----*/");
                writer.WriteLine("/*----- Nombre           : [" + procedureName + "]" + "".PadRight(50 - procedureName.Length, k) + "-----*/");
                writer.WriteLine("/*----- Proposito        : Procedimiento que inserta un Registro en la Tabla-----*/");
                writer.WriteLine("/*-----                    [" + table.Name + "]" + "".PadRight(47 - table.Name.Length, k) + "-----*/");
                writer.WriteLine("/*----- Desarrollado por : " + SystemInformation.ComputerName + "".PadRight(49 - SystemInformation.ComputerName.Length, k) + "-----*/");
                writer.WriteLine("/*----- Fecha            : " + DateTime.Now.ToShortDateString() + "".PadRight(39, k) + "-----*/");
                writer.WriteLine("/*----- Base de Datos    : " + DataBase + "".PadRight(49 - DataBase.Length,k) + "-----*/");
                writer.WriteLine("/*----- Version BD       :                                                  -----*/");
                writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                writer.WriteLine("/*----- Modificado por   :                                                  -----*/");
                writer.WriteLine("/*----- Fecha de Modific.:                                                  -----*/");
                writer.WriteLine("/*----- Comentarios      :                                                  -----*/");
                writer.WriteLine("/*-----                                                                     -----*/");
                writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                writer.WriteLine();
                // Create the SQL for the stored procedure
                switch (proveedor)
                {
                    case Util.Provider.Access:
                        writer.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
                        break;
                    case Util.Provider.SqlClient:
                        writer.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
                        break;
                    case Util.Provider.Oracle:
                        writer.WriteLine("CREATE OR REPLACE PROCEDURE " + procedureName);
                        break;
                    case Util.Provider.Db2:
                        break;
                    case Util.Provider.MySql:
                        writer.WriteLine("CREATE PROCEDURE " + procedureName);
                        break;
                    case Util.Provider.PostgreSQL:
                        writer.WriteLine("CREATE OR REPLACE FUNCTION " + procedureName);
                        break;
                    default:
                        break;
                }
                
                writer.WriteLine("(");
                // Create the parameter list
                for (int i = 0; i < table.Columns.Count; i++) { 
                    Column column = (Column)table.Columns[i];
                    if (column.Name.Length > maxsize) {
                        maxsize = column.Name.Length;
                    }
                }
                for (int i = 0; i < table.Columns.Count; i++){
                    Column column = (Column)table.Columns[i];
                    if (i == (table.Columns.Count-1)){
                        switch (proveedor){
                            case Util.Provider.Access:
                                writer.WriteLine("" + Utility.CreateParameterString(column, true, maxsize) + "");
                                break;
                            case Util.Provider.SqlClient:
                                writer.WriteLine("" + Utility.CreateParameterString(column, true, maxsize) + "");
                                break;
                            case Util.Provider.Oracle:
                                writer.WriteLine("" + Utility.CreaOracleParametroStore(column, true, maxsize) + "");
                                break;
                            case Util.Provider.Db2:
                                writer.WriteLine("" + Utility.CreaOracleParametroStore(column, true, maxsize) + "");
                                break;
                            case Util.Provider.MySql:
                                writer.WriteLine("" + Utility.CreateParameterMySQLString(column, true, maxsize) + "");
                                break;
                            case Util.Provider.PostgreSQL:
                                writer.WriteLine("" + Utility.CreateParameterPostgreSQLString(column, true,maxsize) + "");
                                break;
                            default:
                                break;
                        }
                    }else{
                        switch (proveedor){
                            case Util.Provider.Access:
                                writer.WriteLine("" + Utility.CreateParameterString(column, true, maxsize) + ",");
                                break;
                            case Util.Provider.SqlClient:
                                writer.WriteLine("" + Utility.CreateParameterString(column, true, maxsize) + ",");
                                break;
                            case Util.Provider.Oracle:
                                writer.WriteLine("" + Utility.CreaOracleParametroStore(column, true, maxsize) + ",");
                                break;
                            case Util.Provider.Db2:
                                writer.WriteLine("" + Utility.CreaOracleParametroStore(column, true, maxsize) + ",");
                                break;
                            case Util.Provider.MySql:
                                writer.WriteLine("" + Utility.CreateParameterMySQLString(column, true, maxsize) + ",");
                                break;
                            case Util.Provider.PostgreSQL:
                                writer.WriteLine("" + Utility.CreateParameterPostgreSQLString(column, true, maxsize) + ",");
                                break;
                            default:
                                break;
                        }
                       
                    }
                }
                writer.WriteLine(")");

                writer.WriteLine();
                                
                switch (proveedor){
                    case Util.Provider.Access:
                        writer.WriteLine("BEGIN");
                        break;
                    case Util.Provider.SqlClient:
                        writer.WriteLine("AS");
                        writer.WriteLine("BEGIN");
                        writer.WriteLine("\tDECLARE @Table_Identity TABLE ( Id_Principal BIGINT )");
                        writer.WriteLine("\tDECLARE @Id_Principal BIGINT");
                        break;
                    case Util.Provider.Oracle:
                        writer.WriteLine("AS");
                        writer.WriteLine("BEGIN");
                        break;
                    case Util.Provider.Db2:
                        writer.WriteLine("BEGIN");
                        break;
                    case Util.Provider.MySql:
                        writer.WriteLine("BEGIN");
                        break;
                    case Util.Provider.PostgreSQL:
                        writer.WriteLine("RETURNS VOID AS");
                        writer.WriteLine("$BODY$");
                        writer.WriteLine("BEGIN");
                        break;
                    default:
                        break;
                }
                
                writer.WriteLine();

                // Initialize all RowGuidCol columns
                foreach (Column column in table.Columns){
                    if (column.IsRowGuidCol){
                        writer.WriteLine("SET @" + column.Name + " = NEWID()");
                        writer.WriteLine();
                        break;
                    }
                }
                switch (proveedor){
                    case Util.Provider.Access:
                        writer.WriteLine("\tINSERT INTO [" + table.Name + "] (");
                        break;
                    case Util.Provider.SqlClient:
                        writer.WriteLine("\tINSERT INTO [" + table.Name + "] (");
                        break;
                    case Util.Provider.Oracle:
                        writer.WriteLine("\tINSERT INTO " + table.Name + "(");
                        break;
                    case Util.Provider.Db2:
                        writer.WriteLine("\tINSERT INTO " + table.Name + "(");
                        break;
                    case Util.Provider.MySql:
                        writer.WriteLine("\tINSERT INTO " + table.Name + "(");
                        break;
                    case Util.Provider.PostgreSQL:
                        writer.WriteLine("\tINSERT INTO " + table.Name + "(");
                        break;
                    default:
                        break;
                }
                

                // Create the parameter list
                for (int i = 0; i < table.Columns.Count; i++){
                    Column column = (Column)table.Columns[i];
                    // Ignore any identity columns
                    if (column.IsIdentity == false){
                        // Append the column name as a parameter of the insert statement
                        if (i < (table.Columns.Count - 1)){
                            switch (proveedor){
                                case Util.Provider.Access:
                                    writer.WriteLine("\t\t[" + column.Name + "],");
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.WriteLine("\t\t[" + column.Name + "],");
                                    break;
                                case Util.Provider.Oracle:
                                    writer.WriteLine("\t\t" + column.Name + ",");
                                    break;
                                case Util.Provider.Db2:
                                    writer.WriteLine("\t\t" + column.Name + ",");
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine("\t\t" + column.Name + ",");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine("\t\t" + column.Name + ",");
                                    break;
                                default:
                                    break;
                            }
                            
                        }else{
                            switch (proveedor){
                                case Util.Provider.Access:
                                    writer.WriteLine("\t\t[" + column.Name + "]");
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.WriteLine("\t\t[" + column.Name + "]");
                                    break;
                                case Util.Provider.Oracle:
                                    writer.WriteLine("\t\t" + column.Name);
                                    break;
                                case Util.Provider.Db2:
                                    writer.WriteLine("\t\t" + column.Name);
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine("\t\t" + column.Name);
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine("\t\t" + column.Name);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                writer.WriteLine("\t)");
                foreach (Column column in table.Columns){
                    // Is the current column an identity column?
                    if (column.IsIdentity){
                        switch (proveedor){
                            case Util.Provider.SqlClient:
                                writer.WriteLine("\tOUTPUT INSERTED." + column.Name + " INTO @Table_Identity");
                                break;
                        }
                        break;
                    }
                }
                writer.WriteLine("\tVALUES (");

                // Create the values list
                for (int i = 0; i < table.Columns.Count; i++){
                    Column column = (Column)table.Columns[i];
                    // Is the current column an identity column?
                    if (column.IsIdentity == false){
                        // Append the necessary line breaks and commas
                        if (i < (table.Columns.Count - 1)){
                            switch (proveedor){
                                case Util.Provider.Access:
                                    writer.WriteLine("\t\t@" + column.Name + ",");
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.WriteLine("\t\t@" + column.Name + ",");
                                    break;
                                case Util.Provider.Oracle:
                                    writer.WriteLine("\t\tp" + column.Name + ",");
                                    break;
                                case Util.Provider.Db2:
                                    writer.WriteLine("\t\t" + column.Name + ",");
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine("\t\tp" + column.Name + ",");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine("\t\tp" + column.Name + ",");
                                    //writer.WriteLine("\tp" + column.Name + ",");
                                    break;
                                default:
                                    break;
                            }
                        }else{
                            switch (proveedor){
                                case Util.Provider.Access:
                                    writer.WriteLine("\t\t@" + column.Name);
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.WriteLine("\t\t@" + column.Name);
                                    break;
                                case Util.Provider.Oracle:
                                    writer.WriteLine("\t\tp" + column.Name);
                                    break;
                                case Util.Provider.Db2:
                                    writer.WriteLine("\t\t" + column.Name);
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine("\t\t" + column.Name);
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine("\t\tp" + column.Name);
                                    //writer.WriteLine("\t" + column.Name);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                switch (proveedor){
                    case Util.Provider.SqlClient:
                        writer.WriteLine("\t)");
                        break;
                    case Util.Provider.Access:
                        writer.WriteLine("\t)");
                        break;
                    case Util.Provider.Oracle:
                        writer.WriteLine("\t);");
                        break;
                    case Util.Provider.Db2:
                        writer.WriteLine("\t)");
                        break;
                    case Util.Provider.MySql:
                        writer.WriteLine("\t);");
                        break;
                    case Util.Provider.PostgreSQL:
                        writer.WriteLine("\t);");
                        break;
                }

                // Should we include a line for returning the identity?
                foreach (Column column in table.Columns){
                    // Is the current column an identity column?
                    if (column.IsIdentity){
                        writer.WriteLine();
                        switch (proveedor) {
                            case Util.Provider.SqlClient:
                                writer.WriteLine("\tSELECT @Id_Principal = Id_Principal FROM @Table_Identity");
                                writer.WriteLine("\tSET @" + column.Name + " = CONVERT(INT, @Id_Principal)");
                                break;
                            case Util.Provider.MySql:
                                writer.WriteLine("\tSET @" + column.Name + " = SCOPE_IDENTITY()");
                                break;
                            case Util.Provider.Oracle:
                                writer.WriteLine("\tSET @" + column.Name + " = SCOPE_IDENTITY()");
                                break;
                            case Util.Provider.PostgreSQL:
                                writer.WriteLine("\tSET @" + column.Name + " = SCOPE_IDENTITY()");
                                break;
                        }
                        break;
                    }
                }

                switch (proveedor){
                    case Util.Provider.SqlClient:
                        writer.WriteLine();
                        writer.WriteLine("END");
                        writer.WriteLine();
                        writer.WriteLine("GO");
                        break;
                    case Util.Provider.MySql:
                        writer.WriteLine();
                        writer.WriteLine("END $$");
                        writer.WriteLine();
                        writer.WriteLine("DELIMITER ;");
                        break;
                    case Util.Provider.Oracle:
                        writer.WriteLine();
                        writer.WriteLine("END;");
                        break;
                    case Util.Provider.PostgreSQL:
                        writer.WriteLine();
                        writer.WriteLine("END;");
                        writer.WriteLine("$BODY$");
                        writer.WriteLine("LANGUAGE plpgsql;");
                        break;
                }
                
                // Create the grant statement, if a user was specified
                if (grantLoginName.Length > 0){
                    writer.WriteLine();
                    writer.WriteLine("GRANT EXECUTE ON [dbo].[" + procedureName + "] TO [" + grantLoginName + "]");
                    writer.WriteLine("GO");
                }
            }
        }

        /// <summary>
        /// Creates an update stored procedure SQL script for the specified table
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
        /// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
        /// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
        /// <param name="path">Path where the stored procedure script should be created.</param>
        /// <param name="createMultipleFiles">Indicates the procedure(s) generated should be created in its own file.</param>
        public static void CreateUpdateStoredProcedure(Table table, string grantLoginName, string storedProcedurePrefix, string path, bool createMultipleFiles)
        {
            if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
            {
                // Create the stored procedure name
                string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "_Actualizar";
                //string procedureName = storedProcedurePrefix + (table.Name.Substring(3, 1) == "_" ? table.Name.Substring(0, 3) + "S" + table.Name.Substring(3, table.Name.Length - 3) : table.Name.Substring(0, 3) + "S" + table.Name.Substring(4, table.Name.Length - 4)) + "_Actualizar";
                string fileName;
                char k = ' ';
                string DataBase = "";
                int maxsize = 0;
                // Determine the file name to be used
                if (createMultipleFiles)
                {
                    fileName = path + procedureName + ".sql";
                }
                else
                {
                    fileName = path + "StoredProcedures.sql";
                }

                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    // Create the seperator
                    if (createMultipleFiles == false)
                    {
                        writer.WriteLine();
                        writer.WriteLine("/******************************************************************************");
                        writer.WriteLine("******************************************************************************/");
                    }

                    // Create the drop statment
                    switch (proveedor)
                    {
                        case Util.Provider.SqlClient:
                            writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                            writer.WriteLine("\tdrop procedure [dbo].[" + procedureName + "]");
                            writer.WriteLine("GO");
                            DataBase = "SQL Server";
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("DELIMITER $$");
                            writer.WriteLine("DROP PROCEDURE IF EXISTS " + procedureName + " $$");
                            DataBase = "MySQL";
                            break;
                        case Util.Provider.Oracle:
                            DataBase = "Oracle";
                            break;
                        case Util.Provider.Db2:
                            DataBase = "DB2";
                            break;
                        case Util.Provider.PostgreSQL:
                            DataBase = "PostgreSQL";
                            break;
                    }
                    writer.WriteLine();
                    writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                    writer.WriteLine("/*----- Empresa          :                                                  -----*/");
                    writer.WriteLine("/*----- Sistema          :                                                  -----*/");
                    writer.WriteLine("/*----- Modulo           :                                                  -----*/");
                    writer.WriteLine("/*----- Programa         :                                                  -----*/");
                    writer.WriteLine("/*----- Nombre           : [" + procedureName + "]" + "-----*/");
                    writer.WriteLine("/*----- Proposito        : Procedimiento que actualiza un Registro en la    -----*/");
                    writer.WriteLine("/*-----                    Tabla [" + table.Name + "]" + "-----*/");
                    writer.WriteLine("/*----- Desarrollado por : " + SystemInformation.ComputerName + "".PadRight(49 - SystemInformation.ComputerName.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Fecha            : " + DateTime.Now.ToShortDateString() + "".PadRight(39, k) + "-----*/");
                    writer.WriteLine("/*----- Base de Datos    : " + DataBase + "".PadRight(49 - DataBase.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Version BD       :                                                  -----*/");
                    writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                    writer.WriteLine("/*----- Modificado por   :                                                  -----*/");
                    writer.WriteLine("/*----- Fecha de Modific.:                                                  -----*/");
                    writer.WriteLine("/*----- Comentarios      :                                                  -----*/");
                    writer.WriteLine("/*-----                                                                     -----*/");
                    writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                    writer.WriteLine();
                    // Create the SQL for the stored procedure
                    switch (proveedor)
                    {
                        case Util.Provider.Access:
                            writer.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
                            break;
                        case Util.Provider.SqlClient:
                            writer.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine("CREATE OR REPLACE PROCEDURE " + procedureName);
                            break;
                        case Util.Provider.Db2:
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("CREATE PROCEDURE " + procedureName);
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("CREATE OR REPLACE FUNCTION " + procedureName);
                            break;
                        default:
                            break;
                    }

                    writer.WriteLine("(");

                    // Create the parameter list
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        Column column = (Column)table.Columns[i];
                        if (column.Name.Length > maxsize)
                        {
                            maxsize = column.Name.Length;
                        }
                    }
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        Column column = (Column)table.Columns[i];

                        if (i == (table.Columns.Count-1))
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.Access:
                                    writer.WriteLine("   " + Utility.CreateParameterString(column, false, maxsize) + "");
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.WriteLine("   " + Utility.CreateParameterString(column, false, maxsize) + "");
                                    break;
                                case Util.Provider.Oracle:
                                    writer.WriteLine("   " + Utility.CreaOracleParametroStore(column, false, maxsize) + "");
                                    break;
                                case Util.Provider.Db2:
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine("   " + Utility.CreateParameterMySQLString(column, true, maxsize) + "");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine("   " + Utility.CreateParameterPostgreSQLString(column, true, maxsize) + "");
                                    break;
                                default:
                                    break;
                            }

                        }
                        else
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.Access:
                                    writer.WriteLine("   " + Utility.CreateParameterString(column, false, maxsize) + ",");
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.WriteLine("   " + Utility.CreateParameterString(column, false, maxsize) + ",");
                                    break;
                                case Util.Provider.Oracle:
                                    writer.WriteLine("   " + Utility.CreaOracleParametroStore(column, false, maxsize) + ",");
                                    break;
                                case Util.Provider.Db2:
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine("   " + Utility.CreateParameterMySQLString(column, true, maxsize) + ",");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine("   " + Utility.CreateParameterPostgreSQLString(column, true, maxsize) + ",");
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                    writer.WriteLine(")");
                    
                    
                    
                    switch (proveedor) {
                        case Util.Provider.SqlClient:
                            writer.WriteLine("AS");
                            writer.WriteLine();
                            writer.WriteLine("BEGIN");
                            writer.WriteLine();
                            writer.WriteLine("UPDATE [" + table.Name + "]");
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("BEGIN");
                            writer.WriteLine();
                            writer.WriteLine("UPDATE " + table.Name + "");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine("AS");
                            writer.WriteLine();
                            writer.WriteLine("BEGIN");
                            writer.WriteLine();
                            writer.WriteLine("UPDATE " + table.Name + "");
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("RETURNS VOID AS ");
                            writer.WriteLine("$BODY$");
                            writer.WriteLine("BEGIN");
                            writer.WriteLine();
                            writer.WriteLine("UPDATE " + table.Name + "");
                            break;
                    }
                    
                    writer.Write("SET ");

                    // Create the set statement
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        Column column = (Column)table.Columns[i];

                        // Ignore Identity and RowGuidCol columns
                        if (table.PrimaryKeys.Contains(column) == false)
                        {
                            if (i < (table.Columns.Count - 1))
                            {
                                switch (proveedor) {
                                    case Util.Provider.SqlClient:
                                        writer.WriteLine("\t[" + column.Name + "] = @" + column.Name + ",");
                                        break;
                                    case Util.Provider.MySql:
                                        writer.WriteLine("\t" + column.Name + " = p" + column.Name + ",");
                                        break;
                                    case Util.Provider.Oracle:
                                        writer.WriteLine("\t" + column.Name + " = p" + column.Name + ",");
                                        break;
                                    case Util.Provider.PostgreSQL:
                                        writer.WriteLine("\t" + column.Name + " = p" + column.Name + ",");
                                        break;
                                    default:
                                        writer.WriteLine("\t[" + column.Name + "] = @" + column.Name + ",");
                                        break;
                                }
                                
                            }
                            else
                            {
                                switch (proveedor)
                                {
                                    case Util.Provider.SqlClient:
                                        writer.WriteLine("\t[" + column.Name + "] = @" + column.Name);
                                        break;
                                    case Util.Provider.MySql:
                                        writer.WriteLine("\t" + column.Name + " = " + column.Name);
                                        break;
                                    case Util.Provider.Oracle:
                                        writer.WriteLine("\t" + column.Name + " = p" + column.Name);
                                        break;
                                    case Util.Provider.PostgreSQL:
                                        writer.WriteLine("\t" + column.Name + " = p" + column.Name);
                                        //writer.WriteLine("\t" + column.Name + " = " + column.Name);
                                        break;
                                    default:
                                        writer.WriteLine("\t[" + column.Name + "] = @" + column.Name);
                                        break;
                                }
                                
                            }
                        }
                    }

                    writer.Write("WHERE");

                    // Create the where clause
                    for (int i = 0; i < table.PrimaryKeys.Count; i++)
                    {
                        Column column = (Column)table.PrimaryKeys[i];

                        if (i > 0)
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.SqlClient:
                                    writer.Write(" AND [" + column.Name + "] = @" + column.Name);
                                    break;
                                case Util.Provider.MySql:
                                    writer.Write(" AND " + column.Name + " = p" + column.Name);
                                    break;
                                case Util.Provider.Oracle:
                                    writer.Write(" AND " + column.Name + " = p" + column.Name);
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.Write(" AND " + column.Name + " = p" + column.Name);
                                    //writer.Write("\tAND " + column.Name + " = p" + column.Name);
                                    break;
                                default:
                                    writer.Write(" AND [" + column.Name + "] = @" + column.Name);
                                    break;
                            }
                            
                        }
                        else
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.SqlClient:
                                    writer.Write(" [" + column.Name + "] = @" + column.Name);
                                    break;
                                case Util.Provider.MySql:
                                    writer.Write(" " + column.Name + " = p" + column.Name);
                                    break;
                                case Util.Provider.Oracle:
                                    writer.Write(" " + column.Name + " = p" + column.Name);
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.Write(" " + column.Name + " = p" + column.Name);
                                    //writer.Write("\t " + column.Name + " = p" + column.Name);
                                    break;
                                default:
                                    writer.Write(" [" + column.Name + "] = @" + column.Name);
                                    break;
                            }
                        }
                        if (i == (table.PrimaryKeys.Count - 1)) {
                            writer.Write(";");
                        }
                    }
                    writer.WriteLine();
                    switch (proveedor)
                    {
                        case Util.Provider.SqlClient:
                            writer.WriteLine();
                            writer.WriteLine("END");
                            writer.WriteLine();
                            writer.WriteLine("GO");
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine();
                            writer.WriteLine("END $$");
                            writer.WriteLine();
                            writer.WriteLine("DELIMITER ;");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine();
                            writer.WriteLine("END;");
                            writer.WriteLine();
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine();
                            writer.WriteLine("END;");
                            writer.WriteLine("$BODY$");
                            writer.WriteLine("LANGUAGE plpgsql;");
                            break;
                    }

                    // Create the grant statement, if a user was specified
                    if (grantLoginName.Length > 0)
                    {
                        writer.WriteLine();
                        writer.WriteLine("GRANT EXECUTE ON [dbo].[" + procedureName + "] TO [" + grantLoginName + "]");
                        writer.WriteLine("GO");
                    }
                }
            }
            else
            {
               
            }
        }

        /// <summary>
        /// Creates an delete stored procedure SQL script for the specified table
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
        /// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
        /// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
        /// <param name="path">Path where the stored procedure script should be created.</param>
        /// <param name="createMultipleFiles">Indicates the procedure(s) generated should be created in its own file.</param>
        public static void CreateDeleteStoredProcedure(Table table, string grantLoginName, string storedProcedurePrefix, string path, bool createMultipleFiles)
        {
            if (table.PrimaryKeys.Count > 0)
            {
                // Create the stored procedure name
                string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "_Eliminar";
                //string procedureName = storedProcedurePrefix + (table.Name.Substring(3, 1) == "_" ? table.Name.Substring(0, 3) + "S" + table.Name.Substring(3, table.Name.Length - 3) : table.Name.Substring(0, 3) + "S" + table.Name.Substring(4, table.Name.Length - 4)) + "_Eliminar";
                string fileName;
                char k = ' ';
                string DataBase = "";
                int maxsize = 0;
                // Determine the file name to be used
                if (createMultipleFiles)
                {
                    fileName = path + procedureName + ".sql";
                }
                else
                {
                    fileName = path + "StoredProcedures.sql";
                }

                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    // Create the seperator
                    if (createMultipleFiles == false)
                    {
                        writer.WriteLine();
                        writer.WriteLine("/******************************************************************************");
                        writer.WriteLine("******************************************************************************/");
                    }
                    switch (proveedor)
                    {
                        case Util.Provider.SqlClient:
                            writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                            writer.WriteLine("\tdrop procedure [dbo].[" + procedureName + "]");
                            writer.WriteLine("GO");
                            DataBase = "SQL Server";
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("DELIMITER $$");
                            writer.WriteLine("DROP PROCEDURE IF EXISTS " + procedureName + " $$");
                            DataBase = "MySQL";
                            break;
                        case Util.Provider.Oracle:
                            DataBase = "Oracle";
                            break;
                        case Util.Provider.Db2:
                            DataBase = "DB2";
                            break;
                        case Util.Provider.PostgreSQL:
                            DataBase = "PostgreSQL";
                            break;
                    }
                    // Create the drop statment
                    //writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                    //writer.WriteLine("\tdrop procedure [dbo].[" + procedureName + "]");
                    //writer.WriteLine("GO");
                    //writer.WriteLine();
                    writer.WriteLine();
                    writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                    writer.WriteLine("/*----- Empresa          :                                                  -----*/");
                    writer.WriteLine("/*----- Sistema          :                                                  -----*/");
                    writer.WriteLine("/*----- Modulo           :                                                  -----*/");
                    writer.WriteLine("/*----- Programa         :                                                  -----*/");
                    writer.WriteLine("/*----- Nombre           : [" + procedureName + "]" + "".PadRight(50 - procedureName.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Proposito        : Procedimiento que elimina un Registro en la Tabla-----*/");
                    writer.WriteLine("/*-----                    [" + table.Name + "]" + "".PadRight(47 - table.Name.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Desarrollado por : " + SystemInformation.ComputerName + "".PadRight(49 - SystemInformation.ComputerName.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Fecha            : " + DateTime.Now.ToShortDateString() + "".PadRight(39, k) + "-----*/");
                    writer.WriteLine("/*----- Base de Datos    : " + DataBase + "".PadRight(49 - DataBase.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Version BD       :                                                  -----*/");
                    writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                    writer.WriteLine("/*----- Modificado por   :                                                  -----*/");
                    writer.WriteLine("/*----- Fecha de Modific.:                                                  -----*/");
                    writer.WriteLine("/*----- Comentarios      :                                                  -----*/");
                    writer.WriteLine("/*-----                                                                     -----*/");
                    writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                    writer.WriteLine();
                    // Create the SQL for the stored procedure

                    switch (proveedor)
                    {
                        case Util.Provider.Access:
                            writer.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
                            break;
                        case Util.Provider.SqlClient:
                            writer.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine("CREATE OR REPLACE PROCEDURE " + procedureName);
                            break;
                        case Util.Provider.Db2:
                            writer.WriteLine("CREATE PROCEDURE " + procedureName);
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("CREATE PROCEDURE " + procedureName);
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("CREATE OR REPLACE FUNCTION " + procedureName);
                            break;
                        default:
                            break;
                    }
                    
                    writer.WriteLine("(");

                    for (int i = 0; i < table.PrimaryKeys.Count; i++)
                    {
                        Column column = (Column)table.PrimaryKeys[i];
                        if (column.Name.Length > maxsize)
                        {
                            maxsize = column.Name.Length;
                        }
                    }
                    // Create the parameter list
                    for (int i = 0; i < table.PrimaryKeys.Count; i++)
                    {
                        Column column = (Column)table.PrimaryKeys[i];

                        if (i == (table.PrimaryKeys.Count-1))
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.Access:
                                    writer.WriteLine("   " + Utility.CreateParameterString(column, false, maxsize) + "");
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.WriteLine("   " + Utility.CreateParameterString(column, false, maxsize) + "");
                                    break;
                                case Util.Provider.Oracle:
                                    writer.WriteLine("   " + Utility.CreaOracleParametroStore(column, false, maxsize) + "");
                                    break;
                                case Util.Provider.Db2:
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine("   " + Utility.CreateParameterMySQLString(column, true, maxsize) + "");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine("   " + Utility.CreateParameterPostgreSQLString(column, true, maxsize) + "");
                                    break;
                                default:
                                    break;
                            }
                            
                        }
                        else
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.Access:
                                    writer.WriteLine("   " + Utility.CreateParameterString(column, false, maxsize) + ",");
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.WriteLine("   " + Utility.CreateParameterString(column, false, maxsize) + ",");
                                    break;
                                case Util.Provider.Oracle:
                                    writer.WriteLine("   " + Utility.CreaOracleParametroStore(column, false, maxsize) + ",");
                                    break;
                                case Util.Provider.Db2:
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine("   " + Utility.CreateParameterMySQLString(column, true, maxsize) + ",");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine("   " + Utility.CreateParameterPostgreSQLString(column, true, maxsize) + ",");
                                    break;
                                default:
                                    break;
                            }
                            
                        }
                    }
                    writer.WriteLine(")");
                    writer.WriteLine();

                    switch (proveedor)
                    {
                        case Util.Provider.Access:
                            break;
                        case Util.Provider.SqlClient:
                            writer.WriteLine("AS");
                            writer.WriteLine("BEGIN");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine("AS");
                            writer.WriteLine("BEGIN");
                            break;
                        case Util.Provider.Db2:
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("BEGIN");
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("RETURNS VOID AS");
                            writer.WriteLine("$BODY$");
                            writer.WriteLine("BEGIN");
                            break;
                        default:
                            break;
                    }
                    
                    writer.WriteLine();
                    switch (proveedor)
                    {
                        case Util.Provider.Access:
                            writer.WriteLine("DELETE FROM [" + table.Name + "]");
                            break;
                        case Util.Provider.SqlClient:
                            writer.WriteLine("DELETE FROM [" + table.Name + "]");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine("DELETE FROM " + table.Name + "");
                            break;
                        case Util.Provider.Db2:
                            writer.WriteLine("DELETE FROM " + table.Name + "");
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("DELETE FROM " + table.Name + "");
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("DELETE FROM " + table.Name + "");
                            break;
                        default:
                            break;
                    }
                    
                    writer.Write("WHERE");

                    // Create the where clause
                    for (int i = 0; i < table.PrimaryKeys.Count; i++)
                    {
                        Column column = (Column)table.PrimaryKeys[i];

                        if (i > 0)
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.Access:
                                    writer.Write(" AND [" + column.Name + "] = @" + column.Name);
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.Write(" AND [" + column.Name + "] = @" + column.Name);
                                    break;
                                case Util.Provider.Oracle:
                                    writer.Write(" AND " + column.Name + " = p" + column.Name);
                                    break;
                                case Util.Provider.Db2:
                                    writer.Write(" AND " + column.Name + " = P" + column.Name);
                                    break;
                                case Util.Provider.MySql:
                                    writer.Write(" AND " + column.Name + " = p" + column.Name);
                                    break;
                                case Util.Provider.PostgreSQL:
                                    //writer.Write("\tAND " + column.Name + " = $" + (i + 1));
                                    writer.Write(" AND " + column.Name + " = p" + column.Name);
                                    break;
                                default:
                                    break;
                            }
                            
                        }
                        else
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.Access:
                                    writer.Write(" [" + column.Name + "] = @" + column.Name);
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.Write(" [" + column.Name + "] = @" + column.Name);
                                    break;
                                case Util.Provider.Oracle:
                                    writer.Write(" " + column.Name + " = p" + column.Name);
                                    break;
                                case Util.Provider.Db2:
                                    writer.Write(" " + column.Name + " = P" + column.Name);
                                    break;
                                case Util.Provider.MySql:
                                    writer.Write(" " + column.Name + " = p" + column.Name);
                                    break;
                                case Util.Provider.PostgreSQL:
                                    //writer.Write("\t" + column.Name + " = $" + (i + 1));
                                    writer.Write(" " + column.Name + " = p" + column.Name);
                                    break;
                                default:
                                    break;
                            }
                            
                        }
                        if (i == (table.PrimaryKeys.Count - 1))
                        {
                            writer.Write(";");
                        }
                    }
                    writer.WriteLine();
                    switch (proveedor)
                    {
                        case Util.Provider.SqlClient:
                            writer.WriteLine();
                            writer.WriteLine("END");
                            writer.WriteLine();
                            writer.WriteLine("GO");
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine();
                            writer.WriteLine("END $$");
                            writer.WriteLine();
                            writer.WriteLine("DELIMITER ;");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine();
                            writer.WriteLine("END;");
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine();
                            writer.WriteLine("END;");
                            writer.WriteLine("$BODY$");
                            writer.WriteLine("LANGUAGE plpgsql;");
                            break;
                    }

                    // Create the grant statement, if a user was specified
                    if (grantLoginName.Length > 0)
                    {
                        writer.WriteLine();
                        writer.WriteLine("GRANT EXECUTE ON [dbo].[" + procedureName + "] TO [" + grantLoginName + "]");
                        writer.WriteLine("GO");
                    }
                }
            }
        }

        /// <summary>
        /// Creates one or more delete stored procedures SQL script for the specified table and its foreign keys
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
        /// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
        /// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
        /// <param name="path">Path where the stored procedure script should be created.</param>
        /// <param name="createMultipleFiles">Indicates the procedure(s) generated should be created in its own file.</param>
        public static void CreateDeleteAllByStoredProcedures(Table table, string grantLoginName, string storedProcedurePrefix, string path, bool createMultipleFiles)
        {
            // Create a stored procedure for each foreign key
            int maxsize = 0;
            foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
            {
                // Create the stored procedure name
                StringBuilder stringBuilder = new StringBuilder(255);
                stringBuilder.Append(storedProcedurePrefix + (table.Name.Substring(3, 1) == "_" ? table.Name.Substring(0, 3) + "S" + table.Name.Substring(3, table.Name.Length - 3) : table.Name.Substring(0, 3) + "S" + table.Name.Substring(4, table.Name.Length - 4)) + "_EliminarTodosPor");

                // Create the parameter list
                for (int i = 0; i < compositeKeyList.Count; i++)
                {
                    Column column = (Column)compositeKeyList[i];
                    if (i > 0)
                    {
                        stringBuilder.Append("_" + Utility.FormatPascal(column.Name));
                    }
                    else
                    {
                        stringBuilder.Append(Utility.FormatPascal(column.Name));
                    }
                }

                string procedureName = stringBuilder.ToString();
                string fileName;

                // Determine the file name to be used
                if (createMultipleFiles)
                {
                    fileName = path + procedureName + ".sql";
                }
                else
                {
                    fileName = path + "StoredProcedures.sql";
                }

                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    // Create the seperator
                    if (createMultipleFiles == false)
                    {
                        writer.WriteLine();
                        writer.WriteLine("/******************************************************************************");
                        writer.WriteLine("******************************************************************************/");
                    }

                    // Create the drop statment
                    writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                    writer.WriteLine("\tdrop procedure [dbo].[" + procedureName + "]");
                    writer.WriteLine("GO");
                    writer.WriteLine();

                    // Create the SQL for the stored procedure
                    writer.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "] (");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        Column column = (Column)table.Columns[i];
                        if (column.Name.Length > maxsize)
                        {
                            maxsize = column.Name.Length;
                        }
                    }
                    // Create the parameter list
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i < (compositeKeyList.Count - 1))
                        {
                            writer.WriteLine("\t" + Utility.CreateParameterString(column, false, maxsize) + ",");
                        }
                        else
                        {
                            writer.WriteLine("\t" + Utility.CreateParameterString(column, false, maxsize));
                        }
                    }
                    writer.WriteLine(")");

                    writer.WriteLine();
                    writer.WriteLine("AS");
                    writer.WriteLine();
                    writer.WriteLine("SET NOCOUNT ON");
                    writer.WriteLine();
                    writer.WriteLine("DELETE FROM");
                    writer.WriteLine("\t[" + table.Name + "]");
                    writer.WriteLine("WHERE");

                    // Create the where clause
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            writer.WriteLine("\tAND [" + column.Name + "] = @" + column.Name);
                        }
                        else
                        {
                            writer.WriteLine("\t[" + column.Name + "] = @" + column.Name);
                        }
                    }

                    writer.WriteLine("GO");

                    // Create the grant statement, if a user was specified
                    if (grantLoginName.Length > 0)
                    {
                        writer.WriteLine();
                        writer.WriteLine("GRANT EXECUTE ON [dbo].[" + procedureName + "] TO [" + grantLoginName + "]");
                        writer.WriteLine("GO");
                    }
                }
            }
        }

        /// <summary>
        /// Creates an select stored procedure SQL script for the specified table
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
        /// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
        /// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
        /// <param name="path">Path where the stored procedure script should be created.</param>
        /// <param name="createMultipleFiles">Indicates the procedure(s) generated should be created in its own file.</param>
        public static void CreateSelectStoredProcedure(Table table, string grantLoginName, string storedProcedurePrefix, string path, bool createMultipleFiles)
        {
            if (table.PrimaryKeys.Count > 0 && table.ForeignKeys.Count != table.Columns.Count)
            {
                // Create the stored procedure name
                string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "_Selecciona";
                //string procedureName = storedProcedurePrefix + (table.Name.Substring(3, 1) == "_" ? table.Name.Substring(0, 3) + "S" + table.Name.Substring(3, table.Name.Length - 3) : table.Name.Substring(0, 3) + "S" + table.Name.Substring(4, table.Name.Length - 4)) + "_Seleccionar";
                string fileName;
                char k = ' ';
                string DataBase = "";
                string espacio = "       ";
                int maxsize = 0;
                // Determine the file name to be used
                if (createMultipleFiles)
                {
                    fileName = path + procedureName + ".sql";
                }
                else
                {
                    fileName = path + "StoredProcedures.sql";
                }

                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    // Create the seperator
                    if (createMultipleFiles == false)
                    {
                        writer.WriteLine();
                        writer.WriteLine("/******************************************************************************");
                        writer.WriteLine("******************************************************************************/");
                    }
                    // Create the drop statment
                    switch (proveedor)
                    {
                        case Util.Provider.SqlClient:
                            writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                            writer.WriteLine("\tdrop procedure [dbo].[" + procedureName + "]");
                            writer.WriteLine("GO");
                            DataBase = "SQL Server";
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("DELIMITER $$");
                            writer.WriteLine("DROP PROCEDURE IF EXISTS " + procedureName + " $$");
                            DataBase = "MySQL";
                            break;
                        case Util.Provider.Oracle:
                            DataBase = "Oracle";
                            break;
                        case Util.Provider.Db2:
                            DataBase = "DB2";
                            break;
                        case Util.Provider.PostgreSQL:
                            DataBase = "PostgreSQL";
                            break;
                    }
                    
                    writer.WriteLine();
                    writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                    writer.WriteLine("/*----- Empresa          :                                                  -----*/");
                    writer.WriteLine("/*----- Sistema          :                                                  -----*/");
                    writer.WriteLine("/*----- Modulo           :                                                  -----*/");
                    writer.WriteLine("/*----- Programa         :                                                  -----*/");
                    writer.WriteLine("/*----- Nombre           : [" + procedureName + "]" + "-----*/");
                    writer.WriteLine("/*----- Proposito        : Procedimiento que selecciona un Registro en la   -----*/");
                    writer.WriteLine("/*-----                    Tabla [" + table.Name + "]" + "".PadRight(41 - table.Name.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Desarrollado por : " + SystemInformation.ComputerName + "".PadRight(49 - SystemInformation.ComputerName.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Fecha            : " + DateTime.Now.ToShortDateString() + "".PadRight(39, k) + "-----*/");
                    writer.WriteLine("/*----- Base de Datos    : " + DataBase + "".PadRight(49 - DataBase.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Version BD       :                                                  -----*/");
                    writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                    writer.WriteLine("/*----- Modificado por   :                                                  -----*/");
                    writer.WriteLine("/*----- Fecha de Modific.:                                                  -----*/");
                    writer.WriteLine("/*----- Comentarios      :                                                  -----*/");
                    writer.WriteLine("/*-----                                                                     -----*/");
                    writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                    writer.WriteLine();
                    // Create the SQL for the stored procedure
                    switch (proveedor)
                    {
                        case Util.Provider.Access:
                            writer.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
                            break;
                        case Util.Provider.SqlClient:
                            writer.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine("CREATE OR REPLACE PROCEDURE " + procedureName);
                            break;
                        case Util.Provider.Db2:
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("CREATE PROCEDURE " + procedureName);
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("CREATE OR REPLACE FUNCTION " + procedureName);
                            break;
                        default:
                            break;
                    }
                    writer.WriteLine("(");

                    for (int i = 0; i < table.PrimaryKeys.Count; i++)
                    {
                        Column column = (Column)table.PrimaryKeys[i];
                        if (column.Name.Length > maxsize)
                        {
                            maxsize = column.Name.Length;
                        }
                    }
                    // Create the parameter list
                    for (int i = 0; i < table.PrimaryKeys.Count; i++)
                    {
                        Column column = (Column)table.PrimaryKeys[i];

                        if (i == (table.PrimaryKeys.Count - 1))
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.Access:
                                    writer.WriteLine("   " + Utility.CreateParameterString(column, false, maxsize));
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.WriteLine("   " + Utility.CreateParameterString(column, false, maxsize));
                                    break;
                                case Util.Provider.Oracle:
                                    writer.WriteLine("   " + Utility.CreaOracleParametroStore(column, false, maxsize) + ",");
                                    writer.WriteLine("   "+table.Name+"_DATA OUT SYS_REFCURSOR");
                                    break;
                                case Util.Provider.Db2:
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine("   " + Utility.CreateParameterMySQLString(column, false, maxsize) + "");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine("   " + Utility.CreateParameterPostgreSQLString(column, false, maxsize) + "");
                                    break;
                                default:
                                    break;
                            }
                            
                        }
                        else
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.Access:
                                    writer.WriteLine("   " + Utility.CreateParameterString(column, false, maxsize) + ",");
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.WriteLine("   " + Utility.CreateParameterString(column, false, maxsize) + ",");
                                    break;
                                case Util.Provider.Oracle:
                                    writer.WriteLine("   " + Utility.CreaOracleParametroStore(column, false, maxsize) + ",");
                                    break;
                                case Util.Provider.Db2:
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine("   " + Utility.CreateParameterMySQLString(column, false, maxsize) + ",");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine("   " + Utility.CreateParameterPostgreSQLString(column, false, maxsize) + ",");
                                    break;
                                default:
                                    break;
                            }
                            
                        }
                    }

                    writer.WriteLine(")");

                    
                    switch (proveedor)
                    {
                        case Util.Provider.SqlClient:
                            writer.WriteLine();
                            writer.WriteLine("AS");
                            writer.WriteLine("BEGIN");
                            writer.WriteLine();
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine();
                            writer.WriteLine("BEGIN");
                            writer.WriteLine();
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine();
                            writer.WriteLine("AS");
                            writer.WriteLine("BEGIN");
                            writer.WriteLine();
                            writer.WriteLine("OPEN " + table.Name + "_DATA FOR");
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("RETURNS SETOF " + table.Name + " AS ");
                            writer.WriteLine("$BODY$");
                            writer.WriteLine("DECLARE");
                            writer.WriteLine("" + table.Name+ "_DATA RECORD;");
                            writer.WriteLine("BEGIN");
                            writer.WriteLine();
                            writer.WriteLine("FOR " +table.Name+ "_DATA IN");
                            break;
                    }
                    
                    
                    writer.Write("SELECT ");

                    // Create the list of columns
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        Column column = (Column)table.Columns[i];
                        if (i == (table.Columns.Count-1))
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.SqlClient:
                                    writer.WriteLine(espacio+"[" + column.Name + "]");
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine(espacio + column.Name + "");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine(espacio + column.Name + "");
                                    break;
                                default:
                                    writer.WriteLine(espacio + column.Name + "");
                                    break;
                            }
                        }
                        else
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.SqlClient:
                                    if ((column.Type.ToLower() == "char"))
                                    {
                                        writer.WriteLine((i != 0 ? espacio : "") + "RTRIM(ISNULL([" + column.Name + "],'')) " + column.Name + ",");
                                    }
                                    else if ((column.Type.ToLower() == "varchar"))
                                    {
                                        writer.WriteLine((i != 0 ? espacio : "") + "RTRIM(ISNULL([" + column.Name + "],'')) " + column.Name + ",");
                                    }
                                    else {
                                        writer.WriteLine((i != 0 ? espacio : "") + "[" + column.Name + "],");
                                    }
                                    
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine((i != 0 ? espacio : "") + column.Name + ",");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine((i!=0?espacio:"")+ column.Name + ",");
                                    break;
                                default:
                                    writer.WriteLine((i!=0?espacio:"")+ column.Name + ",");
                                    break;
                            }
                        }

                        //if (i < (table.Columns.Count - 1)) {
                        //	writer.WriteLine("\t[" + column.Name + "]");
                        //} else {
                        //	writer.WriteLine("\t,[" + column.Name + "]");
                        //}
                    }

                    switch (proveedor)
                    {
                        case Util.Provider.SqlClient:
                            writer.WriteLine("FROM [" + table.Name + "]");
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("FROM " + table.Name + "");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine("FROM " + table.Name + "");
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("FROM " + table.Name + "");
                            break;
                        default:
                            writer.WriteLine("FROM [" + table.Name + "]");
                            break;
                    }

                    
                    writer.Write("WHERE");

                    // Create the where clause
                    for (int i = 0; i < table.PrimaryKeys.Count; i++)
                    {
                        Column column = (Column)table.PrimaryKeys[i];

                        if (i > 0)
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.SqlClient:
                                    writer.Write(" AND [" + column.Name + "] = @" + column.Name);
                                    break;
                                case Util.Provider.MySql:
                                    writer.Write(" AND " + column.Name + " = p" + column.Name);
                                    break;
                                case Util.Provider.PostgreSQL:
                                    //writer.Write("\tAND " + column.Name + " = $" + (i + 1));
                                    writer.Write(" AND " + column.Name + " = p" + column.Name);
                                    break;
                                default:
                                    writer.Write(" AND " + column.Name + " = p" + column.Name);
                                    break;
                            }
                        }
                        else
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.SqlClient:
                                    writer.Write(" [" + column.Name + "] = @" + column.Name);
                                    break;
                                case Util.Provider.MySql:
                                    writer.Write(" " + column.Name + " = p" + column.Name);
                                    break;
                                case Util.Provider.PostgreSQL:
                                    //writer.Write("\t" + column.Name + " = $" + (i + 1));
                                    writer.Write(" " + column.Name + " = p" + column.Name);
                                    break;
                                default:
                                    writer.Write(" " + column.Name + " = p" + column.Name);
                                    break;
                            }
                        }
                        if (i == (table.PrimaryKeys.Count - 1)) {
                            switch (proveedor) { 
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine();
                                    writer.WriteLine("LOOP");
                                    break;
                                default:
                                    writer.WriteLine(";");
                                    break;
                            }
                            
                        }
                    }

                    switch (proveedor)
                    {
                        case Util.Provider.SqlClient:
                            writer.WriteLine();
                            writer.WriteLine("END");
                            writer.WriteLine();
                            writer.WriteLine("GO");
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine();
                            writer.WriteLine("END $$");
                            writer.WriteLine();
                            writer.WriteLine("DELIMITER ;");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine();
                            writer.WriteLine("END;");
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("RETURN NEXT " + table.Name + "_DATA;");
                            writer.WriteLine("END LOOP;");
                            writer.WriteLine();
                            writer.WriteLine("END;");
                            writer.WriteLine("$BODY$");
                            writer.WriteLine("LANGUAGE plpgsql;");
                            break;
                    }

                    // Create the grant statement, if a user was specified
                    if (grantLoginName.Length > 0)
                    {
                        writer.WriteLine();
                        writer.WriteLine("GRANT EXECUTE ON [dbo].[" + procedureName + "] TO [" + grantLoginName + "]");
                        writer.WriteLine("GO");
                    }
                }
            }
        }

        /// <summary>
        /// Creates an select all stored procedure SQL script for the specified table
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
        /// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
        /// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
        /// <param name="path">Path where the stored procedure script should be created.</param>
        /// <param name="createMultipleFiles">Indicates the procedure(s) generated should be created in its own file.</param>
        public static void CreateSelectAllStoredProcedure(Table table, string grantLoginName, string storedProcedurePrefix, string path, bool createMultipleFiles)
        {
            if (table.PrimaryKeys.Count > 0 && table.ForeignKeys.Count != table.Columns.Count){
                // Create the stored procedure name
                string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "_Listar";
                //string procedureName = storedProcedurePrefix + (table.Name.Substring(3, 1) == "_" ? table.Name.Substring(0, 3) + "S" + table.Name.Substring(3, table.Name.Length - 3) : table.Name.Substring(0, 3) + "S" + table.Name.Substring(4, table.Name.Length - 4)) + "_Listar";
                string fileName;
                char k = ' ';
                string DataBase = "";
                string espacio = "       ";
                // Determine the file name to be used
                if (createMultipleFiles)
                {
                    fileName = path + procedureName + ".sql";
                }
                else
                {
                    fileName = path + "StoredProcedures.sql";
                }

                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    // Create the seperator
                    if (createMultipleFiles == false)
                    {
                        writer.WriteLine();
                        writer.WriteLine("/******************************************************************************");
                        writer.WriteLine("******************************************************************************/");
                    }
                    // Create the drop statment
                    switch (proveedor)
                    {
                        case Util.Provider.SqlClient:
                            writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                            writer.WriteLine("\tdrop procedure [dbo].[" + procedureName + "]");
                            writer.WriteLine("GO");
                            DataBase = "SQL Server";
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("DELIMITER $$");
                            writer.WriteLine("DROP PROCEDURE IF EXISTS " + procedureName + " $$");
                            DataBase = "MySQL";
                            break;
                        case Util.Provider.Oracle:
                            DataBase = "Oracle";
                            break;
                        case Util.Provider.Db2:
                            DataBase = "DB2";
                            break;
                        case Util.Provider.PostgreSQL:
                            DataBase = "PostgreSQL";
                            break;
                    }
                    
                    writer.WriteLine();
                    writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                    writer.WriteLine("/*----- Empresa          :                                                  -----*/");
                    writer.WriteLine("/*----- Sistema          :                                                  -----*/");
                    writer.WriteLine("/*----- Modulo           :                                                  -----*/");
                    writer.WriteLine("/*----- Programa         :                                                  -----*/");
                    writer.WriteLine("/*----- Nombre           : [" + procedureName + "]" + "".PadRight(50 - procedureName.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Proposito        : Procedimiento que lista todos los Registros de la-----*/");
                    writer.WriteLine("/*-----                    Tabla [" + table.Name + "]" + "".PadRight(41 - table.Name.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Desarrollado por : " + SystemInformation.ComputerName + "".PadRight(49 - SystemInformation.ComputerName.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Fecha            : " + DateTime.Now.ToShortDateString() + "".PadRight(39, k) + "-----*/");
                    writer.WriteLine("/*----- Base de Datos    : " + DataBase + "".PadRight(49 - DataBase.Length, k) + "-----*/");
                    writer.WriteLine("/*----- Version BD       :                                                  -----*/");
                    writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                    writer.WriteLine("/*----- Modificado por   :                                                  -----*/");
                    writer.WriteLine("/*----- Fecha de Modific.:                                                  -----*/");
                    writer.WriteLine("/*----- Comentarios      :                                                  -----*/");
                    writer.WriteLine("/*-----                                                                     -----*/");
                    writer.WriteLine("/*-------------------------------------------------------------------------------*/");
                    writer.WriteLine();
                    // Create the SQL for the stored procedure
                    switch (proveedor)
                    {
                        case Util.Provider.Access:
                            writer.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
                            break;
                        case Util.Provider.SqlClient:
                            writer.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "]");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine("CREATE OR REPLACE PROCEDURE " + procedureName+" ("+table.Name+"_DATA OUT SYS_REFCURSOR)");
                            break;
                        case Util.Provider.Db2:
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("CREATE PROCEDURE " + procedureName);
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("CREATE OR REPLACE FUNCTION " + procedureName+"()");
                            break;
                        default:
                            break;
                    }
                    
                    switch (proveedor)
                    {
                        case Util.Provider.SqlClient:
                            writer.WriteLine();
                            writer.WriteLine("AS");
                            writer.WriteLine("BEGIN");
                            writer.WriteLine();
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine();
                            writer.WriteLine("BEGIN");
                            writer.WriteLine();
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine();
                            writer.WriteLine("AS");
                            writer.WriteLine("BEGIN");
                            writer.WriteLine();
                            writer.WriteLine("OPEN " + table.Name + "_DATA FOR");
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("RETURNS SETOF " + table.Name + " AS ");
                            writer.WriteLine("$BODY$");
                            writer.WriteLine("DECLARE");
                            writer.WriteLine("" + table.Name + "_DATA RECORD;");
                            writer.WriteLine("BEGIN");
                            writer.WriteLine();
                            writer.WriteLine("FOR " + table.Name + "_DATA IN");
                            break;
                    }
                    
                    writer.Write("SELECT ");

                    // Create the list of columns
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        Column column = (Column)table.Columns[i];

                        if (i == (table.Columns.Count-1))
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.Access:
                                    writer.WriteLine(espacio+"[" + column.Name + "]");
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.WriteLine(espacio + "[" + column.Name + "]");
                                    break;
                                case Util.Provider.Oracle:
                                    writer.WriteLine(espacio + "" + column.Name + "");
                                    break;
                                case Util.Provider.Db2:
                                    writer.WriteLine(espacio + "" + column.Name + "");
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine(espacio + "" + column.Name + "");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine(espacio + "" + column.Name + "");
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            switch (proveedor)
                            {
                                case Util.Provider.Access:
                                    writer.WriteLine((i != 0 ? espacio : "") + "[" + column.Name + "],");
                                    break;
                                case Util.Provider.SqlClient:
                                    writer.WriteLine((i != 0 ? espacio : "") + "[" + column.Name + "],");
                                    break;
                                case Util.Provider.Oracle:
                                    writer.WriteLine((i != 0 ? espacio : "") + "" + column.Name + ",");
                                    break;
                                case Util.Provider.Db2:
                                    writer.WriteLine((i != 0 ? espacio : "") + "" + column.Name + ",");
                                    break;
                                case Util.Provider.MySql:
                                    writer.WriteLine((i != 0 ? espacio : "") + "" + column.Name + ",");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    writer.WriteLine((i != 0 ? espacio : "") + "" + column.Name + ",");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    switch (proveedor)
                    {
                        case Util.Provider.Access:
                            writer.WriteLine("FROM [" + table.Name + "]");
                            break;
                        case Util.Provider.SqlClient:
                            writer.WriteLine("FROM [" + table.Name + "]");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine("FROM " + table.Name + ";");
                            break;
                        case Util.Provider.Db2:
                            writer.WriteLine("FROM " + table.Name + "");
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine("FROM " + table.Name + ";");
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("FROM " + table.Name + "");
                            writer.WriteLine("LOOP");
                            break;
                        default:
                            break;
                    }

                    switch (proveedor)
                    {
                        case Util.Provider.Access:
                            writer.WriteLine();
                            writer.WriteLine("END");
                            writer.WriteLine("GO");
                            break;
                        case Util.Provider.SqlClient:
                            writer.WriteLine();
                            writer.WriteLine("END");
                            writer.WriteLine("GO");
                            break;
                        case Util.Provider.Oracle:
                            writer.WriteLine();
                            writer.WriteLine("END;");
                            break;
                        case Util.Provider.Db2:
                            writer.WriteLine();
                            writer.WriteLine("END");
                            writer.WriteLine("GO");
                            break;
                        case Util.Provider.PostgreSQL:
                            writer.WriteLine("RETURN NEXT " + table.Name + "_DATA;");
                            writer.WriteLine("END LOOP;");
                            writer.WriteLine();
                            writer.WriteLine("END;");
                            writer.WriteLine("$BODY$");
                            writer.WriteLine("LANGUAGE plpgsql;");
                            break;
                        case Util.Provider.MySql:
                            writer.WriteLine();
                            writer.WriteLine("END $$");
                            writer.WriteLine();
                            writer.WriteLine("DELIMITER ;");
                            break;
                        default:
                            break;
                    }
                    

                    // Create the grant statement, if a user was specified
                    if (grantLoginName.Length > 0)
                    {
                        writer.WriteLine();
                        writer.WriteLine("GRANT EXECUTE ON [dbo].[" + procedureName + "] TO [" + grantLoginName + "]");
                        writer.WriteLine("GO");
                    }
                }
            }
        }

        /// <summary>
        /// Creates one or more select stored procedures SQL script for the specified table and its foreign keys
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
        /// <param name="grantLoginName">Name of the SQL Server user that should have execute rights on the stored procedure.</param>
        /// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
        /// <param name="path">Path where the stored procedure script should be created.</param>
        /// <param name="createMultipleFiles">Indicates the procedure(s) generated should be created in its own file.</param>
        public static void CreateSelectAllByStoredProcedures(Table table, string grantLoginName, string storedProcedurePrefix, string path, bool createMultipleFiles)
        {
            // Create a stored procedure for each foreign key
            int maxsize = 0;
            foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
            {
                // Create the stored procedure name
                StringBuilder stringBuilder = new StringBuilder(255);
                stringBuilder.Append(storedProcedurePrefix + (table.Name.Substring(3, 1) == "_" ? table.Name.Substring(0, 3) + "S" + table.Name.Substring(3, table.Name.Length - 3) : table.Name.Substring(0, 3) + "S" + table.Name.Substring(4, table.Name.Length - 4)) + "_ListarTodosPor");

                // Create the parameter list
                for (int i = 0; i < compositeKeyList.Count; i++)
                {
                    Column column = (Column)compositeKeyList[i];
                    if (i > 0)
                    {
                        stringBuilder.Append("_" + Utility.FormatPascal(column.Name));
                    }
                    else
                    {
                        stringBuilder.Append(Utility.FormatPascal(column.Name));
                    }
                }

                string procedureName = stringBuilder.ToString();
                string fileName;

                // Determine the file name to be used
                if (createMultipleFiles)
                {
                    fileName = path + procedureName + ".sql";
                }
                else
                {
                    fileName = path + "StoredProcedures.sql";
                }

                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    // Create the seperator
                    if (createMultipleFiles == false)
                    {
                        writer.WriteLine();
                        writer.WriteLine("/******************************************************************************");
                        writer.WriteLine("******************************************************************************/");
                    }

                    // Create the drop statment
                    writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                    writer.WriteLine("\tdrop procedure [dbo].[" + procedureName + "]");
                    writer.WriteLine("GO");
                    writer.WriteLine();

                    // Create the SQL for the stored procedure
                    writer.WriteLine("CREATE PROCEDURE [dbo].[" + procedureName + "] (");

                    // Create the parameter list
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        Column column = (Column)table.Columns[i];
                        if (column.Name.Length > maxsize)
                        {
                            maxsize = column.Name.Length;
                        }
                    }
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i < (compositeKeyList.Count - 1))
                        {
                            writer.WriteLine("\t" + Utility.CreateParameterString(column, false, maxsize) + ",");
                        }
                        else
                        {
                            writer.WriteLine("\t" + Utility.CreateParameterString(column, false, maxsize));
                        }
                    }
                    writer.WriteLine(")");

                    writer.WriteLine();
                    writer.WriteLine("AS");
                    writer.WriteLine();
                    writer.WriteLine("SELECT");

                    // Create the list of columns
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        Column column = (Column)table.Columns[i];

                        if (i < (table.Columns.Count - 1))
                        {
                            writer.WriteLine("\t[" + column.Name + "],");
                        }
                        else
                        {
                            writer.WriteLine("\t[" + column.Name + "]");
                        }
                    }

                    writer.WriteLine("FROM");
                    writer.WriteLine("\t[" + table.Name + "]");
                    writer.WriteLine("WHERE");

                    // Create the where clause
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            writer.WriteLine("\tAND [" + column.Name + "] = @" + column.Name);
                        }
                        else
                        {
                            writer.WriteLine("\t[" + column.Name + "] = @" + column.Name);
                        }
                    }

                    writer.WriteLine("GO");

                    // Create the grant statement, if a user was specified
                    if (grantLoginName.Length > 0)
                    {
                        writer.WriteLine();
                        writer.WriteLine("GRANT EXECUTE ON [dbo].[" + procedureName + "] TO [" + grantLoginName + "]");
                        writer.WriteLine("GO");
                    }
                }
            }
        }
    }
}
