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
	public abstract class XmlModel<T>
	{


        public void SaveToFile(string FileName)
        {
            using (FileStream stream = new FileStream(FileName, FileMode.Create))
            {
                SaveToStream(stream);
            }
        }
        public void SaveToStream(Stream Stream)
        {
            XmlSerializer serializer;

            serializer = new XmlSerializer(GetType());
            serializer.Serialize(Stream, this);
        }
        public static T LoadFromFile(string FileName)
        {
            using (FileStream stream = new FileStream(FileName, FileMode.Open))
            {
                return LoadFromStream(stream);
            }
        }
        public static T LoadFromStream(Stream Stream)
        {
            XmlSerializer serializer;

            serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(Stream);
        }
    }
}
