using Experimental.System.Messaging;
using FundooCommonLayer;
using FundooRepositoryLayer.Context;
using FundooRepositoryLayer.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;

        public UserRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        public IConfiguration Configuration { get; }

        public async Task<string> Register(RegisterModel data)
        {
            try
            {
                var validEmail = this.userContext.Users.Where(x => x.Email == data.Email).FirstOrDefault();
                if (validEmail == null)
                {
                    data.Password = EncryptPassword(data.Password);
                    this.userContext.Add(data);
                    await this.userContext.SaveChangesAsync();
                    return "Registration Succesful";
                }
                return "Email Id Already Exists";
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public LoginModel Login(LoginModel userData)
        {
            try
            {
                var validEmail = this.userContext.Users.Where(x => x.Email == userData.Email).FirstOrDefault();
                if (validEmail != null)
                {
                    var validPass = this.userContext.Users.Where(x => x.Password == EncryptPassword(userData.Password)).FirstOrDefault();
                    if (validPass != null)
                    {
                        return userData;
                    }
                    return null;
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private bool SendEmail(string email)
        {
            string linkToBeSend = this.ReceiveQueue(email);
            if (this.SendMailUsingSMTP(email, linkToBeSend))
            {
                return true;
            }

            return false;
        }


        private MessageQueue QueueDetail()
        {
            MessageQueue messageQueue;
            if (MessageQueue.Exists(@".\Private$\ResetPasswordQueue"))
            {
                messageQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\ResetPasswordQueue");
            }

            return messageQueue;
        }


        private void MSMQSend(string url)
        {
            try
            {
                MessageQueue messageQueue = this.QueueDetail();
                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = url;
                messageQueue.Label = "url link";
                messageQueue.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        private string ReceiveQueue(string email)
        {
            ////for reading from MSMQ
            var receiveQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            var receiveMsg = receiveQueue.Receive();
            receiveMsg.Formatter = new BinaryMessageFormatter();

            string linkToBeSend = receiveMsg.Body.ToString();
            return linkToBeSend;
        }


        private bool SendMailUsingSMTP(string email, string message)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mailMessage.From = new MailAddress("ani964449@gmail.com");
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Link to reset you password for fundoo Application";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("ani964449@gmail.com", "xrypuxxqkzqvlqca");
            smtp.Send(mailMessage);
            return true;
        }
        public bool ForgetPassword(string email)
        {
            try
            {
                var validEmail = this.userContext.Users.Where(x => x.Email == email).FirstOrDefault();
                if (validEmail != null)
                {
                    this.MSMQSend("Link for resetting the password");
                    return this.SendEmail(email);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<string> ResetPassword(ResetPassModel resetPass)
        {
            try
            {
                var isEmail = this.userContext.Users.Where(x => x.Email == resetPass.Email).FirstOrDefault();
                if(resetPass != null)
                {
                    isEmail.Password = EncryptPassword(resetPass.Password);
                    this.userContext.Update(isEmail);
                    await this.userContext.SaveChangesAsync();
                    return "Password Updated Successfully";
                }
                return "Cannot Reset Password";
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string EncryptPassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodeData = Convert.ToBase64String(encData_byte);
                return encodeData;
            }
            catch(Exception e)
            {
                throw new Exception("Error in Base64Encoding" + e.Message);
            }
        }

    }
}
