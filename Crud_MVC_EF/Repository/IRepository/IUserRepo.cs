using Crud_MVC_EF.Models;
namespace Crud_MVC_EF.Repository.IRepository
{
    public interface IUserRepo
    {
        Task<bool> Login(User model);
        Task<User> GetUserByModel(User model);

        Task<User> Register(User model);
    }
}
