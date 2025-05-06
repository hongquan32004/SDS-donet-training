using ProtoBuf;

namespace StudentManagementgRPC.Contracts.Requests
{
    [ProtoContract]
    public class StudentUpdateRequest
    {
        [ProtoMember(1)] public int Id { get; set; }
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public DateTime DateOfBirth { get; set; }
        [ProtoMember(4)] public string Address { get; set; }
    }
}
