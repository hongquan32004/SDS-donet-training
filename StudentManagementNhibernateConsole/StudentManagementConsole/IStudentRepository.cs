﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementConsole
{
    public interface IStudentRepository
    {
        void Add(Student student);
        void Update(Student student);
        void Delete(int id);
        Student GetId(int id);
        List<Student> GetAll();
        List<Student> GetSortedByName();
    }
}
