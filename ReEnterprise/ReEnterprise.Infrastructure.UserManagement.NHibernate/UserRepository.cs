using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Domain.UserManagement.Contract.Repository;
using ReEnterprise.Infrastructure.NHibernate;
using ReEnterprise.Infrastructure.NHibernate.Interface;

namespace ReEnterprise.Infrastructure.UserManagement.NHibernate
{
    public class UserRepository : RepositoryBaseNHibernate, IUserRepository
    {
        public UserRepository(ISessionFactoryManager sessionFactoryManager)
            : base(sessionFactoryManager)
        {
        }

        #region IUserRepository Members

        public void Create(User newUser)
        {
            Session.SaveOrUpdate(newUser);
        }

        #endregion
    }
}