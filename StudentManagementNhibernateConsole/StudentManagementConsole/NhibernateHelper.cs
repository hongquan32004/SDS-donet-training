using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagementConsole.Mapping;

namespace StudentManagementConsole
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
                            .ConnectionString(@"Server=LAPTOP-S8G1R5O6\SQLEXPRESS;Database=QLSV1;Integrated Security=True;TrustServerCertificate=True"))
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<StudentMap>())
                        .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
