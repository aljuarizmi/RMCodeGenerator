using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataTierGenerator;
using System.IO;
namespace RMCodeGen
{
    public partial class frmParametros : Form
    {
        private string _Servidor = "";
        private string _DataBase = "";
        private string _User = "";
        private string _Password = "";
        private Util.Provider proveedor = Util.Provider.SqlClient;

        public Util.Provider Proveedor
        {
            get { return proveedor; }
            set { proveedor = value; }
        }
        
        public string DataBase
        {
            get { return _DataBase; }
            set { _DataBase = value; }
        }
        public string User
        {
            get { return _User; }
            set { _User = value; }
        }
        public string Servidor
        {
            get { return _Servidor; }
            set { _Servidor = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public frmParametros()
        {
            InitializeComponent();
        }
        private void frmParametros_Load(object sender, EventArgs e)
        {
            switch (proveedor) { 
                case Util.Provider.SqlClient:
                    break;
                case Util.Provider.PostgreSQL:
                    break;
                case Util.Provider.Oracle:
                    lblBaseDatos.Visible = false;
                    txtBD.Visible = false;
                    break;
                case Util.Provider.MySql:
                    break;
                case Util.Provider.Access:
                    break;

            }
            txtServidor.Text = Servidor;
            txtBD.Text = DataBase;
            txtUsuario.Text = User;
            txtPassword.Text = Password;
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Servidor = txtServidor.Text;
            DataBase = txtBD.Text;
            User = txtUsuario.Text;
            Password = txtPassword.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtServidor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (!txtServidor.Text.Equals(""))
                {
                    txtBD.Focus();
                }
            }
        }

        private void txtBD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!txtBD.Text.Equals(""))
                {
                    txtUsuario.Focus();
                }
            }
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!txtUsuario.Text.Equals(""))
                {
                    txtPassword.Focus();
                }
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!txtPassword.Text.Equals(""))
                {
                    btnAceptar.Focus();
                }
            }
        }
    }
}