using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementConsole
{
    public class StudentService 
    {
        private readonly IStudentRepository _repository;
        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }
        public void Add(Student student) {
            _repository.Add(student);
            Console.WriteLine("Add student Success!!!");
        }
        public void Update(Student student) { 
            _repository.Update(student);
            Console.WriteLine("Update student sucsess!!!");
        }
        public void Delete(int id) {
            _repository.Delete(id);
            Console.WriteLine("Delete student sucsess!!!");
        }
        public void displayListStudent()
        {
            var students = _repository.GetAll();
            foreach (var student in students)
            {
                Console.WriteLine($"Ma: {student.id} - Ten: {student.name} - Ngay sinh: {student.dateOfBirth} - Dia chi: {student.address} - Lop: {student.ClassRoom.id}");

            }
        }
        public void SortStudentByName()
        {
            var students = _repository.GetSortedByName();
            foreach (var student in students)
            {
                Console.WriteLine($"Ma: {student.id} - Ten: {student.name} - Ngay sinh: {student.dateOfBirth} - Dia chi: {student.address} - Lop: {student.ClassRoom.id}");
            }
        }
        public void SearchStudentById(int id)
        {
            var student = _repository.GetId(id);
            if (student != null)
            {
                Console.WriteLine($"Ma: {student.id} - Ten: {student.name} - Ngay sinh: {student.dateOfBirth} - Dia chi: {student.address} - Lop: {student.ClassRoom.id}");
            }
            else
            {
                Console.WriteLine("Khong tim thay sinh vien");
            }
        }
    }
}
