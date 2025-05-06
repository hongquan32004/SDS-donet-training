using System;

namespace StudentManagement
{
    class Teacher
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    class Classroom
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public Teacher Teacher { get; set; }
    }
    class Student
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public Classroom Classroom { get; set; }
    }
    class Program
    {
        static List<Student> students = new List<Student>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n===== MENU QUẢN LÝ SINH VIÊN =====");
                Console.WriteLine("1. Xem danh sách sinh viên");
                Console.WriteLine("2. Thêm mới sinh viên");
                Console.WriteLine("3. Chỉnh sửa thông tin sinh viên");
                Console.WriteLine("4. Xóa sinh viên");
                Console.WriteLine("5. Sắp xếp sinh viên theo tên");
                Console.WriteLine("6. Tìm kiếm sinh viên theo mã số");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": viewStudent(); break;
                    case "2": addStudents(); break;
                    case "3": editStudent(); break;
                    case "4": deleteStudent(); break;
                    case "5": sortStudentByName(); break;
                    case "6": searchStudentByID(); break;
                    case "0": return;
                }
            }

        }
        static void viewStudent()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Danh sách trống!!");
                return;
            }
            foreach (Student student in students)
            {
                Console.WriteLine($"Mã: {student.ID}, Tên: {student.Name}, Ngày sinh: {student.DateOfBirth}, Địa chỉ: {student.Address},Lớp: {student.Classroom.Name}");
            }
        }
        static void addStudents()
        {
            Console.WriteLine("Nhập ID: ");
            string ID = Console.ReadLine();
            Console.WriteLine("Nhập tên: ");
            string Name = Console.ReadLine();
            Console.WriteLine("Nhập ngày sinh: ");
            DateTime Birth = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Nhập địa chỉ: ");
            string Address = Console.ReadLine();
            Console.WriteLine("Nhập lớp: ");
            string className = Console.ReadLine();
            students.Add(new Student { ID = ID, Name = Name, DateOfBirth = Birth, Address = Address, Classroom = new Classroom { Name = className } });
            Console.WriteLine("Thêm thành viên thành công!!!!");
        }

        static void editStudent()
        {
            Console.WriteLine("Nhập mã sinh viên cần sửa: ");
            string ID = Console.ReadLine();
            Student student = students.FirstOrDefault(s => s.ID == ID);
            if (student == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên!!!");
            }
            Console.WriteLine("Nhập tên mới:");
            student.Name = Console.ReadLine();
            Console.WriteLine("Nhập ngày sinh mới:");
            student.DateOfBirth = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Nhập địa chỉ mới: ");
            student.Address = Console.ReadLine();
            Console.WriteLine("Cập nhật thành công ");

        }
        static void deleteStudent()
        {
            Console.WriteLine("Nhập mã sinh viên cần xóa: ");
            string ID = Console.ReadLine();
            Student student = students.FirstOrDefault(s => s.ID == ID);
            if (student == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên!!!");
            }
            students.Remove(student);
            Console.WriteLine("Xóa thành công!!");
        }
        static void sortStudentByName()
        {
            students = students.OrderBy(s => s.Name).ToList();
            Console.WriteLine("Sắp xếp thành viên theo tên thành công");
        }
        static void searchStudentByID()
        {
            Console.WriteLine("Nhập mã sinh viên cần tìm: ");
            string ID = Console.ReadLine();
            Student student = students.FirstOrDefault(s => s.ID == ID);
            if (student != null)
            {
                Console.WriteLine($"Tìm thấy student:");
                Console.WriteLine($"Mã: {student.ID}, Tên: {student.Name}, Ngày sinh: {student.DateOfBirth}, Địa chỉ: {student.Address},Lớp: {student.Classroom.Name}");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên");
            }

        }
    }
}