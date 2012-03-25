using NHibernate;

namespace ReEnterprise.Infrastructure.NHibernate.Interface
{
    public interface ISessionFactoryManager
    {
        ISessionFactory GetSessionFactory();
        ISession GetSession();
    }
}
