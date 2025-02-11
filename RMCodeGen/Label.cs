using System;
using DataTierGenerator;
namespace DataTierGenerator
{
	/// <summary>
	/// Summary description for Label.
	/// </summary>
	public class ctlLabel:Common
	{
		private System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
		private string strHeightHeader;
		private int Height;
		private string strHeightFooter;
		private string strWidthHeader;
		private int Width;
		private string strWidthFooter;
		private string strcssClass;
        private string Text;
		public ctlLabel()
		{
			
		}

		public ctlLabel(string name,int left,int width,int top,int height,int tabindex,string text,int StyleCount)
		{
			this.strcssClass= " CssClass=" + Quote +"CoffeeFieldCaptionTD" +Quote;
			this.strHeightHeader=" HEIGHT="+Quote;
			this.strHeightFooter= "px"  + "  "  +Quote;
			this.strWidthHeader=" WIDTH=" + Quote;
			this.strWidthFooter="px" + "  " +Quote;
			this.MAJORHEADER="<asp:";
			this.CONTROLHEADER="Label";
			this.CONTROLFOOTER=">";
			this.MAJORFOOTER="</asp:Label>";
			this.STYLECOUNT =StyleCount;
			this.STYLE = " style=" +Quote + "Z-INDEX:" + this.STYLECOUNT.ToString()+ ";" ;

            this.ID=name;
			this.LEFT=left;
			this.Width =width;
			this.TOP=top;
			this.Height=height;
			this.TABINDEX=tabindex;
			this.Text=text;
		}
		public override string  ToString()
		{
			strBuilder.Append(this.MAJORHEADER);
			strBuilder.Append(this.CONTROLHEADER);
			strBuilder.Append(this.IDHEADER);
			strBuilder.Append(this.ID.ToString());
			strBuilder.Append(this.IDFOOTER);
            //strBuilder.Append(this.STYLE);
			strBuilder.Append(this.STRLEFTHEADER);
			strBuilder.Append(this.LEFT.ToString());
			strBuilder.Append(this.STRLEFTFOOTER);
			strBuilder.Append(this.POSITION);
			strBuilder.Append(this.STRTOPHEADER);
			strBuilder.Append(this.TOP.ToString());
			strBuilder.Append(this.STRTOPFOOTER);
			strBuilder.Append(Quote);
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
			strBuilder.Append(this.CONTROLFOOTER);

			strBuilder.Append(this.Text);
			strBuilder.Append(this.MAJORFOOTER);
			

			return strBuilder.ToString();
			
		}
		
	}
}
