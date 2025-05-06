using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;
using StudentManagementgRPC.Contracts.Requests;
using StudentManagementgRPC.Contracts.Responses;
using StudentManagementgRPC.Models;
using StudentManagementgRPCService.Contracts.Requests;
using StudentManagementgRPCService.Contracts.Responses;
using System.ServiceModel;


namespace StudentManagementgRPC.Services.Interfaces
{
    [Service]
    public interface IStudentService
    {
        Task<StudentList> GetAllStudents(Empty request, CallContext context = default);
        Task<Student> CreateStudent(StudentCreateRequest request, CallContext context = default);
        Task<Empty> DeleteStudent(StudentRequest request, CallContext context = default);
        Task<Student> GetStudentById(StudentRequest request, CallContext context = default);
        Task<Student> UpdateStudent(StudentUpdateRequest request, CallContext context = default);
        Task<StudentList> SortStudentByName(Empty request, CallContext context = default);
        Task<StudentList> GetStudentByTeacher(StudentByTeacherRequest request, CallContext context = default);
        Task<ExportExcel> ExportFileExcel(Empty request, CallContext context = default);
    }
}
