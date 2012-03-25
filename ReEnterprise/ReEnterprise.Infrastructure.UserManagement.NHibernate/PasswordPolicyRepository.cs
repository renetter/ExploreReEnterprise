using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Domain.UserManagement.Contract.Repository;
using ReEnterprise.Infrastructure.NHibernate.Interface;
using NHibernate;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Infrastructure.NHibernate;
using NHibernate.Linq;

namespace ReEnterprise.Infrastructure.UserManagement.NHibernate
{
    public class PasswordPolicyRepository: RepositoryBaseNHibernate, IPasswordPolicyRepository
    {
        private const int PasswordMinimumLength = 5;

        public PasswordPolicyRepository(ISessionFactoryManager sessionFactoryManager)
            :base(sessionFactoryManager)
        {
        }

        public void SavePasswordPolicy(Domain.UserManagement.Contract.Entity.PasswordPolicy passwordPolicy)
        {
            Session.SaveOrUpdate(passwordPolicy);
        }

        public Domain.UserManagement.Contract.Entity.PasswordPolicy GetPasswordPolicy()
        {
            PasswordPolicy result = Session.Query<PasswordPolicy>().FirstOrDefault();

            return result ?? new PasswordPolicy { MinimumLength = PasswordMinimumLength, StrongPassword = false };
        }
    }
}
