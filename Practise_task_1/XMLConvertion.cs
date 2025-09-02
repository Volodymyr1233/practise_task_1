using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Practise_task_1
{
    internal class XMLConvertion
    {
        internal static void serialize_file(List<COper> serialize_obj, string path)
        {
            var serializer = new XmlSerializer(typeof(List<COper>));

            using (var writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, serialize_obj);
            }
        }

        internal static List<COper> deserialize_file()
        {
            string xmlContent = DataBaseSQL.getXMLFile();
            if (xmlContent == "")
            {
                return new List<COper>();
            }
            var deserializer = new XmlSerializer(typeof(List<COper>));
            List<COper> obj_results;
            using (var reader = new StringReader(xmlContent))
            {
                obj_results = (List<COper>)deserializer.Deserialize(reader);

            }


            return obj_results;
        }
    }
}
