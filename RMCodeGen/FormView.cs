using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace DataTierGenerator
{
    class FormViewGen :Common
    {
        private System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
        private string strHeightHeader;
        private int Height=0;
        private string strHeightFooter;
        private string strWidthHeader;
        private int Width=0;
        private string strWidthFooter;
        private string strcssClass;
        private string datakeynames;
        private string cellpading;
        private string datasourceid;
        private DataSet ds;
        private string name;
        private string BOGen;
        private string EntGen;
        private ArrayList arrClaves;
        private ArrayList arrClavesTipos;
        
        public FormViewGen()
        {
        }
        public FormViewGen(string name, string datakeynames, string cellpading, string datasourceid, DataSet ds,string BOGen,string EntGen)
        {
            arrClaves = new ArrayList();
            arrClavesTipos = new ArrayList();
            this.name = name;
            this.BOGen = BOGen;
            this.EntGen = EntGen;
            this.strcssClass = " CssClass=" + Quote + "CoffeeFieldCaptionTD" + Quote;
            this.strHeightHeader = " HEIGHT=" + Quote;
            this.strHeightFooter = "px" + "  " + Quote;
            this.strWidthHeader = " WIDTH=" + Quote;
            this.strWidthFooter = "px" + "  " + Quote;
            this.MAJORHEADER = "<asp:";
            this.CONTROLHEADER = "FormView";
            this.CONTROLFOOTER = ">";           
            this.MAJORFOOTER = "</asp:FormView>";
            //this.STYLECOUNT = StyleCount;
            //this.STYLE = " style=" + Quote + "Z-INDEX:" + this.STYLECOUNT.ToString() + ";";
            this.ID = "fv" + name;
            this.datakeynames = datakeynames;
            this.cellpading = cellpading;
            if (datasourceid.Trim() != "")
            { this.datasourceid = datasourceid;
            }
            else
            {
                this.datasourceid = " DataSourceID=" + Quote + "odsfv" + name + Quote;
                
            }
            
            
            this.ds = ds;
            if (datakeynames.Trim() != "")
            { this.datakeynames = datakeynames; }
            else
            {
                bool first = true;
                System.Text.StringBuilder datakeyGen = new StringBuilder();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isPK"].ToString()) == true)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            datakeyGen.Append(",");
                        }
                        if (dr[7].ToString().Trim() != "")
                        {
                            datakeyGen.Append(dr[7].ToString());
                            arrClaves.Add(dr[7].ToString());
                        }
                        else
                        {
                            datakeyGen.Append(dr[0].ToString());
                            arrClaves.Add(dr[0].ToString());
                        }
                        
                        arrClavesTipos.Add(dr[1].ToString());
                    }

                }
                this.datakeynames = " DataKeyNames=" + Quote + datakeyGen.ToString().Trim() + Quote;

            }
        }
        public override string ToString()
        {
            //int ControlStyleCount = 0;
            string rvName;

            strBuilder.AppendLine(" ");
            strBuilder.Append(this.MAJORHEADER);
            strBuilder.Append(this.CONTROLHEADER);
            strBuilder.Append(this.IDHEADER);
            strBuilder.Append(this.ID.ToString());
            strBuilder.Append(this.IDFOOTER);
            //strBuilder.Append(this.STYLE);
           // strBuilder.Append(this.STRLEFTHEADER);
           // strBuilder.Append(this.LEFT.ToString());
          //  strBuilder.Append(this.STRLEFTFOOTER);
           // strBuilder.Append(this.POSITION);
           // strBuilder.Append(this.STRTOPHEADER);
          //  strBuilder.Append(this.TOP.ToString());
          //  strBuilder.Append(this.STRTOPFOOTER);
          //  strBuilder.Append(Quote);
            strBuilder.Append(this.strHeightHeader);
            strBuilder.Append(this.Height.ToString());
            strBuilder.Append(this.strHeightFooter);
            strBuilder.Append(this.strWidthHeader);
            strBuilder.Append(this.Width.ToString());
            strBuilder.Append(this.strWidthFooter);
            strBuilder.Append(this.strcssClass);
            strBuilder.Append(this.STRTABINDEXHEADER);
            strBuilder.Append(this.TABINDEX.ToString().TrimStart(null));
            strBuilder.Append(this.STRTABINDEXFOOTER);
            strBuilder.Append(this.RUNATSERVER);
            strBuilder.Append(this.datasourceid);
            strBuilder.Append(this.datakeynames);
            strBuilder.Append(" OnItemDeleted="+Quote +this.ID+ "_ItemDeleted"+Quote );
            strBuilder.Append(" OnItemInserted=" + Quote + this.ID + "_ItemInserted" + Quote);
            strBuilder.Append(" OnItemUpdated=" + Quote + this.ID + "_ItemUpdated" + Quote);
            strBuilder.Append(this.CONTROLFOOTER);
            //OnItemDeleted="fvRECT_CERTIFICACION_ItemDeleted" OnItemInserted="fvRECT_CERTIFICACION_ItemInserted" OnItemUpdated="fvRECT_CERTIFICACION_ItemUpdated"

            //seccion para agregar el ItemTemplate
            strBuilder.AppendLine(" ");
            strBuilder.Append("<ItemTemplate>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<table>");
            strBuilder.AppendLine(" ");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (Convert.ToBoolean((dr[8] == null ? false : dr[8])) == true)
                {
                    strBuilder.Append("<tr>");
                    strBuilder.AppendLine(" ");
                    strBuilder.Append("<td>");
                    strBuilder.AppendLine(" ");
                    if (dr[7].ToString().Trim() != "")
                    { strBuilder.Append("" + dr[7].ToString() + ":"); }
                    else
                    { strBuilder.Append("" + dr[0].ToString() + ":"); }

                    strBuilder.Append("</td>");
                    strBuilder.AppendLine(" ");
                    strBuilder.Append("<td>");
                    strBuilder.AppendLine(" ");
                    if (dr[1].ToString().Trim() == "bit")
                    {
                        strBuilder.Append("<asp:CheckBox ");
                        if (dr[7].ToString().Trim() != "")
                        { strBuilder.Append(" ID= " + Quote + "chk_" + dr[7].ToString().Trim() + Quote); }
                        else
                        { strBuilder.Append(" ID= " + Quote + "chk_" + dr[0].ToString().Trim() + Quote); }

                        strBuilder.Append(" runat=" + Quote + "server" + Quote);
                        if (dr[7].ToString().Trim() != "")
                        { strBuilder.Append(" Checked='<%# Bind(" + Quote + dr[7].ToString().Trim() + Quote + ") %>'"); }
                        else
                        { strBuilder.Append(" Checked='<%# Bind(" + Quote + dr[0].ToString().Trim() + Quote + ") %>'"); }
                        strBuilder.Append(" Enabled=" + Quote + "false" + Quote + ">");
                        strBuilder.Append("</asp:CheckBox><br/>");

                    }
                    else
                    {
                        strBuilder.Append("<asp:Label ");
                        if (dr[7].ToString().Trim() != "")
                        { strBuilder.Append(" ID= " + Quote + "lbl_" + dr[7].ToString().Trim() + Quote); }
                        else
                        { strBuilder.Append(" ID= " + Quote + "lbl_" + dr[0].ToString().Trim() + Quote); }
                        strBuilder.Append(" runat=" + Quote + "server" + Quote);
                        if (dr[7].ToString().Trim() != "")
                        { strBuilder.Append(" Text='<%# Eval(" + Quote + dr[7].ToString().Trim() + Quote + ") %>'"); }
                        else
                        { strBuilder.Append(" Text='<%# Eval(" + Quote + dr[0].ToString().Trim() + Quote + ") %>'"); }

                        strBuilder.Append(">");
                        strBuilder.Append("</asp:Label><br/>");
                    }
                    strBuilder.AppendLine(" ");

                    strBuilder.Append("</td>");
                    strBuilder.AppendLine(" ");
                    strBuilder.Append("</tr>");
                    strBuilder.AppendLine(" ");
                }
            }
            strBuilder.Append("<tr>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<td colspan="+ Quote + "2"+ Quote +">");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<asp:LinkButton ID=" + Quote + "EditButton" + Quote + " runat=" + Quote + "server" + Quote + " CausesValidation=" + Quote + "False" + Quote + " CommandName=" + Quote + "Edit" + Quote + " Text=" + Quote + "Editar" + Quote + "></asp:LinkButton>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<asp:LinkButton ID=" + Quote + "DeleteButton" + Quote + " runat=" + Quote + "server" + Quote + " CausesValidation=" + Quote + "False" + Quote + " CommandName=" + Quote + "Delete" + Quote + " Text=" + Quote + "Eliminar" + Quote + "></asp:LinkButton>");
            strBuilder.AppendLine(" ");
            //strBuilder.Append("<asp:LinkButton ID=" + Quote + "NewButton" + Quote + " runat=" + Quote + "server" + Quote + " CausesValidation=" + Quote + "False" + Quote + " CommandName=" + Quote + "New" + Quote + " Text=" + Quote + "Nuevo" + Quote + "></asp:LinkButton>");
            //strBuilder.AppendLine(" ");
            strBuilder.Append("</td>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("</tr>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("</table>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("</ItemTemplate>");
            strBuilder.AppendLine(" ");
            
            //seccion para agregar el EditItemTemplate
            strBuilder.AppendLine(" ");
            strBuilder.Append("<EditItemTemplate>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<table>");
            strBuilder.AppendLine(" ");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {               
                if(Convert.ToBoolean((dr[8]==null ?false:dr[8])) == true)
                {
                    strBuilder.Append("<tr>");
                    strBuilder.AppendLine(" ");
                    strBuilder.Append("<td>");
                    strBuilder.AppendLine(" ");
                    if (dr[7].ToString().Trim() != "")
                    { strBuilder.Append("" + dr[7].ToString() + ":"); }
                    else
                    { strBuilder.Append("" + dr[0].ToString() + ":"); }
                    strBuilder.Append("</td>");
                    strBuilder.AppendLine(" ");
                    strBuilder.Append("<td>");
                    strBuilder.AppendLine(" ");
                    
                    //agregar el codigo para el combobox en caso sea foreign key
                    if (dr[4].ToString().Trim() == "Y")
                    {
                        //<asp:DropDownList ID="DropDownList2" runat="server" SelectedValue='<%# Bind("POS_Codigo") %>'>
                        //</asp:DropDownList>
                        strBuilder.Append("<asp:DropDownList ");
                        if (dr[7].ToString().Trim() != "")
                        { strBuilder.Append(" ID= " + Quote + "ddl_" + dr[7].ToString().Trim() + Quote); }
                        else
                        { strBuilder.Append(" ID= " + Quote + "ddl_" + dr[0].ToString().Trim() + Quote); }

                        strBuilder.Append(" runat=" + Quote + "server" + Quote);
                        if (dr[7].ToString().Trim() != "")
                        { strBuilder.Append(" SelectedValue='<%# Bind(" + Quote + dr[7].ToString().Trim() + Quote + ") %>'"); }
                        else
                        { strBuilder.Append(" SelectedValue='<%# Bind(" + Quote + dr[0].ToString().Trim() + Quote + ") %>'"); }
                        
                        strBuilder.Append(">");
                        strBuilder.Append("</asp:DropDownList><br/>");
                    }
                    else
                    {
                        //aqui genera el textbox
                        if (dr[1].ToString().Trim() == "bit")
                        {
                            strBuilder.Append("<asp:CheckBox ");
                            if (dr[7].ToString().Trim() != "")
                            { strBuilder.Append(" ID= " + Quote + "chk_" + dr[7].ToString().Trim() + Quote); }
                            else
                            { strBuilder.Append(" ID= " + Quote + "chk_" + dr[0].ToString().Trim() + Quote); }

                            strBuilder.Append(" runat=" + Quote + "server" + Quote);
                            if (dr[7].ToString().Trim() != "")
                            { strBuilder.Append(" Checked='<%# Bind(" + Quote + dr[7].ToString().Trim() + Quote + ") %>'"); }
                            else
                            { strBuilder.Append(" Checked='<%# Bind(" + Quote + dr[0].ToString().Trim() + Quote + ") %>'"); }
                            strBuilder.Append(">");
                            strBuilder.Append("</asp:CheckBox><br/>");

                        }
                        else
                        {
                            strBuilder.Append("<asp:TextBox ");
                            if (dr[7].ToString().Trim() != "")
                            { strBuilder.Append(" ID= " + Quote + "txt_" + dr[7].ToString().Trim() + Quote); }
                            else
                            { strBuilder.Append(" ID= " + Quote + "txt_" + dr[0].ToString().Trim() + Quote); }

                            strBuilder.Append(" runat=" + Quote + "server" + Quote);
                            if (dr[7].ToString().Trim() != "")
                            { strBuilder.Append(" Text='<%# Bind(" + Quote + dr[7].ToString().Trim() + Quote + ") %>'"); }
                            else
                            { strBuilder.Append(" Text='<%# Bind(" + Quote + dr[0].ToString().Trim() + Quote + ") %>'"); }
                            strBuilder.Append(">");
                            strBuilder.Append("</asp:TextBox><br/>");

                            //if(Nullable.TrimEnd(null)=="No")

                            if (dr[2].ToString().TrimEnd(null) == "No")
                            {
                                //ControlStyleCount = ControlStyleCount + 1;
                                //ControlTop = ControlTop + 35;
                                if (dr[7].ToString().Trim() != "")
                                {
                                    rvName = "rv_" + dr[7].ToString();
                                    Validator newvalidator = new Validator(rvName, dr[7].ToString(), "txt_" + dr[7].ToString(), 0, 0, 99, 0);
                                    strBuilder.Append(newvalidator.ToString());
                                }
                                else
                                {
                                    rvName = "rv_" + dr[0].ToString();
                                    Validator newvalidator = new Validator(rvName, dr[0].ToString(), "txt_" + dr[0].ToString(), 0, 0, 99, 0);
                                    strBuilder.Append(newvalidator.ToString());
                                }


                            }
                        }
                    }
                    strBuilder.Append("</td>");
                    strBuilder.AppendLine(" ");
                    strBuilder.Append("</tr>");
                    strBuilder.AppendLine(" ");
                }
            }
            strBuilder.Append("<tr>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<td colspan=" + Quote + "2" + Quote + ">");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<asp:LinkButton ID=" + Quote + "UpdateButton" + Quote + " runat=" + Quote + "server" + Quote + " CausesValidation=" + Quote + "True" + Quote + " CommandName=" + Quote + "Update" + Quote + " Text=" + Quote + "Actualizar" + Quote + "></asp:LinkButton>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<asp:LinkButton ID=" + Quote + "UpdateCancelButton" + Quote + " runat=" + Quote + "server" + Quote + " CausesValidation=" + Quote + "False" + Quote + " CommandName=" + Quote + "Cancel" + Quote + " Text=" + Quote + "Cancelar" + Quote + "></asp:LinkButton>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("</td>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("</tr>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("</table>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("</EditItemTemplate>");
            strBuilder.AppendLine(" ");
                        
            //seccion para agregar el InsertItemTemplate
            strBuilder.AppendLine(" ");
            strBuilder.Append("<InsertItemTemplate>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<table>");
            strBuilder.AppendLine(" ");
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (Convert.ToBoolean((dr[8] == null ? false : dr[8])) == true)
                {
                    strBuilder.Append("<tr>");
                    strBuilder.AppendLine(" ");
                    strBuilder.Append("<td>");
                    strBuilder.AppendLine(" ");
                    if (dr[7].ToString() != "")
                    { strBuilder.Append("" + dr[7].ToString() + ":"); }
                    else
                    { strBuilder.Append("" + dr[0].ToString() + ":"); }

                    strBuilder.Append("</td>");
                    strBuilder.AppendLine(" ");
                    strBuilder.Append("<td>");
                    strBuilder.AppendLine(" ");

                    //agregar el codigo para el combobox en caso sea foreign key
                    if (dr[4].ToString().Trim() == "Y")
                    {
                        //<asp:DropDownList ID="DropDownList2" runat="server" SelectedValue='<%# Bind("POS_Codigo") %>'>
                        //</asp:DropDownList>
                        strBuilder.Append("<asp:DropDownList ");
                        if (dr[7].ToString().Trim() != "")
                        { strBuilder.Append(" ID= " + Quote + "ddl_" + dr[7].ToString().Trim() + Quote); }
                        else
                        { strBuilder.Append(" ID= " + Quote + "ddl_" + dr[0].ToString().Trim() + Quote); }

                        strBuilder.Append(" runat=" + Quote + "server" + Quote);
                        if (dr[7].ToString().Trim() != "")
                        { strBuilder.Append(" SelectedValue='<%# Bind(" + Quote + dr[7].ToString().Trim() + Quote + ") %>'"); }
                        else
                        { strBuilder.Append(" SelectedValue='<%# Bind(" + Quote + dr[0].ToString().Trim() + Quote + ") %>'"); }

                        strBuilder.Append(">");
                        strBuilder.Append("</asp:DropDownList><br/>");
                    }
                    else
                    {
                        if (dr[1].ToString().Trim() == "bit")
                        {
                            strBuilder.Append("<asp:CheckBox ");
                            if (dr[7].ToString().Trim() != "")
                            { strBuilder.Append(" ID= " + Quote + "chk_" + dr[7].ToString().Trim() + Quote); }
                            else
                            { strBuilder.Append(" ID= " + Quote + "chk_" + dr[0].ToString().Trim() + Quote); }

                            strBuilder.Append(" runat=" + Quote + "server" + Quote);
                            if (dr[7].ToString().Trim() != "")
                            { strBuilder.Append(" Checked='<%# Bind(" + Quote + dr[7].ToString().Trim() + Quote + ") %>'"); }
                            else
                            { strBuilder.Append(" Checked='<%# Bind(" + Quote + dr[0].ToString().Trim() + Quote + ") %>'"); }
                            strBuilder.Append(">");
                            strBuilder.Append("</asp:CheckBox><br/>");
                        }
                        else
                        {
                            strBuilder.Append("<asp:TextBox ");
                            if (dr[7].ToString() != "")
                            { strBuilder.Append(" ID= " + Quote + "txt_" + dr[7].ToString().Trim() + Quote); }
                            else
                            { strBuilder.Append(" ID= " + Quote + "txt_" + dr[0].ToString().Trim() + Quote); }

                            strBuilder.Append(" runat=" + Quote + "server" + Quote);
                            if (dr[7].ToString() != "")
                            { strBuilder.Append(" Text='<%# Bind(" + Quote + dr[7].ToString().Trim() + Quote + ") %>'"); }
                            else
                            { strBuilder.Append(" Text='<%# Bind(" + Quote + dr[0].ToString().Trim() + Quote + ") %>'"); }

                            strBuilder.Append(">");
                            strBuilder.Append("</asp:TextBox><br/>");


                            if (dr[2].ToString().TrimEnd(null) == "No")
                            {
                                //ControlStyleCount = ControlStyleCount + 1;
                                //ControlTop = ControlTop + 35;
                                if (dr[7].ToString() != "")
                                {
                                    rvName = "rv_" + dr[7].ToString();
                                    Validator newvalidator = new Validator(rvName, dr[7].ToString(), "txt_" + dr[7].ToString(), 0, 0, 99, 0);
                                    strBuilder.Append(newvalidator.ToString());
                                }
                                else
                                {
                                    rvName = "rv_" + dr[0].ToString();
                                    Validator newvalidator = new Validator(rvName, dr[0].ToString(), "txt_" + dr[0].ToString(), 0, 0, 99, 0);
                                    strBuilder.Append(newvalidator.ToString());
                                }


                            }
                        }
                    }
                    strBuilder.Append("</td>");
                    strBuilder.AppendLine(" ");
                    strBuilder.Append("</tr>");
                    strBuilder.AppendLine(" ");
                    strBuilder.AppendLine(" ");
                }
            }
            strBuilder.Append("<tr>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<td colspan=" + Quote + "2" + Quote + ">");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<asp:LinkButton ID=" + Quote + "InsertButton" + Quote + " runat=" + Quote + "server" + Quote + " CausesValidation=" + Quote + "True" + Quote + " CommandName=" + Quote + "Insert" + Quote + " Text=" + Quote + "Insertar" + Quote + "></asp:LinkButton>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<asp:LinkButton ID=" + Quote + "InsertCancelButton" + Quote + " runat=" + Quote + "server" + Quote + " CausesValidation=" + Quote + "False" + Quote + " CommandName=" + Quote + "Cancel" + Quote + " Text=" + Quote + "Cancelar" + Quote + "></asp:LinkButton>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("</td>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("</tr>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("</table>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("</InsertItemTemplate>");
            strBuilder.AppendLine(" ");
            
            strBuilder.Append(this.MAJORFOOTER);
            strBuilder.AppendLine(" ");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<asp:ObjectDataSource ID=" + Quote + "odsfv" + name + Quote + " runat=" + Quote + "server" + Quote + " SelectMethod=" + Quote + "Seleccionar" + Quote + " UpdateMethod=" + Quote + "Actualizar" + Quote + " DeleteMethod=" + Quote + "Eliminar" + Quote + " InsertMethod="+Quote +"Insertar"+Quote  + " TypeName=" + Quote + this.BOGen + Quote + " DataObjectTypeName=" + Quote + this.EntGen + Quote + " >");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<SelectParameters>");
            strBuilder.AppendLine(" ");
            //repetir para cada clave
            int cont = 0; string strTipo = "";
            foreach (string strClave in arrClaves)
            {   if (arrClavesTipos.ToArray().GetValue(cont).ToString() == "int")
                {  strTipo = "Int32";   }
                else
                {  strTipo = "String";  }

                strBuilder.Append("<asp:ControlParameter ControlID=" + Quote + "gv"+this.name  + Quote + " DefaultValue=" + Quote + "0" + Quote + " Name=" + Quote + strClave + Quote + " ");
                strBuilder.AppendLine(" ");
                strBuilder.Append(" PropertyName=" + Quote + "SelectedDataKey['"+strClave+"']" + Quote + " Type=" + Quote + strTipo + Quote + "/>");
                strBuilder.AppendLine(" ");
                cont++;
            }
            //<asp:ControlParameter ControlID="gvRECT_CERTIFICACION"  DefaultValue="0" Name="TCN_Codigo"
            //     PropertyName="SelectedDataKey['TCN_Codigo']" Type="Int32" />

            strBuilder.Append("</SelectParameters>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("</asp:ObjectDataSource>");
            //agregar boton para modificar el insert 
            
            strBuilder.AppendLine(" ");
            strBuilder.Append("<asp:Button ID=" + Quote + "btnInsert" + Quote + " runat=" + Quote + "server" + Quote + " OnClick=" + Quote + "btnInsert_Click" + Quote + " Text=" + Quote + "Nuevo" + Quote+"/>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<script runat="+ Quote  +"server"+ Quote +">");
            strBuilder.AppendLine(" ");
            strBuilder.Append("protected void btnInsert_Click(object sender, EventArgs e)");
            strBuilder.AppendLine(" ");
            strBuilder.Append("{");
            strBuilder.AppendLine(" ");
            strBuilder.Append(this.ID.ToString() + ".ChangeMode(FormViewMode.Insert);");
            strBuilder.AppendLine(" ");
            strBuilder.Append("}");
            strBuilder.AppendLine(" ");
            //aqui genera la actualizacion del gridview cuando actualice inserte o elimine
            //Eliminar
            strBuilder.Append("protected void " + this.ID + "_ItemDeleted(object sender, FormViewDeletedEventArgs e)");
            strBuilder.AppendLine(" ");
            strBuilder.Append("{");
            strBuilder.AppendLine(" ");
            strBuilder.Append("gv"+this.name +".DataBind();");
            strBuilder.AppendLine(" ");
            strBuilder.Append("}");
            strBuilder.AppendLine(" ");
            //actualizar
            strBuilder.Append("protected void " + this.ID + "_ItemInserted(object sender, FormViewInsertedEventArgs e)");
            strBuilder.AppendLine(" ");
            strBuilder.Append("{");
            strBuilder.AppendLine(" ");
            strBuilder.Append("gv" + this.name + ".DataBind();");
            strBuilder.AppendLine(" ");
            strBuilder.Append("}");
            strBuilder.AppendLine(" ");
            //Eliminar
            strBuilder.Append("protected void " + this.ID + "_ItemUpdated(object sender, FormViewUpdatedEventArgs e)");
            strBuilder.AppendLine(" ");
            strBuilder.Append("{");
            strBuilder.AppendLine(" ");
            strBuilder.Append("gv" + this.name + ".DataBind();");
            strBuilder.AppendLine(" ");
            strBuilder.Append("}");
            strBuilder.AppendLine(" ");

            strBuilder.Append("</script>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<br/>");
            
            strBuilder.AppendLine(" ");
                       
            return strBuilder.ToString();

        }


    }
}
