using System;

namespace DataTierGenerator
{
	/// <summary>
	/// Summary description for Common.
	/// </summary>
	public abstract class Common
	{
		public string Quote ="\"";
		private string MajorHeader;
		private string MajorFooter;
		private string ControlHeader;
		private string idHeader;
		private string id;
		private string idFooter;
		private string style;
		private string strLeftHeader;
		private int    Left;
		private string strLeftFooter;
		private string Position;
		private string strTopHeader;
		private int    Top;
		private string strTopFooter;
		private string strTabIndexHeader;
		private int    TabIndex;
		private string strTabIndexFooter;
		private string runatServer;
		private string ControlFooter;
		private int    StyleCount;

		public Common()
		{
			this.IDHEADER=" id=\"";
			this.IDFOOTER=Quote;
			this.STRLEFTHEADER=" LEFT: ";
			this.STRLEFTFOOTER="px; ";
			this.POSITION=" POSITION: absolute; ";
			this.STRTOPHEADER= " TOP: ";
			this.STRTOPFOOTER="px " +  "  ";
			this.STRTABINDEXHEADER = " tabindex=" + Quote + "  ";
			this.STRTABINDEXFOOTER=Quote;
			this.RUNATSERVER="  runat=" + Quote +"server" +Quote + "   ";
			
		}
		public string MAJORHEADER
		{
			get{return MajorHeader;}
			set{MajorHeader=value;}
		}
		public string MAJORFOOTER
		{
			get{return MajorFooter;}
			set{MajorFooter=value;}
		}
		public string ID {get{return id ;}set{id =value;}}
		public string STYLE{get{return style;}set{style=value;}}
		public string STRLEFTHEADER{get{return strLeftHeader;}set{strLeftHeader=value;}}
		public int LEFT{get{return Left;}set{Left=value;}}
		public string STRLEFTFOOTER{get{return strLeftFooter;}set{strLeftFooter=value;}}
		public string POSITION{get{return Position;}set{Position=value;}}
		public string STRTOPHEADER{get{return strTopHeader;}set{strTopHeader=value;}}
		public int TOP{get{return Top;}set{Top=value;}}
		public string STRTOPFOOTER{get{return strTopFooter;}set{strTopFooter=value;}}
		public string STRTABINDEXHEADER{get{return strTabIndexHeader;}set{strTabIndexHeader=value;}}
		public int TABINDEX{get{return TabIndex;}set{TabIndex=value;}}
		public string STRTABINDEXFOOTER{get{return strTabIndexFooter;}set{strTabIndexFooter=value;}}
		public string RUNATSERVER{get{return runatServer;}set{runatServer=value;}}
		public string CONTROLHEADER{get{return ControlHeader;}set{ControlHeader=value;}}
		public string CONTROLFOOTER{get{return ControlFooter;}set{ControlFooter=value;}}
		public string IDHEADER{get{return idHeader;}set{idHeader=value;}}
		public string IDFOOTER{get{return idFooter;}set{idFooter=value;}}
		public int    STYLECOUNT{get{return StyleCount;}set{StyleCount=value;}}
	
}
}