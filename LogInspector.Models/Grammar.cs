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
    public class Grammar:XmlModel<Grammar>
    {
        public List<Rule> Items
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

        public Grammar()
        {
            Items = new List<Rule>();
        }




    }
}
