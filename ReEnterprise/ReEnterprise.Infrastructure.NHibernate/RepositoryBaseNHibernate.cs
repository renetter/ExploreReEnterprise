using NHibernate;
using ReEnterprise.Infrastructure.NHibernate.Interface;

namespace ReEnterprise.Infrastructure.NHibernate
{
    public class RepositoryBaseNHibernate
    {
        private readonly ISessionFactoryManager _sessionFactoryManager;

        public ISession Session { get { return _sessionFactoryManager.GetSession(); } }

        public RepositoryBaseNHibernate(ISessionFactoryManager sessionFactoryManager)
        {
            _sessionFactoryManager = sessionFactoryManager;
        }
    }
}
