using API.Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repositories.Email.EmailService;

namespace Repositories.Email
{
    public interface IEmailService
    {
        Task SendEmail(string toEmail, string subject, string body);

        void SaveInCache(RequestRegisterAccount requestRegisterAccount);

        VerifyResult VerifyCode(string VerifyCode);
    }
}
