using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class EmailService
    {
       
        public async Task SendEmailAsync(SendEmailModal sendEmailModal)
        {
            using (var smtpClient = new SmtpClient(sendEmailModal.Smtp))
            {
                smtpClient.Port = sendEmailModal.Port;
                smtpClient.Credentials = new NetworkCredential(sendEmailModal.Email, sendEmailModal.Password);
                smtpClient.EnableSsl = sendEmailModal.SSL;

                using (var mailMessage = new MailMessage())
                {
                  
                    mailMessage.From = new MailAddress(sendEmailModal.Email);

                  
                    foreach (var email in sendEmailModal.Emails)
                    {
                        mailMessage.To.Add(email);
                    }

                 
                    mailMessage.Subject = sendEmailModal.Subject;
                    mailMessage.Body = sendEmailModal.Body;
                    mailMessage.IsBodyHtml = sendEmailModal.Html;

                   
                    if (sendEmailModal.Attachments != null)
                    {
                        foreach (var attachment in sendEmailModal.Attachments)
                        {
                            mailMessage.Attachments.Add(attachment);
                        }
                    }

                 
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }
    }
}
