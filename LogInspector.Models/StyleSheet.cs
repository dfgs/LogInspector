using LexerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LogInspector.Models
{
    [Serializable]
    public class StyleSheet:XmlModel<StyleSheet>
    {
        public List<Style> Items
        {
            get;
            set;
        }
        [XmlAttribute]
        public string NameSpace
        {
            get;
            set;
        }

        public StyleSheet()
        {
            Items = new List<Style>();
        }




    }
}
