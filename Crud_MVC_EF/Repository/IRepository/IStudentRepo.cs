using Crud_MVC_EF.Models;

namespace Crud_MVC_EF.Repository.IRepository
{
    public interface IStudentRepo
    {
        public Task<int> CreateStudent(Student model);
        public Task<List<Student>> GetAll();
        public Task<Student> GetById(int id);
        public Task<bool> UpdateStudent(Student model);
        public Task<bool> DeleteStudent(int id);
        public Task<List<Country>> GetAllCountry();
        public Task<List<CityDTOW>> GetCityById(int countryId);
    }
}
