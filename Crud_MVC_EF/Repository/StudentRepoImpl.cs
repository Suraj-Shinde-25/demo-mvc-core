using Crud_MVC_EF.Data;
using Crud_MVC_EF.Models;
using Crud_MVC_EF.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;

namespace Crud_MVC_EF.Repository
{
    public class StudentRepoImpl : IStudentRepo
    {
        public MyDbContext _dbContext;
        public readonly ILogger<StudentRepoImpl> _logger;
        public StudentRepoImpl(MyDbContext dbContext, ILogger<StudentRepoImpl> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        async Task<int> IStudentRepo.CreateStudent(Student model)
        {
            var res = 0;
            try
            {
                if(model != null)
                {
                    var student = await _dbContext.AddAsync(model);
                    await _dbContext.SaveChangesAsync();
                    res = 1;
                }
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError("---------------- Create StudentRepoImpl Log  ---------------" + ex);
            }
            return res;
        }

        async Task<bool> IStudentRepo.DeleteStudent(int id)
        {
            try
            {
                var s = await _dbContext.students.Where(m => m.StudentId == id).ExecuteDeleteAsync();
                await _dbContext.SaveChangesAsync();
                var res = await _dbContext.students.FindAsync(id);
                if(res != null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("---------------- Delete StudentRepoImpl Log  ---------------" + ex);
            }
            return false;
        }

        async Task<List<Student>> IStudentRepo.GetAll()
        {
            List<Student> list = new List<Student>();
            try
            {
                list = await _dbContext.students.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError("---------------- GetAll StudentRepoImpl Log  ---------------" + ex);
            }
            return list;
        }

        async Task<Student> IStudentRepo.GetById(int id)
        {
            Student stud = new Student();
            try
            {
                stud =  await _dbContext.students.FindAsync(id);
                if(stud != null)
                {
                    return stud;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("---------------- GetById StudentRepoImpl Log  ---------------" + ex);
            }
            return stud;
        }

        async Task<bool> IStudentRepo.UpdateStudent(Student model)
        {
            try
            {
                Student oldrs = await _dbContext.students.FirstOrDefaultAsync(o => o.StudentId == model.StudentId);
                if (oldrs != null)
                {
                    oldrs.StudentName = model.StudentName;
                    oldrs.StudentEmail = model.StudentEmail;
                    oldrs.StudentPhone = model.StudentPhone;
                    oldrs.Gender = model.Gender;
                    oldrs.Education = model.Education;
                    oldrs.WorkStatus = model.WorkStatus;
                    oldrs.CountryId = model.CountryId;
                    oldrs.CityId = model.CityId;
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("---------------- UpdateStudent StudentRepoImpl Log  ---------------" + ex);
            }
            return false;
        }
    
        
        async Task<List<Country>> IStudentRepo.GetAllCountry()
        {
            List<Country> list = new List<Country>();
            try
            {
                list = await _dbContext.countries.ToListAsync();
                if(list.Count > 0)
                {
                    _logger.LogInformation("---------  Getting AllCountries  --------");
                    return list;
                }
            }
            catch (Exception ex )
            {
                _logger.LogError("GetAllCountry Repo Log"+ ex);
            }
            return list;
        }

        async Task<List<CityDTOW>> IStudentRepo.GetCityById(int countryId)
        {
            List <City> list = new List<City>();
            try
            {
                if (countryId > 0)
                {
                    list = await _dbContext.cities.Where(c => c.CountryId == countryId)
                        .ToListAsync();
                    
                    return ConvertDTO(list);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("---------  GetCityById Repo Log  --------"+ ex);
            }
            return ConvertDTO(list);
        }

        private List<CityDTOW> ConvertDTO (List<City> cities)
        {
            List<CityDTOW> cityDTOWs = new List<CityDTOW>();
            foreach (var item in cities)
            {
                var c = new CityDTOW();
                c.CityId = item.CityId;
                c.CityName = item.CityName;
                cityDTOWs.Add(c);
            }
            return cityDTOWs;
        }
    }
}
