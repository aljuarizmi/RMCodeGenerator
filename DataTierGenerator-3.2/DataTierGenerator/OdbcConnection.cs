using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
namespace DataTierGenerator
{
    class OdbcConnection
    {
        private System.Data.Odbc.OdbcConnection oConnection;
        private OdbcTransaction oTransaction;

        private IDbConnection Conexion
        {
            get
            {
                if (oConnection == null)
                {
                    oConnection = new System.Data.Odbc.OdbcConnection();
                }
                return oConnection;
            }
        }
        private void LiberarObjeto(ref OdbcCommand pObjCommand)
        {
            if (pObjCommand != null)
            {
                pObjCommand.Dispose();
                pObjCommand = null;
            }
        }
        private void LiberarObjeto(ref OdbcCommandBuilder pObjCommandBuilder)
        {
            if (pObjCommandBuilder != null)
            {
                pObjCommandBuilder.Dispose();
                pObjCommandBuilder = null;
            }
        }
        private void LiberarObjeto(ref OdbcDataReader pObjDataReader)
        {
            if (pObjDataReader != null)
            {
                pObjDataReader.Dispose();
                pObjDataReader = null;
            }
        }
        private void LiberarObjeto(ref OdbcDataAdapter daAdaptador)
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
        private void LiberarObjeto(ref OdbcTransaction pObjTransaction)
        {
            if (pObjTransaction != null)
            {
                try
                {
                    pObjTransaction.Rollback();
                    pObjTransaction.Dispose();
                    pObjTransaction = null;
                }
                catch (OdbcException ex)
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
            catch (OdbcException ex)
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
            catch (OdbcException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AttachParameters(OdbcCommand objCommand, IDataParameter[] pObjCommandParameters)
        {
            if (pObjCommandParameters != null)
            {
                foreach (OdbcParameter objParameter in pObjCommandParameters)
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

        private void PrepareCommand(OdbcCommand objCommand, System.Data.Odbc.OdbcConnection objConnection, OdbcTransaction objTransaction, CommandType pObjCommandType, string pObjCommandText, IDataParameter[] pObjCommandParameters)
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

        private void PrepareCommand(OdbcCommand objCommand, System.Data.Odbc.OdbcConnection objConnection, OdbcTransaction objTransaction, CommandType pObjCommandType, string pObjCommandText, int pObjCommandTimeout, IDataParameter[] pObjCommandParameters)
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
                catch (OdbcException ex)
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
                catch (OdbcException ex)
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
            catch (OdbcException ex)
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
            catch (OdbcException ex)
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
            catch (OdbcException ex)
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
            OdbcCommand objCommand = new OdbcCommand();
            try
            {
                AbrirConexion();
                PrepareCommand(objCommand, oConnection, oTransaction, pObjCommandType, pObjCommandText, pObjCommandParameters);
                objRetVal = objCommand.ExecuteNonQuery();
                objCommand.Parameters.Clear();
            }
            catch (OdbcException ex)
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
            OdbcCommand objCommand = new OdbcCommand();
            DataTable objRetVal = new DataTable();
            OdbcDataAdapter objDataAdapter = null;
            try
            {
                AbrirConexion();
                PrepareCommand(objCommand, oConnection, oTransaction, pObjCommandType, pObjCommandText, pObjCommandParameters);
                objDataAdapter = new OdbcDataAdapter(objCommand);
                objDataAdapter.Fill(objRetVal);
            }
            catch (OdbcException ex)
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
