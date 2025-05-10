using MedicalCheckUpASP.Common;
using MedicalCheckUpASP.DbContexts;
using MedicalCheckUpASP.Models;

namespace MedicalCheckUpASP.Services.LoginService
{
    public class LoginService: ILoginService
    {
        private readonly CommonDbContext _dbContext;

        public LoginService(CommonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User AdminLogin(string email, string password, Role role)
        {
            return VerifyPassword(email, password, Role.Admin); // Use the method to verify password
        }
        // Verify if the password matches the stored encrypted password
        private User? VerifyPassword(string email, string enteredPassword, Role role)
        {
            var user = _dbContext.Users
                        .FirstOrDefault(u => u.Email == email && u.Role == role);

            if (user == null)
            {
                return null; // User not found
            }

            // Decrypt the stored password and compare
            PasswordEncryptionService encryptionService = new PasswordEncryptionService();

            string decryptedPassword = encryptionService.DecryptPassword(user.Password);
            if (decryptedPassword == enteredPassword) {

                return user;
            }
            return null;
        }

    }
}
