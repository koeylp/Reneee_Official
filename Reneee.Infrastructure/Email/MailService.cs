using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Reneee.Application.Contracts.ThirdService;
using Reneee.Application.DTOs.Mail;
using Reneee.Application.DTOs.Order;

namespace Reneee.Infrastructure.Email
{
    public class MailService(IOptions<MailSettings> options, ILogger<MailService> logger) : IMailService
    {
        private readonly MailSettings _mailSettings = options.Value;
        private readonly ILogger<MailService> _logger = logger;

        public async Task<bool> SendMail(MailData mailData)
        {
            try
            {
                using (MimeMessage emailMessage = new MimeMessage())
                {
                    MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                    emailMessage.From.Add(emailFrom);
                    MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
                    emailMessage.To.Add(emailTo);

                    emailMessage.Subject = mailData.EmailSubject;

                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.TextBody = mailData.EmailBody;

                    emailMessage.Body = emailBodyBuilder.ToMessageBody();

                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        mailClient.Connect(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                        mailClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                        mailClient.Send(emailMessage);
                        mailClient.Disconnect(true);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> SendOrderConfirmationEmail(string recipientEmail, OrderDto order, string name)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
                message.To.Add(new MailboxAddress(name, recipientEmail));
                message.Subject = "Order Confirmation";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                <h2>Order Confirmation</h2>
                <p>Dear {name},</p>
                <p>Thank you for your order! Here are the details:</p>
                <ul>
                    <li>Order Number: {order.Id}</li>
                    <li>Order Date: {order.OrderDate}</li>
                    <li>Total Amount: ${order.Total}</li>
                </ul>
                <p>You can track your order status and view more details on your account page.</p>
                <p>Thank you for shopping with us!</p>"
                };

                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        public async Task<bool> SendPasswordResetEmail(string recipientEmail, string resetLink, string name)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
                message.To.Add(new MailboxAddress(name, recipientEmail));
                message.Subject = "Password Reset Request";
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                <h2>Password Reset</h2>
                <p>You requested to reset your password. Click the link below to reset it:</p>
                <a href='{resetLink}'>Reset Password</a>
                <p>If you did not request a password reset, please ignore this email.</p>"
                };

                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
