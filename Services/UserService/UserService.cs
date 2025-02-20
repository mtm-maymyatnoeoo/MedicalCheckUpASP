using MedicalCheckUpASP.Common;
using MedicalCheckUpASP.DbContexts;
using MedicalCheckUpASP.Models;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace MedicalCheckUpASP.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly CommonDbContext _dbContext;

        public UserService(CommonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public Task<List<User>> Search(string searchString)
        {
            var results = _dbContext.Users
                        .Where(u => EF.Functions.Like(u.UserName, $"%{searchString}%") ||
                                    EF.Functions.Like(u.Email, $"%{searchString}%") ||
                                    EF.Functions.Like(u.EmployeeNumber, $"%{searchString}%"))
                        .ToListAsync();
            return results;
        }


        public async Task CreateUserAsync(User user)
        {
            try
            {
                // Encrypt password
                PasswordEncryptionService encryptionService = new PasswordEncryptionService();
                user.Password = encryptionService.EncryptPassword(user.Password);
                await _dbContext.Users.AddAsync(user);
                Console.WriteLine($"CreatedAt: {user.CreatedAt}");
                Console.WriteLine($"Email: {user.Email}");
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        public async Task<bool> CheckDuplicateUser(string employeeNumber, string email)
        {
            try
            {
                var duplicateCount = await _dbContext.Users
                .CountAsync(e => e.Email == email || e.EmployeeNumber == employeeNumber);
                return duplicateCount > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        //public async Task UpdateSalesCategoryAsync(SalesCategory salesCategory)
        //{
        //    _dbContext.SalesCategory.Update(salesCategory);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task DeleteSalesCategoryAsync(int id)
        //{
        //    var salesCategory = await _dbContext.SalesCategory.FindAsync(id);
        //    if (salesCategory != null)
        //    {
        //        _dbContext.SalesCategory.Remove(salesCategory);
        //        await _dbContext.SaveChangesAsync();
        //    }
        //}

    }
}
