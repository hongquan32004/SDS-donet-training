using FluentNHibernate.Mapping;
using StudentManagementgRPC.Models;

namespace StudentManagementgRPC.Mapping
{
    public class StudentMap : ClassMap<Students>
    {
        public StudentMap()
        {
            Table("Students");
            Id(x => x.id).Column("studentID");
            Map(x => x.name).Column("name");
            Map(x => x.address).Column("address");
            Map(x => x.dateOfBirth).Column("dob");
            References(x => x.classRoom).Column("classId").LazyLoad();
        }
    }
}
