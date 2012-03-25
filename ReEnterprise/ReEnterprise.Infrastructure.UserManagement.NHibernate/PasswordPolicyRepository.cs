using System.Linq;
using NHibernate.Linq;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Domain.UserManagement.Contract.Repository;
using ReEnterprise.Infrastructure.NHibernate;
using ReEnterprise.Infrastructure.NHibernate.Interface;

namespace ReEnterprise.Infrastructure.UserManagement.NHibernate
{
    public class PasswordPolicyRepository : RepositoryBaseNHibernate, IPasswordPolicyRepository
    {
        private const int PasswordMinimumLength = 5;

        public PasswordPolicyRepository(ISessionFactoryManager sessionFactoryManager)
            : base(sessionFactoryManager)
        {
        }

        #region IPasswordPolicyRepository Members

        public void SavePasswordPolicy(PasswordPolicy passwordPolicy)
        {
            Session.SaveOrUpdate(passwordPolicy);
        }

        public PasswordPolicy GetPasswordPolicy()
        {
            PasswordPolicy result = Session.Query<PasswordPolicy>().FirstOrDefault();

            return result ?? new PasswordPolicy {MinimumLength = PasswordMinimumLength, StrongPassword = false};
        }

        #endregion
    }
}