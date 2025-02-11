using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace DataTierGenerator
{
   public class SQLServerConnection
    {
        private SqlConnection oConnection;
        private SqlTransaction oTransaction;

        private IDbConnection Conexion
        {
            get
            {
                if (oConnection == null)
                {
                    oConnection = new SqlConnection();
                }
                return oConnection;
            }
        }
        private void LiberarObjeto(ref SqlCommand pObjCommand)
        {
            if (pObjCommand != null)
            {
                pObjCommand.Dispose();
                pObjCommand = null;
            }
        }
        private void LiberarObjeto(ref SqlCommandBuilder pObjCommandBuilder)
        {
            if (pObjCommandBuilder != null)
            {
                pObjCommandBuilder.Dispose();
                pObjCommandBuilder = null;
            }
        }
        private void LiberarObjeto(ref SqlDataReader pObjDataReader)
        {
            if (pObjDataReader != null)
            {
                pObjDataReader.Dispose();
                pObjDataReader = null;
            }
        }
        private void LiberarObjeto(ref SqlDataAdapter daAdaptador)
        {
            if (daAdaptador != null)
            {
                daAdaptador.Dispose();
                daAdaptador = null;
            }
        }
        private void LiberarObjeto(ref DataTable pObjDataTable)
        {
            if (pObjDataTable != null)
            {
                pObjDataTable.Clear();
                pObjDataTable.Dispose();
                pObjDataTable = null;
            }
        }
        private void LiberarObjeto(ref SqlTransaction pObjTransaction)
        {
            if (pObjTransaction != null)
            {
                try
                {
                    pObjTransaction.Rollback();
                    pObjTransaction.Dispose();
                    pObjTransaction = null;
                }
                catch (SqlException ex)
                {
                }
            }
        }

        private void AbrirConexion()
        {
            try
            {
                CerrarConexion();
                if (Conexion.State == ConnectionState.Closed)
                {
                    Conexion.Open();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CerrarConexion()
        {
            try
            {
                if ((oTransaction == null) && (Conexion.State == ConnectionState.Open))
                {
                    Conexion.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AttachParameters(SqlCommand objCommand, IDataParameter[] pObjCommandParameters)
        {
            if (pObjCommandParameters != null)
            {
                foreach (SqlParameter objParameter in pObjCommandParameters)
                {
                    if (objParameter != null)
                    {
                        if (((objParameter.Direction == ParameterDirection.InputOutput) || (objParameter.Direction == ParameterDirection.Input)) && (objParameter.Value == null))
                        {
                            objParameter.Value = DBNull.Value;
                        }
                        objCommand.Parameters.Add(objParameter);
                    }
                }

            }
        }

        private void PrepareCommand(SqlCommand objCommand, SqlConnection objConnection, SqlTransaction objTransaction, CommandType pObjCommandType, string pObjCommandText, IDataParameter[] pObjCommandParameters)
        {
            objCommand.Connection = objConnection;
            objCommand.CommandText = pObjCommandText;
            if (objTransaction != null)
            {
                if (objTransaction.Connection == null)
                {
                    throw new ArgumentException("La Transaccion ya ha sido Commited o Rollback, por favor proporciona una Transaccion Abierta.", "Transaccion");
                }
                objCommand.Transaction = objTransaction;
            }
            objCommand.CommandType = pObjCommandType;
            if (pObjCommandParameters != null)
            {
                AttachParameters(objCommand, pObjCommandParameters);
            }
        }

        private void PrepareCommand(SqlCommand objCommand, SqlConnection objConnection, SqlTransaction objTransaction, CommandType pObjCommandType, string pObjCommandText, int pObjCommandTimeout, IDataParameter[] pObjCommandParameters)
        {
            objCommand.Connection = objConnection;
            objCommand.CommandText = pObjCommandText;
            objCommand.CommandTimeout = pObjCommandTimeout;
            if (objTransaction != null)
            {
                if (objTransaction.Connection == null)
                {
                    throw new ArgumentException("La Transaccion ya ha sido Commited o Rollback, por favor proporciona una Transaccion Abierta.", "Transaccion");
                }
                objCommand.Transaction = objTransaction;
            }
            objCommand.CommandType = pObjCommandType;
            if (pObjCommandParameters != null)
            {
                AttachParameters(objCommand, pObjCommandParameters);
            }
        }

        public string ConnectionString
        {
            get
            {
                return Conexion.ConnectionString;
            }
            set
            {
                CerrarConexion();
                Conexion.ConnectionString = value;
            }
        }

        public bool ConexionCorrecta()
        {
            bool objRetVal = false;
            if (oConnection.ConnectionString != null)
            {
                try
                {
                    CerrarConexion();
                    Conexion.ConnectionString = oConnection.ConnectionString;
                    AbrirConexion();
                    objRetVal = true;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CerrarConexion();
                }

            }
            return objRetVal;
        }

        public bool ConexionCorrecta(string pStrCadenaConexion)
        {
            bool objRetVal = false;
            if (oConnection.ConnectionString != null)
            {
                try
                {
                    CerrarConexion();
                    Conexion.ConnectionString = pStrCadenaConexion;
                    AbrirConexion();
                    objRetVal = true;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CerrarConexion();
                }

            }
            return objRetVal;
        }

        public void Dispose()
        {
            if (oTransaction != null)
            {
                oTransaction.Rollback();
                oTransaction.Dispose();
            }
            oTransaction = null;
            if (oConnection != null)
            {
                if (oConnection.State != ConnectionState.Closed)
                {
                    oConnection.Close();
                }
                oConnection.Dispose();
            }
            oConnection = null;
        }

        public void TransactionBegin()
        {
            try
            {
                AbrirConexion();
                if (oTransaction == null)
                {
                    oTransaction = oConnection.BeginTransaction();
                    //oConnection.BeginTransaction;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TransactionCommit()
        {
            try
            {
                AbrirConexion();
                if (oTransaction != null)
                {
                    oTransaction.Commit();
                    oTransaction.Dispose();
                    oTransaction = null;
                }
                CerrarConexion();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TransactionRollBack()
        {
            try
            {
                AbrirConexion();
                if (oTransaction != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                    oTransaction = null;
                }
                CerrarConexion();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteNonQuery(CommandType pObjCommandType, string pObjCommandText, IDataParameter[] pObjCommandParameters)
        {
            int objRetVal;
            SqlCommand objCommand = new SqlCommand();
            try
            {
                AbrirConexion();
                PrepareCommand(objCommand, oConnection, oTransaction, pObjCommandType, pObjCommandText, pObjCommandParameters);
                objRetVal = objCommand.ExecuteNonQuery();
                objCommand.Parameters.Clear();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LiberarObjeto(ref objCommand);
                CerrarConexion();
            }
            return objRetVal;
        }

        public DataTable ExecuteDataTable(CommandType pObjCommandType, string pObjCommandText, IDataParameter[] pObjCommandParameters)
        {
            SqlCommand objCommand = new SqlCommand();
            DataTable objRetVal = new DataTable();
            SqlDataAdapter objDataAdapter = null;
            try
            {
                AbrirConexion();
                PrepareCommand(objCommand, oConnection, oTransaction, pObjCommandType, pObjCommandText, pObjCommandParameters);
                objDataAdapter = new SqlDataAdapter(objCommand);
                objDataAdapter.Fill(objRetVal);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand.Parameters.Clear();
                LiberarObjeto(ref objDataAdapter);
                LiberarObjeto(ref objCommand);
                CerrarConexion();
            }
            return objRetVal;
        }
    }
}
