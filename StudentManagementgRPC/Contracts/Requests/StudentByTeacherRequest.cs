using ProtoBuf;
namespace StudentManagementgRPCService.Contracts.Requests
{
    [ProtoContract]
    public class StudentByTeacherRequest
    {
        [ProtoMember(1)] public string TeacherName { get; set; }
    }
}
