using CleanArchitecture.Application.Services;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public sealed class MailService : IMailService
    {
        private readonly EmailService _emailService;

     
        public MailService(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendMailAsync(List<string> emails, string subject, string body, List<Attachment> attachments = null)
        {
           
            SendEmailModal sendEmailModal = new()
            {
                Emails = emails,
                Subject = subject,
                Body = body,
                Html = true, 
                Attachments = attachments,
                Smtp = "smtp.gmail.com", 
                Port = 587,               
                SSL = true,              
                Email = "your-email@gmail.com", 
                Password = "your-email-password" 
            };

           
            await _emailService.SendEmailAsync(sendEmailModal);
        }
    }
}
