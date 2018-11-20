using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication4
{
    public class InOutType
    {
        public string Name { get; set; }
        public List<InOutClass> InOutClasses { get; set; }
        public InOutType()
        {
            InOutClasses = new List<InOutClass>();
        }

    }
}
