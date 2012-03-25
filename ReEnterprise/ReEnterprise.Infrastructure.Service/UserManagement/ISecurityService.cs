using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Domain.UserManagement.Contract.Service;

namespace ReEnterprise.Infrastructure.Service.UserManagement
{
    [ServiceContract]
    public interface ISecurityService
    {
        [OperationContract]
        CreateUserResponse CreateUser(User newUser);
    }
}
