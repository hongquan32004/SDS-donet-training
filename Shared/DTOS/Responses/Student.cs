using ProtoBuf.Grpc;
using DTOS.Requests;
using System.Runtime.Serialization;

namespace DTOS.Responses
{
    [DataContract]
    public class Student
    {
        [DataMember(Order = 1)] public int Id { get; set; }
        [DataMember(Order = 2)] public string Name { get; set; }
        [DataMember(Order = 3)] public DateTime DateOfBirth { get; set; }
        [DataMember(Order = 4)] public string Address { get; set; }
        [DataMember(Order = 5)] public ClassRoom Classroom { get; set; }
    }
}
