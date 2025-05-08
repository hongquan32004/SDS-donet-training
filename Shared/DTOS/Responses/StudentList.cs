using ProtoBuf.Grpc;
using System.Runtime.Serialization;

namespace DTOS.Responses
{
    [DataContract]
    public class StudentList
    {
        [DataMember(Order = 1)] public List<Student> Students { get; set; } = new();
    }
}
