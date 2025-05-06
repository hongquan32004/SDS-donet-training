using ProtoBuf;
namespace StudentManagementgRPC.Contracts.Requests
{
    [ProtoContract]
    public class StudentRequest
    {
        [ProtoMember(1)] public int Id { get; set; }
    }
}
