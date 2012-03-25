using NHibernate;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Domain.UserManagement.Contract.Service;
using ReEnterprise.Infrastructure.NHibernate.Interface;

namespace ReEnterprise.Infrastructure.Service.UserManagement
{
    public class SecurityService : ISecurityService
    {
        private readonly ISessionFactoryManager _sessionFactoryManager;
        private readonly IUserManagementService _userManagementService;

        public SecurityService(IUserManagementService userManagementService,
                               ISessionFactoryManager sessionFactoryManager)
        {
            _userManagementService = userManagementService;
            _sessionFactoryManager = sessionFactoryManager;
        }

        #region ISecurityService Members

        public CreateUserResponse CreateUser(User newUser)
        {
            ISession session = _sessionFactoryManager.GetSession();
            ITransaction transaction = session.BeginTransaction();

            CreateUserResponse result = _userManagementService.CreateUser(newUser);

            transaction.Commit();

            return result;
        }

        #endregion
    }
}