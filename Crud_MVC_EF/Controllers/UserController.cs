using Crud_MVC_EF.Models;
using Crud_MVC_EF.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Crud_MVC_EF.Controllers
{
    public class UserController : Controller
    {
        public IUserRepo _repository;
        public ILogger<UserController> _logger;
        public UserController(IUserRepo repository, ILogger<UserController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            try
            {
                if(model != null)
                {
                    bool res =  await _repository.Login(model);
                    if (res)
                    {
                        var user = await _repository.GetUserByModel(model);
                        if (user != null)
                        {
                            HttpContext.Session.SetString("User",JsonConvert.SerializeObject(user));//store object in session
                            HttpContext.Session.SetString("Username",user.Username);
                            HttpContext.Session.SetString("Password",user.Password);
                            return RedirectToAction("Index", "Student");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Login", "User");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("-----------  User Contro Login log  -----------" + ex);
            }
            return View();
        }

        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError("------------  Logout user contro log  -------------" + ex);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register_Post(User model)
        {
            try
            {
                if(model != null)
                {
                    await _repository.Register(model);
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("-----------  Register User contro log  -------------" + ex);
            }
            return RedirectToAction("Register","User");
        }

    }
}
