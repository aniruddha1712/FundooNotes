using FundooBusinessLayer.Interface;
using FundooCommonLayer;
using FundooRepositoryLayer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.Services
{
    public class UserManager:IUserManager
    {
        private readonly IUserRepository repository;
        public IConfiguration Configuration { get; }


        public UserManager(IConfiguration configuration,IUserRepository repository)
        {
            this.repository = repository;
            this.Configuration = configuration;
        }

        public async Task<string> Register(RegisterModel data)
        {
            try
            {
                return await this.repository.Register(data);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string Login(LoginModel userData)
        {
            try
            {
                return this.repository.Login(userData);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> ResetPassword(ResetPassModel resetPass)
        {
            try
            {
                return await this.repository.ResetPassword(resetPass);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ForgetPassword(string email)
        {
            try
            {
                return this.repository.ForgetPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //public string JWTTokenGeneration(string email,int UserId)
        //{
        //    byte[] key = Encoding.ASCII.GetBytes(this.Configuration["SecretKey"]); //encrypting secret key
        //    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
        //    SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim(ClaimTypes.Name, email),
        //            new Claim("userId", UserId.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(90), //expiry time
        //        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler(); //creating and validating jwt
        //    JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
        //    return handler.WriteToken(token); //write serialize security token to web token
        //}
    }
}
