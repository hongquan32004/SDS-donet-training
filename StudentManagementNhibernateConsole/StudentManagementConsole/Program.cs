using StudentManagementConsole;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;




public class Program 
{

    static void Main()
    {
        IStudentRepository repository = new StudentRepository();
        StudentService service = new StudentService(repository);
        while (true)
        {
            Console.WriteLine("\n1. Add Student\n2. Edit Student\n3. Delete Student\n4. View All Students\n5. Sort by Name\n6. Search by ID\n7. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter ID: "); int id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Name: "); string name = Console.ReadLine();
                    Console.Write("Enter Birth Date (dd/MM/yyyy): "); DateTime birthDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Console.Write("Enter Address: "); string address = Console.ReadLine();
                    Console.Write("Enter Class ID: "); string classId = Console.ReadLine();
                    service.Add(new Student { id = id, name = name, dateOfBirth = birthDate, address = address, ClassRoom = new ClassRoom {id = classId } });
                    break;
                case "2":
                    Console.Write("Enter ID to edit: "); id = Convert.ToInt32(Console.ReadLine());
                    var student = repository.GetId(id);
                    if (student != null)
                    {
                        Console.Write("Enter New Name: "); student.name = Console.ReadLine();
                        Console.Write("Enter New Address: "); student.address = Console.ReadLine();
                        service.Update(student);
                    }
                    else
                        Console.WriteLine("Student not found!");
                    break;
                case "3":
                    Console.Write("Enter ID to delete: "); id = Convert.ToInt32(Console.ReadLine());
                    service.Delete(id);
                    break;
                case "4":
                    service.displayListStudent();
                    break;
                case "5":
                    service.SortStudentByName();
                    break;
                case "6":
                    Console.Write("Enter ID to search: "); id = Convert.ToInt32(Console.ReadLine());
                    service.SearchStudentById(id);
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option! Try again.");
                    break;
            }
        }
    }
}
