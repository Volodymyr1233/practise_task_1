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
        internal static void serialize_file(List<COper> serialize_obj)
        {
            var serializer = new XmlSerializer(typeof(List<COper>));

            using (var writer = new StreamWriter(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "results.xml")))
            {
                serializer.Serialize(writer, serialize_obj);
            }
        }
    }
}
