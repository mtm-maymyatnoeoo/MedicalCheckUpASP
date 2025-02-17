using MedicalCheckUpASP.Models;

namespace MedicalCheckUpASP.Services.LoginService
{
    public interface ILoginService
    {
        public User AdminLogin(string email, string password, Role role);
    }
}
