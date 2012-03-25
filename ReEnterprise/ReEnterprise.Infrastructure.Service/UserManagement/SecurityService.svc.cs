using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ReEnterprise.Domain.UserManagement.Contract.Service;
using ReEnterprise.Infrastructure.NHibernate.Interface;
using NHibernate;
using ReEnterprise.Domain.UserManagement.Contract.Entity;

namespace ReEnterprise.Infrastructure.Service.UserManagement
{
    public class SecurityService : ISecurityService
    {
        private IUserManagementService _userManagementService;
        private ISessionFactoryManager _sessionFactoryManager;

        public SecurityService(IUserManagementService userManagementService, ISessionFactoryManager sessionFactoryManager)
        {
            _userManagementService = userManagementService;
            _sessionFactoryManager = sessionFactoryManager;
        }

        public CreateUserResponse CreateUser(User newUser)
        {
            ISession session = _sessionFactoryManager.GetSession();
            ITransaction transaction = session.BeginTransaction();

            CreateUserResponse result = _userManagementService.CreateUser(newUser);

            transaction.Commit();

            return result;
        }
    }
}
