using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Infrastructure.NHibernate;
using ReEnterprise.Domain.UserManagement.Contract.Repository;
using ReEnterprise.Infrastructure.NHibernate.Interface;
using ReEnterprise.Domain.UserManagement.Contract.Entity;

namespace ReEnterprise.Infrastructure.UserManagement.NHibernate
{
    public class UserRepository: RepositoryBaseNHibernate, IUserRepository
    {
        public UserRepository(ISessionFactoryManager sessionFactoryManager)
            :base(sessionFactoryManager)
        {

        }

        public void Create(User newUser)
        {
            Session.SaveOrUpdate(newUser);
        }
    }
}
