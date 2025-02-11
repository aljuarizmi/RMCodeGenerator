using System;
using System.Collections;
using System.IO;
using System.Text;

namespace DataTierGenerator
{
    /// <summary>
    /// Generates C# data access and data transfer classes.
    /// </summary>
    public sealed class CsGenerator
    {
        public static String DataBaseSys = "";
        public static String DriverJDBC = "";
        public static String Protocolo = "";
        public static String Puerto = "";
        private CsGenerator() { }

        /// <summary>
        /// Creates a copy of the SqlClientUtility class.
        /// </summary>
        /// <param name="targetNamespace">The namespace where the SqlClientUtility class should be created in.</param>
        /// <param name="path">The location where the file that contains teh SqlClientUtility class should be created.</param>
        public static void CreateSqlClientUtilityClass(string targetNamespace, string path)
        {
            if (Directory.Exists(path + "\\Data\\MSSQL") == false)
            {
                Directory.CreateDirectory(path + "\\Data\\MSSQL");
            }

            using (StreamWriter streamWriter = new StreamWriter(path + "Data\\MSSQL\\SqlHelper.cs"))
            {
                streamWriter.Write(Utility.GetSqlClientUtilityClass(targetNamespace));//,"\\Data"));
            }
        }

        public static void CreateJDBCUtilityClass(string targetNamespace, string path)
        {
            if (Directory.Exists(path + "\\Data\\MSSQL") == false)
            {
                Directory.CreateDirectory(path + "\\Data\\MSSQL");
            }

            //Reemplazamos los valores del driver y protocolo por sus respectivos datos segun el SGBD
            using (StreamWriter streamWriter = new StreamWriter(path + "Data\\MSSQL\\DBMSConection.java"))
            {
                streamWriter.Write(Utility.GetDBMSConnectionClass(DriverJDBC, Protocolo, Puerto, targetNamespace));//,"\\Data"));
            }

            using (StreamWriter streamWriter = new StreamWriter(path + "Data\\MSSQL\\CommandType.java"))
            {
                streamWriter.Write(Utility.GetJDBCCommandTypeClass(targetNamespace));//,"\\Data"));
            }

            using (StreamWriter streamWriter = new StreamWriter(path + "Data\\MSSQL\\Direction.java"))
            {
                streamWriter.Write(Utility.GetJDBCDirectionClass(targetNamespace));//,"\\Data"));
            }

            using (StreamWriter streamWriter = new StreamWriter(path + "Data\\MSSQL\\Parameter.java"))
            {
                streamWriter.Write(Utility.GetJDBCParameterClass(targetNamespace));//,"\\Data"));
            }

            using (StreamWriter streamWriter = new StreamWriter(path + "Data\\MSSQL\\Connection.java"))
            {
                streamWriter.Write(Utility.GetJDBCConnectionClass(targetNamespace));//,"\\Data"));
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

            using (StreamWriter streamWriter = new StreamWriter(path + "Data\\MSSQL\\UtilDA.cs"))
            {
                streamWriter.Write(Utility.GetUtilDAClass(targetNamespace));//,"\\Data"));
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

            using (StreamWriter streamWriter = new StreamWriter(path + "BO\\UtilBO.cs"))
            {
                streamWriter.Write(Utility.GetUtilBOClass(targetNamespace));//,"\\Data"));
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

            using (StreamWriter streamWriter = new StreamWriter(path + "BO\\GenericsBO.cs"))
            {
                streamWriter.Write(Utility.GetGenericsBOClass(targetNamespace));//,"\\Data"));
            }
        }

        /// <summary>
        /// Creates a copy of the AuditoriaEO class.
        /// </summary>
        /// <param name="targetNamespace">The namespace where the UtilDA class should be created in.</param>
        /// <param name="path">The location where the file that contains the UtilDA class should be created.</param>
        public static void CreateAuditoriaEOClass(string targetNamespace, string path)
        {
            if (Directory.Exists(path + "\\Entities") == false)
            {
                Directory.CreateDirectory(path + "\\Entities");
            }
            using (StreamWriter streamWriter = new StreamWriter(path + "Entities\\AuditoriaEO.cs"))
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

            using (StreamWriter streamWriter = new StreamWriter(path + "\\Data\\MSSQL\\GenericsDA.cs"))
            {
                streamWriter.Write(Utility.GetGenericsDAClass(targetNamespace));//,"\\Data"));
            }
        }

        /// <summary>
        /// Creates a data transfer class that represents a row of data for the specified table.
        /// </summary>
        /// <param name="table">The table to create the data transfer class for.</param>
        /// <param name="targetNamespace">The namespace that the data transfer class should be created in.</param>
        /// <param name="path">The location where the file that contains the data transfer class definition should be created.</param>
        public static void CreateDataTransferClass(Table table, string targetNamespace, string path)
        {
            string className = Utility.FormatPascal(table.Alias);
            if (Directory.Exists(path + "\\Entities") == false)
            {
                Directory.CreateDirectory(path + "\\Entities");
            }
            using (StreamWriter streamWriter = new StreamWriter(path + "\\Entities\\" + className + ".cs"))
            {
                // Create the header for the class
                streamWriter.WriteLine("using System;");
                streamWriter.WriteLine();
                streamWriter.WriteLine("namespace " + targetNamespace + ".EntityObjects {");

                streamWriter.WriteLine("\t/// <summary>");
                streamWriter.WriteLine("\t/// Provides data access for the " + table.Name + " database table.");
                streamWriter.WriteLine("\t/// </summary>");
                streamWriter.WriteLine("\t[Serializable]");
                streamWriter.WriteLine("\tpublic class " + className + " {");

                // Create the private members
                //Comentado por Jackie Giovanni Mora Nonaro
                //Ahora se usa la forma get;set
                //foreach (Column column in table.Columns){
                //    switch (SqlGenerator.proveedor){
                //        case Util.Provider.Access:
                //            streamWriter.WriteLine("\t\tprivate " + Utility.CreateMethodParameter(column) + ";");
                //            break;
                //        case Util.Provider.SqlClient:
                //            streamWriter.WriteLine("\t\tprivate " + Utility.CreateMethodParameter(column) + ";");
                //            break;
                //        case Util.Provider.Oracle:
                //            streamWriter.WriteLine("\t\tprivate " + Utility.CreateMethodParameterOracle(column) + ";");
                //            break;
                //        case Util.Provider.Db2:
                //            break;
                //        case Util.Provider.MySql:
                //            streamWriter.WriteLine("\t\tprivate " + Utility.CreateMethodParameter(column) + ";");
                //            break;
                //        case Util.Provider.PostgreSQL:
                //            streamWriter.WriteLine("\t\tprivate " + Utility.CreateMethodParameter(column) + ";");
                //            break;
                //        default:
                //            break;
                //    }
                //}
                //streamWriter.WriteLine("\t\t");

                // Create an explicit public constructor
                //streamWriter.WriteLine("\t\tpublic " + className + "() {");
                //streamWriter.WriteLine("\t\t}");

                // Create the public properties
                //Comentado por Jackie Giovanni Mora Nonaro
                //Ahora se usa la forma get;set
                //foreach (Column column in table.Columns){
                //    string parameter = string.Empty;
                //    switch (SqlGenerator.proveedor){
                //        case Util.Provider.Access:
                //            parameter = Utility.CreateMethodParameter(column);
                //            break;
                //        case Util.Provider.SqlClient:
                //            parameter = Utility.CreateMethodParameter(column);
                //            break;
                //        case Util.Provider.Oracle:
                //            parameter = Utility.CreateMethodParameterOracle(column);
                //            break;
                //        case Util.Provider.Db2:
                //            break;
                //        case Util.Provider.MySql:
                //            parameter = Utility.CreateMethodParameter(column);
                //            break;
                //        case Util.Provider.PostgreSQL:
                //            parameter = Utility.CreateMethodParameter(column);
                //            break;
                //        default:
                //            break;
                //    }
                //    string type = parameter.Split(' ')[0];
                //    string name = parameter.Split(' ')[1];

                //    streamWriter.WriteLine("\t\t");
                //    streamWriter.WriteLine("\t\tpublic " + type + " " + name.Substring(1, name.Length - 1) + " {");
                //    streamWriter.WriteLine("\t\t\tget { return " + name + "; }");
                //    streamWriter.WriteLine("\t\t\tset { " + name + " = value; }");
                //    streamWriter.WriteLine("\t\t}");
                //}

                // Close out the class and namespace
                //streamWriter.WriteLine("\t}");

                //Agregado por JGMN
                // Create the public properties
                foreach (Column column in table.Columns)
                {
                    string parameter = string.Empty;
                    switch (SqlGenerator.proveedor)
                    {
                        case Util.Provider.Access:
                            parameter = Utility.CreateMethodParameter(column);
                            break;
                        case Util.Provider.SqlClient:
                            parameter = Utility.CreateMethodParameter(column);
                            break;
                        case Util.Provider.Oracle:
                            parameter = Utility.CreateMethodParameterOracle(column);
                            break;
                        case Util.Provider.Db2:
                            break;
                        case Util.Provider.MySql:
                            parameter = Utility.CreateMethodParameter(column);
                            break;
                        case Util.Provider.PostgreSQL:
                            parameter = Utility.CreateMethodParameter(column);
                            break;
                        default:
                            break;
                    }
                    string type = parameter.Split(' ')[0];
                    string name = parameter.Split(' ')[1];
                    //También debemos indicar el tamaño y la validacion en cada campo. Nota: nuevas caracteristicas de EntityFramework
                    if (!column.IsNullable) {
                        streamWriter.WriteLine("\t\t[Required]");
                    }
                    switch (column.Type.ToLower())
                    {
                        case "char":
                        case "character":
                        case "character varying":
                        case "nchar":
                        case "nvarchar":
                        case "varchar":
                            if(Convert.ToInt32(column.Length) > 0)
                            {
                                streamWriter.WriteLine("\t\t[StringLength(" + column.Length + ")]");
                            }
                            break;
                    }
                    streamWriter.WriteLine("\t\tpublic " + type + " " + name.Substring(1, name.Length - 1) + " { get; set; }");
                }

                // Close out the class and namespace
                streamWriter.WriteLine("\t}");

                streamWriter.WriteLine("}");
            }
            //Creando la Coleccion para esta entidad
            using (StreamWriter streamWriter = new StreamWriter(path + "\\Entities\\" + className + "List.cs"))
            {
                // Create the header for the class
                streamWriter.WriteLine("using System;");
                streamWriter.WriteLine("using System.Collections.ObjectModel;");
                streamWriter.WriteLine();
                streamWriter.WriteLine("namespace " + targetNamespace + ".EntityObjects {");

                streamWriter.WriteLine("\t/// <summary>");
                streamWriter.WriteLine("\t/// Provides  Collection data access for the " + table.Alias + " database table.");
                streamWriter.WriteLine("\t/// </summary>");
                //streamWriter.WriteLine("\t[Serializable]");
                streamWriter.WriteLine("\tpublic class " + className + "List:Collection<" + className + "> {");

                // Close out the class and namespace
                streamWriter.WriteLine("\t}");
                streamWriter.WriteLine("}");
            }
        }

        public static void CreateDataBussinessJavaClass(Table table, string targetNamespace, string path, string storedProcedurePrefix)
        {
            string procedureName = storedProcedurePrefix.Length > 0 ? "_" : SqlGenerator.DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "";
            string className = Utility.FormatPascal(table.Alias);
            string espacio = "";
            int Rows = 0;
            espacio = espacio.PadRight(4, ' ');
            try
            {
                if (Directory.Exists(path + "\\Entities") == false)
                {
                    Directory.CreateDirectory(path + "\\Entities");
                }
                using (StreamWriter streamWriter = new StreamWriter(path + "\\Entities\\" + className + "BO.java"))
                {
                    streamWriter.WriteLine("package " + targetNamespace + " ");
                    streamWriter.WriteLine("import java.sql.ResultSet;");
                    streamWriter.WriteLine("import java.sql.Types;");
                    streamWriter.WriteLine("import java.util.ArrayList;");
                    streamWriter.WriteLine("import java.util.List;");
                    streamWriter.WriteLine("import jdbc.JDBCCommandType;");
                    streamWriter.WriteLine("/**");
                    streamWriter.WriteLine(" * La clase <code>" + className + "</code> es una representación de la tabla");
                    streamWriter.WriteLine(" * <code>" + table.Name + "</code> para poder acceder a ella");
                    streamWriter.WriteLine("*/");
                    streamWriter.WriteLine("public class " + className + "BO {");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(espacio + "/**");
                    streamWriter.WriteLine(espacio + " * Metodo que inserta un registro en la tabla " + table.Name);
                    streamWriter.WriteLine(espacio + " * @param E" + className + " Objeto del tipo " + className + " que contiene los datos a insertar en la tabla " + table.Name);
                    streamWriter.WriteLine(espacio + " * @param error Objeto del tipo Error que contiene el mensaje de error en caso de que ocurriera");
                    streamWriter.WriteLine(espacio + " * @return Retorna <tt>true</tt> Si el registro se inserto con exito en la tabla " + table.Name + ";");
                    streamWriter.WriteLine(espacio + " * @return Retorna <tt>false</tt> Si no se logro insertar el registro con exito");
                    streamWriter.WriteLine(espacio + " * @throws Exception Si ocurre un error al insertar el registro a la Base de Datos");
                    streamWriter.WriteLine(espacio + " */");
                    streamWriter.WriteLine(espacio + "public boolean Insertar" + className + "(" + className + " E" + className + ", Error error)throws Exception{");
                    streamWriter.WriteLine(espacio + espacio + "boolean OK = false;");
                    streamWriter.WriteLine(espacio + espacio + "Connection Conexion = null;");
                    streamWriter.WriteLine(espacio + espacio + "int Registros=0;");
                    streamWriter.WriteLine(espacio + espacio + "try{");
                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion = new Connection();");
                    foreach (Column column in table.Columns){
                        Rows++;
                        switch (SqlGenerator.proveedor){
                            case Util.Provider.Access:
                                if (column.IsIdentity){
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                }else{
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                }
                            case Util.Provider.SqlClient:
                                if (column.IsIdentity){
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                }else{
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                }
                            case Util.Provider.Oracle:
                                if (column.IsIdentity){
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                }else{
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                }
                            case Util.Provider.Db2:
                                break;
                            case Util.Provider.MySql:
                                if (column.IsIdentity){
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                }else{
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                }
                                break;
                            case Util.Provider.PostgreSQL:
                                if (column.IsIdentity){
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                }else{
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    streamWriter.WriteLine(espacio + espacio + espacio + "Registros = Conexion.ExecuteUpdate(JDBCCommandType.STORE_PROCEDURE, \"" + procedureName + "_Insertar\");");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if (Registros >0){");
                    streamWriter.WriteLine(espacio + espacio + espacio + espacio + "OK = true;");
                    streamWriter.WriteLine(espacio + espacio + espacio + "}");
                    streamWriter.WriteLine(espacio + espacio + "}catch(Exception ex){");
                    streamWriter.WriteLine(espacio + espacio + espacio + "OK = false;");
                    streamWriter.WriteLine(espacio + espacio + espacio + "error.setMensaje(ex.getMessage());");
                    streamWriter.WriteLine(espacio + espacio + espacio + "throw ex;");
                    streamWriter.WriteLine(espacio + espacio + "}finally{");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Conexion != null){Conexion.Dispose();}");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Conexion != null){Conexion = null;}");
                    streamWriter.WriteLine(espacio + espacio + "}");
                    streamWriter.WriteLine(espacio + espacio + "return OK;");
                    streamWriter.WriteLine(espacio + "}");

                    Rows = 0;
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(espacio + "/**");
                    streamWriter.WriteLine(espacio + " * Metodo que actualiza un registro en la tabla " + table.Name);
                    streamWriter.WriteLine(espacio + " * @param E" + className + " Objeto del tipo " + className + " que contiene los datos del registro a actualizar en la tabla " + table.Name);
                    streamWriter.WriteLine(espacio + " * @param error Objeto del tipo Error que contiene el mensaje de error en caso de que ocurriera");
                    streamWriter.WriteLine(espacio + " * @return Retorna <tt>true</tt> Si el registro se actualizó con exito en la tabla " + table.Name + ";");
                    streamWriter.WriteLine(espacio + " * @return Retorna <tt>false</tt> Si no se logro actualizar el registro con exito");
                    streamWriter.WriteLine(espacio + " * @throws Exception Si ocurre un error al actualizar el registro a la Base de Datos");
                    streamWriter.WriteLine(espacio + " */");
                    streamWriter.WriteLine(espacio + "public boolean Actualizar" + className + "(" + className + " E" + className + ", Error error)throws Exception{");
                    streamWriter.WriteLine(espacio + espacio + "boolean OK = false;");
                    streamWriter.WriteLine(espacio + espacio + "Connection Conexion = null;");
                    streamWriter.WriteLine(espacio + espacio + "int Registros=0;");
                    streamWriter.WriteLine(espacio + espacio + "try{");
                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion = new Connection();");
                    foreach (Column column in table.Columns)
                    {
                        Rows++;

                        switch (SqlGenerator.proveedor)
                        {
                            case Util.Provider.Access:
                                streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                break;
                            case Util.Provider.SqlClient:
                                streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                break;
                            case Util.Provider.Oracle:
                                streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                break;
                            case Util.Provider.Db2:
                                break;
                            case Util.Provider.MySql:
                                streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                break;
                            case Util.Provider.PostgreSQL:
                                streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                break;
                            default:
                                break;
                        }
                    }
                    streamWriter.WriteLine(espacio + espacio + espacio + "Registros = Conexion.ExecuteUpdate(JDBCCommandType.STORE_PROCEDURE, \"" + procedureName + "_Actualizar\");");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if (Registros >0){");
                    streamWriter.WriteLine(espacio + espacio + espacio + espacio + "OK = true;");
                    streamWriter.WriteLine(espacio + espacio + espacio + "}");
                    streamWriter.WriteLine(espacio + espacio + "}catch(Exception ex){");
                    streamWriter.WriteLine(espacio + espacio + espacio + "OK = false;");
                    streamWriter.WriteLine(espacio + espacio + espacio + "error.setMensaje{ex.getMessage());");
                    streamWriter.WriteLine(espacio + espacio + espacio + "throw ex;");
                    streamWriter.WriteLine(espacio + espacio + "}finally{");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Conexion != null){Conexion.Dispose();}");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Conexion != null){Conexion = null;}");
                    streamWriter.WriteLine(espacio + espacio + "}");
                    streamWriter.WriteLine(espacio + espacio + "return OK;");
                    streamWriter.WriteLine(espacio + "}");
                    Rows = 0;
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(espacio + "/**");
                    streamWriter.WriteLine(espacio + " * Metodo que elimina un registro en la tabla " + table.Name);
                    streamWriter.WriteLine(espacio + " * @param E" + className + " Objeto del tipo " + className + " que contiene los datos del registro a eliminar en la tabla " + table.Name);
                    streamWriter.WriteLine(espacio + " * @param error Objeto del tipo Error que contiene el mensaje de error en caso de que ocurriera");
                    streamWriter.WriteLine(espacio + " * @return Retorna <tt>true</tt> Si el registro se eliminó con exito en la tabla " + table.Name + ";");
                    streamWriter.WriteLine(espacio + " * @return Retorna <tt>false</tt> Si no se logro eliminar el registro con exito");
                    streamWriter.WriteLine(espacio + " * @throws Exception Si ocurre un error al eliminar el registro a la Base de Datos");
                    streamWriter.WriteLine(espacio + " */");
                    streamWriter.WriteLine(espacio + "public boolean Eliminar" + className + "(" + className + " E" + className + ", Error error)throws Exception{");
                    streamWriter.WriteLine(espacio + espacio + "boolean OK = false;");
                    streamWriter.WriteLine(espacio + espacio + "Connection Conexion = null;");
                    streamWriter.WriteLine(espacio + espacio + "int Registros=0;");
                    streamWriter.WriteLine(espacio + espacio + "try{");
                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion = new Connection();");
                    foreach (Column column in table.Columns)
                    {
                        if (column.IsPrimaryKey)
                        {
                            Rows++;
                            switch (SqlGenerator.proveedor)
                            {
                                case Util.Provider.Access:
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                case Util.Provider.SqlClient:
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                case Util.Provider.Oracle:
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                case Util.Provider.Db2:
                                    break;
                                case Util.Provider.MySql:
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    streamWriter.WriteLine(espacio + espacio + espacio + "Registros = Conexion.ExecuteUpdate(JDBCCommandType.STORE_PROCEDURE, \"" + procedureName + "_Eliminar\");");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if (Registros >0){");
                    streamWriter.WriteLine(espacio + espacio + espacio + espacio + "OK = true;");
                    streamWriter.WriteLine(espacio + espacio + espacio + "}");
                    streamWriter.WriteLine(espacio + espacio + "}catch(Exception ex){");
                    streamWriter.WriteLine(espacio + espacio + espacio + "OK = false;");
                    streamWriter.WriteLine(espacio + espacio + espacio + "error.setMensaje(ex.getMessage());");
                    streamWriter.WriteLine(espacio + espacio + espacio + "throw ex;");
                    streamWriter.WriteLine(espacio + espacio + "}finally{");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Conexion != null){Conexion.Dispose();}");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Conexion != null){Conexion = null;}");
                    streamWriter.WriteLine(espacio + espacio + "}");
                    streamWriter.WriteLine(espacio + espacio + "return OK;");
                    streamWriter.WriteLine(espacio + "}");

                    Rows = 0;
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(espacio + "/**");
                    streamWriter.WriteLine(espacio + " * Metodo que consulta un registro en la tabla " + table.Name);
                    streamWriter.WriteLine(espacio + " * @param E" + className + " Objeto del tipo " + className + " que contiene algun dato del registro que se desea consultar en la tabla " + table.Name);
                    streamWriter.WriteLine(espacio + " * @param error Objeto del tipo Error que contiene el mensaje de error en caso de que ocurriera");
                    streamWriter.WriteLine(espacio + " * @return Devuelve un objeto del tipo " + className + " que contiene los datos del registro consultado en la tabla " + table.Name);
                    streamWriter.WriteLine(espacio + " * @throws Exception Si ocurre un error al consultar el registro a la Base de Datos");
                    streamWriter.WriteLine(espacio + " */");
                    streamWriter.WriteLine(espacio + "public " + className + " Consultar" + className + "(" + className + " E" + className + ", Error error)throws Exception{");
                    streamWriter.WriteLine(espacio + espacio + "Connection Conexion = null;");
                    streamWriter.WriteLine(espacio + espacio + "ResultSet Resultados = null;");
                    streamWriter.WriteLine(espacio + espacio + "try{");
                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion = new Connection();");
                    foreach (Column column in table.Columns)
                    {
                        if (column.IsPrimaryKey)
                        {
                            Rows++;
                            switch (SqlGenerator.proveedor)
                            {
                                case Util.Provider.Access:
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                case Util.Provider.SqlClient:
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                case Util.Provider.Oracle:
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                case Util.Provider.Db2:
                                    break;
                                case Util.Provider.MySql:
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                case Util.Provider.PostgreSQL:
                                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion.AddParameter(" + Rows + ", E" + className + ".get" + column.Alias + "(), " + Utility.CreateMethodJavaTypeParameter(column) + ");");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    Rows = 0;
                    streamWriter.WriteLine(espacio + espacio + espacio + "Resultados = Conexion.ExecuteQuery(JDBCCommandType.STORE_PROCEDURE, \"" + procedureName + "_Consultar\");");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if (Resultados != null){");
                    streamWriter.WriteLine(espacio + espacio + espacio + espacio + "while(Resultados.next()){");
                    foreach (Column column in table.Columns)
                    {
                        Rows++;
                        switch (SqlGenerator.proveedor)
                        {
                            case Util.Provider.Access:
                                streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + ".set" + column.Alias + "(" + Utility.CreateMethodJavaDataParameter(column) + "(" + Rows + "));");
                                break;
                            case Util.Provider.SqlClient:
                                streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + ".set" + column.Alias + "(" + Utility.CreateMethodJavaDataParameter(column) + "(" + Rows + "));");
                                break;
                            case Util.Provider.Oracle:
                                streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + ".set" + column.Alias + "(" + Utility.CreateMethodJavaDataParameter(column) + "(" + Rows + "));");
                                break;
                            case Util.Provider.Db2:
                                break;
                            case Util.Provider.MySql:
                                streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + ".set" + column.Alias + "(" + Utility.CreateMethodJavaDataParameter(column) + "(" + Rows + "));");
                                break;
                            case Util.Provider.PostgreSQL:
                                streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + ".set" + column.Alias + "(" + Utility.CreateMethodJavaDataParameter(column) + "(" + Rows + "));");
                                break;
                            default:
                                break;
                        }
                    }
                    streamWriter.WriteLine(espacio + espacio + espacio + espacio + "}");
                    streamWriter.WriteLine(espacio + espacio + espacio + "}");
                    streamWriter.WriteLine(espacio + espacio + "}catch(Exception ex){");
                    streamWriter.WriteLine(espacio + espacio + espacio + "error.setMensaje(ex.getMessage());");
                    streamWriter.WriteLine(espacio + espacio + espacio + "throw ex;");
                    streamWriter.WriteLine(espacio + espacio + "}finally{");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Conexion != null){Conexion.Dispose();}");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Conexion != null){Conexion = null;}");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Resultados != null){Resultados.close();}");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Resultados != null){Resultados = null;}");
                    streamWriter.WriteLine(espacio + espacio + "}");
                    streamWriter.WriteLine(espacio + espacio + "return E" + className + ";");
                    streamWriter.WriteLine(espacio + "}");

                    Rows = 0;
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(espacio + "/**");
                    streamWriter.WriteLine(espacio + " * Metodo que consulta todos los registros en la tabla " + table.Name);
                    streamWriter.WriteLine(espacio + " * @param error Objeto del tipo Error que contiene el mensaje de error en caso de que ocurriera");
                    streamWriter.WriteLine(espacio + " * @return Devuelve una colección del tipo <tt>List<" + className + "></tt> que contiene los datos de los registros consultados en la tabla " + table.Name);
                    streamWriter.WriteLine(espacio + " * @throws Exception Si ocurre un error al consultar los registros en la Base de Datos");
                    streamWriter.WriteLine(espacio + " */");
                    streamWriter.WriteLine(espacio + "public List<" + className + "> Consultar" + className + "(Error error)throws Exception{");
                    streamWriter.WriteLine(espacio + espacio + "Connection Conexion = null;");
                    streamWriter.WriteLine(espacio + espacio + "ResultSet Resultados = null;");
                    streamWriter.WriteLine(espacio + espacio + "List<" + className + "> E" + className + "s = null;");
                    streamWriter.WriteLine(espacio + espacio + "" + className + " E" + className + " = null;");
                    streamWriter.WriteLine(espacio + espacio + "try{");
                    streamWriter.WriteLine(espacio + espacio + espacio + "Conexion = new Connection();");
                    streamWriter.WriteLine(espacio + espacio + espacio + "Resultados = Conexion.ExecuteQuery(JDBCCommandType.STORE_PROCEDURE, \"" + procedureName + "_Listar\");");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if (Resultados != null){");
                    streamWriter.WriteLine(espacio + espacio + espacio + espacio + "E" + className + "s = new ArrayList<" + className + ">();");
                    streamWriter.WriteLine(espacio + espacio + espacio + espacio + "while(Resultados.next()){");
                    streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + " = new " + className + "();");
                    foreach (Column column in table.Columns)
                    {
                        Rows++;
                        switch (SqlGenerator.proveedor)
                        {
                            case Util.Provider.Access:
                                streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + ".set" + column.Alias + "(" + Utility.CreateMethodJavaDataParameter(column) + "(" + Rows + "));");
                                break;
                            case Util.Provider.SqlClient:
                                streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + ".set" + column.Alias + "(" + Utility.CreateMethodJavaDataParameter(column) + "(" + Rows + "));");
                                break;
                            case Util.Provider.Oracle:
                                streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + ".set" + column.Alias + "(" + Utility.CreateMethodJavaDataParameter(column) + "(" + Rows + "));");
                                break;
                            case Util.Provider.Db2:
                                break;
                            case Util.Provider.MySql:
                                streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + ".set" + column.Alias + "(" + Utility.CreateMethodJavaDataParameter(column) + "(" + Rows + "));");
                                break;
                            case Util.Provider.PostgreSQL:
                                streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + ".set" + column.Alias + "(" + Utility.CreateMethodJavaDataParameter(column) + "(" + Rows + "));");
                                break;
                            default:
                                break;
                        }
                    }
                    streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + "s.add(E" + className + ");");
                    streamWriter.WriteLine(espacio + espacio + espacio + espacio + espacio + "E" + className + " = null;");

                    streamWriter.WriteLine(espacio + espacio + espacio + espacio + "}");
                    streamWriter.WriteLine(espacio + espacio + espacio + "}");
                    streamWriter.WriteLine(espacio + espacio + "}catch(Exception ex){");
                    streamWriter.WriteLine(espacio + espacio + espacio + "error.setMensaje(ex.getMessage());");
                    streamWriter.WriteLine(espacio + espacio + espacio + "throw ex;");
                    streamWriter.WriteLine(espacio + espacio + "}finally{");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Conexion != null){Conexion.Dispose();}");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Conexion != null){Conexion = null;}");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Resultados != null){Resultados.close();}");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(Resultados != null){Resultados = null;}");
                    streamWriter.WriteLine(espacio + espacio + espacio + "if(E" + className + " != null){E" + className + " = null;}");
                    streamWriter.WriteLine(espacio + espacio + "}");
                    streamWriter.WriteLine(espacio + espacio + "return E" + className + "s;");
                    streamWriter.WriteLine(espacio + "}");

                    streamWriter.WriteLine("}");
                    streamWriter.WriteLine();
                }
            }
            catch ( Exception  ex) {
                Utility.P_Generalog(ex);
                throw (new Exception("CreateDataBussinessJavaClass: ", ex));

            }

            
        }

        public static void CreateDataTransferJavaClass(Table table, string targetNamespace, string path)
        {
            string className = Utility.FormatPascal(table.Alias);
            string espacio = "";
            espacio = espacio.PadRight(4, ' ');
            try{
                if (Directory.Exists(path + "\\Entities") == false){
                    Directory.CreateDirectory(path + "\\Entities");
                }
                using (StreamWriter streamWriter = new StreamWriter(path + "\\Entities\\" + className + ".java")){
                    // Create the header for the class
                    //streamWriter.WriteLine("using System;");
                    streamWriter.WriteLine("package " + targetNamespace + " ");

                    streamWriter.WriteLine("/**");
                    streamWriter.WriteLine(" * La clase <code>" + className + "</code> es una representación de la tabla");
                    streamWriter.WriteLine(" * <code>" + table.Name + "</code> para poder acceder a ella");
                    streamWriter.WriteLine("*/");
                    streamWriter.WriteLine("public class " + className + " {");

                    // Create the private members
                    //foreach (Column column in table.Columns){
                    //    switch (SqlGenerator.proveedor){
                    //        case Util.Provider.Access:
                    //            streamWriter.WriteLine(espacio + "private " + Utility.CreateMethodJavaParameter(column) + ";");
                    //            break;
                    //        case Util.Provider.SqlClient:
                    //            streamWriter.WriteLine(espacio + "private " + Utility.CreateMethodJavaParameter(column) + ";");
                    //            break;
                    //        case Util.Provider.Oracle:
                    //            streamWriter.WriteLine(espacio + "private " + Utility.CreateMethodJavaParameter(column) + ";");
                    //            break;
                    //        case Util.Provider.Db2:
                    //            break;
                    //        case Util.Provider.MySql:
                    //            streamWriter.WriteLine(espacio + "private " + Utility.CreateMethodJavaParameter(column) + ";");
                    //            break;
                    //        case Util.Provider.PostgreSQL:
                    //            streamWriter.WriteLine(espacio + "private " + Utility.CreateMethodJavaParameter(column) + ";");
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}
                    streamWriter.WriteLine("\t");

                    // Create an explicit public constructor
                    streamWriter.WriteLine(espacio + "public " + className + "() {");
                    //Inicializamos los atributos de la clase
                    //InicializarJavaParameter(column)
                    foreach (Column column in table.Columns){
                        string parameter = string.Empty;
                        switch (SqlGenerator.proveedor){
                            case Util.Provider.Access:
                                parameter = Utility.InicializarJavaParameter(column);
                                break;
                            case Util.Provider.SqlClient:
                                parameter = Utility.InicializarJavaParameter(column);
                                break;
                            case Util.Provider.Oracle:
                                parameter = Utility.InicializarJavaParameter(column);
                                break;
                            case Util.Provider.Db2:
                                break;
                            case Util.Provider.MySql:
                                parameter = Utility.InicializarJavaParameter(column);
                                break;
                            case Util.Provider.PostgreSQL:
                                parameter = Utility.InicializarJavaParameter(column);
                                break;
                            default:
                                break;
                        }
                        //streamWriter.WriteLine(espacio + espacio + "" + parameter);
                    }
                    //streamWriter.WriteLine(espacio + "");
                    foreach (Column column in table.Columns)
                    {
                        string parameter = string.Empty;
                        switch (SqlGenerator.proveedor)
                        {
                            case Util.Provider.Access:
                                parameter = Utility.InicializarJavaParameter(column);
                                break;
                            case Util.Provider.SqlClient:
                                parameter = Utility.InicializarJavaParameter(column);
                                break;
                            case Util.Provider.Oracle:
                                parameter = Utility.InicializarJavaParameter(column);
                                break;
                            case Util.Provider.Db2:
                                break;
                            case Util.Provider.MySql:
                                parameter = Utility.InicializarJavaParameter(column);
                                break;
                            case Util.Provider.PostgreSQL:
                                parameter = Utility.InicializarJavaParameter(column);
                                break;
                            default:
                                break;
                        }
                        streamWriter.WriteLine(espacio + espacio + "this." + parameter);
                    }
                    streamWriter.WriteLine(espacio + "}");

                    // Create the public properties
                    foreach (Column column in table.Columns){
                        string parameter = string.Empty;
                        switch (SqlGenerator.proveedor){
                            case Util.Provider.Access:
                                parameter = Utility.CreateMethodJavaParameter(column);
                                break;
                            case Util.Provider.SqlClient:
                                parameter = Utility.CreateMethodJavaParameter(column);
                                break;
                            case Util.Provider.Oracle:
                                parameter = Utility.CreateMethodJavaParameter(column);
                                break;
                            case Util.Provider.Db2:
                                break;
                            case Util.Provider.MySql:
                                parameter = Utility.CreateMethodJavaParameter(column);
                                break;
                            case Util.Provider.PostgreSQL:
                                parameter = Utility.CreateMethodJavaParameter(column);
                                break;
                            default:
                                break;
                        }
                        string type = parameter.Split(' ')[0];
                        string name = parameter.Split(' ')[1];

                        //streamWriter.WriteLine("\t");
                        //streamWriter.WriteLine(espacio + "/**");
                        //streamWriter.WriteLine(espacio + " * Metodo que devuelve el atributo " + name);
                        //streamWriter.WriteLine(espacio + " * @return Devuelve el atributo " + name);
                        //streamWriter.WriteLine(espacio + " */");
                        //streamWriter.WriteLine(espacio + "public " + type + " get" + name + " () {");
                        //streamWriter.WriteLine(espacio + espacio + "return " + name + ";");
                        //streamWriter.WriteLine(espacio + "}");

                        //streamWriter.WriteLine("\t");
                        //streamWriter.WriteLine(espacio + "/**");
                        //streamWriter.WriteLine(espacio + " * Metodo que asigna un valor al atributo " + name);
                        //streamWriter.WriteLine(espacio + " * @param " + name + " Parametro de tipo " + type + " que contiene el valor para asignar al atributo " + name);
                        //streamWriter.WriteLine(espacio + " */");
                        //streamWriter.WriteLine(espacio + "public void set" + name + " (" + type + " " + name + ") {");
                        //streamWriter.WriteLine(espacio + espacio + "this." + name + " = " + name + ";");
                        //streamWriter.WriteLine(espacio + "}");

                    }

                    // Create the public properties Jquery Model
                    /*foreach (Column column in table.Columns)
                    {
                        string parameter = string.Empty;
                        switch (SqlGenerator.proveedor)
                        {
                            case Util.Provider.Access:
                                parameter = Utility.CreateMethodJavaParameter(column);
                                break;
                            case Util.Provider.SqlClient:
                                parameter = Utility.CreateMethodJavaParameter(column);
                                break;
                            case Util.Provider.Oracle:
                                parameter = Utility.CreateMethodJavaParameter(column);
                                break;
                            case Util.Provider.Db2:
                                break;
                            case Util.Provider.MySql:
                                parameter = Utility.CreateMethodJavaParameter(column);
                                break;
                            case Util.Provider.PostgreSQL:
                                parameter = Utility.CreateMethodJavaParameter(column);
                                break;
                            default:
                                break;
                        }
                        string type = parameter.Split(' ')[0];
                        string name = parameter.Split(' ')[1];
                        streamWriter.WriteLine("\t");
                        streamWriter.WriteLine(espacio + espacio + "this." + name + " = " + name + ";");

                    }*/

                    // Close out the class and namespace
                    streamWriter.WriteLine("}");
                    streamWriter.WriteLine();
                }
                //Creando la Coleccion para esta entidad
                using (StreamWriter streamWriter = new StreamWriter(path + "\\Entities\\" + className + "List.cs"))
                {
                    // Create the header for the class
                    streamWriter.WriteLine("using System;");
                    streamWriter.WriteLine("using System.Collections.ObjectModel;");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("namespace " + targetNamespace + ".EntityObjects {");

                    streamWriter.WriteLine("\t/// <summary>");
                    streamWriter.WriteLine("\t/// Provides  Collection data access for the " + table.Alias + " database table.");
                    streamWriter.WriteLine("\t/// </summary>");
                    //streamWriter.WriteLine("\t[Serializable]");
                    streamWriter.WriteLine("\tpublic class " + className + "List:Collection<" + className + "> {");

                    // Close out the class and namespace
                    streamWriter.WriteLine("\t}");
                    streamWriter.WriteLine("}");
                }
            }
            catch (Exception ex) {
                Utility.P_Generalog(ex);
                throw (new Exception("CreateDataTransferJavaClass: ", ex));
            }
            
        }

        /// <summary>
        /// Creates a C# data access class for all of the table's stored procedures.
        /// Se crea la clase de datos para C#
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="storedProcedurePrefix">Prefix to be appended to the name of the stored procedure.</param>
        /// <param name="path">Path where the class should be created.</param>
        public static void CreateDataAccessClass(Table table, string targetNamespace, string storedProcedurePrefix, string path, System.Data.DataTable dtQuery, string storedProcedurePrefixExecute)
        {
            if (Directory.Exists(path + "\\Data\\MSSQL") == false){
                Directory.CreateDirectory(path + "\\Data\\MSSQL");
            }

            string className = table.Alias + "DA";
            using (StreamWriter streamWriter = new StreamWriter(path + "\\Data\\MSSQL\\" + className + ".cs")){
                // Create the header for the class
                streamWriter.WriteLine("using System;");
                streamWriter.WriteLine("using System.Collections;");
                streamWriter.WriteLine("using System.Collections.Generic;");
                streamWriter.WriteLine("using System.Data;");
                streamWriter.WriteLine("using System.Data.SqlClient;");
                //streamWriter.WriteLine("using Microsoft.ApplicationsBlocks.Data;");
                streamWriter.WriteLine();
                streamWriter.WriteLine("using " + targetNamespace + ".EntityObjects;");
                streamWriter.WriteLine();
                streamWriter.WriteLine("namespace " + targetNamespace + ".Data.SqlServer {");

                streamWriter.WriteLine("\tpublic class " + className + " {");
                streamWriter.WriteLine("\t\tprivate ContextService contextService;");
                //streamWriter.WriteLine("\t\tprivate string conString;");
                //streamWriter.WriteLine("\t\tprivate " + table.Alias + " _" + Utility.FormatCamel(table.Alias) + ";");
                //streamWriter.WriteLine("\t\tprivate GenericoManager<" + table.Alias + "> _GM=new GenericoManager<" + table.Alias + ">();");

                streamWriter.WriteLine("\t\tpublic " + className + "(ContextService currentContextService){");
                streamWriter.WriteLine("\t\t\tthis.contextService = currentContextService;");
                streamWriter.WriteLine("\t\t}");
                streamWriter.WriteLine("\t\tpublic " + className + "() {}");

                //streamWriter.WriteLine("\t\tpublic " + className + "() {conString=UtilDA.connectionString();_GM.SetMetodoAsignaObjeto(MapearObjeto);}");
                //streamWriter.WriteLine("\t\tpublic " + className + "(string conString) {this.conString=conString;_GM.SetMetodoAsignaObjeto(MapearObjeto);}");

                // Append the access methods
                CreateDAInsertMethod(table, storedProcedurePrefix, streamWriter);
                CreateDAUpdateMethod(table, storedProcedurePrefix, streamWriter);
                CreateDADeleteMethod(table, storedProcedurePrefix, streamWriter);
                //CreateDeleteAllByMethods(table, storedProcedurePrefix, streamWriter);
                CreateDASelectMethod(table, storedProcedurePrefix, streamWriter);
                CreateDASelectAllMethod(table, storedProcedurePrefix, streamWriter);
                CreateDASelectAllGenericsMethod(table, storedProcedurePrefix, streamWriter);
                CreateDAObjectMapperMethod(table, storedProcedurePrefix, streamWriter);
                CreateDAObjectMapperMethodExecute(table, storedProcedurePrefixExecute, streamWriter, dtQuery);
                //CreateSelectAllByMethods(table, storedProcedurePrefix, streamWriter);
                //CreateMakeMethod(table, streamWriter);

                // Close out the class and namespace
                streamWriter.WriteLine("\t}");
                streamWriter.WriteLine("}");
            }
        }


        /// <summary>
        /// Creates a string that represents the insert functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDAExecuteMethod(Table table, string storedProcedureexecute, StreamWriter streamWriter)
        {
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t/// <summary>");
            streamWriter.WriteLine("\t\t/// Muestra el resultado del execute " + storedProcedureexecute + ".");
            streamWriter.WriteLine("\t\t/// </summary>");
            streamWriter.Write("\t\tpublic int Insertar(");
            streamWriter.Write(Utility.FormatPascal(table.Alias) + " " + Utility.FormatCamel(table.Alias) + "");
            streamWriter.WriteLine(") {");


        }
        /// <summary>
        /// Creates a string that represents the insert functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDAInsertMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            // Append the method header
            string procedureName = storedProcedurePrefix.Length > 0 ? "_" : DataBaseSys.Substring(0, 3) + "_" + "SP" + table.Name + "_Insertar";
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t/// <summary>");
            streamWriter.WriteLine("\t\t/// Inserta un registro en la tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t/// </summary>");

            streamWriter.Write("\t\tpublic int Insertar(");
            streamWriter.Write(Utility.FormatPascal(table.Alias) + " " + Utility.FormatCamel(table.Alias) + "");
            streamWriter.WriteLine(") {");

            //streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");

            // Create the parameters
            StringBuilder builder = new StringBuilder();
            foreach (Column column in table.Columns){
                builder.Append("\t\t\t\t\t" + Utility.CreateSqlParameterCSharp(table, column, false) + "" + Environment.NewLine);
                //builder.Append("_" + Utility.FormatCamel(table.Alias) + "." + column.Alias + ",");
            }

            //streamWriter.WriteLine();
            //streamWriter.WriteLine("\t\t\t};");
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t\tint result=-1;");
            streamWriter.WriteLine("\t\t\ttry{");
            //streamWriter.WriteLine("\t\t\tusing(SqlConnection _sqlConnection=new SqlConnection(this.conString))");
            streamWriter.WriteLine("\t\t\t\tusing (SqlCommand sql_comando = new SqlCommand()){");
            streamWriter.WriteLine("\t\t\t\t\tsql_comando.Connection = contextService.getConnection;");
            streamWriter.WriteLine("\t\t\t\t\tsql_comando.Transaction = contextService.getTransaction;");
            streamWriter.WriteLine("\t\t\t\t\tsql_comando.CommandType = CommandType.StoredProcedure;");
            streamWriter.WriteLine("\t\t\t\t\tsql_comando.CommandText = \"" + procedureName + "\";");
            streamWriter.WriteLine("" + builder.ToString(0, builder.Length - 1) + "");
            streamWriter.WriteLine("\t\t\t\t\tresult = sql_comando.ExecuteNonQuery();");
            builder = new StringBuilder();
            foreach (Column column in table.Columns){
                if (column.IsIdentity || column.IsRowGuidCol) {
                    builder.Append("\t\t\t\t\t" + Utility.CreateSqlParameterCSharpOutPut(table, column) + "" + Environment.NewLine);
                }
            }
            if (builder.Length > 0) {
                streamWriter.WriteLine("" + builder.ToString(0, builder.Length - 1) + "");
            }
            streamWriter.WriteLine("\t\t\t\t}");
            streamWriter.WriteLine("\t\t\t\treturn result;");
            streamWriter.WriteLine("\t\t\t}catch (Exception e){");
            streamWriter.WriteLine("\t\t\t\tthrow e;");
            streamWriter.WriteLine("\t\t\t}");
            // Append the parameter value extraction
            //bool lineInserted = false;
            //for (int i = 0; i < table.Columns.Count; i++) {
            //    Column column = (Column) table.Columns[i];
            //    if (column.IsIdentity || column.IsRowGuidCol) {
            //        if (lineInserted == false) {
            //            streamWriter.WriteLine();
            //            lineInserted = true;
            //        }

            //        if (column.IsIdentity) {
            //            if (column.Length == "4") {
            //                streamWriter.WriteLine("\t\t\t" + Utility.FormatCamel(table.Alias) + "." + Utility.FormatPascal(column.Alias) + " = (int) parameters[" + i.ToString() + "].Value;");
            //            } else if (column.Length == "8") {
            //                streamWriter.WriteLine("\t\t\t" + Utility.FormatCamel(table.Alias) + "." + Utility.FormatPascal(column.Alias) + " = (long) parameters[" + i.ToString() + "].Value;");
            //            }
            //        } else {
            //            streamWriter.WriteLine("\t\t\t" + Utility.FormatCamel(table.Alias) + "." + Utility.FormatPascal(column.Alias) + " = (Guid) parameters[" + i.ToString() + "].Value;");
            //        }
            //    }
            //}

            // Append the method footer
            streamWriter.WriteLine("\t\t}");
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
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Actualiza un registro en la Tabla " + table.Name + ".");
                streamWriter.WriteLine("\t\t/// </summary>");

                streamWriter.Write("\t\tpublic int Actualizar(");
                streamWriter.Write(Utility.FormatPascal(table.Alias) + " _" + Utility.FormatCamel(table.Alias) + "");
                streamWriter.WriteLine(") {");

                //streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");

                // Create the parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);
                    builder.Append("_" + Utility.FormatCamel(table.Alias) + "." + column.Alias + ",");
                }
                //streamWriter.WriteLine();
                //streamWriter.WriteLine("\t\t\t};");
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t\tint result=-1;");
                streamWriter.WriteLine("\t\t\tusing(SqlConnection _sqlConnection=new SqlConnection(this.conString))");
                streamWriter.WriteLine("\t\t\t{_sqlConnection.Open();");
                streamWriter.WriteLine("\t\t\t result=SqlHelper.ExecuteNonQuery(_sqlConnection, \"" + procedureName + "\", " + builder.ToString(0, builder.Length - 1) + ");");
                streamWriter.WriteLine("\t\t\t}");
                streamWriter.WriteLine("\t\t\treturn result;");
                // Append the method footer
                streamWriter.WriteLine("\t\t}");

                //streamWriter.Write("\t\tpublic static void Update(");
                //streamWriter.Write(Utility.FormatPascal(table.Alias) + " _" + Utility.FormatCamel(table.Alias) + ", ");
                //streamWriter.WriteLine("SqlConnection connection, SqlTransaction transaction) {");

                //// Append the parameters
                //streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");
                //StringBuilder builder = new StringBuilder();
                //foreach (Column column in table.Columns) {
                //    builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, true) + "," + Environment.NewLine);
                //}
                //streamWriter.WriteLine(builder.ToString(0, builder.Length - (Environment.NewLine.Length + 1)));
                //streamWriter.WriteLine("\t\t\t};");
                //streamWriter.WriteLine();

                //streamWriter.WriteLine("\t\t\tSqlClientUtility.ExecuteNonQuery(connection, transaction, \"" + table.Name + "Update\", parameters);");

                //// Append the method footer
                //streamWriter.WriteLine("\t\t}");
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
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Elimina un Registro de la Tabla " + table.Name + " table por su llave primaria.");
                streamWriter.WriteLine("\t\t/// </summary>");

                streamWriter.Write("\t\tpublic int Eliminar(");
                streamWriter.Write(Utility.FormatPascal(table.Alias) + " _" + Utility.FormatCamel(table.Alias) + "");
                streamWriter.WriteLine(") {");

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
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t\tint result=-1;");
                streamWriter.WriteLine("\t\t\tusing(SqlConnection _sqlConnection=new SqlConnection(this.conString))");
                streamWriter.WriteLine("\t\t\t{_sqlConnection.Open();");
                streamWriter.WriteLine("\t\t\t result = SqlHelper.ExecuteNonQuery(_sqlConnection, \"" + procedureName + "\", " + builder.ToString(0, builder.Length - 1) + ");");
                streamWriter.WriteLine("\t\t\t}");
                streamWriter.WriteLine("\t\t\treturn result;");
                // Append the method footer
                streamWriter.WriteLine("\t\t}");

                //streamWriter.Write("\t\tpublic static void Delete(");
                //foreach (Column column in table.PrimaryKeys) {
                //    streamWriter.Write(Utility.CreateMethodParameter(column) + ", ");
                //}
                //streamWriter.WriteLine("SqlConnection connection, SqlTransaction transaction) {");

                //// Create the parameters
                //streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");
                //StringBuilder builder = new StringBuilder();
                //foreach (Column column in table.PrimaryKeys) {
                //    builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(null, column, true) + "," + Environment.NewLine);
                //}
                //streamWriter.WriteLine(builder.ToString(0, builder.Length - (Environment.NewLine.Length + 1)));
                //streamWriter.WriteLine("\t\t\t};");
                //streamWriter.WriteLine();

                //// Append the stored procedure execution
                //streamWriter.WriteLine("\t\t\tSqlClientUtility.ExecuteNonQuery(connection, transaction, \"" + table.Name + "Delete\", parameters);");

                //// Append the method footer
                //streamWriter.WriteLine("\t\t}");
            }
        }

        /// <summary>
        /// Creates a string that represents the "delete by" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDADeleteAllByMethods(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            // Create a stored procedure for each foreign key
            foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
            {
                // Create the stored procedure name
                StringBuilder stringBuilder = new StringBuilder(255);
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
                stringBuilder.Append("DeleteAllBy");
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

                // Create the delete function based on keys
                // Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Deletes a record from the " + table.Name + " table by a foreign key.");
                streamWriter.WriteLine("\t\t/// </summary>");

                streamWriter.Write("\t\tpublic int Delete(");
                streamWriter.Write(Utility.FormatPascal(table.Alias) + " _" + Utility.FormatCamel(table.Alias) + "");
                streamWriter.WriteLine(") {");

                //streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");

                // Create the parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in compositeKeyList)
                {
                    //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);
                    builder.Append("_" + table.Alias + "." + column.Alias + ",");
                }
                //streamWriter.WriteLine();
                //streamWriter.WriteLine("\t\t\t};");
                streamWriter.WriteLine();

                streamWriter.WriteLine("\t\t\treturn SqlHelper.ExecuteNonQuery(this.conString, \"" + methodName + "\", " + builder.ToString(0, builder.Length - 1) + ");");
                // Append the method footer
                streamWriter.WriteLine("\t\t}");

                //streamWriter.Write("\t\tpublic static void " + methodName + "(");
                //foreach (Column column in compositeKeyList) {
                //    streamWriter.Write(Utility.CreateMethodParameter(column) + ", ");
                //}
                //streamWriter.WriteLine("SqlConnection connection, SqlTransaction transaction) {");

                //// Create the parameters
                //streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");
                //StringBuilder builder = new StringBuilder();
                //foreach (Column column in compositeKeyList) {
                //    builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(null, column, true) + "," + Environment.NewLine);
                //}
                //streamWriter.WriteLine(builder.ToString(0, builder.Length - (Environment.NewLine.Length + 1)));
                //streamWriter.WriteLine("\t\t\t};");
                //streamWriter.WriteLine();

                //// Append the stored procedure execution
                //streamWriter.WriteLine("\t\t\tSqlClientUtility.ExecuteNonQuery(connection, transaction, \"" + methodName + "\", parameters);");

                //// Append the method footer
                //streamWriter.WriteLine("\t\t}");
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
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Mapea un Registro de la Tabla Hacia una Entidad " + table.Name + ".");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\tpublic " + table.Alias + " MapearObjeto(IDataReader reader) {");
                streamWriter.WriteLine("\t\t\t " + table.Alias + " obj" + table.Alias + " = new " + table.Alias + " ();");
                foreach (Column column in table.Columns)
                {
                    //streamWriter.WriteLine("\t\t\t obj" + table.Alias + "." + column.Alias + " = " + (column.IsNullable ? "(reader[\"" + column.Name + "\"]==DBNull.Value?null:" + Utility.CreateConvertXxxMethod(column) + "(reader[\"" + column.Name + "\"])" + ");" : Utility.CreateConvertXxxMethod(column) + "(reader[\"" + column.Name + "\"]);"));
                    streamWriter.WriteLine("\t\t\t obj" + table.Alias + "." + column.Alias + " = reader[\"" + column.Name + "\"].getNullOrValue<" + Utility.CreateGetTableTypeMethod(column.Type.ToString()) + ", object>();");
                }

                streamWriter.WriteLine("\t\t\t return obj" + table.Alias + ";");

                // Append the method footer
                streamWriter.WriteLine("\t\t}");
            }
        }


        private static void CreateDAObjectMapperMethodExecute(Table table, string storedProcedurePrefix, StreamWriter streamWriter, System.Data.DataTable dtQuery)
        {
            if (storedProcedurePrefix.Trim()!=""){
                // Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Mapea un Registro de la Tabla Hacia una Entidad " + table.Name + ".");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\tpublic " + table.Alias + " MapearObjetoConsulta(IDataReader reader) {");
                streamWriter.WriteLine("\t\t\t " + table.Alias + " obj" + table.Alias + " = new " + table.Alias + " ();");
                foreach (System.Data.DataColumn column in dtQuery.Columns){
                    streamWriter.WriteLine("\t\t\t obj" + table.Alias + "." + column.ColumnName + " = reader[\"" + column.ColumnName + "\"].getNullOrValue<" + Utility.CreateGetTableTypeMethod(column.DataType.Name) + ", object>();");
                    //Utility.CreateConvertXxxMethod(column)
                }
                streamWriter.WriteLine("\t\t\t return obj" + table.Alias + ";");
                // Append the method footer
                streamWriter.WriteLine("\t\t}");
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
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Selecciona un registro de la tabla " + table.Name + ".");
                streamWriter.WriteLine("\t\t/// </summary>");

                // Create the parameters
                StringBuilder builderParam = new StringBuilder();
                foreach (Column column in table.PrimaryKeys)
                {
                    //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);
                    builderParam.Append("" + column.Type + " " + column.Alias + ",");
                }

                streamWriter.Write("\t\tpublic " + table.Alias + " Seleccionar(");
                streamWriter.Write("" + builderParam.ToString(0, builderParam.Length - 1));
                streamWriter.WriteLine(") {");

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
                streamWriter.WriteLine("\t\t\tusing(SqlConnection _sqlConnection=new SqlConnection(this.conString))");
                streamWriter.WriteLine("\t\t\t{_sqlConnection.Open();");
                streamWriter.WriteLine("\t\t\tusing(SqlDataReader reader = SqlHelper.ExecuteReader(_sqlConnection, \"" + procedureName + "\", " + builder.ToString(0, builder.Length - 1) + "))");

                //aqui convertimos el DataSet en Entidad
                streamWriter.WriteLine("\t\t\t{");
                //streamWriter.WriteLine("\t\t\t{while(reader.Read()){");

                //streamWriter.WriteLine("\t\t\t _" + Utility.FormatCamel(table.Alias) + "=new " + table.Alias + "();");

                //for (int i = 0; i < table.Columns.Count; i++)
                //{
                //    Column column = (Column)table.Columns[i];
                //    string columnNamePascal = column.Alias;
                //    streamWriter.WriteLine("\t\t\t\t_" + Utility.FormatCamel(table.Alias) + "." + columnNamePascal + " = " + Utility.CreateConvertXxxMethod(column) + "(reader[\"" + column.Name  + "\"]);");
                //}
                streamWriter.WriteLine("\t\t\t_" + Utility.FormatCamel(table.Alias) + " = MapearObjeto(reader);");
                //streamWriter.WriteLine("\t\t\t}");
                streamWriter.WriteLine("\t\t\t}");
                streamWriter.WriteLine("\t\t\t}");
                streamWriter.WriteLine("\t\t\treturn _" + Utility.FormatCamel(table.Alias) + ";");
                // Append the method footer
                streamWriter.WriteLine("\t\t}");

                //streamWriter.Write("\t\tpublic " + Utility.FormatPascal(table.Alias) + " Select(");
                //foreach (Column column in table.PrimaryKeys) {
                //    streamWriter.Write(Utility.CreateMethodParameter(column) + ", ");
                //}
                //streamWriter.WriteLine(") {");

                //// Create the parameters
                //streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");
                //StringBuilder builder = new StringBuilder();
                //foreach (Column column in table.PrimaryKeys) {
                //    builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(null, column, true) + "," + Environment.NewLine);
                //}
                //streamWriter.WriteLine(builder.ToString(0, builder.Length - (Environment.NewLine.Length + 1)));
                //streamWriter.WriteLine("\t\t\t};");
                //streamWriter.WriteLine();

                //// Append the stored procedure execution
                //streamWriter.WriteLine("\t\t\tusing (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connection, transaction, \"" + table.Alias + "Select\", parameters)) {");
                //streamWriter.WriteLine("\t\t\t\tif (dataReader.Read()) {");
                //streamWriter.WriteLine("\t\t\t\t\treturn Make" + Utility.FormatPascal(table.Alias) + "(dataReader, transaction);");
                //streamWriter.WriteLine("\t\t\t\t} else {");
                //streamWriter.WriteLine("\t\t\t\t\treturn null;");
                //streamWriter.WriteLine("\t\t\t\t}");
                //streamWriter.WriteLine("\t\t\t}");

                //// Append the method footer
                //streamWriter.WriteLine("\t\t}");
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
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Selecciona Todos los Registros de la Tabla " + table.Name + ".");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\tpublic DataTable Listar() {");
                streamWriter.WriteLine("\t\t\tDataSet ds = new DataSet();");
                streamWriter.WriteLine("\t\t\tusing(SqlConnection _sqlConnection=new SqlConnection(this.conString))");
                streamWriter.WriteLine("\t\t\t{_sqlConnection.Open();");
                streamWriter.WriteLine("\t\t\tds = SqlHelper.ExecuteDataset(_sqlConnection,CommandType.StoredProcedure, \"" + procedureName + "\");");
                streamWriter.WriteLine("\t\t\t}");
                foreach (Column column in table.Columns)
                {
                    streamWriter.WriteLine("\t\t\t ds.Tables[0].Columns[\"" + column.Name + "\"].ColumnName = \"" + column.Alias + "\";");
                }
                streamWriter.WriteLine("\t\t\t ds.AcceptChanges();");
                streamWriter.WriteLine("\t\t\t return ds.Tables[0];");
                // Append the stored procedure execution
                //streamWriter.WriteLine("\t\t\tusing (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connection, transaction, \"" + table.Alias + "SelectAll\")) {");
                //streamWriter.WriteLine("\t\t\t\tif (dataReader.HasRows) {");
                //streamWriter.WriteLine("\t\t\t\t\tArrayList list = new ArrayList();");
                //streamWriter.WriteLine("\t\t\t\t\twhile (dataReader.Read()) {");
                //streamWriter.WriteLine("\t\t\t\t\t\t" + Utility.FormatPascal(table.Alias) + " " + Utility.FormatCamel(table.Alias) + " = Make" + Utility.FormatPascal(table.Alias) + "(dataReader, transaction);");
                //streamWriter.WriteLine("\t\t\t\t\t\tlist.Add(" + Utility.FormatCamel(table.Alias) + ");");
                //streamWriter.WriteLine("\t\t\t\t\t}");
                //streamWriter.WriteLine("\t\t\t\t\treturn (" + Utility.FormatPascal(table.Alias) + "[]) list.ToArray(typeof(" + Utility.FormatPascal(table.Alias) + "));");
                //streamWriter.WriteLine("\t\t\t\t} else {");
                //streamWriter.WriteLine("\t\t\t\t\treturn null;");
                //streamWriter.WriteLine("\t\t\t\t}");
                //streamWriter.WriteLine("\t\t\t}");

                // Append the method footer
                streamWriter.WriteLine("\t\t}");
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
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Selecciona Todos los Registros de la Tabla " + table.Name + ". en Lista Generica");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\tpublic List<" + table.Alias + "> ListarGenerics() {");
                streamWriter.WriteLine("\t\t\t List<" + table.Alias + "> lstTemp;");
                streamWriter.WriteLine("\t\t\tusing(SqlConnection _sqlConnection=new SqlConnection(this.conString))");
                streamWriter.WriteLine("\t\t\t{_sqlConnection.Open();");
                streamWriter.WriteLine("\t\t\t using(SqlDataReader reader = SqlHelper.ExecuteReader(_sqlConnection,CommandType.StoredProcedure, \"" + procedureName + "\"))");
                streamWriter.WriteLine("\t\t\t { lstTemp =  _GM.GetList(reader);");
                streamWriter.WriteLine("\t\t\t }");
                streamWriter.WriteLine("\t\t\t}");
                streamWriter.WriteLine("\t\t\t return lstTemp;");
                //foreach (Column column in table.Columns)
                //{
                //    streamWriter.WriteLine("\t\t\t ds.Tables[0].Columns[\"" + column.Name + "\"].ColumnName = \"" + column.Alias + "\";");
                //}
                //streamWriter.WriteLine("\t\t\t ds.AcceptChanges();");
                //streamWriter.WriteLine("\t\t\t return ds.Tables[0];");

                // Append the method footer
                streamWriter.WriteLine("\t\t}");
            }
        }
        /// <summary>
        /// Creates a string that represents the "select by" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDASelectAllByMethods(Table table, string storedProcedurePrefix, StreamWriter streamWriter)
        {
            //// Create a stored procedure for each foreign key
            //foreach (ArrayList compositeKeyList in table.ForeignKeys.Values) {
            //    // Create the stored procedure name
            //    StringBuilder stringBuilder = new StringBuilder(255);
            //    stringBuilder.Append(storedProcedurePrefix + table.Name + "SelectAllBy");
            //    for (int i = 0; i < compositeKeyList.Count; i++) {
            //        Column column = (Column) compositeKeyList[i];

            //        if (i > 0) {
            //            stringBuilder.Append("_" + Utility.FormatPascal(column.Name));
            //        } else {
            //            stringBuilder.Append(Utility.FormatPascal(column.Name));
            //        }
            //    }
            //    string procedureName = stringBuilder.ToString();

            //    // Create the method name
            //    stringBuilder = new StringBuilder(255);
            //    stringBuilder.Append("SelectAllBy");
            //    for (int i = 0; i < compositeKeyList.Count; i++) {
            //        Column column = (Column) compositeKeyList[i];

            //        if (i > 0) {
            //            stringBuilder.Append("_" + Utility.FormatPascal(column.Alias));
            //        } else {
            //            stringBuilder.Append(Utility.FormatPascal(column.Alias));
            //        }
            //    }
            //    string methodName = stringBuilder.ToString();

            //    // Create the select function based on keys
            //    // Append the method header
            //    streamWriter.WriteLine();
            //    streamWriter.WriteLine("\t\t/// <summary>");
            //    streamWriter.WriteLine("\t\t/// Selects all records from the " + table.Name + " table by a foreign key.");
            //    streamWriter.WriteLine("\t\t/// </summary>");

            //    streamWriter.Write("\t\tpublic static " + Utility.FormatPascal(table.Alias) + "[] " + methodName + "(");
            //    for (int i = 0; i < compositeKeyList.Count; i++) {
            //        Column column = (Column) compositeKeyList[i];
            //        streamWriter.Write(Utility.CreateMethodParameter(column) + ", ");
            //    }
            //    streamWriter.WriteLine("SqlConnection connection, SqlTransaction transaction) {");

            //    streamWriter.WriteLine("\t\t\tSqlParameter[] parameters = new SqlParameter[] {");

            //    // Create the parameters
            //    StringBuilder builder = new StringBuilder();
            //    foreach (Column column in compositeKeyList) {
            //        builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(null, column, true) + "," + Environment.NewLine);
            //    }
            //    streamWriter.WriteLine(builder.ToString(0, builder.Length - (Environment.NewLine.Length + 1)));
            //    streamWriter.WriteLine("\t\t\t};");
            //    streamWriter.WriteLine();

            //    // Append the stored procedure execution
            //    streamWriter.WriteLine("\t\t\tusing (SqlDataReader dataReader = SqlClientUtility.ExecuteReader(connection, transaction, \"" + methodName + "\", parameters)) {");
            //    streamWriter.WriteLine("\t\t\t\tif (dataReader.HasRows) {");
            //    streamWriter.WriteLine("\t\t\t\t\tArrayList list = new ArrayList();");
            //    streamWriter.WriteLine("\t\t\t\t\twhile (dataReader.Read()) {");
            //    streamWriter.WriteLine("\t\t\t\t\t\t" + Utility.FormatPascal(table.Alias) + " " + Utility.FormatCamel(table.Alias) + " = Make" + Utility.FormatPascal(table.Alias) + "(dataReader, transaction);");
            //    streamWriter.WriteLine("\t\t\t\t\t\tlist.Add(" + Utility.FormatCamel(table.Alias) + ");");
            //    streamWriter.WriteLine("\t\t\t\t\t}");
            //    streamWriter.WriteLine("\t\t\t\t\treturn (" + Utility.FormatPascal(table.Alias) + "[]) list.ToArray(typeof(" + Utility.FormatPascal(table.Alias) + "));");
            //    streamWriter.WriteLine("\t\t\t\t} else {");
            //    streamWriter.WriteLine("\t\t\t\t\treturn null;");
            //    streamWriter.WriteLine("\t\t\t\t}");
            //    streamWriter.WriteLine("\t\t\t}");

            //    // Append the method footer
            //    streamWriter.WriteLine("\t\t}");
            //}
        }


        /// <summary>
        /// Creates a string that represents the "make" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        private static void CreateDAMakeMethod(Table table, StreamWriter streamWriter)
        {
            string tableNamePascal = Utility.FormatPascal(table.Alias);
            string tableNameCamel = Utility.FormatCamel(table.Alias);

            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t/// <summary>");
            streamWriter.WriteLine("\t\t/// Creates a new instance of the " + table.Name + " class and populates it with data from the specified SqlDataReader.");
            streamWriter.WriteLine("\t\t/// </summary>");
            streamWriter.WriteLine("\t\tprivate static " + tableNamePascal + " Make" + Utility.FormatPascal(table.Alias) + "(SqlDataReader dataReader) {");
            streamWriter.WriteLine("\t\t\t" + tableNamePascal + " " + tableNameCamel + " = new " + tableNamePascal + "();");
            streamWriter.WriteLine("\t\t\t");

            for (int i = 0; i < table.Columns.Count; i++)
            {
                Column column = (Column)table.Columns[i];
                string columnNamePascal = Utility.FormatPascal(column.Alias);

                streamWriter.WriteLine("\t\t\tif (dataReader.IsDBNull(" + i.ToString() + ") == false) {");
                streamWriter.WriteLine("\t\t\t\t" + tableNameCamel + "." + columnNamePascal + " = dataReader." + Utility.CreateGetXxxMethod(column) + "(" + i.ToString() + ");");
                streamWriter.WriteLine("\t\t\t}");
            }

            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t\treturn " + tableNameCamel + ";");
            streamWriter.WriteLine("\t\t}");
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
            using (StreamWriter streamWriter = new StreamWriter(path + "\\BO\\MSSQL\\" + className + ".cs"))
            {
                // Create the header for the class
                streamWriter.WriteLine("using System;");
                streamWriter.WriteLine("using System.Collections.Generic;");
                streamWriter.WriteLine("using System.Collections;");
                streamWriter.WriteLine("using System.Data;");
                streamWriter.WriteLine();
                streamWriter.WriteLine("using " + targetNamespace + ".Data.SqlServer;");
                streamWriter.WriteLine("using " + targetNamespace + ".EntityObjects;");
                streamWriter.WriteLine();
                streamWriter.WriteLine("namespace " + targetNamespace + ".BusinessObjects {");

                streamWriter.WriteLine("\tpublic class " + className + " {");
                streamWriter.WriteLine("\t\tprivate " + table.Alias + "DA" + " _" + Utility.FormatCamel(table.Alias) + "DA;");
                streamWriter.WriteLine("\t\tpublic " + className + "() {_" + Utility.FormatCamel(table.Alias) + "DA=new " + table.Alias + "DA();}");

                // Append the access methods
                CreateBOInsertMethod(table, storedProcedurePrefix, streamWriter);
                CreateBOUpdateMethod(table, storedProcedurePrefix, streamWriter);
                CreateBODeleteMethod(table, storedProcedurePrefix, streamWriter);
                //CreateBODeleteAllByMethods(table, storedProcedurePrefix, streamWriter);
                CreateBOSelectMethod(table, storedProcedurePrefix, streamWriter);
                CreateBOSelectAllMethod(table, storedProcedurePrefix, streamWriter);
                CreateBOSelectAllGenericsMethod(table, storedProcedurePrefix, streamWriter);
                CreateBOSelectAllComboMethod(table, storedProcedurePrefix, streamWriter, "Seleccione");
                CreateBOSelectAllComboMethod(table, storedProcedurePrefix, streamWriter, "Todos");
                //CreateBOSelectAllByMethods(table, storedProcedurePrefix, streamWriter);
                //CreateBOMakeMethod(table, streamWriter);

                // Close out the class and namespace
                streamWriter.WriteLine("\t}");
                streamWriter.WriteLine("}");
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
            streamWriter.WriteLine("\t\t/// <summary>");
            streamWriter.WriteLine("\t\t/// Inserta un Registro en la Tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t/// </summary>");

            streamWriter.Write("\t\tpublic void Insertar(");
            streamWriter.Write(Utility.FormatPascal(table.Alias) + " _" + Utility.FormatCamel(table.Alias) + "");
            streamWriter.WriteLine(") {");

            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\ttry{");
            streamWriter.WriteLine("\t\t\tint result= _" + Utility.FormatCamel(table.Alias) + "DA.Insertar(_" + Utility.FormatCamel(table.Alias) + ");");
            streamWriter.WriteLine("\t\t}");
            streamWriter.WriteLine("\t\tcatch(Exception ex)");
            streamWriter.WriteLine("\t\t{UtilBO.ManejaExcepcion(ex, this, \"Insertar\");}");
            streamWriter.WriteLine("\t\t}");
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
            streamWriter.WriteLine("\t\t/// <summary>");
            streamWriter.WriteLine("\t\t/// Actualiza un Registro en la Tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t/// </summary>");

            streamWriter.Write("\t\tpublic void Actualizar(");
            streamWriter.Write(Utility.FormatPascal(table.Alias) + " _" + Utility.FormatCamel(table.Alias) + "");
            streamWriter.WriteLine(") {");

            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\ttry{");
            streamWriter.WriteLine("\t\t\tint result = _" + Utility.FormatCamel(table.Alias) + "DA.Actualizar(_" + Utility.FormatCamel(table.Alias) + ");");
            streamWriter.WriteLine("\t\t}");
            streamWriter.WriteLine("\t\tcatch(Exception ex)");
            streamWriter.WriteLine("\t\t{UtilBO.ManejaExcepcion(ex, this, \"Actualizar\");}");
            streamWriter.WriteLine("\t\t}");
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
            streamWriter.WriteLine("\t\t/// <summary>");
            streamWriter.WriteLine("\t\t/// Elimina un Registro de la Tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t/// </summary>");

            streamWriter.Write("\t\tpublic void Eliminar(");
            streamWriter.Write(Utility.FormatPascal(table.Alias) + " _" + Utility.FormatCamel(table.Alias) + "");
            streamWriter.WriteLine(") {");

            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\ttry{");
            streamWriter.WriteLine("\t\t\tint result =  _" + Utility.FormatCamel(table.Alias) + "DA.Eliminar(_" + Utility.FormatCamel(table.Alias) + ");");
            streamWriter.WriteLine("\t\t}");
            streamWriter.WriteLine("\t\tcatch(Exception ex)");
            streamWriter.WriteLine("\t\t{UtilBO.ManejaExcepcion(ex, this, \"Eliminar\");}");
            streamWriter.WriteLine("\t\t}");
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
            streamWriter.WriteLine("\t\t/// <summary>");
            streamWriter.WriteLine("\t\t/// Selecciona un Registro de la Tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t/// </summary>");

            // Create the parameters
            StringBuilder builderParam = new StringBuilder();
            foreach (Column column in table.PrimaryKeys)
            {
                //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);
                builderParam.Append("" + column.Type + " " + column.Alias + ",");
            }

            streamWriter.Write("\t\tpublic " + table.Alias + " Seleccionar(");
            if (builderParam.Length > 0)
            {
                streamWriter.Write("" + builderParam.ToString(0, builderParam.Length - 1));
            }
            streamWriter.WriteLine(") {");

            //streamWriter.Write("\t\tpublic _"+table.Alias +" Select(");
            //streamWriter.Write(Utility.FormatPascal(table.Alias) + " _" + Utility.FormatCamel(table.Alias) + "");
            //streamWriter.WriteLine(") {");
            streamWriter.WriteLine("\t\t" + table.Alias + " _" + Utility.FormatCamel(table.Alias) + "=new " + table.Alias + "();");
            streamWriter.WriteLine();

            // Create the parameters
            StringBuilder builderParam2 = new StringBuilder();
            foreach (Column column in table.PrimaryKeys)
            {
                //builder.Append("\t\t\t\t" + Utility.CreateSqlParameter(table, column, false) + "," + Environment.NewLine);
                builderParam2.Append("" + column.Alias + ",");
            }

            streamWriter.WriteLine("\t\ttry{");
            if (builderParam2.Length > 0)
            {
                streamWriter.WriteLine("\t\t\t_" + Utility.FormatCamel(table.Alias) + " = _" + Utility.FormatCamel(table.Alias) + "DA.Seleccionar(" + builderParam2.ToString(0, builderParam2.Length - 1) + ");");
            }
            
            streamWriter.WriteLine("\t\t}");
            streamWriter.WriteLine("\t\tcatch(Exception ex)");
            streamWriter.WriteLine("\t\t{UtilBO.ManejaExcepcion(ex, this, \"Seleccionar\");}");
            streamWriter.WriteLine("\t\treturn _" + Utility.FormatCamel(table.Alias) + ";");
            streamWriter.WriteLine("\t\t}");
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
            streamWriter.WriteLine("\t\t/// <summary>");
            streamWriter.WriteLine("\t\t/// Selecciona todos los Registros de la Tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t/// </summary>");

            streamWriter.Write("\t\tpublic DataTable Listar()");
            streamWriter.WriteLine(" {");
            streamWriter.WriteLine(" DataTable dt=new DataTable();");
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\ttry{");
            streamWriter.WriteLine("\t\t\tdt = _" + Utility.FormatCamel(table.Alias) + "DA.Listar();");
            streamWriter.WriteLine("\t\t}");
            streamWriter.WriteLine("\t\tcatch(Exception ex)");
            streamWriter.WriteLine("\t\t{UtilBO.ManejaExcepcion(ex, this, \"Listar\");}");
            streamWriter.WriteLine("\t\treturn dt;");
            streamWriter.WriteLine("\t\t}");
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
            streamWriter.WriteLine("\t\t/// <summary>");
            streamWriter.WriteLine("\t\t/// Selecciona todos los Registros de la Tabla " + table.Name + ". en una Lista Generica");
            streamWriter.WriteLine("\t\t/// </summary>");

            streamWriter.Write("\t\tpublic List<" + table.Alias + "> ListarGenerics()");
            streamWriter.WriteLine(" {");
            streamWriter.WriteLine(" List<" + table.Alias + "> list=new List<" + table.Alias + ">();");
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t try{");
            streamWriter.WriteLine("\t\t\t list = _" + Utility.FormatCamel(table.Alias) + "DA.ListarGenerics();");
            streamWriter.WriteLine("\t\t}");
            streamWriter.WriteLine("\t\tcatch(Exception ex)");
            streamWriter.WriteLine("\t\t{UtilBO.ManejaExcepcion(ex, this, \"ListarGenerics\");}");
            streamWriter.WriteLine("\t\treturn list;");
            streamWriter.WriteLine("\t\t}");
        }
        /// <summary>
        /// Creates a string that represents the SelectAllCombo functionality of the Business Objects class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="storedProcedurePrefix">The prefix that is used on the stored procedure that this method will call.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="strSelecItem">La Descripcion del Item 0 del Combo Ejemplo : Seleccione, Todos, Ninguno, etc.</param>
        private static void CreateBOSelectAllComboMethod(Table table, string storedProcedurePrefix, StreamWriter streamWriter, string strSelecItem){
            // Append the method header
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\t/// <summary>");
            streamWriter.WriteLine("\t\t/// Selecciona todos los Registros en un Combo(" + strSelecItem + ") de la Tabla " + table.Name + ".");
            streamWriter.WriteLine("\t\t/// </summary>");

            streamWriter.Write("\t\tpublic DataTable ListarCombo" + strSelecItem + "()");
            streamWriter.WriteLine(" {");
            streamWriter.WriteLine(" DataTable dt=new DataTable();");
            streamWriter.WriteLine();
            streamWriter.WriteLine("\t\ttry{");
            streamWriter.WriteLine("\t\t\tdt = _" + Utility.FormatCamel(table.Alias) + "DA.Listar();");
            streamWriter.WriteLine("\t\t\tDataRow dr = dt.NewRow();");
            streamWriter.WriteLine("\t\t\t//Cambiar los Nombre de estas Columnas de Ser Necesario");
            streamWriter.WriteLine("\t\t\tdr[\"intSecuencial\"] = 0;");
            streamWriter.WriteLine("\t\t\tdr[\"strDescripcion\"] = \" " + strSelecItem + " \";");
            streamWriter.WriteLine("\t\t\tdt.Rows.InsertAt(dr, 0);");
            streamWriter.WriteLine("\t\t\tdt.AcceptChanges();");
            streamWriter.WriteLine("\t\t}");
            streamWriter.WriteLine("\t\tcatch(Exception ex)");
            streamWriter.WriteLine("\t\t{UtilBO.ManejaExcepcion(ex, this, \"ListarCombo" + strSelecItem + "\");}");
            streamWriter.WriteLine("\t\treturn dt;");
            streamWriter.WriteLine("\t\t}");
        }
    }
}