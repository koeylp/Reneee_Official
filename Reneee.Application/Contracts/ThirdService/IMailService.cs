using Reneee.Application.DTOs.Mail;

namespace Reneee.Application.Contracts.ThirdService
{
    public interface IMailService
    {
        Task<bool> SendMail(MailData Mail_Data);
        Task<bool> SendPasswordResetEmail(string recipientEmail, string resetLink, string name);
    }
}
