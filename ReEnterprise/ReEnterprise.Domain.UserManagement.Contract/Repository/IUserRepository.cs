using ReEnterprise.Domain.UserManagement.Contract.Entity;

namespace ReEnterprise.Domain.UserManagement.Contract.Repository
{
    public interface IUserRepository
    {
        void Create(User newUser);
    }
}
