using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Domain.UserManagement.Contract.Entity;

namespace ReEnterprise.Domain.UserManagement.Contract.Repository
{
    public interface IUserRepository
    {
        void Create(User newUser);
    }
}
