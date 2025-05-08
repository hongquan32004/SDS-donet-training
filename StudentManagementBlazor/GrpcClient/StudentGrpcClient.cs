using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Share.Interface;
using DTOS.Requests;
using DTOS.Responses;

namespace StudentManagementBlazor.GrpcClient
{
    public class StudentGrpcClient
    {
        private readonly IStudentService _client;
         public StudentGrpcClient(IStudentService client)
        {
            _client = client;
        }
        public async Task<StudentList> GetAllStudents()
        {
            return await _client.GetAllStudents(new Empty());                                                                                                       
        }
        public async Task<Student> CreateStudent(StudentCreateRequest request)                             
        {  
            return await _client.CreateStudent(request);          
        }
        public async Task<Empty> DeleteStudent(StudentRequest request)
        {
            return await _client.DeleteStudent(request);
        }
        public async Task<StudentList> SortStudentByName()
        {
            return await _client.SortStudentByName(new Empty());
        }
        public async Task<Student> UpdateStudent(StudentUpdateRequest request)
        {
            return await _client.UpdateStudent(request);
        }
        public async Task<Student> GetStudentById(StudentRequest request)
        {
            return await _client.GetStudentById(request);
        }
        public async Task<StudentList> GetStudentByTeacher(StudentByTeacherRequest request)
        {
            return await _client.GetStudentByTeacher(request);
        }
        public async Task<ExportExcel> ExportFileExcel()
        {
            return await _client.ExportFileExcel(new Empty());
        }
    }
}
