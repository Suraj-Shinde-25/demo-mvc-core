using Crud_MVC_EF.Models;
using Crud_MVC_EF.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Crud_MVC_EF.Controllers
{
    public class StudentController : Controller
    {
        public IStudentRepo _repository;
        public ILogger<StudentController> _logger;
        public StudentController(IStudentRepo repository, ILogger<StudentController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var s = HttpContext.Session.GetString("Username");
            if (s == null)
            {
                return RedirectToAction("Login", "User");
            }
            var countries = _repository.GetAllCountry().Result;
            ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");  // this is object of SELECT element
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student model)
        {
            try
            {
                var s = HttpContext.Session.GetString("Username");
                if (s == null)
                {
                    return RedirectToAction("Login", "User");
                }
                if (model != null)
                {
                    var res = await _repository.CreateStudent(model);
                    if(res >  0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("-------------- Create Contro Log ---------------" + ex);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                //string s = null;
                var s = HttpContext.Session.GetString("User");
                if (s == null)
                {
                    return RedirectToAction("Login", "User");
                }
                var result = await _repository.GetAll();
                if (result != null)
                {
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("-------------- Index Contro Log ---------------" + ex);
            }
            return View();
        }

        
        public async Task<IActionResult> Delete([FromQuery]int StudentId)
        {
            try
            {
                var s = HttpContext.Session.GetString("Username");
                if (s == null)
                {
                    return RedirectToAction("Login", "User");
                }
                var res =  await _repository.DeleteStudent(StudentId);
                if (res)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("-------------- Delete Contro Log ---------------" + ex);
            }
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Edit_Get([FromQuery] int StudentId)
        {
            try
            {
                var s = HttpContext.Session.GetString("Username");
                if (s == null)
                {
                    return RedirectToAction("Login", "User");
                }
                var res = await _repository.GetById(StudentId);
                var countries = _repository.GetAllCountry().Result;
                ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");  // this is object of SELECT element
                var cities = _repository.GetCityById(res.CountryId).Result;
                ViewBag.Cities = new SelectList(cities, "CityId", "CityName");  // this is object of SELECT element
                if (res  != null)
                {
                    return View(res);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("-------------- Edit get Contro Log ---------------" + ex);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Post(Student model)
        {
            try
            {
                var s = HttpContext.Session.GetString("Username");
                if (s == null)
                {
                    return RedirectToAction("Login", "User");
                }
                bool res = await _repository.UpdateStudent(model); 
                if (res)
                {
                    return RedirectToAction("Index"); 
                }   
                return View("Edit_Get",model);
            }
            catch (Exception ex)
            {
                _logger.LogError("-------------- Edit post Contro Log ---------------" + ex);
            }
            return View("Edit_Get",model);
        }

        [HttpGet]
        public JsonResult GetCityById(int id)
        {
            try
            {
                var res =  _repository.GetCityById(id).Result;
                return Json(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("--------  GetCityById Control Log  ---------"+ ex);
            }
            return Json(null);
        }
    }
}
