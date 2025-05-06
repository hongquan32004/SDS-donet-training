using FluentNHibernate.Mapping;
using StudentManagementgRPC.Models;

namespace StudentManagementgRPCService.Mapping
{
    public class TeacherMap : ClassMap<Teacher>
    {
        public TeacherMap() {
            Table("Teachers");
            Id(x => x.id).Column("teacherId");
            Map(x => x.name).Column("name");
            Map(x => x.dateOfBirth).Column("dob");
        }
    }
}
