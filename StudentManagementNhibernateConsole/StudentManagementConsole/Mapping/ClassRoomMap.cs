using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementConsole.Mapping
{
    public class ClassRoomMap : ClassMap<ClassRoom>
    {
        public ClassRoomMap() {
            Table("Classrooms");
            Id(x => x.id).Column("classId");
        }
    }
}
