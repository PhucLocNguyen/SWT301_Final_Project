using API.Model.UserModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Repositories.Email
{
    public class EmailService : IEmailService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly EmailSetting emailSetting;
        private readonly IMemoryCache _cache;
        public EmailService(UnitOfWork unitOfWork, IOptions<EmailSetting> options, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            this.emailSetting = options.Value;
            _cache = cache;
        }

        public async Task SendEmail(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailSetting.Email);
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            var build = new BodyBuilder();
            build.HtmlBody = body;
            email.Body = build.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(emailSetting.Host, emailSetting.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSetting.Email,emailSetting.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public void SaveInCache(RequestRegisterAccount requestRegisterAccount)
        {
            string verificationCode = Guid.NewGuid().ToString();
            TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);
            requestRegisterAccount.VerifyEmail = verificationCode;
            requestRegisterAccount.Duration = DateTime.Now.AddMinutes(5);
            _cache.Set(verificationCode, requestRegisterAccount, _cacheDuration);
            SendEmail(requestRegisterAccount.Email, "Verify Account", "Verify Code: " + verificationCode);
        }

        

        public VerifyResult VerifyCode(string VerifyCode)
        {
            if (_cache.TryGetValue(VerifyCode, out RequestRegisterAccount requestRegisterAccount))
            {
                if (requestRegisterAccount.Duration >= DateTime.Now)
                {

                    var roleEntity = _unitOfWork.RoleRepository.Get(filter: x => x.Name.Equals(RoleConst.Customer)).FirstOrDefault();
                    var registerAccount = requestRegisterAccount.toUserEntity(roleEntity);
                    _unitOfWork.UserRepository.Insert(registerAccount);
                    _unitOfWork.Save();
                    _cache.Remove(VerifyCode);
                    return VerifyResult.Success;
                }
                else
                {
                    _cache.Remove(VerifyCode);
                    return VerifyResult.Expired;
                }
            }
            return VerifyResult.Invalid;
        }

        public enum VerifyResult
        {
            Success,
            Expired,
            Invalid
        }
    }
}
