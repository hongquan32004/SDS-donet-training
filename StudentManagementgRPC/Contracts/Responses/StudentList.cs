using ProtoBuf;

namespace StudentManagementgRPC.Contracts.Responses
{
    [ProtoContract]
    public class StudentList
    {
        [ProtoMember(1)] public List<Student> Students { get; set; } = new();
    }
}
