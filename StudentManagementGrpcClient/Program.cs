using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using StudentManagementgRPC.Contracts.Requests;
using StudentManagementgRPC.Services.Interfaces;
namespace StudentManagementGrpcClient;
class Program
{
    static async Task Main(string[] args)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5232");

        var studentService = channel.CreateGrpcService<IStudentService>();

        var response = await studentService.GetAllStudents(new StudentManagementgRPC.Contracts.Requests.Empty());

        Console.WriteLine("Danh sách sinh viên:");
        foreach (var student in response.Students)
        {
            Console.WriteLine($"ID: {student.Id}, Tên: {student.Name}, Ngày sinh: {student.DateOfBirth}, Địa chỉ: {student.Address}, Lớp: {student.Classroom?.Id}");
        }
        Console.WriteLine("Sắp xếp sinh viên theo tên:");
        var sorted = await studentService.SortStudentByName(new StudentManagementgRPC.Contracts.Requests.Empty());
        foreach(var student in sorted.Students)
        {
            Console.WriteLine($"ID: {student.Id}, Tên: {student.Name}, Ngày sinh: {student.DateOfBirth}, Địa chỉ: {student.Address}, Lớp: {student.Classroom?.Id}");
        }
        Console.WriteLine("Thêm mới thành viên ....");
        var createStudent = new StudentCreateRequest
        {
            Name = "Nguyễn Văn A",
            DateOfBirth = new DateTime(2003, 5, 15),
            Address = "123 Đường ABC",
            Classroom = new ClassRoom
            {
                Id = "C001" // ID lớp học có thật trong DB
            }
        };
        await studentService.CreateStudent(createStudent);
    }
}
