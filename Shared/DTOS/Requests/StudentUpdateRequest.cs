using ProtoBuf.Grpc;
using System.Runtime.Serialization;

namespace DTOS.Requests
{
    [DataContract]
    public class StudentUpdateRequest
    {
        [DataMember(Order = 1)] public int Id { get; set; }
        [DataMember(Order = 2)] public string Name { get; set; }
        [DataMember(Order = 3)] public DateTime DateOfBirth { get; set; }
        [DataMember(Order = 4)] public string Address { get; set; }
    }
}
