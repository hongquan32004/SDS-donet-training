using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using StudentManagementgRPC.Mapping;
using StudentManagementgRPCService.Mapping;

namespace StudentManagementgRPC.NhibernerHelper
{
    public class NhibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2012
                            .Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>()
                            .ConnectionString(@"Server=LAPTOP-S8G1R5O6\SQLEXPRESS;Database=QLSV1;Integrated Security=True; MultipleActiveResultSets=True;TrustServerCertificate=True"))
                        .Mappings(m => {
                            m.FluentMappings.AddFromAssemblyOf<StudentMap>();
                            m.FluentMappings.AddFromAssemblyOf<ClassRoomMap>();
                            m.FluentMappings.AddFromAssemblyOf<TeacherMap>();
                        })
                        .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static NHibernate.ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
