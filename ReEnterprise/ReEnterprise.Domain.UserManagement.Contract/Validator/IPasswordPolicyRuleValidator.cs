using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReEnterprise.Domain.UserManagement.Contract.Entity;
using ReEnterprise.Core.Generic;

namespace ReEnterprise.Domain.UserManagement.Contract.Validator
{
    public interface IPasswordPolicyRuleValidator: IRuleValidator<User>
    {
    }
}
