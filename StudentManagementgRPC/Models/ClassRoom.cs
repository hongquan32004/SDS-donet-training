namespace StudentManagementgRPC.Models
{
    public class ClassRooms
    {
        public virtual string? id { get; set; }
        public virtual string? name { get; set; }
        public virtual string? subject {  get; set; }
        public virtual Teacher? teacher { get; set; }
    }
}
