using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LogInspector.Models
{
	[Serializable]
	public class Style
	{
		[XmlAttribute]
		public string Class
		{
			get;
			set;
		}
		[XmlAttribute]
		public string Foreground
		{
			get;
			set;
		}
		[XmlAttribute]
		public bool Bold
		{
			get;
			set;
		}
		[XmlAttribute]
		public bool Italic
		{
			get;
			set;
		}
		[XmlAttribute]
		public bool Underline
		{
			get;
			set;
		}
		public Style()
		{

		}
	}
}
