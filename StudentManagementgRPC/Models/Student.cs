namespace StudentManagementgRPC.Models
{
    public class Students
    {
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual DateTime dateOfBirth { get; set; }
        public virtual string? address { get; set; }
        public virtual ClassRooms? classRoom { get; set; }
        public Students() { }
    }
}
