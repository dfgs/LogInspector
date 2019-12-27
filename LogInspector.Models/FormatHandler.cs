using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LogInspector.Models
{
	[Serializable]
	public class FormatHandler:XmlModel<FormatHandler>
	{
		[XmlAttribute]
		public string Name
		{
			get;
			set;
		}
		[XmlAttribute]
		public string FileNamePattern
		{
			get;
			set;
		}
		[XmlAttribute]
		public string LineFeedClass
		{
			get;
			set;
		}
		[XmlAttribute]
		public string LogStartClass
		{
			get;
			set;
		}
		[XmlAttribute]
		public string LogStartValue
		{
			get;
			set;
		}
		public List<string> Grammars
		{
			get;
			set;
		}
		public List<string> StyleSheets
		{
			get;
			set;
		}

		public FormatHandler()
		{
			Grammars = new List<string>();
			StyleSheets = new List<string>();
		}

	}
}
