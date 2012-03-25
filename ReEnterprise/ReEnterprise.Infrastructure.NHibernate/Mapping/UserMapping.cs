using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using FluentNHibernate.Mapping;

namespace ReEnterprise.Infrastructure.NHibernate.Mapping
{
    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            Id(c => c.UserId);
            Map(c => c.FirstName);
            Map(c => c.LastName);
            Map(c => c.Email);
            Map(c => c.PhoneNumber);
            Map(c => c.Password);
            Map(c => c.Address);
            Map(c => c.SecurityQuestion);
            Map(c => c.ForceChangePassword);
            Map(c => c.ApplyPasswordPolicy);
        }
    }
}
