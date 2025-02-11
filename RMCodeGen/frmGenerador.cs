using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using DataTierGenerator;
using RMCodeGen;
using Wilco.Windows.SyntaxHighlighting;
using System.Data.OracleClient;
using Wilco.SyntaxHighlighting;

namespace DataTierGenerator
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class frmGenerador : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button btnGenerate;
        private int FirstControlLeft = 100;
        private int SecondControlLeft = 240;
        private int ControlTop = 10;
        private int ControlHeight = 25;
        private int ControlWidth = 120;
        private int ControlTabIndex = 0;
        private int ControlStyleCount = 100;
        private System.Windows.Forms.CheckedListBox cbTableNames;
        private TextBox txtCon;
        private Label label2;
        //private string Quote ="\"";
        private TextBox txtBOGen;
        private TextBox txtEntGen;
        private Label label3;
        private Label label4;
        private ComboBox cmbTipo;
        private DataGridView dgvColumnas;
        private DataSet dsColumns;
        private TabControl tabControlPrincipal;
        private TabPage tabConfiguracion;
        private Label label7;
        private TabPage tabASPX;
        private Label label5;
        private Button btnCargar;
        private TabPage tabSPPre;
        private TabControl tabControl2;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private Label label10;
        private Label label6;
        private GroupBox csGroupBox;
        private TextBox namespaceTextBox;
        private Label namespaceLabel;
        private GroupBox sqlGroupBox;
        private TextBox storedProcedurePrefixTextBox;
        private Label storedProcedurePrefixLabel;
        private TextBox grantUserTextBox;
        private Label grantUserLabel;
        private GroupBox groupBox1;
        private TabPage tabEntidad;
        private Button btnGenerar;
        private ProgressBar progressBar;
        private RadioButton radVB;
        private RadioButton radCS;
        private TabPage tabSPS;
        private RadioButton radMySQL;
        private RadioButton radSQLServer;
        private TabPage tabSQLHelper;
        private TabPage tabDataAccess;
        private Label label16;
        private TextBox txtAliasTabla;
        private Panel panel2;
        private TabPage tabUtilDA;
        private TabPage tabUtilBO;
        private TabPage tabBusinessObject;
        private RichTextBox txtEntidad;
        private Wilco.SyntaxHighlighting.CSharpHighlighter cSharpHighlighter1;
        private IContainer components;
        private RichTextBox txtASPX;
        private RichTextBox txtPrerrequisito;
        private RichTextBox txtSQLSP;
        private RichTextBox txtAccesoDatosVisual;
        private RichTextBox txtSQLUtility;
        private RichTextBox txtUtilDA;
        private RichTextBox txtUtilBO;
        private RichTextBox txtBusinessObject;
        private ASPXHighlighter aspxHighlighter1;
        private SQLHighlighter sqlHighlighter1;
        private VBHighlighter vbHighlighter1;
        private SplitContainer splitContainer1;
        private TabPage tabGenericsBO;
        private RichTextBox txtGenericsBO;
        private TabPage tabGenericsDA;
        private RichTextBox txtGenericsDA;
        private TabPage tabAuditoria;
        private RichTextBox txtAuditoria;
        private TextBox txtNomTabe;
        private ComboBox cbProveedor;
        private Label label8;
        private Label label1;
        private TextBox txtTableName;
        private ComboBox cbPropietario;
        private Panel panelEntidades;
        private TabControl tabControlEntidades;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private RichTextBox txtClase;
        private Panel panelDataAccess;
        private TabControl tabControlData;
        private TabPage tabPageDataVisual;
        private TabPage tabPageDataJava;
        private RichTextBox txtAccesoDatosJava;
        private Panel panelSQLHelper;
        private TabControl tabControlSQLHelper;
        private TabPage tabSQLHelperVisual;
        private TabPage tabSQLHelperJava;
        private RichTextBox txtSQLUtilityJava;
        private TabPage tabPageCommand;
        private TabPage tabPageDirection;
        private TabPage tabPageParameter;
        private RichTextBox txtAccesoDatosJavaCommand;
        private RichTextBox txtAccesoDatosJavaDirection;
        private RichTextBox txtAccesoDatosJavaParameter;
        private Panel panelBO;
        private TabControl tabControlBO;
        private TabPage tabPageBONET;
        private TabPage tabPageBOJava;
        private RichTextBox txtBusinessObjectJava;
        private IParser parser;
        private Button btnParametros;
        private string NombreDB = "";
        private string _Servidor = "giovanni";
        private string _DataBase = "data_400";
        private string _User = "SUPERVISOR_SQL";
        private string _Password = "XfSgsh2Wna7KtZLBkLgTlbdhuW1of";
        private string _DriverJDBC = "";
        private string _Protocolo = "";
        private string _Puerto = "";
        private TextBox txt_execute;
        private Label label9;
        private string _ConnectionString = "";
        public frmGenerador()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Generator.DatabaseCounted += new CountUpdate(Generator_DatabaseCounted);
            Generator.TableCounted += new CountUpdate(Generator_TableCounted);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenerador));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.cbTableNames = new System.Windows.Forms.CheckedListBox();
            this.txtCon = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBOGen = new System.Windows.Forms.TextBox();
            this.txtEntGen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.dgvColumnas = new System.Windows.Forms.DataGridView();
            this.tabControlPrincipal = new System.Windows.Forms.TabControl();
            this.tabConfiguracion = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtNomTabe = new System.Windows.Forms.TextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.csGroupBox = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtAliasTabla = new System.Windows.Forms.TextBox();
            this.radVB = new System.Windows.Forms.RadioButton();
            this.namespaceTextBox = new System.Windows.Forms.TextBox();
            this.radCS = new System.Windows.Forms.RadioButton();
            this.namespaceLabel = new System.Windows.Forms.Label();
            this.sqlGroupBox = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_execute = new System.Windows.Forms.TextBox();
            this.radMySQL = new System.Windows.Forms.RadioButton();
            this.radSQLServer = new System.Windows.Forms.RadioButton();
            this.storedProcedurePrefixTextBox = new System.Windows.Forms.TextBox();
            this.storedProcedurePrefixLabel = new System.Windows.Forms.Label();
            this.grantUserTextBox = new System.Windows.Forms.TextBox();
            this.grantUserLabel = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnParametros = new System.Windows.Forms.Button();
            this.cbPropietario = new System.Windows.Forms.ComboBox();
            this.cbProveedor = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCargar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.tabSPPre = new System.Windows.Forms.TabPage();
            this.txtPrerrequisito = new System.Windows.Forms.RichTextBox();
            this.tabSPS = new System.Windows.Forms.TabPage();
            this.txtSQLSP = new System.Windows.Forms.RichTextBox();
            this.tabEntidad = new System.Windows.Forms.TabPage();
            this.panelEntidades = new System.Windows.Forms.Panel();
            this.tabControlEntidades = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtEntidad = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtClase = new System.Windows.Forms.RichTextBox();
            this.tabUtilDA = new System.Windows.Forms.TabPage();
            this.txtUtilDA = new System.Windows.Forms.RichTextBox();
            this.tabGenericsDA = new System.Windows.Forms.TabPage();
            this.txtGenericsDA = new System.Windows.Forms.RichTextBox();
            this.tabSQLHelper = new System.Windows.Forms.TabPage();
            this.panelSQLHelper = new System.Windows.Forms.Panel();
            this.tabControlSQLHelper = new System.Windows.Forms.TabControl();
            this.tabSQLHelperVisual = new System.Windows.Forms.TabPage();
            this.txtSQLUtility = new System.Windows.Forms.RichTextBox();
            this.tabSQLHelperJava = new System.Windows.Forms.TabPage();
            this.txtSQLUtilityJava = new System.Windows.Forms.RichTextBox();
            this.tabDataAccess = new System.Windows.Forms.TabPage();
            this.panelDataAccess = new System.Windows.Forms.Panel();
            this.tabControlData = new System.Windows.Forms.TabControl();
            this.tabPageDataVisual = new System.Windows.Forms.TabPage();
            this.txtAccesoDatosVisual = new System.Windows.Forms.RichTextBox();
            this.tabPageDataJava = new System.Windows.Forms.TabPage();
            this.txtAccesoDatosJava = new System.Windows.Forms.RichTextBox();
            this.tabPageCommand = new System.Windows.Forms.TabPage();
            this.txtAccesoDatosJavaCommand = new System.Windows.Forms.RichTextBox();
            this.tabPageDirection = new System.Windows.Forms.TabPage();
            this.txtAccesoDatosJavaDirection = new System.Windows.Forms.RichTextBox();
            this.tabPageParameter = new System.Windows.Forms.TabPage();
            this.txtAccesoDatosJavaParameter = new System.Windows.Forms.RichTextBox();
            this.tabUtilBO = new System.Windows.Forms.TabPage();
            this.txtUtilBO = new System.Windows.Forms.RichTextBox();
            this.tabGenericsBO = new System.Windows.Forms.TabPage();
            this.txtGenericsBO = new System.Windows.Forms.RichTextBox();
            this.tabBusinessObject = new System.Windows.Forms.TabPage();
            this.panelBO = new System.Windows.Forms.Panel();
            this.tabControlBO = new System.Windows.Forms.TabControl();
            this.tabPageBONET = new System.Windows.Forms.TabPage();
            this.txtBusinessObject = new System.Windows.Forms.RichTextBox();
            this.tabPageBOJava = new System.Windows.Forms.TabPage();
            this.txtBusinessObjectJava = new System.Windows.Forms.RichTextBox();
            this.tabASPX = new System.Windows.Forms.TabPage();
            this.txtASPX = new System.Windows.Forms.RichTextBox();
            this.tabAuditoria = new System.Windows.Forms.TabPage();
            this.txtAuditoria = new System.Windows.Forms.RichTextBox();
            this.cSharpHighlighter1 = new Wilco.SyntaxHighlighting.CSharpHighlighter();
            this.aspxHighlighter1 = new Wilco.SyntaxHighlighting.ASPXHighlighter();
            this.sqlHighlighter1 = new Wilco.SyntaxHighlighting.SQLHighlighter();
            this.vbHighlighter1 = new Wilco.SyntaxHighlighting.VBHighlighter();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumnas)).BeginInit();
            this.tabControlPrincipal.SuspendLayout();
            this.tabConfiguracion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.csGroupBox.SuspendLayout();
            this.sqlGroupBox.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabSPPre.SuspendLayout();
            this.tabSPS.SuspendLayout();
            this.tabEntidad.SuspendLayout();
            this.panelEntidades.SuspendLayout();
            this.tabControlEntidades.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabUtilDA.SuspendLayout();
            this.tabGenericsDA.SuspendLayout();
            this.tabSQLHelper.SuspendLayout();
            this.panelSQLHelper.SuspendLayout();
            this.tabControlSQLHelper.SuspendLayout();
            this.tabSQLHelperVisual.SuspendLayout();
            this.tabSQLHelperJava.SuspendLayout();
            this.tabDataAccess.SuspendLayout();
            this.panelDataAccess.SuspendLayout();
            this.tabControlData.SuspendLayout();
            this.tabPageDataVisual.SuspendLayout();
            this.tabPageDataJava.SuspendLayout();
            this.tabPageCommand.SuspendLayout();
            this.tabPageDirection.SuspendLayout();
            this.tabPageParameter.SuspendLayout();
            this.tabUtilBO.SuspendLayout();
            this.tabGenericsBO.SuspendLayout();
            this.tabBusinessObject.SuspendLayout();
            this.panelBO.SuspendLayout();
            this.tabControlBO.SuspendLayout();
            this.tabPageBONET.SuspendLayout();
            this.tabPageBOJava.SuspendLayout();
            this.tabASPX.SuspendLayout();
            this.tabAuditoria.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(12, 99);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(123, 23);
            this.btnGenerate.TabIndex = 12;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbTableNames
            // 
            this.cbTableNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbTableNames.Location = new System.Drawing.Point(0, 0);
            this.cbTableNames.Name = "cbTableNames";
            this.cbTableNames.Size = new System.Drawing.Size(293, 286);
            this.cbTableNames.TabIndex = 6;
            this.cbTableNames.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.cbTableNames_ItemCheck);
            // 
            // txtCon
            // 
            this.txtCon.Location = new System.Drawing.Point(131, 37);
            this.txtCon.Name = "txtCon";
            this.txtCon.Size = new System.Drawing.Size(428, 20);
            this.txtCon.TabIndex = 1;
            this.txtCon.TextChanged += new System.EventHandler(this.txtCon_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cadena Conexion";
            // 
            // txtBOGen
            // 
            this.txtBOGen.Location = new System.Drawing.Point(199, 39);
            this.txtBOGen.Name = "txtBOGen";
            this.txtBOGen.Size = new System.Drawing.Size(353, 20);
            this.txtBOGen.TabIndex = 10;
            // 
            // txtEntGen
            // 
            this.txtEntGen.Location = new System.Drawing.Point(199, 17);
            this.txtEntGen.Name = "txtEntGen";
            this.txtEntGen.Size = new System.Drawing.Size(353, 20);
            this.txtEntGen.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Nombre del BO";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Nombre de la Entidad";
            // 
            // cmbTipo
            // 
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "FormView",
            "GridView"});
            this.cmbTipo.Location = new System.Drawing.Point(199, 68);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(137, 21);
            this.cmbTipo.TabIndex = 11;
            // 
            // dgvColumnas
            // 
            this.dgvColumnas.AllowUserToResizeRows = false;
            this.dgvColumnas.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvColumnas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvColumnas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(72)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(72)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvColumnas.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvColumnas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColumnas.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(195)))), ((int)(((byte)(209)))));
            this.dgvColumnas.Location = new System.Drawing.Point(0, 0);
            this.dgvColumnas.Name = "dgvColumnas";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvColumnas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvColumnas.Size = new System.Drawing.Size(587, 286);
            this.dgvColumnas.TabIndex = 16;
            // 
            // tabControlPrincipal
            // 
            this.tabControlPrincipal.Controls.Add(this.tabConfiguracion);
            this.tabControlPrincipal.Controls.Add(this.tabSPPre);
            this.tabControlPrincipal.Controls.Add(this.tabSPS);
            this.tabControlPrincipal.Controls.Add(this.tabEntidad);
            this.tabControlPrincipal.Controls.Add(this.tabUtilDA);
            this.tabControlPrincipal.Controls.Add(this.tabGenericsDA);
            this.tabControlPrincipal.Controls.Add(this.tabSQLHelper);
            this.tabControlPrincipal.Controls.Add(this.tabDataAccess);
            this.tabControlPrincipal.Controls.Add(this.tabUtilBO);
            this.tabControlPrincipal.Controls.Add(this.tabGenericsBO);
            this.tabControlPrincipal.Controls.Add(this.tabBusinessObject);
            this.tabControlPrincipal.Controls.Add(this.tabASPX);
            this.tabControlPrincipal.Controls.Add(this.tabAuditoria);
            this.tabControlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tabControlPrincipal.Name = "tabControlPrincipal";
            this.tabControlPrincipal.SelectedIndex = 0;
            this.tabControlPrincipal.Size = new System.Drawing.Size(898, 582);
            this.tabControlPrincipal.TabIndex = 17;
            // 
            // tabConfiguracion
            // 
            this.tabConfiguracion.Controls.Add(this.splitContainer1);
            this.tabConfiguracion.Controls.Add(this.tabControl2);
            this.tabConfiguracion.Controls.Add(this.panel2);
            this.tabConfiguracion.Location = new System.Drawing.Point(4, 22);
            this.tabConfiguracion.Name = "tabConfiguracion";
            this.tabConfiguracion.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfiguracion.Size = new System.Drawing.Size(890, 556);
            this.tabConfiguracion.TabIndex = 0;
            this.tabConfiguracion.Text = "Configuracion";
            this.tabConfiguracion.UseVisualStyleBackColor = true;
            this.tabConfiguracion.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 267);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtNomTabe);
            this.splitContainer1.Panel1.Controls.Add(this.cbTableNames);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvColumnas);
            this.splitContainer1.Size = new System.Drawing.Size(884, 286);
            this.splitContainer1.SplitterDistance = 293;
            this.splitContainer1.TabIndex = 23;
            // 
            // txtNomTabe
            // 
            this.txtNomTabe.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtNomTabe.Location = new System.Drawing.Point(162, 0);
            this.txtNomTabe.Name = "txtNomTabe";
            this.txtNomTabe.Size = new System.Drawing.Size(131, 20);
            this.txtNomTabe.TabIndex = 20;
            this.txtNomTabe.TextChanged += new System.EventHandler(this.txtNomTabe_TextChanged);
            this.txtNomTabe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomTabe_KeyDown);
            this.txtNomTabe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNomTabe_KeyPress);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl2.Location = new System.Drawing.Point(3, 102);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(884, 165);
            this.tabControl2.TabIndex = 20;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.progressBar);
            this.tabPage5.Controls.Add(this.btnGenerar);
            this.tabPage5.Controls.Add(this.csGroupBox);
            this.tabPage5.Controls.Add(this.sqlGroupBox);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(876, 139);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Entidades, DA y BO";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.tabPage5.Click += new System.EventHandler(this.tabPage5_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(295, 108);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(229, 23);
            this.progressBar.TabIndex = 25;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(530, 108);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(124, 23);
            this.btnGenerar.TabIndex = 24;
            this.btnGenerar.Text = "Generar Codigo";
            this.btnGenerar.Click += new System.EventHandler(this.button3_Click);
            // 
            // csGroupBox
            // 
            this.csGroupBox.Controls.Add(this.label16);
            this.csGroupBox.Controls.Add(this.txtAliasTabla);
            this.csGroupBox.Controls.Add(this.radVB);
            this.csGroupBox.Controls.Add(this.namespaceTextBox);
            this.csGroupBox.Controls.Add(this.radCS);
            this.csGroupBox.Controls.Add(this.namespaceLabel);
            this.csGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.csGroupBox.Location = new System.Drawing.Point(295, 7);
            this.csGroupBox.Name = "csGroupBox";
            this.csGroupBox.Size = new System.Drawing.Size(407, 87);
            this.csGroupBox.TabIndex = 23;
            this.csGroupBox.TabStop = false;
            this.csGroupBox.Text = "C#  y VB";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 50);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 13);
            this.label16.TabIndex = 29;
            this.label16.Text = "Alias Tabla:";
            // 
            // txtAliasTabla
            // 
            this.txtAliasTabla.Location = new System.Drawing.Point(138, 47);
            this.txtAliasTabla.Name = "txtAliasTabla";
            this.txtAliasTabla.Size = new System.Drawing.Size(182, 20);
            this.txtAliasTabla.TabIndex = 28;
            // 
            // radVB
            // 
            this.radVB.AutoSize = true;
            this.radVB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radVB.Location = new System.Drawing.Point(339, 45);
            this.radVB.Name = "radVB";
            this.radVB.Size = new System.Drawing.Size(45, 18);
            this.radVB.TabIndex = 27;
            this.radVB.Text = "VB";
            this.radVB.UseVisualStyleBackColor = true;
            // 
            // namespaceTextBox
            // 
            this.namespaceTextBox.Location = new System.Drawing.Point(138, 21);
            this.namespaceTextBox.Name = "namespaceTextBox";
            this.namespaceTextBox.Size = new System.Drawing.Size(182, 20);
            this.namespaceTextBox.TabIndex = 9;
            this.namespaceTextBox.TextChanged += new System.EventHandler(this.namespaceTextBox_TextChanged);
            // 
            // radCS
            // 
            this.radCS.AutoSize = true;
            this.radCS.Checked = true;
            this.radCS.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radCS.Location = new System.Drawing.Point(339, 22);
            this.radCS.Name = "radCS";
            this.radCS.Size = new System.Drawing.Size(45, 18);
            this.radCS.TabIndex = 26;
            this.radCS.TabStop = true;
            this.radCS.Text = "C#";
            this.radCS.UseVisualStyleBackColor = true;
            // 
            // namespaceLabel
            // 
            this.namespaceLabel.AutoSize = true;
            this.namespaceLabel.Location = new System.Drawing.Point(12, 24);
            this.namespaceLabel.Name = "namespaceLabel";
            this.namespaceLabel.Size = new System.Drawing.Size(118, 13);
            this.namespaceLabel.TabIndex = 10;
            this.namespaceLabel.Text = "Namespace / Paquete:";
            // 
            // sqlGroupBox
            // 
            this.sqlGroupBox.Controls.Add(this.label9);
            this.sqlGroupBox.Controls.Add(this.txt_execute);
            this.sqlGroupBox.Controls.Add(this.radMySQL);
            this.sqlGroupBox.Controls.Add(this.radSQLServer);
            this.sqlGroupBox.Controls.Add(this.storedProcedurePrefixTextBox);
            this.sqlGroupBox.Controls.Add(this.storedProcedurePrefixLabel);
            this.sqlGroupBox.Controls.Add(this.grantUserTextBox);
            this.sqlGroupBox.Controls.Add(this.grantUserLabel);
            this.sqlGroupBox.Location = new System.Drawing.Point(18, 7);
            this.sqlGroupBox.Name = "sqlGroupBox";
            this.sqlGroupBox.Size = new System.Drawing.Size(271, 126);
            this.sqlGroupBox.TabIndex = 22;
            this.sqlGroupBox.TabStop = false;
            this.sqlGroupBox.Text = "SQL";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Ejecutar SP:";
            // 
            // txt_execute
            // 
            this.txt_execute.Location = new System.Drawing.Point(79, 100);
            this.txt_execute.Name = "txt_execute";
            this.txt_execute.Size = new System.Drawing.Size(162, 20);
            this.txt_execute.TabIndex = 13;
            // 
            // radMySQL
            // 
            this.radMySQL.AutoSize = true;
            this.radMySQL.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radMySQL.Location = new System.Drawing.Point(109, 77);
            this.radMySQL.Name = "radMySQL";
            this.radMySQL.Size = new System.Drawing.Size(66, 18);
            this.radMySQL.TabIndex = 12;
            this.radMySQL.Text = "MySQL";
            this.radMySQL.UseVisualStyleBackColor = true;
            this.radMySQL.Visible = false;
            // 
            // radSQLServer
            // 
            this.radSQLServer.AutoSize = true;
            this.radSQLServer.Checked = true;
            this.radSQLServer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.radSQLServer.Location = new System.Drawing.Point(12, 77);
            this.radSQLServer.Name = "radSQLServer";
            this.radSQLServer.Size = new System.Drawing.Size(83, 18);
            this.radSQLServer.TabIndex = 11;
            this.radSQLServer.TabStop = true;
            this.radSQLServer.Text = "SQLServer";
            this.radSQLServer.UseVisualStyleBackColor = true;
            // 
            // storedProcedurePrefixTextBox
            // 
            this.storedProcedurePrefixTextBox.Location = new System.Drawing.Point(79, 47);
            this.storedProcedurePrefixTextBox.Name = "storedProcedurePrefixTextBox";
            this.storedProcedurePrefixTextBox.Size = new System.Drawing.Size(162, 20);
            this.storedProcedurePrefixTextBox.TabIndex = 7;
            // 
            // storedProcedurePrefixLabel
            // 
            this.storedProcedurePrefixLabel.AutoSize = true;
            this.storedProcedurePrefixLabel.Location = new System.Drawing.Point(12, 50);
            this.storedProcedurePrefixLabel.Name = "storedProcedurePrefixLabel";
            this.storedProcedurePrefixLabel.Size = new System.Drawing.Size(56, 13);
            this.storedProcedurePrefixLabel.TabIndex = 10;
            this.storedProcedurePrefixLabel.Text = "Prefijo SP:";
            // 
            // grantUserTextBox
            // 
            this.grantUserTextBox.Location = new System.Drawing.Point(79, 21);
            this.grantUserTextBox.Name = "grantUserTextBox";
            this.grantUserTextBox.Size = new System.Drawing.Size(162, 20);
            this.grantUserTextBox.TabIndex = 6;
            // 
            // grantUserLabel
            // 
            this.grantUserLabel.AutoSize = true;
            this.grantUserLabel.Location = new System.Drawing.Point(12, 24);
            this.grantUserLabel.Name = "grantUserLabel";
            this.grantUserLabel.Size = new System.Drawing.Size(61, 13);
            this.grantUserLabel.TabIndex = 8;
            this.grantUserLabel.Text = "Grant User:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(876, 139);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Configurar ASPX";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnGenerate);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmbTipo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtEntGen);
            this.groupBox1.Controls.Add(this.txtBOGen);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(870, 133);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FormView y GridView";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(558, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "(*)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(558, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "(*)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Tipo a Generar";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(380, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(251, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "(*) Incluir Namespace : Com.Paquete.Clase";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnParametros);
            this.panel2.Controls.Add(this.cbPropietario);
            this.panel2.Controls.Add(this.cbProveedor);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.btnCargar);
            this.panel2.Controls.Add(this.txtCon);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtTableName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(884, 99);
            this.panel2.TabIndex = 22;
            // 
            // btnParametros
            // 
            this.btnParametros.Location = new System.Drawing.Point(131, 66);
            this.btnParametros.Name = "btnParametros";
            this.btnParametros.Size = new System.Drawing.Size(123, 22);
            this.btnParametros.TabIndex = 13;
            this.btnParametros.Text = "Parametros";
            this.btnParametros.UseVisualStyleBackColor = true;
            this.btnParametros.Click += new System.EventHandler(this.btnParametros_Click);
            // 
            // cbPropietario
            // 
            this.cbPropietario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPropietario.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbPropietario.FormattingEnabled = true;
            this.cbPropietario.Items.AddRange(new object[] {
            "System.Data.SqlClient",
            "System.Data.OracleClient",
            "IBM.Data.DB2"});
            this.cbPropietario.Location = new System.Drawing.Point(789, 9);
            this.cbPropietario.Name = "cbPropietario";
            this.cbPropietario.Size = new System.Drawing.Size(88, 21);
            this.cbPropietario.TabIndex = 12;
            this.cbPropietario.Visible = false;
            this.cbPropietario.SelectedIndexChanged += new System.EventHandler(this.cbPropietario_SelectedIndexChanged);
            // 
            // cbProveedor
            // 
            this.cbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProveedor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbProveedor.FormattingEnabled = true;
            this.cbProveedor.Items.AddRange(new object[] {
            "System.Data.SqlClient",
            "MySql.Data.MySqlClient",
            "System.Data.OracleClient",
            "IBM.Data.DB2"});
            this.cbProveedor.Location = new System.Drawing.Point(131, 9);
            this.cbProveedor.Name = "cbProveedor";
            this.cbProveedor.Size = new System.Drawing.Size(275, 21);
            this.cbProveedor.TabIndex = 10;
            this.cbProveedor.SelectedIndexChanged += new System.EventHandler(this.cbProveedor_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(49, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Proveedor";
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(583, 35);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(123, 22);
            this.btnCargar.TabIndex = 2;
            this.btnCargar.Text = "Cargar";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(711, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nombre de la Tabla";
            this.label1.Visible = false;
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(817, 66);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(60, 20);
            this.txtTableName.TabIndex = 3;
            this.txtTableName.Visible = false;
            // 
            // tabSPPre
            // 
            this.tabSPPre.Controls.Add(this.txtPrerrequisito);
            this.tabSPPre.Location = new System.Drawing.Point(4, 22);
            this.tabSPPre.Name = "tabSPPre";
            this.tabSPPre.Padding = new System.Windows.Forms.Padding(3);
            this.tabSPPre.Size = new System.Drawing.Size(890, 556);
            this.tabSPPre.TabIndex = 2;
            this.tabSPPre.Text = "Requisitos";
            this.tabSPPre.UseVisualStyleBackColor = true;
            // 
            // txtPrerrequisito
            // 
            this.txtPrerrequisito.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtPrerrequisito.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrerrequisito.Location = new System.Drawing.Point(3, 3);
            this.txtPrerrequisito.Name = "txtPrerrequisito";
            this.txtPrerrequisito.ReadOnly = true;
            this.txtPrerrequisito.Size = new System.Drawing.Size(884, 550);
            this.txtPrerrequisito.TabIndex = 3;
            this.txtPrerrequisito.Text = resources.GetString("txtPrerrequisito.Text");
            // 
            // tabSPS
            // 
            this.tabSPS.Controls.Add(this.txtSQLSP);
            this.tabSPS.Location = new System.Drawing.Point(4, 22);
            this.tabSPS.Name = "tabSPS";
            this.tabSPS.Size = new System.Drawing.Size(890, 556);
            this.tabSPS.TabIndex = 4;
            this.tabSPS.Text = "SP\'S";
            this.tabSPS.UseVisualStyleBackColor = true;
            // 
            // txtSQLSP
            // 
            this.txtSQLSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLSP.Location = new System.Drawing.Point(0, 0);
            this.txtSQLSP.Name = "txtSQLSP";
            this.txtSQLSP.Size = new System.Drawing.Size(890, 556);
            this.txtSQLSP.TabIndex = 5;
            this.txtSQLSP.Text = "";
            // 
            // tabEntidad
            // 
            this.tabEntidad.Controls.Add(this.panelEntidades);
            this.tabEntidad.Location = new System.Drawing.Point(4, 22);
            this.tabEntidad.Name = "tabEntidad";
            this.tabEntidad.Size = new System.Drawing.Size(890, 556);
            this.tabEntidad.TabIndex = 3;
            this.tabEntidad.Text = "Entidades";
            this.tabEntidad.UseVisualStyleBackColor = true;
            // 
            // panelEntidades
            // 
            this.panelEntidades.Controls.Add(this.tabControlEntidades);
            this.panelEntidades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEntidades.Location = new System.Drawing.Point(0, 0);
            this.panelEntidades.Name = "panelEntidades";
            this.panelEntidades.Size = new System.Drawing.Size(890, 556);
            this.panelEntidades.TabIndex = 1;
            // 
            // tabControlEntidades
            // 
            this.tabControlEntidades.Controls.Add(this.tabPage1);
            this.tabControlEntidades.Controls.Add(this.tabPage2);
            this.tabControlEntidades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEntidades.Location = new System.Drawing.Point(0, 0);
            this.tabControlEntidades.Name = "tabControlEntidades";
            this.tabControlEntidades.SelectedIndex = 0;
            this.tabControlEntidades.Size = new System.Drawing.Size(890, 556);
            this.tabControlEntidades.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtEntidad);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(882, 530);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Entidad";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtEntidad
            // 
            this.txtEntidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEntidad.Location = new System.Drawing.Point(3, 3);
            this.txtEntidad.Name = "txtEntidad";
            this.txtEntidad.Size = new System.Drawing.Size(876, 524);
            this.txtEntidad.TabIndex = 0;
            this.txtEntidad.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtClase);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(882, 530);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Clase";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtClase
            // 
            this.txtClase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClase.Location = new System.Drawing.Point(3, 3);
            this.txtClase.Name = "txtClase";
            this.txtClase.Size = new System.Drawing.Size(876, 524);
            this.txtClase.TabIndex = 1;
            this.txtClase.Text = "";
            // 
            // tabUtilDA
            // 
            this.tabUtilDA.Controls.Add(this.txtUtilDA);
            this.tabUtilDA.Location = new System.Drawing.Point(4, 22);
            this.tabUtilDA.Name = "tabUtilDA";
            this.tabUtilDA.Padding = new System.Windows.Forms.Padding(3);
            this.tabUtilDA.Size = new System.Drawing.Size(890, 556);
            this.tabUtilDA.TabIndex = 8;
            this.tabUtilDA.Text = "UtilDA";
            this.tabUtilDA.UseVisualStyleBackColor = true;
            // 
            // txtUtilDA
            // 
            this.txtUtilDA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUtilDA.Location = new System.Drawing.Point(3, 3);
            this.txtUtilDA.Name = "txtUtilDA";
            this.txtUtilDA.Size = new System.Drawing.Size(884, 550);
            this.txtUtilDA.TabIndex = 6;
            this.txtUtilDA.Text = "";
            // 
            // tabGenericsDA
            // 
            this.tabGenericsDA.Controls.Add(this.txtGenericsDA);
            this.tabGenericsDA.Location = new System.Drawing.Point(4, 22);
            this.tabGenericsDA.Name = "tabGenericsDA";
            this.tabGenericsDA.Size = new System.Drawing.Size(890, 556);
            this.tabGenericsDA.TabIndex = 12;
            this.tabGenericsDA.Text = "GenericsDA";
            this.tabGenericsDA.UseVisualStyleBackColor = true;
            // 
            // txtGenericsDA
            // 
            this.txtGenericsDA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGenericsDA.Location = new System.Drawing.Point(0, 0);
            this.txtGenericsDA.Name = "txtGenericsDA";
            this.txtGenericsDA.Size = new System.Drawing.Size(890, 556);
            this.txtGenericsDA.TabIndex = 0;
            this.txtGenericsDA.Text = "";
            // 
            // tabSQLHelper
            // 
            this.tabSQLHelper.Controls.Add(this.panelSQLHelper);
            this.tabSQLHelper.Location = new System.Drawing.Point(4, 22);
            this.tabSQLHelper.Name = "tabSQLHelper";
            this.tabSQLHelper.Size = new System.Drawing.Size(890, 556);
            this.tabSQLHelper.TabIndex = 5;
            this.tabSQLHelper.Text = "SQL Helper";
            this.tabSQLHelper.UseVisualStyleBackColor = true;
            // 
            // panelSQLHelper
            // 
            this.panelSQLHelper.Controls.Add(this.tabControlSQLHelper);
            this.panelSQLHelper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSQLHelper.Location = new System.Drawing.Point(0, 0);
            this.panelSQLHelper.Name = "panelSQLHelper";
            this.panelSQLHelper.Size = new System.Drawing.Size(890, 556);
            this.panelSQLHelper.TabIndex = 5;
            // 
            // tabControlSQLHelper
            // 
            this.tabControlSQLHelper.Controls.Add(this.tabSQLHelperVisual);
            this.tabControlSQLHelper.Controls.Add(this.tabSQLHelperJava);
            this.tabControlSQLHelper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSQLHelper.Location = new System.Drawing.Point(0, 0);
            this.tabControlSQLHelper.Name = "tabControlSQLHelper";
            this.tabControlSQLHelper.SelectedIndex = 0;
            this.tabControlSQLHelper.Size = new System.Drawing.Size(890, 556);
            this.tabControlSQLHelper.TabIndex = 5;
            // 
            // tabSQLHelperVisual
            // 
            this.tabSQLHelperVisual.Controls.Add(this.txtSQLUtility);
            this.tabSQLHelperVisual.Location = new System.Drawing.Point(4, 22);
            this.tabSQLHelperVisual.Name = "tabSQLHelperVisual";
            this.tabSQLHelperVisual.Padding = new System.Windows.Forms.Padding(3);
            this.tabSQLHelperVisual.Size = new System.Drawing.Size(882, 530);
            this.tabSQLHelperVisual.TabIndex = 0;
            this.tabSQLHelperVisual.Text = "Visual .NET";
            this.tabSQLHelperVisual.UseVisualStyleBackColor = true;
            // 
            // txtSQLUtility
            // 
            this.txtSQLUtility.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLUtility.Location = new System.Drawing.Point(3, 3);
            this.txtSQLUtility.Name = "txtSQLUtility";
            this.txtSQLUtility.Size = new System.Drawing.Size(876, 524);
            this.txtSQLUtility.TabIndex = 4;
            this.txtSQLUtility.Text = "";
            // 
            // tabSQLHelperJava
            // 
            this.tabSQLHelperJava.Controls.Add(this.txtSQLUtilityJava);
            this.tabSQLHelperJava.Location = new System.Drawing.Point(4, 22);
            this.tabSQLHelperJava.Name = "tabSQLHelperJava";
            this.tabSQLHelperJava.Padding = new System.Windows.Forms.Padding(3);
            this.tabSQLHelperJava.Size = new System.Drawing.Size(882, 530);
            this.tabSQLHelperJava.TabIndex = 1;
            this.tabSQLHelperJava.Text = "Java";
            this.tabSQLHelperJava.UseVisualStyleBackColor = true;
            // 
            // txtSQLUtilityJava
            // 
            this.txtSQLUtilityJava.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLUtilityJava.Location = new System.Drawing.Point(3, 3);
            this.txtSQLUtilityJava.Name = "txtSQLUtilityJava";
            this.txtSQLUtilityJava.Size = new System.Drawing.Size(876, 524);
            this.txtSQLUtilityJava.TabIndex = 5;
            this.txtSQLUtilityJava.Text = "";
            // 
            // tabDataAccess
            // 
            this.tabDataAccess.Controls.Add(this.panelDataAccess);
            this.tabDataAccess.Location = new System.Drawing.Point(4, 22);
            this.tabDataAccess.Name = "tabDataAccess";
            this.tabDataAccess.Size = new System.Drawing.Size(890, 556);
            this.tabDataAccess.TabIndex = 6;
            this.tabDataAccess.Text = "Data Access";
            this.tabDataAccess.UseVisualStyleBackColor = true;
            // 
            // panelDataAccess
            // 
            this.panelDataAccess.Controls.Add(this.tabControlData);
            this.panelDataAccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDataAccess.Location = new System.Drawing.Point(0, 0);
            this.panelDataAccess.Name = "panelDataAccess";
            this.panelDataAccess.Size = new System.Drawing.Size(890, 556);
            this.panelDataAccess.TabIndex = 6;
            // 
            // tabControlData
            // 
            this.tabControlData.Controls.Add(this.tabPageDataVisual);
            this.tabControlData.Controls.Add(this.tabPageDataJava);
            this.tabControlData.Controls.Add(this.tabPageCommand);
            this.tabControlData.Controls.Add(this.tabPageDirection);
            this.tabControlData.Controls.Add(this.tabPageParameter);
            this.tabControlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlData.Location = new System.Drawing.Point(0, 0);
            this.tabControlData.Name = "tabControlData";
            this.tabControlData.SelectedIndex = 0;
            this.tabControlData.Size = new System.Drawing.Size(890, 556);
            this.tabControlData.TabIndex = 6;
            // 
            // tabPageDataVisual
            // 
            this.tabPageDataVisual.Controls.Add(this.txtAccesoDatosVisual);
            this.tabPageDataVisual.Location = new System.Drawing.Point(4, 22);
            this.tabPageDataVisual.Name = "tabPageDataVisual";
            this.tabPageDataVisual.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDataVisual.Size = new System.Drawing.Size(882, 530);
            this.tabPageDataVisual.TabIndex = 0;
            this.tabPageDataVisual.Text = "Visual .NET";
            this.tabPageDataVisual.UseVisualStyleBackColor = true;
            // 
            // txtAccesoDatosVisual
            // 
            this.txtAccesoDatosVisual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAccesoDatosVisual.Location = new System.Drawing.Point(3, 3);
            this.txtAccesoDatosVisual.Name = "txtAccesoDatosVisual";
            this.txtAccesoDatosVisual.Size = new System.Drawing.Size(876, 524);
            this.txtAccesoDatosVisual.TabIndex = 5;
            this.txtAccesoDatosVisual.Text = "";
            // 
            // tabPageDataJava
            // 
            this.tabPageDataJava.Controls.Add(this.txtAccesoDatosJava);
            this.tabPageDataJava.Location = new System.Drawing.Point(4, 22);
            this.tabPageDataJava.Name = "tabPageDataJava";
            this.tabPageDataJava.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDataJava.Size = new System.Drawing.Size(882, 530);
            this.tabPageDataJava.TabIndex = 1;
            this.tabPageDataJava.Text = "Java Connection";
            this.tabPageDataJava.UseVisualStyleBackColor = true;
            // 
            // txtAccesoDatosJava
            // 
            this.txtAccesoDatosJava.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAccesoDatosJava.Location = new System.Drawing.Point(3, 3);
            this.txtAccesoDatosJava.Name = "txtAccesoDatosJava";
            this.txtAccesoDatosJava.Size = new System.Drawing.Size(876, 524);
            this.txtAccesoDatosJava.TabIndex = 6;
            this.txtAccesoDatosJava.Text = "";
            // 
            // tabPageCommand
            // 
            this.tabPageCommand.Controls.Add(this.txtAccesoDatosJavaCommand);
            this.tabPageCommand.Location = new System.Drawing.Point(4, 22);
            this.tabPageCommand.Name = "tabPageCommand";
            this.tabPageCommand.Size = new System.Drawing.Size(882, 530);
            this.tabPageCommand.TabIndex = 2;
            this.tabPageCommand.Text = "Java Command Type";
            this.tabPageCommand.UseVisualStyleBackColor = true;
            // 
            // txtAccesoDatosJavaCommand
            // 
            this.txtAccesoDatosJavaCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAccesoDatosJavaCommand.Location = new System.Drawing.Point(0, 0);
            this.txtAccesoDatosJavaCommand.Name = "txtAccesoDatosJavaCommand";
            this.txtAccesoDatosJavaCommand.Size = new System.Drawing.Size(882, 530);
            this.txtAccesoDatosJavaCommand.TabIndex = 7;
            this.txtAccesoDatosJavaCommand.Text = "";
            // 
            // tabPageDirection
            // 
            this.tabPageDirection.Controls.Add(this.txtAccesoDatosJavaDirection);
            this.tabPageDirection.Location = new System.Drawing.Point(4, 22);
            this.tabPageDirection.Name = "tabPageDirection";
            this.tabPageDirection.Size = new System.Drawing.Size(882, 530);
            this.tabPageDirection.TabIndex = 3;
            this.tabPageDirection.Text = "Java Direction";
            this.tabPageDirection.UseVisualStyleBackColor = true;
            // 
            // txtAccesoDatosJavaDirection
            // 
            this.txtAccesoDatosJavaDirection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAccesoDatosJavaDirection.Location = new System.Drawing.Point(0, 0);
            this.txtAccesoDatosJavaDirection.Name = "txtAccesoDatosJavaDirection";
            this.txtAccesoDatosJavaDirection.Size = new System.Drawing.Size(882, 530);
            this.txtAccesoDatosJavaDirection.TabIndex = 7;
            this.txtAccesoDatosJavaDirection.Text = "";
            // 
            // tabPageParameter
            // 
            this.tabPageParameter.Controls.Add(this.txtAccesoDatosJavaParameter);
            this.tabPageParameter.Location = new System.Drawing.Point(4, 22);
            this.tabPageParameter.Name = "tabPageParameter";
            this.tabPageParameter.Size = new System.Drawing.Size(882, 530);
            this.tabPageParameter.TabIndex = 4;
            this.tabPageParameter.Text = "Java Parameter";
            this.tabPageParameter.UseVisualStyleBackColor = true;
            // 
            // txtAccesoDatosJavaParameter
            // 
            this.txtAccesoDatosJavaParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAccesoDatosJavaParameter.Location = new System.Drawing.Point(0, 0);
            this.txtAccesoDatosJavaParameter.Name = "txtAccesoDatosJavaParameter";
            this.txtAccesoDatosJavaParameter.Size = new System.Drawing.Size(882, 530);
            this.txtAccesoDatosJavaParameter.TabIndex = 7;
            this.txtAccesoDatosJavaParameter.Text = "";
            // 
            // tabUtilBO
            // 
            this.tabUtilBO.Controls.Add(this.txtUtilBO);
            this.tabUtilBO.Location = new System.Drawing.Point(4, 22);
            this.tabUtilBO.Name = "tabUtilBO";
            this.tabUtilBO.Padding = new System.Windows.Forms.Padding(3);
            this.tabUtilBO.Size = new System.Drawing.Size(890, 556);
            this.tabUtilBO.TabIndex = 9;
            this.tabUtilBO.Text = "UtilBO";
            this.tabUtilBO.UseVisualStyleBackColor = true;
            // 
            // txtUtilBO
            // 
            this.txtUtilBO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUtilBO.Location = new System.Drawing.Point(3, 3);
            this.txtUtilBO.Name = "txtUtilBO";
            this.txtUtilBO.Size = new System.Drawing.Size(884, 550);
            this.txtUtilBO.TabIndex = 6;
            this.txtUtilBO.Text = "";
            // 
            // tabGenericsBO
            // 
            this.tabGenericsBO.Controls.Add(this.txtGenericsBO);
            this.tabGenericsBO.Location = new System.Drawing.Point(4, 22);
            this.tabGenericsBO.Name = "tabGenericsBO";
            this.tabGenericsBO.Size = new System.Drawing.Size(890, 556);
            this.tabGenericsBO.TabIndex = 11;
            this.tabGenericsBO.Text = "GenericsBO";
            this.tabGenericsBO.UseVisualStyleBackColor = true;
            // 
            // txtGenericsBO
            // 
            this.txtGenericsBO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGenericsBO.Location = new System.Drawing.Point(0, 0);
            this.txtGenericsBO.Name = "txtGenericsBO";
            this.txtGenericsBO.Size = new System.Drawing.Size(890, 556);
            this.txtGenericsBO.TabIndex = 0;
            this.txtGenericsBO.Text = "";
            // 
            // tabBusinessObject
            // 
            this.tabBusinessObject.Controls.Add(this.panelBO);
            this.tabBusinessObject.Location = new System.Drawing.Point(4, 22);
            this.tabBusinessObject.Name = "tabBusinessObject";
            this.tabBusinessObject.Padding = new System.Windows.Forms.Padding(3);
            this.tabBusinessObject.Size = new System.Drawing.Size(890, 556);
            this.tabBusinessObject.TabIndex = 10;
            this.tabBusinessObject.Text = "Business Object";
            this.tabBusinessObject.UseVisualStyleBackColor = true;
            // 
            // panelBO
            // 
            this.panelBO.Controls.Add(this.tabControlBO);
            this.panelBO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBO.Location = new System.Drawing.Point(3, 3);
            this.panelBO.Name = "panelBO";
            this.panelBO.Size = new System.Drawing.Size(884, 550);
            this.panelBO.TabIndex = 8;
            // 
            // tabControlBO
            // 
            this.tabControlBO.Controls.Add(this.tabPageBONET);
            this.tabControlBO.Controls.Add(this.tabPageBOJava);
            this.tabControlBO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlBO.Location = new System.Drawing.Point(0, 0);
            this.tabControlBO.Name = "tabControlBO";
            this.tabControlBO.SelectedIndex = 0;
            this.tabControlBO.Size = new System.Drawing.Size(884, 550);
            this.tabControlBO.TabIndex = 8;
            // 
            // tabPageBONET
            // 
            this.tabPageBONET.Controls.Add(this.txtBusinessObject);
            this.tabPageBONET.Location = new System.Drawing.Point(4, 22);
            this.tabPageBONET.Name = "tabPageBONET";
            this.tabPageBONET.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBONET.Size = new System.Drawing.Size(876, 524);
            this.tabPageBONET.TabIndex = 0;
            this.tabPageBONET.Text = "Business Object .NET";
            this.tabPageBONET.UseVisualStyleBackColor = true;
            // 
            // txtBusinessObject
            // 
            this.txtBusinessObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBusinessObject.Location = new System.Drawing.Point(3, 3);
            this.txtBusinessObject.Name = "txtBusinessObject";
            this.txtBusinessObject.Size = new System.Drawing.Size(870, 518);
            this.txtBusinessObject.TabIndex = 7;
            this.txtBusinessObject.Text = "";
            // 
            // tabPageBOJava
            // 
            this.tabPageBOJava.Controls.Add(this.txtBusinessObjectJava);
            this.tabPageBOJava.Location = new System.Drawing.Point(4, 22);
            this.tabPageBOJava.Name = "tabPageBOJava";
            this.tabPageBOJava.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBOJava.Size = new System.Drawing.Size(876, 524);
            this.tabPageBOJava.TabIndex = 1;
            this.tabPageBOJava.Text = "Business Object Java";
            this.tabPageBOJava.UseVisualStyleBackColor = true;
            // 
            // txtBusinessObjectJava
            // 
            this.txtBusinessObjectJava.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBusinessObjectJava.Location = new System.Drawing.Point(3, 3);
            this.txtBusinessObjectJava.Name = "txtBusinessObjectJava";
            this.txtBusinessObjectJava.Size = new System.Drawing.Size(870, 518);
            this.txtBusinessObjectJava.TabIndex = 8;
            this.txtBusinessObjectJava.Text = "";
            // 
            // tabASPX
            // 
            this.tabASPX.Controls.Add(this.txtASPX);
            this.tabASPX.Location = new System.Drawing.Point(4, 22);
            this.tabASPX.Name = "tabASPX";
            this.tabASPX.Padding = new System.Windows.Forms.Padding(3);
            this.tabASPX.Size = new System.Drawing.Size(890, 556);
            this.tabASPX.TabIndex = 1;
            this.tabASPX.Text = "ASPX";
            this.tabASPX.UseVisualStyleBackColor = true;
            // 
            // txtASPX
            // 
            this.txtASPX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtASPX.Location = new System.Drawing.Point(3, 3);
            this.txtASPX.Name = "txtASPX";
            this.txtASPX.Size = new System.Drawing.Size(884, 550);
            this.txtASPX.TabIndex = 2;
            this.txtASPX.Text = "";
            // 
            // tabAuditoria
            // 
            this.tabAuditoria.Controls.Add(this.txtAuditoria);
            this.tabAuditoria.Location = new System.Drawing.Point(4, 22);
            this.tabAuditoria.Name = "tabAuditoria";
            this.tabAuditoria.Padding = new System.Windows.Forms.Padding(3);
            this.tabAuditoria.Size = new System.Drawing.Size(890, 556);
            this.tabAuditoria.TabIndex = 13;
            this.tabAuditoria.Text = "Auditoria";
            this.tabAuditoria.UseVisualStyleBackColor = true;
            // 
            // txtAuditoria
            // 
            this.txtAuditoria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAuditoria.Location = new System.Drawing.Point(3, 3);
            this.txtAuditoria.Name = "txtAuditoria";
            this.txtAuditoria.Size = new System.Drawing.Size(884, 550);
            this.txtAuditoria.TabIndex = 5;
            this.txtAuditoria.Text = "";
            // 
            // frmGenerador
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(898, 582);
            this.Controls.Add(this.tabControlPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGenerador";
            this.Text = "R&M Generador de Codigo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGenerador_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumnas)).EndInit();
            this.tabControlPrincipal.ResumeLayout(false);
            this.tabConfiguracion.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.csGroupBox.ResumeLayout(false);
            this.csGroupBox.PerformLayout();
            this.sqlGroupBox.ResumeLayout(false);
            this.sqlGroupBox.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabSPPre.ResumeLayout(false);
            this.tabSPS.ResumeLayout(false);
            this.tabEntidad.ResumeLayout(false);
            this.panelEntidades.ResumeLayout(false);
            this.tabControlEntidades.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabUtilDA.ResumeLayout(false);
            this.tabGenericsDA.ResumeLayout(false);
            this.tabSQLHelper.ResumeLayout(false);
            this.panelSQLHelper.ResumeLayout(false);
            this.tabControlSQLHelper.ResumeLayout(false);
            this.tabSQLHelperVisual.ResumeLayout(false);
            this.tabSQLHelperJava.ResumeLayout(false);
            this.tabDataAccess.ResumeLayout(false);
            this.panelDataAccess.ResumeLayout(false);
            this.tabControlData.ResumeLayout(false);
            this.tabPageDataVisual.ResumeLayout(false);
            this.tabPageDataJava.ResumeLayout(false);
            this.tabPageCommand.ResumeLayout(false);
            this.tabPageDirection.ResumeLayout(false);
            this.tabPageParameter.ResumeLayout(false);
            this.tabUtilBO.ResumeLayout(false);
            this.tabGenericsBO.ResumeLayout(false);
            this.tabBusinessObject.ResumeLayout(false);
            this.panelBO.ResumeLayout(false);
            this.tabControlBO.ResumeLayout(false);
            this.tabPageBONET.ResumeLayout(false);
            this.tabPageBOJava.ResumeLayout(false);
            this.tabASPX.ResumeLayout(false);
            this.tabAuditoria.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        ///// <summary>
        ///// The main entry point for the application.
        ///// </summary>
        //[STAThread]
        //static void Main() 
        //{
        //    Application.Run(new Form1());
        //}

        private void cargarProveedores()
        {
            DataTable Providers = new DataTable("Providers");
            Providers.Columns.Add("", GetType("System.String"));

            DataColumn columna = new DataColumn();
            DataRow row = null;
            /*columna.ColumnName = "PROVEEDOR";
            Providers.Columns.Add(columna);
            columna.ColumnName = "CLASE";
            Providers.Columns.Add(columna);
            columna.ColumnName = "CADENA";
            Providers.Columns.Add(columna);*/
        }

        private Type GetType(string p)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            DataRow row ;
            DataTable datos = new DataTable();
            datos = DbProviderFactories.GetFactoryClasses();
            if (datos != null) {
                DataColumn columna = new DataColumn("Protocolo", System.Type.GetType("System.String"));
                datos.Columns.Add(columna);
                datos.Clear();
                
                row = datos.NewRow();
                row[0] = "SQL Server Data Provider";
                row[1] = ".Net Framework Data Provider para SQL Server";
                row[2] = "System.Data.SqlClient";
                row[3] = "com.microsoft.sqlserver.jdbc.SQLServerDriver";
                row[4] = "jdbc:sqlserver://";
                datos.Rows.Add(row);

                row = datos.NewRow();
                row[0] = "Oracle Data Provider";
                row[1] = ".Net Framework Data Provider para Oracle";
                row[2] = "System.Data.OracleClient";
                row[3] = "oracle.jdbc.driver.OracleDriver";
                row[4] = "jdbc:oracle:thin:@";
                datos.Rows.Add(row);

                row = datos.NewRow();
                row[0] = "MySQL Data Provider";
                row[1] = ".Net Framework Data Provider para MySQL";
                row[2] = "MySql.Data.MySqlClient";
                row[3] = "com.mysql.jdbc.Driver";
                row[4] = "jdbc:mysql://";
                datos.Rows.Add(row);

                row = datos.NewRow();
                row[0] = "PostgreSQL Data Provider";
                row[1] = ".Net Framework Data Provider para PostgreSQL";
                row[2] = "Npgsql";
                row[3] = "org.postgresql.Driver";
                row[4] = "jdbc:postgresql://";
                datos.Rows.Add(row);

                row = datos.NewRow();
                row[0] = "Odbc Data Provider";
                row[1] = ".Net Framework Data Provider para Odbc";
                row[2] = "System.Data.Odbc";
                row[3] = "sun.jdbc.odbc.JdbcOdbcDriver";
                row[4] = "jdbc:odbc:";
                datos.Rows.Add(row);
                row = datos.NewRow();
                row[0] = "OleDB Data Provider";
                row[1] = ".Net Framework Data Provider para OleDB";
                row[2] = "System.Data.OleDb";
                row[3] = "";
                row[4] = "";
                datos.Rows.Add(row);

            }
            

            cbProveedor.DataSource = datos;
            cbProveedor.DisplayMember = "Description";
            cbProveedor.ValueMember = "InvariantName";
            this.txtCon.Text = RMCodeGen.Properties.Settings.Default.CadenaConexion;
            this.ActiveControl = this.txtASPX;
            //this.txtCon.Text  = "Data Source=CASA;Initial Catalog=Royal;Persist Security Info=True;User ID=juanpablo;password=juanpablo";
            parser = new RtfParser(this.txtPrerrequisito);
            if (this.sqlHighlighter1 != null) {
                this.sqlHighlighter1.Parser = parser;
                txtPrerrequisito.Rtf = this.sqlHighlighter1.Parse(this.txtPrerrequisito.Text);
            }
            
            if (cbProveedor.Items.Count > 0) { cbProveedor.SelectedIndex = 0; }
            Text = "R&M Generador de Codigo - versión: " + Application.ProductVersion;
        }

        private void waste()
        {
            /*
             * ctlLabel NewLabel = new ctlLabel("lblApplication",100,100,100,100,99,"Application");
            this.txtASPX.Text= NewLabel.ToString();
            ctlText newText = new ctlText("txtApplication",220,100,100,100,0,10);
			

            Validator newValidator = new Validator("rvApplication"," Application ","txtApplication",100,201,99);
			

            ddl newddl = new ddl("ddlApplication",100,100,100,100,1);
            this.txtASPX.Text = newddl.ToString();
             
            */

        }

        //public enum Provider
        //{
        //    Access = 0,
        //    SqlClient = 1,
        //    Oracle = 2,
        //    Db2 = 3,
        //    MySql = 4
        //};

        public Util.Provider GetProvider
        {
            get
            {
                string strProvider = this.cbProveedor.SelectedValue.ToString();
                switch (strProvider)
                {
                    case "System.Data.SqlClient":
                        return Util.Provider.SqlClient;
                    case "Npgsql":
                        return Util.Provider.PostgreSQL;
                    case "System.Data.OracleClient":
                        return Util.Provider.Oracle;
                    case "MySql.Data.MySqlClient":
                        return Util.Provider.MySql;
                    case "System.Data.Odbc":
                        return Util.Provider.ODBC;
                    case "System.Data.OleDb":
                        return Util.Provider.OleDB;
                    default:
                        return Util.Provider.Access;
                }

            }
        }

        private string FormatNameParam(string nameParam)
        {
            string strReturn = String.Empty;
            switch (GetProvider)
            {
                case Util.Provider.Access:
                    if (!nameParam.StartsWith("@"))
                    {
                        strReturn = String.Format("@{0}", nameParam);
                    }
                    break;
                case Util.Provider.SqlClient:
                    if (!nameParam.StartsWith("@"))
                    {
                        strReturn = String.Format("@{0}", nameParam);
                    }
                    break;
                case Util.Provider.Oracle:
                    strReturn = String.Format("P{0}", nameParam);
                    break;
                case Util.Provider.Db2:
                    strReturn = String.Format("P{0}", nameParam);
                    break;
                case Util.Provider.MySql:
                    strReturn = String.Format("@{0}", nameParam);
                    break;
                default:
                    strReturn = String.Format("@{0}", nameParam);
                    break;
            }
            return strReturn;
        }

        public bool AplicaCursor()
        {
            bool booAplica = false;
            if (GetProvider == Util.Provider.Oracle)
            {
                booAplica = true;
            }
            return booAplica;
        }

        private bool AplicaPropietario()
        {
            bool booAplica = false;
            switch (GetProvider)
            {
                case Util.Provider.Access:
                    break;
                case Util.Provider.SqlClient:
                    break;
                case Util.Provider.Oracle:
                    booAplica = true;
                    break;
                case Util.Provider.Db2:
                    booAplica = true;
                    break;
                case Util.Provider.MySql:
                    break;
                case Util.Provider.PostgreSQL:
                    break;
                default:
                    break;
            }
            return booAplica;
        }

        DbProviderFactory dbProvider;
        DbConnection cn;

        private void GetPropietarios()
        {
            string strProveedor;
            strProveedor = this.cbProveedor.SelectedValue.ToString();

            dbProvider = DbProviderFactories.GetFactory(strProveedor);
            cn = dbProvider.CreateConnection();

            string ConnectionString;
            ConnectionString = txtCon.Text.Trim();

            cn.ConnectionString = ConnectionString;
            cn.Open();

            if (GetProvider == Util.Provider.Oracle)
            {
                using (DbDataAdapter _da = dbProvider.CreateDataAdapter())
                {
                    using (DbCommand _cmd = dbProvider.CreateCommand())
                    {
                        _cmd.Connection = cn;
                        DataTable _dt = new DataTable();
                        _cmd.CommandType = CommandType.Text;
                        _cmd.CommandText = " select USERNAME from ALL_USERS";
                        _da.SelectCommand = _cmd;
                        _da.Fill(_dt);
                        this.cbPropietario.DisplayMember = "USERNAME";
                        this.cbPropietario.ValueMember = "USERNAME";

                        DataRow dr;
                        dr = _dt.NewRow();
                        dr["USERNAME"] = "-- SELECCIONE --";
                        _dt.Rows.InsertAt(dr, 0);
                        this.cbPropietario.DataSource = _dt;
                    }
                }
            }
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }

        private void Propietario()
        {
            Util.strProp = string.Empty;
            bool booAplica = false;
            switch (GetProvider)
            {
                case Util.Provider.Access:
                    break;
                case Util.Provider.SqlClient:
                    break;
                case Util.Provider.Oracle:
                    booAplica = true;
                    break;
                case Util.Provider.Db2:
                    break;
                case Util.Provider.MySql:
                    break;
                default:
                    break;
            }
            if (booAplica)
            {
                string strCad = this.txtCon.Text.Trim();
                //strCad.Substring(this.txtCon.Text.Trim().ToUpper().LastIndexOf("USER ID"), 
            }
        }

        private void GetTables(string strNombre)
        {
            DataTable data;
            IDataParameter[] parametros = new IDataParameter[1];
            IDataParameter NOMBRE;
            System.Data.DataSet ds = new DataSet();
            string strProveedor;
            strProveedor = this.cbProveedor.SelectedValue.ToString();

            //dbProvider = DbProviderFactories.GetFactory(strProveedor);
            //cn = dbProvider.CreateConnection();
            
            string ConnectionString;
            ConnectionString = txtCon.Text.Trim();
            _ConnectionString = ConnectionString;
            //cn.ConnectionString = ConnectionString;
            //cn.Open();

            //DbDataAdapter da = dbProvider.CreateDataAdapter();
            //DbCommand cmd = dbProvider.CreateCommand();
            //cmd.Connection = cn;

            
            switch (GetProvider) {
                case Util.Provider.OleDB:
                    break;
                case Util.Provider.ODBC:
                    break;
                case Util.Provider.SqlClient:
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandText = String.Format("{0}GET_TABLENAME", Util.strProp);
                    //DbParameter dbParam = dbProvider.CreateParameter();
                    /*dbParam.Direction = ParameterDirection.Input;
                    dbParam.ParameterName = FormatNameParam("NOMBRE");
                    dbParam.Value = strNombre;
                    dbParam.Size = 50;
                    cmd.Parameters.Add(dbParam);*/
                    SQLServerConnection sqlserver = new SQLServerConnection();
                    sqlserver.ConnectionString = ConnectionString;
                    NOMBRE = new SqlParameter("@NOMBRE", SqlDbType.VarChar, 50);
                    NOMBRE.Value = strNombre;
                    parametros[0] = NOMBRE;
                    data = sqlserver.ExecuteDataTable(CommandType.StoredProcedure, "GET_TABLENAME", parametros);
                    data.TableName = "UtterWaste";
                    ds.Tables.Add(data);
                    sqlserver.Dispose();
                    break;
                case Util.Provider.MySql:
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandText = String.Format("{0}GET_TABLENAME", Util.strProp);
                    /*DbParameter dbParam = dbProvider.CreateParameter();
                    dbParam.Direction = ParameterDirection.Input;
                    dbParam.ParameterName = FormatNameParam("NOMBRE");
                    dbParam.Value = strNombre;
                    dbParam.Size = 50;*/
                    //cmd.Parameters.Add(dbParam);
                    MySQLConnection mysql = new MySQLConnection();
                    mysql.ConnectionString = ConnectionString;
                    NOMBRE = new MySql.Data.MySqlClient.MySqlParameter("NOMBRE", MySql.Data.MySqlClient.MySqlDbType.VarChar, 50);
                    NOMBRE.Value = strNombre;
                    parametros[0] = NOMBRE;
                    data = mysql.ExecuteDataTable(CommandType.StoredProcedure, "GET_TABLENAME", parametros);
                    data.TableName = "UtterWaste";
                    ds.Tables.Add(data);
                    mysql.Dispose();
                    //NOMBRE
                    break;
                case Util.Provider.Oracle:
                    DataTierGenerator.OracleConnection oracle = new DataTierGenerator.OracleConnection();
                    oracle.ConnectionString = ConnectionString;
                    //NOMBRE = new OracleParameter("NOMBRE", OracleType.VarChar, 50);
                    //NOMBRE.Value = strNombre;
                    //parametros[0] = NOMBRE;
                    data = oracle.ExecuteDataTable(CommandType.Text, "select table_name as WASTE from user_catalog", parametros);
                    data.TableName = "UtterWaste";
                    ds.Tables.Add(data);
                    oracle.Dispose();
                    break;
                case Util.Provider.Db2:
                    
                    break;
                case Util.Provider.PostgreSQL:
                    PostgreSQLConnection pgsql = new PostgreSQLConnection();
                    pgsql.ConnectionString = ConnectionString;
                    data = pgsql.ExecuteDataTable(CommandType.Text, "SELECT (c.relname) AS WASTE FROM pg_namespace nc, pg_class c WHERE c.relnamespace = nc.oid AND c.relkind IN ('r') AND nc.nspname = 'public' order by c.relname", parametros);
                    data.TableName = "UtterWaste";
                    ds.Tables.Add(data);
                    pgsql.Dispose();
                    //cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = "";
                    break;
                default:
                    break;
            }
            

            /*if (AplicaPropietario())
            {
                DbParameter _dbParam = dbProvider.CreateParameter();
                _dbParam.Direction = ParameterDirection.Input;
                _dbParam.ParameterName = FormatNameParam("OWNER");
                _dbParam.Value = Util.strProp.Replace(".", "");
                _dbParam.Size = 50;
                cmd.Parameters.Add(_dbParam);
            }*/

            /*if (AplicaCursor())
            {
                CreateParameterCursorOut(ref cmd, 1);
            }*/

            //da.SelectCommand = cmd;
            //da.Fill(ds, "UtterWaste");
            cbTableNames.DataSource = ds.Tables["UtterWaste"].DefaultView;
            cbTableNames.DisplayMember = "WASTE";
            cbTableNames.SelectedIndex = -1;
            cbTableNames.SelectionMode = SelectionMode.One;

        }

        private void CreateParameterCursorOut(ref DbCommand dbCmd, int pintIndex)
        {
            OracleParameter dbPrm = ((OracleParameter)dbProvider.CreateParameter());
            dbPrm.ParameterName = String.Format("cv_{0}", pintIndex);
            dbPrm.Direction = ParameterDirection.Output;
            dbPrm.OracleType = OracleType.Cursor;
            dbCmd.Parameters.Add(dbPrm);
        }

        public void CargarColumnas(string TableName)
        {
            try
            {
                DataTable data=new DataTable();
                IDataParameter[] parametros = new IDataParameter[2];
                IDataParameter NOMBRE;
                IDataParameter TABLE_NAME;
                this.dsColumns = new DataSet();
                switch (GetProvider) { 
                    case Util.Provider.MySql:
                        MySQLConnection mysql = new MySQLConnection();
                        mysql.ConnectionString = _ConnectionString;
                        NOMBRE = new MySql.Data.MySqlClient.MySqlParameter("NOMBRE", MySql.Data.MySqlClient.MySqlDbType.VarChar, 50);
                        TABLE_NAME = new MySql.Data.MySqlClient.MySqlParameter("TABLE_NAME", MySql.Data.MySqlClient.MySqlDbType.VarChar, 200);
                        NOMBRE.Value = NombreDB;
                        TABLE_NAME.Value = TableName;
                        parametros[0] = NOMBRE;
                        parametros[1] = TABLE_NAME;
                        data = mysql.ExecuteDataTable(CommandType.StoredProcedure, "GET_METADATA", parametros);
                        this.dsColumns.Tables.Add(data);
                        mysql.Dispose();
                        break;
                    case Util.Provider.Oracle:
                        OracleConnection oracle = new OracleConnection();
                        oracle.ConnectionString = _ConnectionString;
                        data = oracle.ExecuteDataTable(CommandType.Text, "select COLUMN_NAME,DATA_TYPE,DATA_LENGTH as MAZSIZE,DATA_PRECISION,DATA_SCALE,'N' AS ISFOREIGNKEY,COLUMN_ID as Ordinal_Position, NULLABLE as IS_NULLABLE, DATA_TYPE_MOD, DATA_TYPE_OWNER, DATA_LENGTH, DEFAULT_LENGTH, DATA_DEFAULT, NUM_DISTINCT, CHARACTER_SET_NAME, CHAR_COL_DECL_LENGTH, CHAR_LENGTH, (Case When (select count(*) from user_cons_columns where a.COLUMN_NAME= column_name And table_name='" + TableName + "' and position is not null and constraint_name like 'SYS%')>0 Then 1 Else 0 End) as ISPK from USER_TAB_COLUMNS a WHERE a.TABLE_NAME='" + TableName + "' order by Column_id", parametros);
                        oracle.Dispose();
                        break;
                        
                    case Util.Provider.PostgreSQL:
                        PostgreSQLConnection pgsql = new PostgreSQLConnection();
                        pgsql.ConnectionString = _ConnectionString;
                        data = pgsql.ExecuteDataTable(CommandType.Text, "SELECT a.attname AS COLUMN_NAME,(CASE WHEN t.typtype = 'd' THEN CASE WHEN bt.typelem <> 0 AND bt.typlen = -1 THEN 'ARRAY' WHEN nbt.nspname = 'pg_catalog' THEN format_type(t.typbasetype, null) ELSE 'USER-DEFINED' END ELSE CASE WHEN t.typelem <> 0 AND t.typlen = -1 THEN 'ARRAY' WHEN nt.nspname = 'pg_catalog' THEN format_type(a.atttypid, null) ELSE 'USER-DEFINED' END END) AS data_type, (CASE WHEN a.attnotnull OR (t.typtype = 'd' AND t.typnotnull) THEN 'NO' ELSE 'YES' END) AS is_nullable, _pg_char_max_length(information_schema._pg_truetypid(a, t), information_schema._pg_truetypmod(a, t)) AS mazsize, ColumnForeignKey(CAST(c.relname AS character varying(30)),CAST(a.attname AS character varying(30))) AS IsForeignKey, a.attnum AS ordinal_position, ColumnPrimaryKey(CAST(c.relname AS character varying(30)),CAST(a.attname AS character varying(30))) AS IsPK FROM (pg_attribute a LEFT JOIN pg_attrdef ad ON attrelid = adrelid AND attnum = adnum), pg_class c, pg_namespace nc, (pg_type t JOIN pg_namespace nt ON (t.typnamespace = nt.oid)) LEFT JOIN (pg_type bt JOIN pg_namespace nbt ON (bt.typnamespace = nbt.oid)) ON (t.typtype = 'd' AND t.typbasetype = bt.oid) WHERE a.attrelid = c.oid AND a.atttypid = t.oid AND nc.oid = c.relnamespace AND (NOT pg_is_other_temp_schema(nc.oid)) AND c.relname = '"+TableName+"' AND a.attnum > 0 AND NOT a.attisdropped AND c.relkind in ('r', 'v') AND nc.nspname='public' AND (pg_has_role(c.relowner, 'USAGE') OR has_table_privilege(c.oid, 'SELECT') OR has_table_privilege(c.oid, 'INSERT') OR has_table_privilege(c.oid, 'UPDATE') OR has_table_privilege(c.oid, 'REFERENCES') )", parametros);
                        pgsql.Dispose();
                        break;
                    case Util.Provider.SqlClient:
                        SQLServerConnection sqlserver = new SQLServerConnection();
                        sqlserver.ConnectionString = _ConnectionString;
                        NOMBRE = new SqlParameter("@NOMBRE", SqlDbType.VarChar, 50);
                        TABLE_NAME = new SqlParameter("@TABLE_NAME", SqlDbType.VarChar, 200);
                        NOMBRE.Value = NombreDB;
                        TABLE_NAME.Value = TableName;
                        parametros[0] = NOMBRE;
                        parametros[1] = TABLE_NAME;
                        data = sqlserver.ExecuteDataTable(CommandType.StoredProcedure, "GET_METADATA", parametros);
                        sqlserver.Dispose();
                        break;
                    case Util.Provider.Db2:
                        break;
                    case Util.Provider.ODBC:
                        break;
                    case Util.Provider.OleDB:
                        break;
                }
                /*DbCommand cmd = dbProvider.CreateCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = String.Format("{0}GET_METADATA", Util.strProp);
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                DbParameter dbParam = dbProvider.CreateParameter();
                dbParam.Direction = ParameterDirection.Input;
                dbParam.ParameterName = FormatNameParam("TABLE_NAME");
                dbParam.Value = TableName;
                dbParam.Size = 200;
                cmd.Parameters.Add(dbParam);

                DbParameter dbParam2 = dbProvider.CreateParameter();
                dbParam2.Direction = ParameterDirection.Input;
                dbParam2.ParameterName = FormatNameParam("NOMBRE");
                dbParam2.Value = NombreDB;
                dbParam2.Size = 50;
                cmd.Parameters.Add(dbParam2);

                if (AplicaPropietario())
                {
                    DbParameter _dbParam = dbProvider.CreateParameter();
                    _dbParam.Direction = ParameterDirection.Input;
                    _dbParam.ParameterName = FormatNameParam("OWNER");
                    _dbParam.Value = Util.strProp.Replace(".", "");
                    _dbParam.Size = 50;
                    cmd.Parameters.Add(_dbParam);
                }

                if (AplicaCursor())
                {
                    CreateParameterCursorOut(ref cmd, 1);
                }

                DbDataAdapter da = dbProvider.CreateDataAdapter();
                da.SelectCommand = cmd;
                
                da.Fill(this.dsColumns);
                cn.Close();*/
                this.dsColumns.Tables.Add(data);
            }
            catch (Exception ex)
            {
                this.txtASPX.Text = ex.ToString();
            }

        }
        private void ProcessTable(string TableName)
        {
            try
            {
                if (this.dsColumns != null)
                {
                    if (cmbTipo.Text == "GridView")
                    { GenerateGridView(TableName, this.dsColumns); }
                    else if (cmbTipo.Text == "FormView")
                    {
                        GenerateFormView(TableName, this.dsColumns);
                    }
                }
            }
            catch (Exception ex)
            {
                this.txtASPX.Text = ex.ToString();
            }

        }
        private void GenerateGridView(string TableName, DataSet ds)
        {
            GridViewGen gn = new GridViewGen(TableName, "", "4", "", ds, txtBOGen.Text, txtEntGen.Text);
            txtASPX.AppendText(gn.ToString());

        }
        private void GenerateFormView(string TableName, DataSet ds)
        {

            FormViewGen fn = new FormViewGen(TableName, "", "4", "", ds, txtBOGen.Text, txtEntGen.Text);
            txtASPX.AppendText(fn.ToString());

        }
        private void GenerateHTML(string ColumnName, string DataType, string Nullable, string MaxLength, string FK)
        {
            try
            {
                string LabelName;
                string text;
                text = ColumnName;
                LabelName = "lbl" + ColumnName;
                if (FK == "N")
                {
                    text = ColumnName;
                }
                else
                {
                    text = ColumnName.Replace("_Id", "");
                    text = ColumnName.Replace("_", " ");
                }
                ctlLabel newLabel = new ctlLabel(LabelName, FirstControlLeft, ControlWidth, ControlTop, ControlHeight, 99, text, ControlStyleCount);
                ControlStyleCount = ControlStyleCount + 1;
                //this.txtASPX.Text = this.txtASPX.Text + newLabel.ToString();
                this.txtASPX.AppendText(newLabel.ToString());
                this.txtASPX.AppendText("\n");
                if (FK == "Y")
                {
                    string ddlName;
                    ddlName = "ddl" + ColumnName;
                    ddl newddl = new ddl(ddlName, SecondControlLeft, ControlWidth, ControlTop, ControlHeight, ControlTabIndex, ControlStyleCount);

                    //this.txtASPX.Text = this.txtASPX.Text+ newddl.ToString();
                    this.txtASPX.AppendText(newddl.ToString());
                    this.txtASPX.AppendText("\n");

                }
                else
                {
                    string txtName;
                    txtName = "txt" + ColumnName;
                    ctlText newtext = new ctlText(txtName, SecondControlLeft, ControlWidth, ControlTop, ControlHeight, ControlTabIndex, Convert.ToInt32(MaxLength), ControlStyleCount);
                    //this.txtASPX.Text= this.txtASPX.Text + newtext.ToString();
                    this.txtASPX.AppendText(newtext.ToString());
                    this.txtASPX.AppendText("\n");

                    if (Nullable.TrimEnd(null) == "No")
                    {
                        ControlStyleCount = ControlStyleCount + 1;
                        string rvName;
                        ControlTop = ControlTop + 35;
                        rvName = "rv" + ColumnName;
                        Validator newvalidator = new Validator(rvName, ColumnName, txtName, FirstControlLeft, ControlTop, 99, ControlStyleCount);
                        //this.txtASPX.Text = this.txtASPX.Text + newvalidator.ToString();
                        this.txtASPX.AppendText(newvalidator.ToString());
                        this.txtASPX.AppendText("\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.P_Generalog(ex);
                MessageBox.Show(this, "Error: " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbTipo.Text.Trim() == "")
                {
                    MessageBox.Show("Debes Elegir un Tipo", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.dsColumns.AcceptChanges();
                //string strtest=this.dsColumns.Tables[0].ToString();
                //dgvColumnas.
                ControlTop = 10;
                ControlTabIndex = 0;
                this.txtASPX.Text = "";
                //agregar encabezado
                //this.txtASPX.AppendText("<%@ Page Language=" + Quote + "C#" + Quote + " AutoEventWireup=" + Quote + "true" + Quote + " CodeFile=" + Quote + "frm" + txtTableName.Text.ToString().TrimEnd(null) + ".aspx.cs" + Quote + " Inherits=" + Quote + "frm" + txtTableName.Text.ToString().TrimEnd(null) + Quote + " %>");
                //this.txtASPX.AppendText("\n");    
                //this.txtASPX.AppendText("<!DOCTYPE html PUBLIC " + Quote + "-//W3C//DTD XHTML 1.0 Transitional//EN" + Quote + " " + Quote + "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" + Quote + ">");
                //this.txtASPX.AppendText("\n");
                //this.txtASPX.AppendText("<html xmlns="+Quote +"http://www.w3.org/1999/xhtml"+Quote +">");
                //this.txtASPX.AppendText("\n");
                //this.txtASPX.AppendText("<head runat="+Quote +"server"+Quote +">");
                //this.txtASPX.AppendText("  <title>Untitled Page</title>");
                //this.txtASPX.AppendText("\n");
                //this.txtASPX.AppendText("</head>");
                //this.txtASPX.AppendText("\n");
                //this.txtASPX.AppendText("<body>");
                //this.txtASPX.AppendText("\n");
                //this.txtASPX.AppendText("<form id="+Quote +"form1" +Quote + " runat="+Quote +"server"+Quote+">");
                //this.txtASPX.AppendText("\n");    
                //this.txtASPX.AppendText("<div>");
                //this.txtASPX.AppendText("\n");
                //agregar componentes
                ProcessTable(txtTableName.Text.ToString().TrimEnd(null));


                //agregar pie de pagina

                //this.txtASPX.AppendText("</div>");
                //this.txtASPX.AppendText("\n");
                //this.txtASPX.AppendText("</form>");
                //this.txtASPX.AppendText("\n");
                //this.txtASPX.AppendText("</body>");
                //this.txtASPX.AppendText("\n");
                //this.txtASPX.AppendText("</html>");
                //this.txtASPX.AppendText("\n");

                string filePath = "ASPX\\";

                if (Directory.Exists(filePath))
                {
                    Directory.Delete(filePath, true);
                }

                Directory.CreateDirectory(filePath);

                using (StreamWriter writer = new StreamWriter(filePath + "frm" + txtTableName.Text.ToString().TrimEnd(null) + ".aspx", true))
                {
                    writer.Write(this.txtASPX.Text);
                    writer.Close();
                }
                parser = new RtfParser(this.txtASPX);
                this.aspxHighlighter1.Parser = parser;
                txtASPX.Rtf = this.aspxHighlighter1.Parse(this.txtASPX.Text);

                MessageBox.Show(this, "Codigo Generado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tabControlPrincipal.SelectTab("tabASPX");
            }
            catch (Exception ex)
            {
                Utility.P_Generalog(ex);
                MessageBox.Show(this, "Error: " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbTableNames_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void txtCon_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbTableNames_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //if (!(cbTableNames.SelectedItem.ToString() == null))
            //{ txtTableName.Text = cbTableNames..ToString(); }
        }

        private void cbTableNames_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue == CheckState.Checked)
                {
                    System.Data.DataRowView row = (System.Data.DataRowView)cbTableNames.Items[e.Index];
                    txtTableName.Text = row["WASTE"].ToString();
                    CargarColumnas(txtTableName.Text);
                    dsColumns.Tables[0].Columns.Add("COLUMN_ALIAS");
                    dsColumns.Tables[0].Columns.Add("COLUMN_VISIBLE", typeof(Boolean));
                    dgvColumnas.AutoGenerateColumns = false;
                    dgvColumnas.DataSource = this.dsColumns.Tables[0];
                    int newColIndex = dgvColumnas.Columns.Add("COLUMN_NAME",
                                     "COLUMN_NAME");
                    dgvColumnas.Columns[newColIndex].DataPropertyName =
                       "COLUMN_NAME";
                    newColIndex = dgvColumnas.Columns.Add("COLUMN_ALIAS",
                                     "COLUMN_ALIAS");
                    dgvColumnas.Columns[newColIndex].DataPropertyName =
                       "COLUMN_ALIAS";
                    DataGridViewCheckBoxColumn chkVisible = new DataGridViewCheckBoxColumn();
                    chkVisible.Name = "COLUMN_VISIBLE";
                    chkVisible.DataPropertyName = "COLUMN_VISIBLE";
                    chkVisible.HeaderText = "COLUMN_VISIBLE";
                    newColIndex = dgvColumnas.Columns.Add(chkVisible);

                    newColIndex = dgvColumnas.Columns.Add("DATA_TYPE",
                                     "DATA_TYPE");
                    dgvColumnas.Columns[newColIndex].DataPropertyName =
                       "DATA_TYPE";
                    newColIndex = dgvColumnas.Columns.Add("IS_NULLABLE",
                                     "IS_NULLABLE");
                    dgvColumnas.Columns[newColIndex].DataPropertyName =
                       "IS_NULLABLE";
                    newColIndex = dgvColumnas.Columns.Add("MAZSIZE",
                                     "MAZSIZE");
                    dgvColumnas.Columns[newColIndex].DataPropertyName =
                       "MAZSIZE";
                    newColIndex = dgvColumnas.Columns.Add("IsForeignKey",
                                     "IsForeignKey");
                    dgvColumnas.Columns[newColIndex].DataPropertyName =
                       "IsForeignKey";
                    newColIndex = dgvColumnas.Columns.Add("Ordinal_Position",
                                     "Ordinal_Position");
                    dgvColumnas.Columns[newColIndex].DataPropertyName =
                       "Ordinal_Position";
                    newColIndex = dgvColumnas.Columns.Add("IsPK",
                                   "IsPK");
                    dgvColumnas.Columns[newColIndex].DataPropertyName =
                       "IsPK";
                    foreach (DataGridViewRow dgrow in dgvColumnas.Rows)
                    {
                        dgrow.Cells["COLUMN_VISIBLE"].Value = true;
                    }

                }
                else
                {
                    //dgvColumnas.Columns.Remove("ALIAS");
                    dgvColumnas.Columns.Remove("COLUMN_NAME");
                    dgvColumnas.Columns.Remove("COLUMN_ALIAS");
                    dgvColumnas.Columns.Remove("COLUMN_VISIBLE");
                    dgvColumnas.Columns.Remove("DATA_TYPE");
                    dgvColumnas.Columns.Remove("IS_NULLABLE");
                    dgvColumnas.Columns.Remove("MAZSIZE");
                    dgvColumnas.Columns.Remove("IsForeignKey");
                    dgvColumnas.Columns.Remove("Ordinal_Position");
                    dgvColumnas.Columns.Remove("IsPK");

                    dgvColumnas.DataSource = new DataSet();
                }
            }
            catch (Exception ex)
            {
                Utility.P_Generalog(ex);
                MessageBox.Show(this, "Error: " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                RMCodeGen.Properties.Settings.Default.CadenaConexion = txtCon.Text;
                RMCodeGen.Properties.Settings.Default.Save();
                //GetPropietarios();

                ///Verificamos el nombre de la base de datos para cargar las tablas
                string[] conexion;
                conexion = txtCon.Text.Split(';');
                string database = "";
                for (int i = 0; i < conexion.Length - 1; i++)
                {
                    string texto = conexion[i];
                    string[] parametro;
                    parametro = texto.Split('=');
                    switch (parametro[0])
                    {
                        case "Database":
                            database = parametro[1];
                            break;
                        case "Initial Catalog":
                            database = parametro[1];
                            break;
                    }
                }
                NombreDB = database;
                //if (!AplicaPropietario())
                //GetTables(database);
                GetTables("");
                RMCodeGen.Properties.Settings.Default.CadenaConexion = this.txtCon.Text;
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Utility.P_Generalog(ex);
                MessageBox.Show(this, "Error: " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally {
                Cursor = Cursors.Default;
            }
        }

        private void namespaceTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (namespaceTextBox.Text == string.Empty)
            {
                MessageBox.Show("Por favor Ingrese un Namespace.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                namespaceTextBox.Focus();
                return;
            }
            if (txtAliasTabla.Text == string.Empty)
            {
                MessageBox.Show("Por favor Ingrese un Alias.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAliasTabla.Focus();
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                this.dsColumns.AcceptChanges();

                // Generate the SQL VB and C# code
                Generator.proveedor = GetProvider;
                Utility.proveedor = GetProvider;
                SqlGenerator.DataBaseSys = _DataBase;
                VBGenerator.DataBaseSys = _DataBase;
                Generator.ConnectionString = txtCon.Text.Trim();
                CsGenerator.DataBaseSys = _DataBase;
                CsGenerator.Protocolo = _Protocolo;
                CsGenerator.Puerto = _Puerto;
                CsGenerator.DriverJDBC = _DriverJDBC;
                Utility.DataBaseSys = _DataBase;
                Utility.ConnectionString = txtCon.Text.Trim();
                Generator.GenerateforTable(dbProvider, txtCon.Text.Trim(), grantUserTextBox.Text, storedProcedurePrefixTextBox.Text, false, namespaceTextBox.Text, txtTableName.Text.ToString().TrimEnd(null), this.txtAliasTabla.Text, this.dsColumns.Tables[0], txt_execute.Text);

                string _base = string.Empty;
                if (Util.strProp != null && Util.strProp.ToString() != string.Empty)
                    _base = Util.strProp.Replace(".", "");
                else
                    _base = _DataBase;

                //SqlConnection con = new SqlConnection(txtCon.Text.Trim());
                //abre el archivo de Entidad
                if (File.Exists(".\\" + _base.ToUpper() + "\\CS\\Entities\\" + txtAliasTabla.Text.ToString().TrimEnd(null) + (radCS.Checked ? ".cs" : ".vb")) == true)
                {
                    using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Entities\\" + txtAliasTabla.Text.ToString().TrimEnd(null) + (radCS.Checked ? ".cs" : ".vb")))
                    {
                        //txtEntidad.rt
                        txtEntidad.Text = streamReader.ReadToEnd();
                        //txtEntidad.Text=CodeHighLight.Highlight(txtEntidad.Text);
                        parser = new RtfParser(this.txtEntidad);
                        if (radCS.Checked == true)
                        {
                            if (this.cSharpHighlighter1 != null) {
                                this.cSharpHighlighter1.Parser = parser;
                                txtEntidad.Rtf = this.cSharpHighlighter1.Parse(this.txtEntidad.Text);
                            }
                        }
                        else
                        {
                            this.vbHighlighter1.Parser = parser;
                            txtEntidad.Rtf = this.vbHighlighter1.Parse(this.txtEntidad.Text);
                        }

                        //txtEntidad.Rtf = CodeHighLight.Highlight(txtEntidad.Text);
                    }

                    using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Entities\\" + txtAliasTabla.Text.ToString().TrimEnd(null) + (radCS.Checked ? ".java" : ".java")))
                    {
                        //txtEntidad.rt
                        txtClase.Text = streamReader.ReadToEnd();
                        //txtEntidad.Text=CodeHighLight.Highlight(txtEntidad.Text);
                        parser = new RtfParser(this.txtClase);
                        if (radCS.Checked == true)
                        {
                            if (this.cSharpHighlighter1 != null) {
                                this.cSharpHighlighter1.Parser = parser;
                                txtClase.Rtf = this.cSharpHighlighter1.Parse(this.txtClase.Text);
                            }
                            
                        }
                        //txtEntidad.Rtf = CodeHighLight.Highlight(txtEntidad.Text);
                    }
                    //abre el archivo de los SP'S
                    if (File.Exists(".\\" + _base + (radSQLServer.Checked ? "\\SQL\\StoredProcedures.sql" : "\\SQL\\MySqlStoredProcedures.sql")) == true)
                    {
                        using (StreamReader streamReader = new StreamReader(".\\" + _base + (radSQLServer.Checked ? "\\SQL\\StoredProcedures.sql" : "\\SQL\\MySqlStoredProcedures.sql")))
                        {

                            txtSQLSP.Text = streamReader.ReadToEnd();
                            parser = new RtfParser(this.txtSQLSP);
                            if (this.cSharpHighlighter1 != null) {
                                this.sqlHighlighter1.Parser = parser;
                                txtSQLSP.Rtf = this.sqlHighlighter1.Parse(this.txtSQLSP.Text);
                            }
                                
                        }
                    }
                    //abre el Archivo de Acceso a Datos
                    if (radSQLServer.Checked)
                    {    //Recupera el Business Object
                        if (File.Exists(".\\" + _base + "\\CS\\BO\\MSSQL\\" + txtAliasTabla.Text.ToString().TrimEnd(null) + "BO" + (radCS.Checked ? ".cs" : ".vb")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\BO\\MSSQL\\" + txtAliasTabla.Text.ToString().TrimEnd(null) + "BO" + (radCS.Checked ? ".cs" : ".vb")))
                            {
                                txtBusinessObject.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtBusinessObject);
                                if (radCS.Checked == true)
                                {
                                    if (this.cSharpHighlighter1 != null) {
                                        this.cSharpHighlighter1.Parser = parser;
                                        txtBusinessObject.Rtf = this.cSharpHighlighter1.Parse(this.txtBusinessObject.Text);
                                    }
                                        
                                }
                                else
                                {
                                    this.vbHighlighter1.Parser = parser;
                                    txtBusinessObject.Rtf = this.vbHighlighter1.Parse(this.txtBusinessObject.Text);
                                }

                            }
                        }

                        if (File.Exists(".\\" + _base + "\\CS\\Entities\\" + txtAliasTabla.Text.ToString().TrimEnd(null) + "BO" + (radCS.Checked ? ".java" : ".java")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Entities\\" + txtAliasTabla.Text.ToString().TrimEnd(null) + "BO" + (radCS.Checked ? ".java" : ".java")))
                            {
                                txtBusinessObjectJava.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtBusinessObjectJava);
                                if (this.cSharpHighlighter1 != null) {
                                    this.cSharpHighlighter1.Parser = parser;
                                    txtBusinessObjectJava.Rtf = this.cSharpHighlighter1.Parse(this.txtBusinessObjectJava.Text);
                                }
                                    
                            }
                        }

                        //Recupera el Acceso a Datos de SQL Server

                        if (File.Exists(".\\" + _base + "\\CS\\Data\\MSSQL\\Connection" + (radCS.Checked ? ".java" : ".java")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Data\\MSSQL\\Connection" + (radCS.Checked ? ".java" : ".java")))
                            {
                                txtAccesoDatosJava.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtAccesoDatosJava);
                                if (this.cSharpHighlighter1 != null) {
                                    this.cSharpHighlighter1.Parser = parser;
                                    txtAccesoDatosJava.Rtf = this.cSharpHighlighter1.Parse(this.txtAccesoDatosJava.Text);
                                }
                                    
                            }
                        }

                        if (File.Exists(".\\" + _base + "\\CS\\Data\\MSSQL\\CommandType" + (radCS.Checked ? ".java" : ".java")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Data\\MSSQL\\CommandType" + (radCS.Checked ? ".java" : ".java")))
                            {
                                txtAccesoDatosJavaCommand.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtAccesoDatosJavaCommand);
                                if (this.cSharpHighlighter1 != null)
                                {
                                    this.cSharpHighlighter1.Parser = parser;
                                    txtAccesoDatosJavaCommand.Rtf = this.cSharpHighlighter1.Parse(this.txtAccesoDatosJavaCommand.Text);
                                }
                                    
                            }
                        }

                        if (File.Exists(".\\" + _base + "\\CS\\Data\\MSSQL\\Direction" + (radCS.Checked ? ".java" : ".java")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Data\\MSSQL\\Direction" + (radCS.Checked ? ".java" : ".java")))
                            {
                                txtAccesoDatosJavaDirection.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtAccesoDatosJavaDirection);
                                if (this.cSharpHighlighter1 != null)
                                {
                                    this.cSharpHighlighter1.Parser = parser;
                                    txtAccesoDatosJavaDirection.Rtf = this.cSharpHighlighter1.Parse(this.txtAccesoDatosJavaDirection.Text);
                                }
                                    
                            }
                        }

                        if (File.Exists(".\\" + _base + "\\CS\\Data\\MSSQL\\Parameter" + (radCS.Checked ? ".java" : ".java")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Data\\MSSQL\\Parameter" + (radCS.Checked ? ".java" : ".java")))
                            {
                                txtAccesoDatosJavaParameter.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtAccesoDatosJavaParameter);
                                if (this.cSharpHighlighter1 != null)
                                {
                                    this.cSharpHighlighter1.Parser = parser;
                                    txtAccesoDatosJavaParameter.Rtf = this.cSharpHighlighter1.Parse(this.txtAccesoDatosJavaParameter.Text);
                                }
                                    
                            }
                        }


                        if (File.Exists(".\\" + _base + "\\CS\\Data\\MSSQL\\" + txtAliasTabla.Text.ToString().TrimEnd(null) + "DA" + (radCS.Checked ? ".cs" : ".vb")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Data\\MSSQL\\" + txtAliasTabla.Text.ToString().TrimEnd(null) + "DA" + (radCS.Checked ? ".cs" : ".vb")))
                            {
                                txtAccesoDatosVisual.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtAccesoDatosVisual);
                                if (radCS.Checked == true)
                                {
                                    if (this.cSharpHighlighter1 != null) {
                                        this.cSharpHighlighter1.Parser = parser;
                                        txtAccesoDatosVisual.Rtf = this.cSharpHighlighter1.Parse(this.txtAccesoDatosVisual.Text);
                                    }
                                        
                                }
                                else
                                {
                                    this.vbHighlighter1.Parser = parser;
                                    txtAccesoDatosVisual.Rtf = this.vbHighlighter1.Parse(this.txtAccesoDatosVisual.Text);
                                }

                            }
                        }

                        /*Recupera el Sql Utility*/
                        if (File.Exists(".\\" + _base + "\\CS\\Data\\MSSQL\\SqlHelper" + (radCS.Checked ? ".cs" : ".vb")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Data\\MSSQL\\SqlHelper" + (radCS.Checked ? ".cs" : ".vb")))
                            {
                                txtSQLUtility.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtSQLUtility);
                                if (radCS.Checked == true)
                                {
                                    if (this.cSharpHighlighter1 != null) {
                                        this.cSharpHighlighter1.Parser = parser;
                                        txtSQLUtility.Rtf = this.cSharpHighlighter1.Parse(this.txtSQLUtility.Text);
                                    }
                                        
                                }
                                else
                                {
                                    this.vbHighlighter1.Parser = parser;
                                    txtSQLUtility.Rtf = this.vbHighlighter1.Parse(this.txtSQLUtility.Text);
                                }

                            }
                        }

                        if (File.Exists(".\\" + _base + "\\CS\\Data\\MSSQL\\DBMSConection" + (radCS.Checked ? ".java" : ".java")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Data\\MSSQL\\DBMSConection" + (radCS.Checked ? ".java" : ".java")))
                            {
                                txtSQLUtilityJava.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtSQLUtilityJava);
                                if (this.cSharpHighlighter1 != null) {
                                    this.cSharpHighlighter1.Parser = parser;
                                    txtSQLUtilityJava.Rtf = this.cSharpHighlighter1.Parse(this.txtSQLUtilityJava.Text);
                                }
                                    
                            }
                        }


                        /*Recupera el Sql Utility*/
                        if (File.Exists(".\\" + _base + "\\CS\\Entities\\AuditoriaEO" + (radCS.Checked ? ".cs" : ".vb")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Entities\\AuditoriaEO" + (radCS.Checked ? ".cs" : ".vb")))
                            {
                                txtAuditoria.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtAuditoria);
                                if (radCS.Checked == true)
                                {
                                    if (this.cSharpHighlighter1 != null) {
                                        this.cSharpHighlighter1.Parser = parser;
                                        txtAuditoria.Rtf = this.cSharpHighlighter1.Parse(this.txtAuditoria.Text);
                                    }
                                        
                                }
                                else
                                {
                                    this.vbHighlighter1.Parser = parser;
                                    txtAuditoria.Rtf = this.vbHighlighter1.Parse(this.txtAuditoria.Text);
                                }

                            }
                        }







                        /*Recupera el UtilDA*/
                        if (File.Exists(".\\" + _base + "\\CS\\Data\\MSSQL\\UtilDA" + (radCS.Checked ? ".cs" : ".vb")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Data\\MSSQL\\UtilDA" + (radCS.Checked ? ".cs" : ".vb")))
                            {
                                txtUtilDA.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtUtilDA);
                                if (radCS.Checked == true)
                                {
                                    if (this.cSharpHighlighter1 != null)
                                    {
                                        this.cSharpHighlighter1.Parser = parser;
                                        txtUtilDA.Rtf = this.cSharpHighlighter1.Parse(this.txtUtilDA.Text);
                                    }
                                        
                                }
                                else
                                {
                                    this.vbHighlighter1.Parser = parser;
                                    txtUtilDA.Rtf = this.vbHighlighter1.Parse(this.txtUtilDA.Text);
                                }

                            }
                        }
                        /*Recupera el UtilBO*/
                        if (File.Exists(".\\" + _base + "\\CS\\BO\\UtilBO" + (radCS.Checked ? ".cs" : ".vb")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\BO\\UtilBO" + (radCS.Checked ? ".cs" : ".vb")))
                            {
                                txtUtilBO.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtUtilBO);
                                if (radCS.Checked == true)
                                {
                                    if (this.cSharpHighlighter1 != null) {
                                        this.cSharpHighlighter1.Parser = parser;
                                        txtUtilBO.Rtf = this.cSharpHighlighter1.Parse(this.txtUtilBO.Text);
                                    }
                                        
                                }
                                else
                                {
                                    this.vbHighlighter1.Parser = parser;
                                    txtUtilBO.Rtf = this.vbHighlighter1.Parse(this.txtUtilBO.Text);
                                }

                            }
                        }
                        /*Recupera el GenericsBO*/
                        if (File.Exists(".\\" + _base + "\\CS\\BO\\GenericsBO" + (radCS.Checked ? ".cs" : ".vb")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\BO\\GenericsBO" + (radCS.Checked ? ".cs" : ".vb")))
                            {
                                txtGenericsBO.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtGenericsBO);
                                if (radCS.Checked == true)
                                {
                                    if (this.cSharpHighlighter1 != null) {
                                        this.cSharpHighlighter1.Parser = parser;
                                        txtGenericsBO.Rtf = this.cSharpHighlighter1.Parse(this.txtGenericsBO.Text);
                                    }
                                        
                                }
                                else
                                {
                                    this.vbHighlighter1.Parser = parser;
                                    txtGenericsBO.Rtf = this.vbHighlighter1.Parse(this.txtGenericsBO.Text);
                                }

                            }
                        }
                        /*Recupera el GenericsDA*/
                        if (File.Exists(".\\" + _base + "\\CS\\Data\\MSSQL\\GenericsDA" + (radCS.Checked ? ".cs" : ".vb")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Data\\MSSQL\\GenericsDA" + (radCS.Checked ? ".cs" : ".vb")))
                            {
                                txtGenericsDA.Text = streamReader.ReadToEnd();
                                parser = new RtfParser(this.txtGenericsDA);
                                if (radCS.Checked == true)
                                {
                                    if (this.cSharpHighlighter1 != null) {
                                        this.cSharpHighlighter1.Parser = parser;
                                        txtGenericsDA.Rtf = this.cSharpHighlighter1.Parse(this.txtGenericsDA.Text);
                                    }
                                        
                                }
                                else
                                {
                                    this.vbHighlighter1.Parser = parser;
                                    txtGenericsDA.Rtf = this.vbHighlighter1.Parse(this.txtGenericsDA.Text);
                                }
                            }
                        }
                    }
                    else if (radMySQL.Checked)
                    {
                        /*Recupera el Acceso a Datos de MySql*/
                        if (File.Exists(".\\" + _base + "\\CS\\Data\\MySQL\\" + txtAliasTabla.Text.ToString().TrimEnd(null) + "_DAL" + (radCS.Checked ? ".cs" : ".vb")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Data\\MySQL\\" + txtAliasTabla.Text.ToString().TrimEnd(null) + "_DAL" + (radCS.Checked ? ".cs" : ".vb")))
                            {
                                txtAccesoDatosVisual.Text = streamReader.ReadToEnd();
                            }
                        }
                        /*Recupera el MySqlUtility*/
                        if (File.Exists(".\\" + _base + "\\CS\\Data\\MySQL\\MySqlClientUtility" + (radCS.Checked ? ".cs" : ".vb")) == true)
                        {
                            using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Data\\MySQL\\MySqlClientUtility" + (radCS.Checked ? ".cs" : ".vb")))
                            {
                                txtSQLUtility.Text = streamReader.ReadToEnd();
                            }
                        }
                        ///*Recupera las colecciones*/
                        //if (File.Exists(".\\" + _base + "\\CS\\Entities\\" + txtAliasTabla.Text.ToString().TrimEnd(null) + "List" + (radCS.Checked ? ".cs" : ".vb")) == true)
                        //{
                        //    using (StreamReader streamReader = new StreamReader(".\\" + _base + "\\CS\\Entities\\" + txtTableName.Text.ToString().TrimEnd(null) + "List" + (radCS.Checked ? ".cs" : ".vb")))
                        //    {
                        //        txtColeccion.Text = streamReader.ReadToEnd();
                        //    }
                        //}
                    }
                    MessageBox.Show(this, "Codigo Generado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar.Value = 0;
                    this.tabControlPrincipal.SelectTab(2);
                }
                else
                {
                    progressBar.Value = 0;
                    MessageBox.Show(this, "El Archivo No ha Sido Generado", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                Utility.P_Generalog(ex);
                progressBar.Value = 0;
                MessageBox.Show(this, "Error: " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally {
                Cursor = Cursors.Default;
            }
        }
        private void Generator_DatabaseCounted(object sender, CountEventArgs e)
        {
            progressBar.Maximum = e.Count;
        }

        private void Generator_TableCounted(object sender, CountEventArgs e)
        {
            progressBar.Value = e.Count;
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void multipleFilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmGenerador_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Desea Cerrar la Aplicacion?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNomTabe_TextChanged(object sender, EventArgs e){
            //try{
            //    GetTables(this.txtNomTabe.Text.Trim());
            //}catch (Exception ex){
            //    MessageBox.Show(this, "Error: " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void cbPropietario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbPropietario.SelectedItem.ToString() != "-- SELECCIONE --")
                {
                    Util.strProp = cbPropietario.SelectedValue.ToString() + ".";
                    this.GetTables(string.Empty);
                }
            }
            catch (Exception ex)
            {
                Utility.P_Generalog(ex);
                Util.strProp = null;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cbPropietario.DataSource = null;
                cbTableNames.DataSource = null;
            }
        }

        private void cbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProveedor.DataSource !=null)
            {
                if(cbProveedor.SelectedIndex!=-1)
                {
                    string strProvider = this.cbProveedor.SelectedValue.ToString();
                    DataTable driver = (DataTable)this.cbProveedor.DataSource;
                    switch (strProvider)
                    {
                        case "System.Data.SqlClient":
                            txtCon.Text = "Data Source=" + _Servidor + ";" + 
                                          "Initial Catalog=" + _DataBase + ";" +
                                          "User Id=" + _User + ";" + 
                                          "Password=" + _Password + "";
                            
                            foreach (DataRow fila in driver.Rows) {
                                if (fila[2].Equals("System.Data.SqlClient"))
                                {
                                    _DriverJDBC = (String)fila[3];
                                    _Protocolo = "\"" + (String)fila[4] + "\" + this.SERVER + \":\" + this.PORT + \"; DatabaseName = \"+ this.DATABASE ";
                                    _Puerto = "1433";
                                    break;
                                }
                            }
                            
                            break;
                        case "Npgsql":
                            txtCon.Text = "Server=" + _Servidor + ";Port=5432;Database=" + _DataBase + ";User Id=" + _User + ";Password=" + _Password + "";
                            foreach (DataRow fila in driver.Rows)
                            {
                                if (fila[2].Equals("Npgsql"))
                                {
                                    _DriverJDBC = (String)fila[3];
                                    _Protocolo = "\""+(String)fila[4]+"\" + this.SERVER + \":\" + this.PORT + \"/\"+ this.DATABASE";
                                    _Puerto = "5432";
                                    break;
                                }
                            }
                            
                            break;
                        case "System.Data.OracleClient":
                            txtCon.Text = "Data Source=" + _Servidor + ";" +
                                          "User Id=" + _User + ";" +
                                          "Password=" + _Password + ";" + 
                                          "Integrated Security=no";
                            foreach (DataRow fila in driver.Rows)
                            {
                                if (fila[2].Equals("System.Data.OracleClient"))
                                {
                                    _DriverJDBC = (String)fila[3];
                                    _Protocolo = "\""+(String)fila[4]+"\" + this.SERVER + \":\" + this.PORT + \":\"+ this.DATABASE";
                                    _Puerto = "1521";
                                    break;
                                }
                            }
                            break;
                        case "MySql.Data.MySqlClient":
                            txtCon.Text = "Server=" + _Servidor + ";" +
                                          "Database=" + _DataBase + ";" +
                                          "Uid=" + _User + ";" +
                                          "Pwd=" + _Password + "";
                            foreach (DataRow fila in driver.Rows)
                            {
                                if (fila[2].Equals("MySql.Data.MySqlClient"))
                                {
                                    _DriverJDBC = (String)fila[3];
                                    _Protocolo = "\""+(String)fila[4]+"\" + this.SERVER + \":\" + this.PORT + \"/\"+ this.DATABASE";
                                    _Puerto = "3306";
                                    break;
                                }
                            }
                            break;
                        case "System.Data.Odbc":
                            txtCon.Text = "";
                            foreach (DataRow fila in driver.Rows)
                            {
                                if (fila[2].Equals("System.Data.Odbc"))
                                {
                                    _DriverJDBC = (String)fila[3];
                                    _Protocolo = "\""+(String)fila[4]+"\" + this.DATABASE";
                                    break;
                                }
                            }
                            break;
                        case "System.Data.OleDb":
                            txtCon.Text = "";
                            break;
                            //return Util.Provider.Access;
                    }
                }
            }
        }

        private void btnParametros_Click(object sender, EventArgs e)
        {
            frmParametros Param = new frmParametros();
            DialogResult resul = DialogResult.Cancel;
            Param.Servidor = _Servidor;
            Param.DataBase = _DataBase;
            Param.User = _User;
            Param.Password = _Password;
            Param.Proveedor = GetProvider;
            resul = Param.ShowDialog();
            if (resul == DialogResult.OK)
            {
                _Servidor = Param.Servidor;
                _DataBase = Param.DataBase;
                _User = Param.User;
                _Password = Param.Password;
                cbProveedor_SelectedIndexChanged(sender, e);
                btnCargar.Focus();
            }
            Param.Dispose();
            Param = null;
        }

        private void txtNomTabe_Enter(object sender, EventArgs e)
        {

        }

        private void txtNomTabe_KeyPress(object sender, KeyPressEventArgs e){
            
        }

        private void txtNomTabe_KeyDown(object sender, KeyEventArgs e){
            if (e.KeyCode == Keys.Enter) {
                try{
                    GetTables(this.txtNomTabe.Text.Trim());
                }catch (Exception ex){
                    Utility.P_Generalog(ex);
                    MessageBox.Show(this, "Error: " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
