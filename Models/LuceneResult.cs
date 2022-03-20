using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LuceneResult
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Brand { get; set; }
        public string Tip { get; set; }
        public string SistemDeOperare { get; set; }
        public string Processor { get; set; }
        public string NumarNuclee { get;set; }
        public string Tehnologie { get;set;}
    }
}
