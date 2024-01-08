using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGroup5
{
    public class Developer
    {

        public string Name { get; set; }
        public string Number { get; set; }

        public Developer(string name, string number)
        {
            Name = name;
            Number = number;
        }
    }
}
