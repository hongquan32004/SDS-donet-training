using ProtoBuf;

namespace StudentManagementgRPC.Contracts.Requests
{
    [ProtoContract]
    public class Teacher
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public DateTime DateOfBirth { get; set; }
    }
    [ProtoContract]
    public class ClassRoom
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public string Subject { get; set; }
        [ProtoMember(4)] public Teacher Teacher { get; set; }
    }

    [ProtoContract]
    public class StudentCreateRequest
    {
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public DateTime DateOfBirth { get; set; }
        [ProtoMember(4)] public string Address { get; set; }
        [ProtoMember(5)] public ClassRoom Classroom { get; set; }
    }
}
