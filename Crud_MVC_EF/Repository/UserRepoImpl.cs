using Crud_MVC_EF.Data;
using Crud_MVC_EF.Models;
using Crud_MVC_EF.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_MVC_EF.Repository
{
    public class UserRepoImpl : IUserRepo
    {

        public MyDbContext _dbContext;
        public readonly ILogger<UserRepoImpl> _logger;
        public UserRepoImpl(MyDbContext dbContext, ILogger<UserRepoImpl> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        async Task<bool> IUserRepo.Login(User model)
        {
			try
			{
                if(model != null)
                {
                    var res = await _dbContext.users.Where(m => m.Username == model.Username && m.Password == model.Password).FirstOrDefaultAsync();
                    if (res != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
			}
			catch (Exception ex)
			{
                _logger.LogError("-----------  login UserRepoImpl log  -----------" + ex);
			}
            return false;
        }

        async Task<User> IUserRepo.GetUserByModel(User model)
        {
            User user = null;
            try
            {
                if (model != null)
                {
                    user = await _dbContext.users.Where(m => m.Username == model.Username && m.Password == model.Password).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        return user;
                    }
                    else
                    {
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("-----------  Getuserbymodel UserRepoImpl log  -----------" + ex);   
            }
            return user;
        }

        async Task<User> IUserRepo.Register(User model)
        {
            User user = new User();
            try
            {
                if(model != null)
                {
                    var s =  await _dbContext.AddAsync(model);
                    await _dbContext.SaveChangesAsync();    
                    return user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("-----------  Register UserRepoImpl log  -----------" + ex);
            }
        return user;
        }
    }
}
