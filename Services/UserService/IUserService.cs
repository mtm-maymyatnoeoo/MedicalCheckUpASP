using MedicalCheckUpASP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalCheckUpASP.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> Search(string searchString);
        Task CreateUserAsync(User user);
        Task<bool> CheckDuplicateUser(string employeeNumber, string email);

        //Task UpdateSalesCategoryAsync(SalesCategory salesCategory);
        //Task DeleteSalesCategoryAsync(int id);
    }
}
