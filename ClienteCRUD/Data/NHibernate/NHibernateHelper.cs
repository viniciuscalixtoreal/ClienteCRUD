using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace ClienteCRUD.Data.NHibernate
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2012
                            .ConnectionString("Data Source=DESKTOP-H066QAM\\SQLEXPRESS; Initial Catalog=DataBase; Integrated Security=True; TrustServerCertificate=True"))
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClienteMap>())
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
