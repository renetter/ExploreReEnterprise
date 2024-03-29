﻿using ReEnterprise.Domain.UserManagement.Contract.Entity;
using FluentNHibernate.Mapping;

namespace ReEnterprise.Infrastructure.NHibernate.Mapping
{
    public class PasswordPolicyMapping : ClassMap<PasswordPolicy>
    {
        public PasswordPolicyMapping()
        {
            Id(c => c.MinimumLength);
            Map(c => c.StrongPassword);
        }
    }
}
