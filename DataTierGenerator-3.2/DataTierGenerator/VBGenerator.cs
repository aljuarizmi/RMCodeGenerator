using System;
using System.Collections;
using System.IO;
using System.Text;

namespace DataTierGenerator
{
    /// <summary>
    /// Generates C# data access and data transfer classes.
    /// </summary>
    public sealed class VBGenerator
    {
        public static String DataBaseSys = "";
        private VBGenerator() { }

        /// <summary>
        /// Creates a copy of the SqlClientUtility class.
        /// </summary>
        /// <param name="targetNamespace">The namespace where the SqlClientUtility class should be created in.</param>
        /// <param name="path">The location where the file that contains teh SqlClientUtility class should be created.</param>
        public static void CreateSqlClientUtilityClassVB(string targetNamespace, string path)
        {
            if (Directory.Exists(path + "\\Data\\MSSQL") == false)
            {
                Directory.CreateDirectory(path + "\\Data\\MSSQL");
            }

            using (StreamWriter streamWriter = new StreamWriter(path + "Data\\MSSQL\\SqlHelper.vb"))
            {
                streamWriter.Write(Utility.GetSqlClientUtilityClassVB(targetNamespace));//,"\\Data"));
            }
        }
        /// <summary>
        /// Creates a copy of the UtilDA class.
        /// </summary>
        /// <param name="targetNamespace">The namespace where the UtilDA class should be created in.</param>
        /// <param name="path">The location where the file that contains the UtilDA class should be created.</param>
        public static void CreateUtilDAClass(string targetNamespace, string path)
        {
            if (Directory.Exists(path + "\\Data\\MSSQL") == false)
            {
                Directory.CreateDirectory(path + "\\Data\\MSSQL");
            }

            using (StreamWriter streamWriter = new StreamWriter(path + "Data\\MSSQL\\UtilDA.vb"))
            {
                streamWriter.Write(Utility.GetUtilDAVBClass(targetNamespace));//,"\\Data"));
            }
        }
        /// <summary>
        /// Creates a copy of the UtilBO class.
        /// </summary>
        /// <param name="targetNamespace">The namespace where the UtilDA class should be created in.</param>
        /// <param name="path">The location where the file that contains the UtilDA class should be created.</param>
        public static void CreateUtilBOClass(string targetNamespace, string path)
        {
            if (Directory.Exists(path + "\\BO") == false)
            {
                Directory.CreateDirectory(path + "\\BO");
            }

            using (StreamWriter streamWriter = new StreamWriter(path + "BO\\UtilBO.vb"))
            {
                streamWriter.Write(Utility.GetUtilBOVBClass(targetNamespace));//,"\\Data"));
            }
        }
        /// <summary>
        /// Creates a copy of the GenericsBO class.
        /// </summary>
        /// <param name="targetNamespace">The namespace where the UtilDA class should be created in.</param>
        /// <param name="path">The location where the file that contains the UtilDA class should be created.</param>
        public static void CreateGenericsBOClass(string targetNamespace, string path)
        {
            if (Directory.Exists(path + "\\BO") == false)
            {
                Directory.CreateDirectory(path + "\\BO");
            }

            using (StreamWriter streamWriter = new StreamWriter(path + "BO\\GenericsBO.vb"))
            {
                streamWriter.Write(Utility.GetGenericsBOVBClass(targetNamespace));//,"\\Data"));
            }
        }

        /// <summary>
        /// Creates a copy of the AuditoriaEO class.
        /// </summary>
        /// <param name="targetNamespace">The namespace where the UtilDA class should be created in.</param>
        /// <param name="path">The location where the file that contains the UtilDA class should be created.</param>
        public static void CreateAuditoriaEOVBClass(string targetNamespace, string path)
        {
            if (Directory.Exists(path + "\\EO") == false)
            {
                Directory.CreateDirectory(path + "\\EO");
            }
            using (StreamWriter streamWriter = new StreamWriter(path + "EO\\AuditoriaEO.vb"))
            {
                streamWriter.Write(Utility.GetAuditoriaEOClass(targetNamespace));
            }
        }

        /// <summary>
        /// Creates a copy of the GenericsDA class.
        /// </summary>
        /// <param name="targetNamespace">The namespace where the UtilDA class should be created in.</param>
        /// <param name="path">The location where the file that contains the UtilDA class should be created.</param>
        public static void CreateGenericsDAClass(string targetNamespace, string path)
        {
            if (Directory.Exists(path + "\\Data\\MSSQL") == false)
            {
                Directory.CreateDirectory(path + "\\Data\\MSSQL");
            }

            using (StreamWriter streamWriter = new StreamWriter(path + "Data\\MSSQL\\GenericsDA.vb"))
            {
                streamWriter.Write(Utility.GetGenericsDAVBClass(targetNamespace));//,"\\Data"));
            }
        }

        /// <summary>
        /// Creates a data transfer class that represents a row of data for the specified table in VB.
        /// </summary>
        /// <param name="table">The table to create the data transfer class for.</param>
        /// <param name="targetNamespace">The namespace that the data transfer class should be created in.</param>
        /// <param name="path">The location where the file that contains the data transfer class definition should be created.</param>
        public static void CreateDataTransferClassVB(Table table, string targetNamespace, string path)
        {
            string className = table.Alias;
            if (Directory.Exists(path + "\\Entities") == false)
            {
                Directory.CreateDirectory(path + "\\Entities");
            }
            using (StreamWriter streamWriter = new StreamWriter(path + "\\Entities\\" + className + ".vb"))
            {
                // Create the header for the class
                streamWriter.WriteLine("Imports System");
                streamWriter.WriteLine();
                streamWriter.WriteLine("Namespace " + targetNamespace + ".EntityObjects ");

                streamWriter.WriteLine("\t''' <summary>");
                streamWriter.WriteLine("\t''' Provides data access for the " + table.Name + " database table.");
                streamWriter.WriteLine("\t''' </summary>");
                streamWriter.WriteLine("\t<Serializable()> _");
                streamWriter.WriteLine("\tPublic Class " + className + " ");

                // Create the private members
                foreach (Column column in table.Columns)
                {
                    switch (SqlGenerator.proveedor)
                    {
                        case Util.Provider.Access:
                            streamWriter.WriteLine("\t\tPrivate " + Utility.CreateMethodParameterVB(column) + "");
                            break;
                        case Util.Provider.SqlClient:
                            streamWriter.WriteLine("\t\tPrivate " + Utility.CreateMethodParameterVB(column) + "");
                            break;
                        case Util.Provider.Oracle:
                            streamWriter.WriteLine("\t\tPrivate " + Utility.CreateMethodParameterVBOracle(column) + "");
                            break;
                        case Util.Provider.Db2:
                            break;
                        case Util.Provider.MySql:
                            streamWriter.WriteLine("\t\tPrivate " + Utility.CreateMethodParameterVB(column) + "");
                            break;
                        case Util.Provider.PostgreSQL:
                            streamWriter.WriteLine("\t\tPrivate " + Utility.CreateMethodParameterVB(column) + "");
                            break;
                        default:
                            break;
                    }

                }
                streamWriter.WriteLine("\t\t");

                // Create an explicit public constructor
                streamWriter.WriteLine("\t\tPublic Sub New() ");
                streamWriter.WriteLine("\t\tEnd Sub");

                // Create the public properties
                foreach (Column column in table.Columns)
                {
                    string parameter = string.Empty;
                    switch (SqlGenerator.proveedor)
                    {
                        case Util.Provider.Access:
                            parameter = " " + Utility.CreateMethodParameterVB(column);
                            break;
                        case Util.Provider.SqlClient:
                            parameter = " " + Utility.CreateMethodParameterVB(column);
                            break;
                        case Util.Provider.Oracle:
                            parameter = " " + Utility.CreateMethodParameterVBOracle(column);
                            break;
                        case Util.Provider.Db2:
                            break;
                        case Util.Provider.MySql:
                            parameter = " " + Utility.CreateMethodParameterVB(column);
                            break;
                        case Util.Provider.PostgreSQL:
                            parameter = " " + Utility.CreateMethodParameterVB(column);
                            break;
                        default:
                            break;
                    }

                    string[] cadenas = parameter.Split(' ');
                    string type = cadenas[3] + " " + cadenas[4];
                    string name = cadenas[1];

                    streamWriter.WriteLine("\t\t");
                    streamWriter.WriteLine("\t\tPublic Property " + Utility.FormatPascal(name).Substring(1, Utility.FormatPascal(name).Length - 1) + " As " + type + " ");
                    streamWriter.WriteLine("\t\t\tGet");
                    streamWriter.WriteLine("\t\t\tReturn Me." + name);
                    streamWriter.WriteLine("\t\t\tEnd Get");
                    streamWriter.WriteLine("\t\t\tSet(ByVal value As " + type + ")");
                    streamWriter.WriteLine("\t\t\tMe." + name + " = value ");
                    streamWriter.WriteLine("\t\t\tEnd Set");
                    streamWriter.WriteLine("\t\tEnd Property");
                }

                // Close out the class and namespace
                streamWriter.WriteLine("\tEnd Class");
                streamWriter.WriteLine("End Namespace");
            }
            using (StreamWriter streamWriter = new StreamWriter(path + "\\Entities\\" + className + "List.vb"))
            {
                // Create the header for the class
                streamWriter.WriteLine("Imports System");
                streamWriter.WriteLine("Imports System.Collections.ObjectModel");
                streamWriter.WriteLine();
                streamWriter.WriteLine("Namespace " + targetNamespace + " ");

                streamWriter.WriteLine("\t''' <summary>");
                streamWriter.WriteLine("\t''' Provides data access for the " + table.Name + " database table.");
                streamWriter.WriteLine("\t''' </summary>");
                //streamWriter.WriteLine("\t<Serializable()> _");
                streamWriter.WriteLine("\tPublic Class " + className + "List ");
                streamWriter.WriteLine("\t Inherits Collection(Of " + className + ")");

                // Close out the class and namespace
                streamWriter.WriteLine("\tEnd Class");
                streamWriter.WriteLine("End Namespace");
            }
        }

        /// <summary>
        /// Creates a VB data access class for all of the table's stored procedures.
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
        /// <param name="path">Path where the class should be created.</param>
        public static void CreateDataAccessClass(Table table, string targetNamespace, string storedProcedurePrefix, string path)
        {
            if (Directory.Exists(path + "\\Data\\MSSQL") == false)
            {
                Directory.CreateDirectory(path + "\\Data\\MSSQL");
            }

            string className = table.Alias + "DA";
            using (StreamWriter streamWriter = new StreamWriter(path + "\\Data\\MSSQL\\" + className + ".vb"))
            {
                // Create the header for the class
                streamWriter.WriteLine("Imports System");
                streamWriter.WriteLine("Imports System.Collections.Generic");
                streamWriter.WriteLine("Imports System.Collections");
                streamWriter.WriteLine("Imports System.Data");
                streamWriter.WriteLine("Imports System.Data.SqlClient");
                //streamWriter.WriteLine("using Microsoft.ApplicationsBlocks.Data;");
                streamWriter.WriteLine();
                streamWriter.WriteLine("Imports " + targetNamespace + ".EntityObjects");
                streamWriter.WriteLine();
                streamWriter.WriteLine("Namespace " + targetNamespace + ".Data.SqlServer ");

                streamWriter.WriteLine("\tPublic Class " + className + " ");
                streamWriter.WriteLine("\t\tPrivate conString as String");
                streamWriter.WriteLine("\t\tPrivate _" + Utility.FormatCamel(table.Alias) + " As " + table.Alias);
                streamWriter.WriteLine("\t\tPrivate _GM As GenericoManager(Of " + table.Alias + ") =New GenericoManager(Of " + table.Alias + ")()");
                streamWriter.WriteLine("\t\tPublic Sub New() ");
                streamWriter.WriteLine("\t\t\tconString=UtilDA.connectionString()");
                streamWriter.WriteLine("\t\t\t_GM.SetMetodoAsignaObjeto(AddressOf MapearObjeto)");
                streamWriter.WriteLine("\t\tEnd Sub");
                streamWriter.WriteLine("\t\tPublic Sub New(ByVal conString as String)");
                streamWriter.WriteLine("\t\t\tMe.conString=conString");
                streamWriter.WriteLine("\t\t\t_GM.SetMetodoAsignaObjeto(AddressOf MapearObjeto)");
                streamWriter.WriteLine("\t\tEnd Sub");


                // Append the access methods
                CreateDAInsertMethod(table, storedProcedurePrefix, streamWriter);
                CreateDAInsertMethodVBGEN(table, storedProcedurePrefix, streamWriter);
                CreateDAUpdateMethod(table, storedProcedurePrefix, streamWriter);
                CreateDADeleteMethod(table, storedProcedurePrefix, streamWriter);
                //CreateDeleteAllByMethods(table, storedProcedurePrefix, streamWriter);
                CreateDASelectMethod(table, storedProcedurePrefix, streamWriter);
                CreateDASelectAllMethod(table, storedProcedurePrefix, streamWriter);
                CreateDASelectAllGenericsMethod(table, storedProcedurePrefix, streamWriter);
                CreateDAObjectMapperMethod(table, storedProcedurePrefix, streamWriter);
                //CreateSelectAllByMethods(table, storedProcedurePrefix, streamWriter);
                //CreateMakeMethod(table, streamWriter);

                // Close out the class and namespace
                streamWriter.WriteLine("\tEnd Class");
                streamWriter.WriteLine("End Namespace");
            }
        }
        /// <summary>
        /// Creates a string that represents the insert functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDAInsertMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "_Insertar";
            // Append the method header
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t''' <summary>");
            streamWriter.WriteLine("\t\t''' Inserta un registro en la tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t''' </summary>");

            streamWriter.Write("\t\tPublic Function Insertar(ByVal pSqlTrans As SqlTransaction, ");
            streamWriter.Write("ByVal _" + Utility.FormatCamel(table.Alias) + " as " + Utility.FormatPascal(table.Alias));
            streamWriter.WriteLine(") as Integer ");

            //streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");

            // Create the parameters
            StringBuilder builder = new StringBuilder();
            foreach (Column column in table.Columns)
            {
                //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);
                builder.Append("_" + Utility.FormatCamel(table.Alias) + "." + column.Alias + ",");
            }
            streamWriter.WriteLine("\t\t\t Return SqlHelper.ExecuteNonQuery(pSqlTrans, \"" + procedureName + "\", " + builder.ToString(0, builder.Length - 1) + ")");
            streamWriter.WriteLine("\t\tEnd Function");
        }

        private static void CreateDAInsertMethodVBGEN(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "_Insertar";
            // Append the method header
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t''' <summary>");
            streamWriter.WriteLine("\t\t''' Inserta un registro en la tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t''' </summary>");

            streamWriter.Write("\t\tPublic Function F_Agregar(ByVal objCredencialesBE As CredencialesBE, ByVal pSqlTrans As SqlTransaction, ");
            streamWriter.Write("ByVal _" + Utility.FormatCamel(table.Alias) + " as " + Utility.FormatPascal(table.Alias));
            streamWriter.WriteLine(") as Integer ");

            //streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");

            // Create the parameters
            StringBuilder builder = new StringBuilder();
            streamWriter.WriteLine("\t\t\tWith objEntidadBE");
            foreach (Column column in table.Columns)
            {
                //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);
                streamWriter.WriteLine("db.AddInParameter(cmd, \"@" + column.Alias + "\", DbType." + Utility.GetSqlDbType(column.Type) + ", ." + column.Alias + ")");
            }
            streamWriter.WriteLine("\t\t\tEnd With");
            streamWriter.WriteLine("\t\tEnd Function");
        }

        /// <summary>
        /// Creates a string that represents the update functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDAUpdateMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
            {
                // Append the method header
                string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "_Actualizar";
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Actualiza un registro en la Tabla " + table.Name + ".");
                streamWriter.WriteLine("\t\t''' </summary>");

                streamWriter.Write("\t\tPublic Function Actualizar(ByVal pSqlTrans As SqlTransaction, ");
                streamWriter.Write("ByVal _" + Utility.FormatCamel(table.Alias) + " as " + Utility.FormatPascal(table.Alias));
                streamWriter.WriteLine(") as Integer");

                //streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");

                // Create the parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);
                    builder.Append("_" + Utility.FormatCamel(table.Alias) + "." + column.Alias + ",");
                }
                //streamWriter.WriteLine("\t\t\t};");
                streamWriter.WriteLine("\t\t\t Return SqlHelper.ExecuteNonQuery(pSqlTrans, \"" + procedureName + "\", " + builder.ToString(0, builder.Length - 1) + ")");
                // Append the method footer
                streamWriter.WriteLine("\t\tEnd Function");
            }
        }


        /// <summary>
        /// Creates a string that represents the delete functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDADeleteMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            if (table.PrimaryKeys.Count > 0)
            {
                // Create the delete function based on keys
                // Append the method header
                string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "_Eliminar";
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Elimina un Registro de la Tabla " + table.Name + " table por su llave primaria.");
                streamWriter.WriteLine("\t\t''' </summary>");

                streamWriter.Write("\t\tPublic Function Eliminar(ByVal pSqlTrans As SqlTransaction, ");
                streamWriter.Write(" _" + Utility.FormatCamel(table.Alias) + " as " + Utility.FormatPascal(table.Alias));
                streamWriter.WriteLine(") as Integer");

                //streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");

                // Create the parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.PrimaryKeys)
                {
                    //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);
                    builder.Append("_" + Utility.FormatCamel(table.Alias) + "." + column.Alias + ",");
                }
                //streamWriter.WriteLine();
                //streamWriter.WriteLine("\t\t\t};");
                streamWriter.WriteLine("\t\t\t Return SqlHelper.ExecuteNonQuery(pSqlTrans, \"" + procedureName + "\", " + builder.ToString(0, builder.Length - 1) + ")");
                // Append the method footer
                streamWriter.WriteLine("\t\tEnd Function");


            }

        }


        /// <summary>
        /// Creates a string that represents the "delete by" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDADeleteAllByMethodsVB(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            // Create a stored procedure for each foreign key
            foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
            {
                // Create the stored procedure name
                StringBuilder stringBuilder = new StringBuilder(255);
                StringBuilder stringBuilderName = new StringBuilder(255);
                stringBuilder.Append(storedProcedurePrefix + table.Name + "DeleteAllBy");
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

                // Create the method name
                stringBuilder = new StringBuilder(255);
                stringBuilderName = new StringBuilder(255);
                stringBuilder.Append("DeleteAllBy");
                stringBuilderName.Append("DeleteAllBy");
                for (int i = 0; i < compositeKeyList.Count; i++)
                {
                    Column column = (Column)compositeKeyList[i];

                    if (i > 0)
                    {
                        stringBuilder.Append("_" + Utility.FormatPascal(column.Alias));
                        stringBuilderName.Append("_" + Utility.FormatPascal(column.Name));
                    }
                    else
                    {
                        stringBuilder.Append(Utility.FormatPascal(column.Alias));
                        stringBuilderName.Append(Utility.FormatPascal(column.Name));
                    }
                }
                string methodName = stringBuilder.ToString();
                string methodNameTable = stringBuilderName.ToString();

                // Create the delete function based on keys
                // Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Deletes a record from the " + table.Name + " table by a foreign key.");
                streamWriter.WriteLine("\t\t''' </summary>");

                streamWriter.Write("\t\tPublic Shared Sub " + methodName + "(");
                foreach (Column column in compositeKeyList)
                {
                    streamWriter.Write(Utility.CreateMethodParameterVB(column) + ", ");
                }
                streamWriter.WriteLine("ByVal connection As SqlConnection, ByVal transaction as SqlTransaction) ");

                // Create the parameters
                streamWriter.WriteLine("\t\t\tDim parameters As SqlParameter() = { _");
                StringBuilder builder = new StringBuilder();
                foreach (Column column in compositeKeyList)
                {
                    builder.Append("\t\t\t\t" + Utility.CreateSqlParameterVB(null, column, true) + ", _" + Environment.NewLine);
                }
                streamWriter.WriteLine(builder.ToString(0, builder.Length - (Environment.NewLine.Length + 3)) + " _");
                streamWriter.WriteLine("\t\t\t}");
                streamWriter.WriteLine();

                // Append the stored procedure execution
                streamWriter.WriteLine("\t\t\tSqlClientUtility.ExecuteNonQuery(connection, transaction, \"" + methodNameTable + "\", parameters)");

                // Append the method footer
                streamWriter.WriteLine("\t\tEnd Sub");
            }
        }

        /// <summary>
        /// Creates a string that represents the select by primary key functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDASelectMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.ForeignKeys.Count)
            {
                // Append the method header
                string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "_Seleccionar";
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Selecciona un registro de la tabla " + table.Name + ".");
                streamWriter.WriteLine("\t\t''' </summary>");

                // Create the parameters
                StringBuilder builderParam = new StringBuilder();
                foreach (Column column in table.PrimaryKeys)
                {
                    //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);
                    builderParam.Append("ByVal " + column.Alias + " as " + (column.Type == "int" ? "Integer" : column.Type) + ",");
                }

                streamWriter.Write("\t\tPublic Function Seleccionar (");
                streamWriter.Write("" + builderParam.ToString(0, builderParam.Length - 1));
                streamWriter.WriteLine(") as " + table.Alias);

                //streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");

                // Create the parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.PrimaryKeys)
                {
                    //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);
                    builder.Append("" + column.Alias + ",");
                }
                //streamWriter.WriteLine();
                //streamWriter.WriteLine("\t\t\t};");
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t\tUsing _sqlConnection As New SqlConnection(Me.conString)");
                streamWriter.WriteLine("\t\t\t_sqlConnection.Open()");
                streamWriter.WriteLine("\t\t\tUsing reader as SqlDataReader = SqlHelper.ExecuteReader(_sqlConnection, \"" + procedureName + "\", " + builder.ToString(0, builder.Length - 1) + ")");

                streamWriter.WriteLine("\t\t\tIf reader.Read() Then");
                streamWriter.WriteLine("\t\t\t\t_" + Utility.FormatCamel(table.Alias) + " = MapearObjeto(reader)");
                streamWriter.WriteLine("\t\t\tEnd If");
                ////aqui convertimos el DataSet en Entidad
                //streamWriter.WriteLine("\t\t\tWhile reader.Read()");

                //streamWriter.WriteLine("\t\t\t _" + Utility.FormatCamel(table.Alias) + "=new " + table.Alias + "()");
                //for (int i = 0; i < table.Columns.Count; i++)
                //{
                //    Column column = (Column)table.Columns[i];
                //    string columnNamePascal = column.Alias;
                //    streamWriter.WriteLine("\t\t\t\t_" + Utility.FormatCamel(table.Alias) + "." + columnNamePascal + " = " + Utility.CreateConvertXxxMethod(column) + "(reader(\"" + column.Name + "\"))");
                //}

                //streamWriter.WriteLine("\t\t\tEnd While");
                streamWriter.WriteLine("\t\t\tEnd Using");
                streamWriter.WriteLine("\t\t\tEnd Using");

                streamWriter.WriteLine("\t\t\tReturn _" + Utility.FormatCamel(table.Alias) + "");
                // Append the method footer
                streamWriter.WriteLine("\t\tEnd Function");

            }
        }


        /// <summary>
        /// Creates a string that represents the select functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDASelectAllGenericsMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
            {
                // Append the method header
                string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "_Listar";
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t' <summary>");
                streamWriter.WriteLine("\t\t' Selecciona Todos los Registros de la Tabla " + table.Name + ". en Lista Generica");
                streamWriter.WriteLine("\t\t' </summary>");
                streamWriter.WriteLine("\t\tPublic Function ListarGenerics() as List(Of " + table.Alias + ") ");
                streamWriter.WriteLine("\t\t\tDim lstTemp As List(Of " + table.Alias + ") = Nothing");
                streamWriter.WriteLine("\t\t\tUsing _sqlConnection As New SqlConnection(Me.conString)");
                streamWriter.WriteLine("\t\t\t_sqlConnection.Open()");
                streamWriter.WriteLine("\t\t\t Using reader As SqlDataReader = SqlHelper.ExecuteReader(_sqlConnection,CommandType.StoredProcedure, \"" + procedureName + "\")");
                streamWriter.WriteLine("\t\t\t lstTemp= _GM.GetList(reader)");
                streamWriter.WriteLine("\t\t\t End Using");
                streamWriter.WriteLine("\t\t\tEnd Using");
                streamWriter.WriteLine("\t\t\t Return lstTemp");
                //foreach (Column column in table.Columns)
                //{
                //    streamWriter.WriteLine("\t\t\t ds.Tables[0].Columns[\"" + column.Name + "\"].ColumnName = \"" + column.Alias + "\";");
                //}
                //streamWriter.WriteLine("\t\t\t ds.AcceptChanges();");
                //streamWriter.WriteLine("\t\t\t return ds.Tables[0];");

                // Append the method footer
                streamWriter.WriteLine("\t\tEnd Function");
            }
        }
        /// <summary>
        /// Creates a string that represents the Object Mapper functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDAObjectMapperMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
            {
                // Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t' <summary>");
                streamWriter.WriteLine("\t\t' Mapea un Registro de la Tabla Hacia una Entidad " + table.Name + ".");
                streamWriter.WriteLine("\t\t' </summary>");
                streamWriter.WriteLine("\t\tPublic Function MapearObjeto(ByVal reader As IDataReader) As " + table.Alias + " ");
                streamWriter.WriteLine("\t\t\t Dim " + " obj" + table.Alias + " As " + table.Alias + " = New " + table.Alias + " ()");
                foreach (Column column in table.Columns)
                {
                    streamWriter.WriteLine("\t\t\t obj" + table.Alias + "." + column.Alias + " = " + Utility.CreateConvertXxxMethod(column) + "(reader,\"" + column.Name + "\")");
                }

                streamWriter.WriteLine("\t\t\t Return obj" + table.Alias);


                // Append the method footer
                streamWriter.WriteLine("\t\tEnd Function");
            }
        }
        /// <summary>
        /// Creates a string that represents the select functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDASelectAllMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
            {
                // Append the method header
                string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "_Listar";
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Selecciona Todos los Registros de la Tabla " + table.Name + ".");
                streamWriter.WriteLine("\t\t''' </summary>");
                streamWriter.WriteLine("\t\tPublic Function Listar() as DataTable");
                streamWriter.WriteLine("\t\t\tDim ds As New DataSet() ");
                streamWriter.WriteLine("\t\t\tUsing _sqlConnection As New SqlConnection(Me.conString)");
                streamWriter.WriteLine("\t\t\t_sqlConnection.Open()");
                streamWriter.WriteLine("\t\t\tds = SqlHelper.ExecuteDataset(_sqlConnection,CommandType.StoredProcedure, \"" + procedureName + "\")");
                streamWriter.WriteLine("\t\t\tEnd Using");
                //foreach (Column column in table.Columns)
                //{
                //    streamWriter.WriteLine("\t\t\t ds.Tables(0).Columns(\"" + column.Name + "\").ColumnName = \"" + column.Alias + "\"");
                //}
                //streamWriter.WriteLine("\t\t\t ds.AcceptChanges()");
                streamWriter.WriteLine("\t\t\t Return ds.Tables(0)");


                // Append the method footer
                streamWriter.WriteLine("\t\tEnd Function");
            }
        }

        /// <summary>
        /// Creates a string that represents the "select by" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDASelectAllByMethodsVB(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            // Create a stored procedure for each foreign key
            foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
            {
                // Create the stored procedure name
                StringBuilder stringBuilder = new StringBuilder(255);
                //StringBuilder stringBuilderName = new StringBuilder(255);
                stringBuilder.Append(storedProcedurePrefix + table.Name + "SelectAllBy");
                //stringBuilderName.Append(storedProcedurePrefix + table.Name + "SelectAllBy");
                for (int i = 0; i < compositeKeyList.Count; i++)
                {
                    Column column = (Column)compositeKeyList[i];

                    if (i > 0)
                    {
                        stringBuilder.Append("_" + Utility.FormatPascal(column.Name));
                        //      stringBuilderName.Append("_" + Utility.FormatPascal(column.Name ));
                    }
                    else
                    {
                        stringBuilder.Append(Utility.FormatPascal(column.Name));
                        //    stringBuilderName.Append("_" + Utility.FormatPascal(column.Name ));
                    }
                }
                string procedureName = stringBuilder.ToString();
                //string procedureNameTable = stringBuilderName.ToString();
                // Create the method name
                stringBuilder = new StringBuilder(255);
                stringBuilder.Append("SelectAllBy");
                for (int i = 0; i < compositeKeyList.Count; i++)
                {
                    Column column = (Column)compositeKeyList[i];

                    if (i > 0)
                    {
                        stringBuilder.Append("_" + Utility.FormatPascal(column.Alias));
                    }
                    else
                    {
                        stringBuilder.Append(Utility.FormatPascal(column.Alias));
                    }
                }
                string methodName = stringBuilder.ToString();

                // Create the select function based on keys
                // Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Selects all records from the " + table.Name + " table by a foreign key.");
                streamWriter.WriteLine("\t\t''' </summary>");

                streamWriter.Write("\t\tPublic Shared Function " + methodName + " (");
                for (int i = 0; i < compositeKeyList.Count; i++)
                {
                    Column column = (Column)compositeKeyList[i];
                    streamWriter.Write(Utility.CreateMethodParameterVB(column) + ", ");
                }
                streamWriter.WriteLine("ByVal connection As SqlConnection, ByVal transaction As SqlTransaction) " + " As " + Utility.FormatPascal(table.Alias) + "()");

                streamWriter.WriteLine("\t\t\tDim parameters As SqlParameter() = { _");

                // Create the parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in compositeKeyList)
                {
                    builder.Append("\t\t\t\t" + Utility.CreateSqlParameterVB(null, column, true) + ", _" + Environment.NewLine);
                }
                streamWriter.WriteLine(builder.ToString(0, builder.Length - (Environment.NewLine.Length + 3)) + " _");
                streamWriter.WriteLine("\t\t\t}");
                streamWriter.WriteLine();

                // Append the stored procedure execution
                streamWriter.WriteLine("\t\t\tDim dataReader As SqlDataReader = SqlClientUtility.ExecuteReader(connection, transaction, \"" + methodName + "\", parameters) ");
                streamWriter.WriteLine("\t\t\t\tIf (dataReader.HasRows) Then");
                streamWriter.WriteLine("\t\t\t\t\tDim list As New ArrayList()");
                streamWriter.WriteLine("\t\t\t\t\tWhile (dataReader.Read()) ");
                streamWriter.WriteLine("\t\t\t\t\t\t" + " Dim _" + Utility.FormatCamel(table.Alias) + " As " + Utility.FormatPascal(table.Alias) + " = Make" + Utility.FormatPascal(table.Alias) + "(dataReader, transaction)");
                streamWriter.WriteLine("\t\t\t\t\t\tlist.Add(_" + Utility.FormatCamel(table.Alias) + ")");
                streamWriter.WriteLine("\t\t\t\t\tEnd While");
                streamWriter.WriteLine("\t\t\t\t\tReturn list.ToArray(GetType(" + Utility.FormatPascal(table.Alias) + "))");
                streamWriter.WriteLine("\t\t\t\tElse ");
                streamWriter.WriteLine("\t\t\t\t\tReturn Nothing");
                streamWriter.WriteLine("\t\t\t\tEnd If");
                //streamWriter.WriteLine("\t\t\t}");

                // Append the method footer
                streamWriter.WriteLine("\t\tEnd Function");
            }
        }

        /// <summary>
        /// Creates a string that represents the "make" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDAMakeMethodVB(Table table, StreamWriter streamWriter)
        {
            string tableNamePascal = Utility.FormatPascal(table.Alias);
            string tableNameCamel = Utility.FormatCamel(table.Alias);

            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t''' <summary>");
            streamWriter.WriteLine("\t\t''' Creates a new instance of the " + table.Name + " class and populates it with data from the specified SqlDataReader.");
            streamWriter.WriteLine("\t\t''' </summary>");
            streamWriter.WriteLine("\t\tPrivate Shared Function " + " Make" + Utility.FormatPascal(table.Alias) + "(ByVal dataReader As SqlDataReader, ByVal transaction As SqlTransaction) As " + tableNamePascal);
            streamWriter.WriteLine("\t\t\t" + " Dim " + tableNameCamel + " As New " + tableNamePascal + "");
            streamWriter.WriteLine("\t\t\t");

            for (int i = 0; i < table.Columns.Count; i++)
            {
                Column column = (Column)table.Columns[i];
                string columnNamePascal = Utility.FormatPascal(column.Alias);

                streamWriter.WriteLine("\t\t\tIf (dataReader.IsDBNull(" + i.ToString() + ") = False) Then");
                streamWriter.WriteLine("\t\t\t\t" + tableNameCamel + "." + columnNamePascal + " = dataReader." + Utility.CreateGetXxxMethod(column) + "(" + i.ToString() + ")");
                streamWriter.WriteLine("\t\t\tEnd If");
            }

            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t\tReturn " + tableNameCamel + "");
            streamWriter.WriteLine("\t\tEnd Function");
        }

        /// <summary>
        /// Creates a C# business objects class for all of the table's stored procedures.
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
        /// <param name="path">Path where the class should be created.</param>
        public static void CreateBusinessObjectsClass(Table table, string targetNamespace, string storedProcedurePrefix, string path)
        {
            if (Directory.Exists(path + "\\BO\\MSSQL") == false)
            {
                Directory.CreateDirectory(path + "\\BO\\MSSQL");
            }

            string className = table.Alias + "BO";
            using (StreamWriter streamWriter = new StreamWriter(path + "\\BO\\MSSQL\\" + className + ".vb"))
            {
                // Create the header for the class
                streamWriter.WriteLine("Imports System");
                streamWriter.WriteLine("Imports System.Collections");
                streamWriter.WriteLine("Imports System.Collections.Generic");
                streamWriter.WriteLine("Imports System.Data");
                streamWriter.WriteLine("Imports System.Data.SqlClient");
                streamWriter.WriteLine();
                streamWriter.WriteLine("Imports " + targetNamespace + ".Data.SqlServer");
                streamWriter.WriteLine("Imports " + targetNamespace + ".EntityObjects");
                streamWriter.WriteLine();
                streamWriter.WriteLine("Namespace " + targetNamespace + ".BusinessObjects ");

                streamWriter.WriteLine("\tPublic Class " + className + " ");
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t#Region \"Declaraciones\"");
                streamWriter.WriteLine();

                streamWriter.WriteLine("\t\tPrivate _" + Utility.FormatCamel(table.Alias) + "DA as " + table.Alias + "DA");
                streamWriter.WriteLine("\t\tPrivate conString As String");
                streamWriter.WriteLine("\t\tPrivate sqlTrans As SqlTransaction");
                streamWriter.WriteLine("\t\tPrivate sqlConexion As SqlConnection");
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t#End Region");
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t#Region \"Constructor\"");
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\tPublic Sub New()");
                streamWriter.WriteLine("_" + Utility.FormatCamel(table.Alias) + "DA=New " + table.Alias + "DA()");
                streamWriter.WriteLine("\t\tconString = UtilDA.connectionString()");

                streamWriter.WriteLine("\t\tEnd Sub");
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t#End Region");
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t#Region \"Metodos Transaccionales\"");
                streamWriter.WriteLine();
                // Append the access methods
                CreateBOInsertMethod(table, storedProcedurePrefix, streamWriter);
                CreateBOUpdateMethod(table, storedProcedurePrefix, streamWriter);
                CreateBODeleteMethod(table, storedProcedurePrefix, streamWriter);
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t#End Region");
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t#Region \"Metodos No Transaccionales\"");
                streamWriter.WriteLine();
                //CreateBODeleteAllByMethods(table, storedProcedurePrefix, streamWriter);
                CreateBOSelectMethod(table, storedProcedurePrefix, streamWriter);
                CreateBOSelectAllGenericsMethod(table, storedProcedurePrefix, streamWriter);
                CreateBOSelectAllMethod(table, storedProcedurePrefix, streamWriter);
                CreateBOSelectAllComboMethod(table, storedProcedurePrefix, streamWriter, "Seleccione");
                CreateBOSelectAllComboMethod(table, storedProcedurePrefix, streamWriter, "Todos");

                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t#End Region");
                streamWriter.WriteLine();

                //CreateBOSelectAllByMethods(table, storedProcedurePrefix, streamWriter);
                //CreateBOMakeMethod(table, streamWriter);

                // Close out the class and namespace
                streamWriter.WriteLine("\tEnd Class");
                streamWriter.WriteLine("End Namespace");
            }

        }
        /// <summary>
        /// Creates a string that represents the insert functionality of the Business Objects class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateBOInsertMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            // Append the method header
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t''' <summary>");
            streamWriter.WriteLine("\t\t''' Inserta un Registro en la Tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t''' </summary>");

            streamWriter.Write("\t\tPublic Sub Insertar(");
            streamWriter.Write("ByVal _" + Utility.FormatCamel(table.Alias) + " as " + Utility.FormatPascal(table.Alias));
            streamWriter.WriteLine(", ByRef pbooTodoOk As Boolean, ByRef pstrError As String) ");
            streamWriter.WriteLine("\t\tDim result As Integer = -1");
            //streamWriter.WriteLine();
            streamWriter.WriteLine("\t\tTry");

            streamWriter.WriteLine("\t\tsqlConexion = New SqlConnection(conString)");
            streamWriter.WriteLine("\t\tsqlConexion.Open()");
            streamWriter.WriteLine("\t\tsqlTrans = sqlConexion.BeginTransaction(IsolationLevel.ReadCommitted)");
            streamWriter.WriteLine("\t\t\t result = _" + Utility.FormatCamel(table.Alias) + "DA.Insertar(sqlTrans, _" + Utility.FormatCamel(table.Alias) + ")");
            streamWriter.WriteLine("\t\tsqlTrans.Commit()");
            //streamWriter.WriteLine("\t\tEnd Try");
            streamWriter.WriteLine("\t\tCatch ex as Exception ");
            streamWriter.WriteLine("\t\tIf Not sqlTrans Is Nothing Then");
            streamWriter.WriteLine("\t\tsqlTrans.Rollback()");
            //streamWriter.WriteLine("\t\t");
            streamWriter.WriteLine("\t\tEnd If");
            streamWriter.WriteLine("\t\tUtilBO.DisposeConnection(sqlConexion, sqlTrans)");
            streamWriter.WriteLine("\t\tUtilBO.AlmacenarErrorLog(ex)");
            streamWriter.WriteLine("\t\tpbooTodoOk = False");
            streamWriter.WriteLine("\t\tpstrError = UtilBO.msgError");
            //streamWriter.WriteLine("\t\t\tUtilBO.ManejaExcepcion(ex, Me, \"Insertar\")");
            streamWriter.WriteLine("\t\tFinally");
            streamWriter.WriteLine("\t\tUtilBO.DisposeConnection(sqlConexion, sqlTrans)");
            streamWriter.WriteLine("\t\tEnd Try");
            streamWriter.WriteLine("\t\tEnd Sub");
        }
        /// <summary>
        /// Creates a string that represents the update functionality of the Business Objects class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateBOUpdateMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            // Append the method header
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t''' <summary>");
            streamWriter.WriteLine("\t\t''' Actualiza un Registro en la Tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t''' </summary>");

            streamWriter.Write("\t\tPublic Sub Actualizar(");
            streamWriter.Write("ByVal _" + Utility.FormatCamel(table.Alias) + " as " + Utility.FormatPascal(table.Alias));
            streamWriter.WriteLine(", ByRef pbooTodoOk As Boolean, ByRef pstrError As String) ");

            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\tDim result As Integer = -1");
            streamWriter.WriteLine("\t\tTry");
            streamWriter.WriteLine("\t\tsqlConexion = New SqlConnection(conString)");
            streamWriter.WriteLine("\t\tsqlConexion.Open()");
            streamWriter.WriteLine("\t\tsqlTrans = sqlConexion.BeginTransaction(IsolationLevel.ReadCommitted)");
            streamWriter.WriteLine("\t\t\t result = _" + Utility.FormatCamel(table.Alias) + "DA.Actualizar(sqlTrans, _" + Utility.FormatCamel(table.Alias) + ")");
            streamWriter.WriteLine("\t\tsqlTrans.Commit()");
            streamWriter.WriteLine("\t\tCatch ex as Exception");
            streamWriter.WriteLine("\t\tIf Not sqlTrans Is Nothing Then");
            streamWriter.WriteLine("\t\tsqlTrans.Rollback()");
            //streamWriter.WriteLine("\t\t");
            streamWriter.WriteLine("\t\tEnd If");
            streamWriter.WriteLine("\t\tUtilBO.DisposeConnection(sqlConexion, sqlTrans)");
            streamWriter.WriteLine("\t\tUtilBO.AlmacenarErrorLog(ex)");
            streamWriter.WriteLine("\t\tpbooTodoOk = False");
            streamWriter.WriteLine("\t\tpstrError = UtilBO.msgError");
            streamWriter.WriteLine("\t\tFinally");
            streamWriter.WriteLine("\t\tUtilBO.DisposeConnection(sqlConexion, sqlTrans)");
            streamWriter.WriteLine("\t\tEnd Try");
            streamWriter.WriteLine("\t\tEnd Sub");
        }
        /// <summary>
        /// Creates a string that represents the delete functionality of the Business Objects class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateBODeleteMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            // Append the method header
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t''' <summary>");
            streamWriter.WriteLine("\t\t''' Elimina un Registro de la Tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t''' </summary>");

            streamWriter.Write("\t\tPublic Sub Eliminar(");
            streamWriter.Write("ByVal _" + Utility.FormatCamel(table.Alias) + " as " + Utility.FormatPascal(table.Alias));
            streamWriter.WriteLine(", ByRef pbooTodoOk As Boolean, ByRef pstrError As String) ");

            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\tDim result As Integer = -1");
            streamWriter.WriteLine("\t\tTry");
            streamWriter.WriteLine("\t\tsqlConexion = New SqlConnection(conString)");
            streamWriter.WriteLine("\t\tsqlConexion.Open()");
            streamWriter.WriteLine("\t\tsqlTrans = sqlConexion.BeginTransaction(IsolationLevel.ReadCommitted)");
            streamWriter.WriteLine("\t\t\tresult = _" + Utility.FormatCamel(table.Alias) + "DA.Eliminar(sqlTrans, _" + Utility.FormatCamel(table.Alias) + ")");
            streamWriter.WriteLine("\t\tsqlTrans.Commit()");
            streamWriter.WriteLine("\t\tCatch ex as Exception");
            streamWriter.WriteLine("\t\tIf Not sqlTrans Is Nothing Then");
            streamWriter.WriteLine("\t\tsqlTrans.Rollback()");
            //streamWriter.WriteLine("\t\t");
            streamWriter.WriteLine("\t\tEnd If");
            streamWriter.WriteLine("\t\tUtilBO.DisposeConnection(sqlConexion, sqlTrans)");
            streamWriter.WriteLine("\t\tUtilBO.AlmacenarErrorLog(ex)");
            streamWriter.WriteLine("\t\tpbooTodoOk = False");
            streamWriter.WriteLine("\t\tpstrError = UtilBO.msgError");
            streamWriter.WriteLine("\t\tFinally");
            streamWriter.WriteLine("\t\tUtilBO.DisposeConnection(sqlConexion, sqlTrans)");
            streamWriter.WriteLine("\t\tEnd Try");
            streamWriter.WriteLine("\t\tEnd Sub");
        }
        /// <summary>
        /// Creates a string that represents the Select functionality of the Business Objects class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateBOSelectMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            // Append the method header
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t''' <summary>");
            streamWriter.WriteLine("\t\t''' Selecciona un Registro de la Tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t''' </summary>");

            // Create the parameters
            StringBuilder builderParam = new StringBuilder();
            foreach (Column column in table.PrimaryKeys)
            {
                //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);

                //builderParam.Append("ByVal" + " " + column.Alias + " as " + (column.Type == "int" ? "Integer" : column.Type) + ",");
                switch (SqlGenerator.proveedor)
                {
                    case Util.Provider.Access:
                        builderParam.Append("ByVal" + " " + Utility.CreateMethodParameterVB(column) + ",");
                        break;
                    case Util.Provider.SqlClient:
                        builderParam.Append("ByVal" + " " + Utility.CreateMethodParameterVB(column) + ",");
                        break;
                    case Util.Provider.Oracle:
                        builderParam.Append("ByVal" + " " + Utility.CreateMethodParameterVBOracle(column) + ",");
                        break;
                    case Util.Provider.Db2:
                        break;
                    case Util.Provider.MySql:
                        break;
                    default:
                        break;
                }
                
            }

            streamWriter.Write("\t\tPublic Function Seleccionar(");
            if (builderParam.Length > 0)
            {
                streamWriter.Write("" + builderParam.ToString(0, builderParam.Length - 1));
            }
            
            streamWriter.WriteLine(", ByRef pbooTodoOk As Boolean, ByRef pstrError As String) as " + table.Alias);

            //streamWriter.Write("\t\tpublic _"+table.Alias +" Select(");
            //streamWriter.Write(Utility.FormatPascal(table.Alias) + " _" + Utility.FormatCamel(table.Alias) + "");
            //streamWriter.WriteLine(") {");
            streamWriter.WriteLine("\t\tDim _" + Utility.FormatCamel(table.Alias) + " as " + table.Alias + "=new " + table.Alias + "()");
            streamWriter.WriteLine();

            // Create the parameters
            StringBuilder builderParam2 = new StringBuilder();
            foreach (Column column in table.PrimaryKeys)
            {
                //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);
                builderParam2.Append("" + column.Alias + ",");
            }

            streamWriter.WriteLine("\t\tTry");
            if (builderParam2.Length > 0)
            {
                streamWriter.WriteLine("\t\t\t_" + Utility.FormatCamel(table.Alias) + " = _" + Utility.FormatCamel(table.Alias) + "DA.Seleccionar(" + builderParam2.ToString(0, builderParam2.Length - 1) + ")");
            }
            
            //streamWriter.WriteLine("\t\t}");
            streamWriter.WriteLine("\t\tCatch ex as Exception");

            streamWriter.WriteLine("\t\tUtilBO.AlmacenarErrorLog(ex)");
            streamWriter.WriteLine("\t\tpbooTodoOk = False");
            streamWriter.WriteLine("\t\tpstrError = UtilBO.msgError");

            streamWriter.WriteLine("\t\tEnd Try");
            streamWriter.WriteLine("\t\tReturn _" + Utility.FormatCamel(table.Alias) + "");
            streamWriter.WriteLine("\t\tEnd Function");
        }

        /// <summary>
        /// Creates a string that represents the SelectAll functionality of the Business Objects class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateBOSelectAllMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            // Append the method header
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t''' <summary>");
            streamWriter.WriteLine("\t\t''' Selecciona todos los Registros de la Tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t''' </summary>");

            streamWriter.Write("\t\tPublic Function Listar(ByRef pbooTodoOk As Boolean, ByRef pstrError As String) as DataTable");
            streamWriter.WriteLine();
            streamWriter.WriteLine(" Dim dt as DataTable=New DataTable()");
            streamWriter.WriteLine("\t\tTry");
            streamWriter.WriteLine("\t\t\tdt = _" + Utility.FormatCamel(table.Alias) + "DA.Listar()");
            //streamWriter.WriteLine("\t\t");
            streamWriter.WriteLine("\t\tCatch ex as Exception");
            streamWriter.WriteLine("\t\tUtilBO.AlmacenarErrorLog(ex)");
            streamWriter.WriteLine("\t\tpbooTodoOk = False");
            streamWriter.WriteLine("\t\tpstrError = UtilBO.msgError");
            streamWriter.WriteLine("\t\tEnd Try");
            streamWriter.WriteLine("\t\tReturn dt");
            streamWriter.WriteLine("\t\tEnd Function");
        }
        /// <summary>
        /// Creates a string that represents the SelectAll functionality of the Business Objects class into a Generic List.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateBOSelectAllGenericsMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            // Append the method header
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t''' <summary>");
            streamWriter.WriteLine("\t\t''' Selecciona todos los Registros de la Tabla " + table.Name + ". en una Lista Generica");
            streamWriter.WriteLine("\t\t''' </summary>");

            streamWriter.Write("\t\tPublic Function ListarGenerics(ByRef pbooTodoOk As Boolean, ByRef pstrError As String) As List(Of " + table.Alias + ")");
            streamWriter.WriteLine(" ");
            streamWriter.WriteLine(" Dim list As List(Of " + table.Alias + ")=New List(Of " + table.Alias + ")()");
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t Try");
            streamWriter.WriteLine("\t\t\t list = _" + Utility.FormatCamel(table.Alias) + "DA.ListarGenerics()");
            streamWriter.WriteLine("\t\tCatch ex As Exception");
            streamWriter.WriteLine("\t\tUtilBO.AlmacenarErrorLog(ex)");
            streamWriter.WriteLine("\t\tpbooTodoOk = False");
            streamWriter.WriteLine("\t\tpstrError = UtilBO.msgError");
            streamWriter.WriteLine("\t\tEnd Try");
            streamWriter.WriteLine("\t\treturn list");
            streamWriter.WriteLine("\t\tEnd Function");
        }
        /// <summary>
        /// Creates a string that represents the SelectAll functionality of the Business Objects class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateBOSelectAllComboMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter, string strSelectItem)
        {
            // Append the method header
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t''' <summary>");
            streamWriter.WriteLine("\t\t''' Selecciona todos los Registros para un combo(" + strSelectItem + ") de la Tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t''' </summary>");

            streamWriter.Write("\t\tPublic Function ListarCombo" + strSelectItem + "(ByRef pbooTodoOk As Boolean, ByRef pstrError As String) As DataTable");
            streamWriter.WriteLine();
            streamWriter.WriteLine(" Dim dt as DataTable=New DataTable()");
            streamWriter.WriteLine("\t\tTry");
            streamWriter.WriteLine("\t\t\tdt = _" + Utility.FormatCamel(table.Alias) + "DA.Listar()");
            streamWriter.WriteLine("\t\t\t'Cambiar los Nombre de estas Columnas de Ser Necesario");
            streamWriter.WriteLine("\t\t\tDim dr As DataRow = dt.NewRow()");
            streamWriter.WriteLine("\t\t\tdr(\"intSecuencial\") = 0");
            streamWriter.WriteLine("\t\t\tdr(\"strDescripcion\") = \" " + strSelectItem + " \"");
            streamWriter.WriteLine("\t\t\tdt.Rows.InsertAt(dr, 0)");
            streamWriter.WriteLine("\t\t\tdt.AcceptChanges()");
            streamWriter.WriteLine("\t\tCatch ex as Exception");
            streamWriter.WriteLine("\t\tUtilBO.AlmacenarErrorLog(ex)");
            streamWriter.WriteLine("\t\tpbooTodoOk = False");
            streamWriter.WriteLine("\t\tpstrError = UtilBO.msgError");
            streamWriter.WriteLine("\t\tEnd Try");
            streamWriter.WriteLine("\t\tReturn dt");
            streamWriter.WriteLine("\t\tEnd Function");
        }
    }
}
