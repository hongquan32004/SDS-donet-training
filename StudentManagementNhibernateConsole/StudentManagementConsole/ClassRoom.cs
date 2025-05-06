using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementConsole
{
    public class ClassRoom
    {
        public virtual string id { get; set; }
        public virtual string? name { get; set; }
        public virtual string? subject { get; set; }
        public virtual Teacher teacher { get; set; }
    }
}
