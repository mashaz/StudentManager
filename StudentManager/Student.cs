using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StudentManager
{
    [Serializable]
    public class Student
    {
        public Student()
        { }
        public Student(string name,string sid,string sclass,bool gender,string subs)
        {
            this.name = name;
            this.sid = sid;
            this.sclass = sclass;
            this.subs = subs;
            this.gender = gender;
        }
        public string name;
        public string sid;
        public string sclass;
        public bool gender;
        public string subs;
    }
}
