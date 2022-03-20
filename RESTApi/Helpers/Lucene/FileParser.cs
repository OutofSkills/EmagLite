using Lucene.Net.Documents;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmagLite.Client.Services.Lucene
{
    public class FileParser
    {
        public Document ParseDocument(string filePath)
        {
            Document document = new Document();
            StreamReader reader = File.OpenText(filePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(':');
                document.Add(new Field(items[0], items[1], TextField.TYPE_STORED));
            }
            reader.Close();
            return document;
        }
    }
}
