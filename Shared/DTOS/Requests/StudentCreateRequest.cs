using ProtoBuf.Grpc;
using System.Runtime.Serialization;

namespace DTOS.Requests
{
    [DataContract]
    public class Teacher
    {
        [DataMember(Order = 1)] public string Id { get; set; }
        [DataMember(Order = 2)] public string Name { get; set; }
        [DataMember(Order = 3)] public DateTime DateOfBirth { get; set; }
    }
    [DataContract]
    public class ClassRoom
    {
        [DataMember(Order = 1)] public string Id { get; set; }
        [DataMember(Order = 2)] public string Name { get; set; }
        [DataMember(Order = 3)] public string Subject { get; set; }
        [DataMember(Order = 4)] public Teacher Teacher { get; set; }
    }

    [DataContract]
    public class StudentCreateRequest
    {
        [DataMember(Order = 1)] public string Name { get; set; }
        [DataMember(Order = 2)] public DateTime DateOfBirth { get; set; }
        [DataMember(Order = 3)] public string Address { get; set; }
        [DataMember(Order = 4)] public ClassRoom Classroom { get; set; }
    }
}
