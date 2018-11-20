using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication4
{
    public class InOutClass
    {
        public InOutClass()
        {
            Items = new List<string>();
        }
        public string Name { get; set; }
        public List<string> Items { get; set; }
    }
}
