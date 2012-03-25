using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using ReEnterprise.Infrastructure.NHibernate.Interface;

namespace ReEnterprise.Infrastructure.NHibernate
{
    public class SessionFactoryManager : ISessionFactoryManager
    {
        private static readonly object SyncObject = new object();
        private static volatile ISessionFactory _sessionFactory;
        private ISession _session;

        #region ISessionFactoryManager Members

        public ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                lock (SyncObject)
                {
                    if (_sessionFactory == null)
                    {
                        var configuration = new Configuration();
                        configuration.Configure();

                        _sessionFactory = Fluently.Configure(configuration)
                                                     .Mappings(x => x.FluentMappings.AddFromAssemblyOf<SessionFactoryManager>())
                                                     .BuildSessionFactory();
                    }
                }
            }

            return _sessionFactory;
        }

        public ISession GetSession()
        {
            if (_session == null)
            {
                _session = GetSessionFactory().OpenSession();
            }

            return _session;
        }

        #endregion
    }
}