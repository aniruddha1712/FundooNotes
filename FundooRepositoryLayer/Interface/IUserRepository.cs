using FundooCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Interface
{
    public interface IUserRepository
    {
        Task<string> Register(RegisterModel data);
        string Login(LoginModel userData);
        Task<string> ResetPassword(ResetPassModel resetPass);
        public bool ForgetPassword(string email);
    }
}
