using ProtoBuf;

namespace StudentManagementgRPC.Contracts.Requests
{
    [ProtoContract]
    public class Empty
    {
        [ProtoMember(1)] public string Message { get; set; }
    }
}
