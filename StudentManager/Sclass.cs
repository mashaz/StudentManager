using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager
{
    [Serializable]
    public class Sclass
    {
        public string className;
        public string collageName;
        public Sclass() { }
        public Sclass(string className, string collageName)
        {
            this.className = className;
            this.collageName = collageName;
        }
        public Sclass(String collageName)
        {
            this.collageName = collageName;
        }
    }
}
