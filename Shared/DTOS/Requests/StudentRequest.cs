using ProtoBuf.Grpc;
using System.Runtime.Serialization;
namespace DTOS.Requests
{
    [DataContract]
    public class StudentRequest
    {
        [DataMember(Order =1)] public int Id { get; set; }
    }
}
