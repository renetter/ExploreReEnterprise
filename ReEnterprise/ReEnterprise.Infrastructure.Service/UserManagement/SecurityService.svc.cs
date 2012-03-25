using Castle.Core;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Domain.UserManagement.Contract.Service;
using ReEnterprise.Infrastructure.Service.Interceptor;

namespace ReEnterprise.Infrastructure.Service.UserManagement
{
    [Interceptor(typeof (ServiceInterceptor))]
    public class SecurityService : ISecurityService
    {
        private readonly IUserManagementService _userManagementService;

        public SecurityService(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        #region ISecurityService Members

        public CreateUserResponse CreateUser(User newUser)
        {
            CreateUserResponse result = _userManagementService.CreateUser(newUser);

            return result;
        }

        #endregion
    }
}