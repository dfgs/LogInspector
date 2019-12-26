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
    public class RuleLibrary
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

        public RuleLibrary()
        {
            Items = new List<Rule>();
        }

        public void SaveToFile(string FileName)
        {
            using(FileStream stream=new FileStream(FileName,FileMode.Create))
            {
                SaveToStream(stream);
            }
        }
        public void SaveToStream(Stream Stream)
        {
            XmlSerializer serializer;

            serializer = new XmlSerializer(typeof(RuleLibrary));
            serializer.Serialize(Stream, this);
        }
        public static RuleLibrary LoadFromFile(string FileName)
        {
            using (FileStream stream = new FileStream(FileName, FileMode.Open))
            {
                return LoadFromStream(stream);
            }
        }
        public static RuleLibrary LoadFromStream(Stream Stream)
        {
            XmlSerializer serializer;

            serializer = new XmlSerializer(typeof(RuleLibrary));
            return (RuleLibrary)serializer.Deserialize(Stream);
        }


    }
}
