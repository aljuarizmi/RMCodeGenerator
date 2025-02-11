using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataTierGenerator
{
    class GridViewGen : Common
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
        private string AutogenerateColumns;
        private string AllowPaging;
        private string name;
        private string BOGen;
        private string EntGen;
        private string AllowSorting;

        public GridViewGen()
        {
        }
        public GridViewGen(string name,string datakeynames,string cellpading,string datasourceid,DataSet ds,string BOGen ,string EntGen)
        {
            this.AllowSorting = " AllowSorting=" + Quote + "true" + Quote;
            this.BOGen = BOGen;
            this.EntGen = EntGen;
            this.name = name;
            this.strcssClass = " CssClass=" + Quote + "CoffeeFieldCaptionTD" + Quote;
            this.strHeightHeader = " HEIGHT=" + Quote;
            this.strHeightFooter = "px" + "  " + Quote;
            this.strWidthHeader = " WIDTH=" + Quote;
            this.strWidthFooter = "px" + "  " + Quote;
            this.MAJORHEADER = "<asp:";
            this.CONTROLHEADER = "GridView";
            this.CONTROLFOOTER = ">";           
            this.MAJORFOOTER = "</asp:GridView>";
            //this.STYLECOUNT = StyleCount;
            //this.STYLE = " style=" + Quote + "Z-INDEX:" + this.STYLECOUNT.ToString() + ";";
            this.ID = "gv" + name;
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
                        //obtengo los primary keys
                        if (dr[7].ToString().Trim() != "")
                        { datakeyGen.Append(dr[7].ToString()); }
                        else
                        { datakeyGen.Append(dr[0].ToString()); }
                        
                    }
                    
                }
                this.datakeynames = " DataKeyNames=" + Quote +datakeyGen.ToString().Trim()+Quote ; 
            
            }
            
            
            this.cellpading = cellpading;
            if (datasourceid != "")
            { this.datasourceid = datasourceid; }
            else
            {
                this.datasourceid =  " DataSourceID="+Quote+"odsgv"+this.name + Quote ;
            }
            this.ds = ds;
            this.AutogenerateColumns = " AutoGenerateColumns="+Quote +"false"+Quote ;
            this.AllowPaging = " AllowPaging=" + Quote + "True" + Quote;
        }
        public override string ToString()
        {
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
            strBuilder.Append(this.AutogenerateColumns );
            strBuilder.Append(this.AllowPaging );
            strBuilder.Append(this.AllowSorting );
            strBuilder.Append(this.datakeynames);
            strBuilder.Append(this.datasourceid );
            strBuilder.Append(this.CONTROLFOOTER);
            
            //seccion para agregar las columnas
            strBuilder.AppendLine(" ");
            strBuilder.Append("<Columns>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<asp:CommandField ShowSelectButton="+Quote +"True"+Quote +" />");
            strBuilder.AppendLine(" ");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (Convert.ToBoolean((dr[8]==null ?false:dr[8])) == true)
                {
                    if (dr[1].ToString().Trim() == "bit")
                    { strBuilder.Append("<asp:CheckBoxField "); }
                    else
                    { strBuilder.Append("<asp:BoundField "); }

                    if (dr[7].ToString().Trim() != "")
                    {
                        strBuilder.Append(" DataField=" + Quote + dr[7].ToString() + Quote);
                        strBuilder.Append(" HeaderText=" + Quote + dr[7].ToString() + Quote);
                        strBuilder.Append(" SortExpression=" + Quote + dr[7].ToString() + Quote);
                    }
                    else
                    {
                        strBuilder.Append(" DataField=" + Quote + dr[0].ToString() + Quote);
                        strBuilder.Append(" HeaderText=" + Quote + dr[0].ToString() + Quote);
                        strBuilder.Append(" SortExpression=" + Quote + dr[0].ToString() + Quote);
                    }
                    //strBuilder.Append(" InsertVisible=" + Quote + "True" + Quote);
                    strBuilder.Append(">");
                    if (dr[1].ToString().Trim() == "bit")
                    { strBuilder.Append("</asp:CheckBoxField>"); }
                    else
                    { strBuilder.Append("</asp:BoundField>"); }
                    strBuilder.AppendLine(" ");
                }
            }

            strBuilder.Append("</Columns>");
            strBuilder.AppendLine(" ");
            strBuilder.Append(this.MAJORFOOTER);

            strBuilder.AppendLine(" ");
            strBuilder.Append("<br/>");
            strBuilder.AppendLine(" ");
            strBuilder.Append("<asp:ObjectDataSource ID=" + Quote + "odsgv" + this.name + Quote + " runat=" + Quote + "server" + Quote + " SelectMethod=" + Quote + "Listar" + Quote + " TypeName=" + Quote + this.BOGen  + Quote + " DataObjectTypeName=" + Quote + this.EntGen + Quote + " ></asp:ObjectDataSource>");
            strBuilder.AppendLine(" ");

            return strBuilder.ToString();

        }


    }
}
