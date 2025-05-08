using ProtoBuf.Grpc;
using System.Runtime.Serialization;
namespace DTOS.Requests
{
    [DataContract]
    public class StudentByTeacherRequest
    {
        [DataMember(Order = 1)] public string TeacherName { get; set; }
    }
}
