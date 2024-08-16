using Microsoft.AspNetCore.Mvc;
using Reneee.Application.Contracts.ThirdService;
using Reneee.Application.DTOs.Mail;

namespace Reneee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController(IMailService mailService) : ControllerBase
    {
        private readonly IMailService _mailService = mailService;

        [HttpPost]
        public async Task<bool> SendMail(MailData Mail_Data)
        {
            return await _mailService.SendMail(Mail_Data);
        }

    }
}
