using System.ServiceModel;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Domain.UserManagement.Contract.Service;
using ReEnterprise.Infrastructure.Service.Attribute;

namespace ReEnterprise.Infrastructure.Service.UserManagement
{
    [ServiceContract]
    public interface ISecurityService
    {
        [OperationContract]
        [EnableTransaction]
        CreateUserResponse CreateUser(User newUser);
    }
}