using FundooBusinessLayer.Interface;
using FundooCommonLayer;
using FundooRepositoryLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesApp.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]

    public class UserController : Controller
    {
        private readonly IUserManager manager;
        private readonly ILogger logger;

        public UserController(IUserManager manager,ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel userData) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = await this.manager.Register(userData);
                //HttpContext.Session.SetString("User Name ", userData.FirstName + " " + userData.LastName);
                //HttpContext.Session.SetString("User Email ", userData.Email);
                if (result.Equals("Registration Succesful"))
                {
                    logger.LogInformation("New user added successfully with userid " + userData.UserId + " & firstname:" + userData.FirstName);
                    //var userName = HttpContext.Session.GetString("User Name");
                    //logger.LogInformation("Username " + userName + result);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    logger.LogInformation("Username " + userData + result);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginModel userData)
        {
            try
            {
                var result = this.manager.Login(userData);
                if(result != null)
                {
                    //using resdi cache
                    ConnectionMultiplexer cMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = cMultiplexer.GetDatabase();
                    string firstName = database.StringGet(key: "First Name");
                    string lastnNme = database.StringGet(key: "Last Name");
                    int userId = Convert.ToInt32(database.StringGet(key: "User Id"));
                    RegisterModel reg = new RegisterModel
                    {
                        FirstName = firstName,
                        LastName = lastnNme,
                        Email = userData.Email,
                        UserId = userId
                    };

                    //string token = this.manager.JWTTokenGeneration(userData.Email,userId);
                    return this.Ok(new { Status = true, Message = "Login Successful",Data = reg, Token = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Login Failed" });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Route("forgot/{email}")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = this.manager.ForgetPassword(email);
                if (result.Equals(true))
                {
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "Forgot Password" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Bad request Forget password" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassModel reset)
        {
            try
            {
                string result = await this.manager.ResetPassword(reset);
                if(result.Equals("Password Updated Successfully"))
                {
                    return this.Ok(new ResponseModel<string> { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}

