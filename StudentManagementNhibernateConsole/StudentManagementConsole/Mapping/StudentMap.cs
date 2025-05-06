using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementConsole.Mapping
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap() {
            Table("Students");
            Id(x => x.id).Column("studentID");
            Map(x => x.name).Column("name");
            Map(x => x.address).Column("address");
            Map(x => x.dateOfBirth).Column("dob");
            References(x => x.ClassRoom).Column("classId").LazyLoad();
        }
    }
}
