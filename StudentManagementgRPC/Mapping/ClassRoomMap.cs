using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using StudentManagementgRPC.Models;

namespace StudentManagementgRPC.Mapping
{
    public class ClassRoomMap: ClassMap<ClassRooms>
    {
        public ClassRoomMap() {
            Table("Classrooms");
            Id(x => x.id).Column("classId");
            Map(x => x.name).Column("className");
            Map(x => x.subject).Column("subject");
            References(x => x.teacher).Column("teacherId").LazyLoad();
        }
    }
}
