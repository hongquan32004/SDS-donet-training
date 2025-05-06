using Azure;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using StudentManagementgRPC.Models;
using System.Threading.Tasks;
using ProtoBuf.Grpc;
using NHibernate;
using StudentManagementgRPC.NhibernerHelper;
using NHibernate.Linq;
using StudentManagementgRPC.Services.Interfaces;
using StudentManagementgRPC.Contracts.Responses;
using StudentManagementgRPC.Contracts.Requests;
using Microsoft.IdentityModel.Tokens;
using StudentManagementgRPCService.Contracts.Requests;
using StudentManagementgRPCService.Contracts.Responses;
using OfficeOpenXml;

namespace StudentManagementgRPC.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly ILogger<StudentService> _logger;
        public StudentService(ILogger<StudentService> logger)
        {
            _logger = logger;
        }
        public async Task<StudentList> GetAllStudents(Empty request, ProtoBuf.Grpc.CallContext context = default)
        {
            try
            {
                using (var session = NhibernateHelper.OpenSession())
                {
                    var students = session.Query<Students>().Fetch(s => s.classRoom).ToList();
                    var response = new StudentList();
                    response.Students.AddRange(students.Select(s => new Student
                    {
                        Id = s.id,
                        Name = s.name,
                        DateOfBirth = s.dateOfBirth,
                        Address = s.address,
                        Classroom = new ClassRoom { Id = s.classRoom?.id, Name = s.classRoom.name, 
                            Subject = s.classRoom.subject, 
                            Teacher = new Contracts.Requests.Teacher { Id = s.classRoom.teacher.id, Name = s.classRoom.teacher.name, DateOfBirth = s.classRoom.teacher.dateOfBirth} },
                    }));
                    return await Task.FromResult(response);
                }
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error fetching students: {ex.Message}"));
            }
        }
        public async Task<Student> CreateStudent(StudentCreateRequest request, ProtoBuf.Grpc.CallContext context = default)
        {
            try
            {
                using (var session = NhibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var student = new Students
                        {
                            name = request.Name,
                            address = request.Address,
                            dateOfBirth = request.DateOfBirth,
                            classRoom = request.Classroom?.Id != null
    ? session.Get<ClassRooms>(request.Classroom.Id)
    : null
                        };
                        var response = new Student
                        {
                            Id = student.id,
                            Name = student.name,
                            DateOfBirth = student.dateOfBirth,
                            Address = student.address,
                            Classroom = new ClassRoom { Id = student.classRoom?.id },
                        };
                        await session.SaveAsync(student);
                        transaction.Commit();
                        return await Task.FromResult(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateStudent: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, $"Failed to create student: {ex.Message}"));
            }
        }

        public async Task<Empty> DeleteStudent(StudentRequest request, ProtoBuf.Grpc.CallContext context = default)
        {
            try
            {
                using (var session = NhibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var student = await session.GetAsync<Students>(request.Id);
                        if (student == null)
                        {
                            throw new RpcException(new Status(StatusCode.Internal, "Request not found"));
                        }
                        await session.DeleteAsync(student);
                        await transaction.CommitAsync();
                        return await Task.FromResult(new Empty { });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateStudent: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, $"Failed to create student: {ex.Message}"));
            }
        }
        public async Task<Student> GetStudentById(StudentRequest request, ProtoBuf.Grpc.CallContext context = default)
        {
            try
            {
                using (var session = NhibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var student = await session.GetAsync<Students>(request.Id);
                        if (student == null)
                        {
                            throw new RpcException(new Status(StatusCode.Internal, "Request not found"));
                        }
                        var response = new Student()
                        {
                            Id = student.id,
                            Name = student.name,
                            Address = student.address,
                            DateOfBirth = student.dateOfBirth,
                            Classroom = new ClassRoom { Id = student.classRoom?.id, Name = student.classRoom?.name, Subject = student.classRoom?.subject, Teacher = new Contracts.Requests.Teacher {Id = student.classRoom.teacher.id, Name = student.classRoom.teacher.name } }
                        };
                        return await Task.FromResult(response);
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateStudent: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, $"Failed to create student: {ex.Message}"));
            }
        }
        public async Task<Student> UpdateStudent(StudentUpdateRequest request, ProtoBuf.Grpc.CallContext context = default)
        {
            try
            {
                using (var session = NhibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var student = await session.GetAsync<Students>(request.Id);
                        if (student == null)
                        {
                            throw new RpcException(new Status(StatusCode.Internal, "Request not found"));
                        }
                        else
                        {
                            student.id = request.Id;
                            student.name = request.Name;
                            student.address = request.Address;
                            student.dateOfBirth = request.DateOfBirth;
                        }
                        var response = new Student()
                        {
                            Id = student.id,
                            Name = student.name,
                            Address = student.address,
                            DateOfBirth = student.dateOfBirth
                        };
                        await session.UpdateAsync(student);
                        await transaction.CommitAsync();
                        return await Task.FromResult(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateStudent: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, $"Failed to create student: {ex.Message}"));
            }
        }
        public async Task<StudentList> SortStudentByName(Empty request, ProtoBuf.Grpc.CallContext context = default)
        {
            try
            {
                using (var session = NhibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        string GetLastName(string fullName)
                        {
                            if (string.IsNullOrWhiteSpace(fullName)) return "";
                            var parts = fullName.Trim().Split(' ');
                            return parts[parts.Length - 1];
                        }
                        var students = session.Query<Students>().ToList().OrderBy(s => GetLastName(s.name)).ToList();
                        var respone = new StudentList();
                        respone.Students.AddRange(students.Select(s => new Student
                        {
                            Id = s.id,
                            Name = s.name,
                            Address = s.address,
                            DateOfBirth = s.dateOfBirth,
                            Classroom = new ClassRoom { Id = s.classRoom?.id }
                        }));
                        return await Task.FromResult(respone);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateStudent: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, $"Failed to create student: {ex.Message}"));
            }
        }
        public async Task<StudentList> GetStudentByTeacher(StudentByTeacherRequest request, ProtoBuf.Grpc.CallContext context = default)
        {
            try
            {
                using (var session = NhibernateHelper.OpenSession())
                {
                    var students = session.Query<Students>()
                        .Where(s => s.classRoom != null && s.classRoom.teacher != null && s.classRoom.teacher.name == request.TeacherName)
                        .Fetch(s => s.classRoom)
                        .ToList();
                    var respone = new StudentList();
                    respone.Students.AddRange(students.Select(s => new Student
                    {
                        Id = s.id,
                        Name = s.name,
                        DateOfBirth= s.dateOfBirth,
                        Address = s.address,
                        Classroom = new ClassRoom
                        {
                           Id=s.classRoom.id,
                           Name = s.classRoom.name,
                           Subject = s.classRoom.subject
                        }
                    }));
                    return await Task.FromResult(respone);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateStudent: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, $"Failed to create student: {ex.Message}"));
            }
        }
        public async Task<ExportExcel> ExportFileExcel(Empty request, ProtoBuf.Grpc.CallContext context = default)
        {
            try
            {
                using (var session = NhibernateHelper.OpenSession())
                {
                    var student = session.Query<Students>()
                        .Fetch(s => s.classRoom).ToList();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using var package = new ExcelPackage();
                    var sheet = package.Workbook.Worksheets.Add("Students");
					sheet.Cells[1, 1].Value = "ID";
					sheet.Cells[1, 2].Value = "Name";
					sheet.Cells[1, 3].Value = "Date of Birth";
					sheet.Cells[1, 4].Value = "Address";
					sheet.Cells[1, 5].Value = "Class";
					sheet.Cells[1, 6].Value = "Teacher";
                    int row = 2;
                    foreach (var s in student) {
						sheet.Cells[row, 1].Value = s.id;
						sheet.Cells[row, 2].Value = s.name;
						sheet.Cells[row, 3].Value = s.dateOfBirth.ToString("dd/MM/yyyy");
						sheet.Cells[row, 4].Value = s.address;
						sheet.Cells[row, 5].Value = s.classRoom?.name;
						sheet.Cells[row, 6].Value = s.classRoom?.teacher?.name;
						row++;
					}
                    var stream = new MemoryStream();
                    package.SaveAs(stream);
                    var bytes = stream.ToArray();

                    return new ExportExcel
                    {
                        fileBytes = bytes,
                        fileName = $"students_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
                    };
				}
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateStudent: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, $"Failed to create student: {ex.Message}"));
            }
        }
    }
}
