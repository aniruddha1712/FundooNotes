using FundooCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.Interface
{
    public interface IUserManager
    {
        Task<string> Register(RegisterModel data);
        string Login(LoginModel userData);
        Task<string> ResetPassword(ResetPassModel resetPass);
        public bool ForgetPassword(string email);
        //string JWTTokenGeneration(string email,int UserId);
    }
}
