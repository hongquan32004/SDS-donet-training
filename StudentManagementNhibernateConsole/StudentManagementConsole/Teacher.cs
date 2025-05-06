using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementConsole
{
    public class Teacher
    {
        public virtual int id { get; set; }
        public virtual string? name { get; set; }
        public virtual DateTime dateOfBirth { get; set; }
    }
}
