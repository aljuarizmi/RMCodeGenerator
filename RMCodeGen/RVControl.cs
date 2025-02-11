using System;
using DataTierGenerator;
namespace DataTierGenerator
{
	/// <summary>
	/// Summary description for Label.
	/// </summary>
	public class Validator:Common
	{
		private System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
		private string strErrorHeader;
		private string ErrorMessage;
		private string strErrorFooter;
		private string strValidateHeader;
		private string Validate;
		private string strValidateFooter;
		
		public Validator()
		{
			
		}

		public Validator(string name,string errormessage,string ControlName,int left,int top,int tabindex,int StyleCount)
		{
			this.MAJORHEADER="<asp:";
			this.CONTROLHEADER="requiredfieldvalidator";
			this.CONTROLFOOTER=">";
			this.MAJORFOOTER="</asp:requiredfieldvalidator>";

			this.strErrorHeader =" ErrorMessage=" + Quote;
			this.ErrorMessage= " Por Favor Ingrese " + errormessage;
			this.strErrorFooter= Quote + " ";
			this.strValidateHeader =" ControlToValidate=" +Quote;
			this.Validate=ControlName;
			this.strValidateFooter =Quote + " ";
			this.STYLECOUNT=StyleCount;
			this.STYLE = " style=" +Quote + "Z-INDEX:" + this.STYLECOUNT.ToString()+ ";" ;
			this.ID=name;
			this.LEFT=left;
			this.TOP=top;
			this.TABINDEX=tabindex;
			
		}
		public override string  ToString()
		{
			strBuilder.Append(this.MAJORHEADER);
			strBuilder.Append(this.CONTROLHEADER);
			strBuilder.Append(this.IDHEADER);
			strBuilder.Append(this.ID.ToString());
			strBuilder.Append(this.IDFOOTER);
            //strBuilder.Append(this.STYLE);
            //strBuilder.Append(this.STRLEFTHEADER);
            //strBuilder.Append(this.LEFT.ToString());
            //strBuilder.Append(this.STRLEFTFOOTER);
            //strBuilder.Append(this.POSITION);
            //strBuilder.Append(this.STRTOPHEADER);
            //strBuilder.Append(this.TOP.ToString());
            //strBuilder.Append(this.STRTOPFOOTER);
            //strBuilder.Append(this.Quote);
			strBuilder.Append(" ");
			strBuilder.Append(this.STRTABINDEXHEADER);
			strBuilder.Append(this.TABINDEX.ToString().TrimStart(null));
			strBuilder.Append(this.STRTABINDEXFOOTER);
			strBuilder.Append(this.RUNATSERVER);
			strBuilder.Append(this.strErrorHeader);
			strBuilder.Append(this.ErrorMessage);
			strBuilder.Append(this.strErrorFooter);
			strBuilder.Append(this.strValidateHeader);
			strBuilder.Append(this.Validate);
			strBuilder.Append(this.strValidateFooter);
			strBuilder.Append(this.CONTROLFOOTER);

			strBuilder.Append(this.MAJORFOOTER);
			

			return strBuilder.ToString();
			
		}
		
	}
}
