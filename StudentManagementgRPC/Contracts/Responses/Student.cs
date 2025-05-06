using ProtoBuf;
using StudentManagementgRPC.Contracts.Requests;

namespace StudentManagementgRPC.Contracts.Responses
{
    [ProtoContract]
    public class Student
    {
        [ProtoMember(1)] public int Id { get; set; }
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public DateTime DateOfBirth { get; set; }
        [ProtoMember(4)] public string Address { get; set; }
        [ProtoMember(5)] public ClassRoom Classroom { get; set; }
    }
}
