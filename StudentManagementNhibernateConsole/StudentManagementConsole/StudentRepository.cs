using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementConsole
{
    public class StudentRepository : IStudentRepository
    {
        public void Add(Student student)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(student);
                    transaction.Commit();
                }
            }
        }

        public void Delete(int id)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var student = session.Get<Student>(id);
                    if (student != null) { 
                        session.Delete(student);
                        transaction.Commit();
                    }
                }
            }
        }

        public List<Student> GetAll()
        {
            using(var session = NhibernateHelper.OpenSession())
            {
                return session.Query<Student>().ToList();
            }
        }

        public Student GetId(int id)
        {
            using(var session = NhibernateHelper.OpenSession())
            {
                return session.Get<Student>(id); 
            }
        }

        public List<Student> GetSortedByName()
        {
            using(var session = NhibernateHelper.OpenSession())
            {
                return session.Query<Student>().OrderBy(s => s.name).ToList();
            }
        }

        public void Update(Student student)
        {
            using(var session = NhibernateHelper.OpenSession())
            {
                using(var transaction = session.BeginTransaction())
                {
                    session.Update(student);
                    transaction.Commit();
                }
            }
        }
    }
}
