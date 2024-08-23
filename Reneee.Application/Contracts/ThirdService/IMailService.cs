using Reneee.Application.DTOs.Mail;
using Reneee.Application.DTOs.Order;

namespace Reneee.Application.Contracts.ThirdService
{
    public interface IMailService
    {
        Task<bool> SendMail(MailData Mail_Data);
        Task<bool> SendPasswordResetEmail(string recipientEmail, string resetLink, string name);
        Task<bool> SendOrderConfirmationEmail(string recipientEmail, OrderDto order, string name);
    }
}
