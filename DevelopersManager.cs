using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGroup5
{
    public static class DevelopersManager
    {
        public static List<Developer> GetDevelopers()
        {
            return new List<Developer>
            {
                new Developer("Amarjot Kaur", "1231234"),
                new Developer("Harmeen Kaur", "8885264"),
                new Developer("Jay Jasoliya", "8862212"),
                new Developer("Rutvi Mistry", "8889104")
            };
        }
    }
}
