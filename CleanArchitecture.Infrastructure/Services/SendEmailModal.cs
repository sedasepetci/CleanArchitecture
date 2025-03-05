using System.Net.Mail;

namespace CleanArchitecture.Infrastructure.Services
{
    public class SendEmailModal
    {
        public string Email { get; set; }  
        public string Password { get; set; } 
        public string Smtp { get; set; }    
        public int Port { get; set; }    
        public bool SSL { get; set; }     
        public List<string> Emails { get; set; } 
        public string Subject { get; set; } 
        public string Body { get; set; }   
        public bool Html { get; set; }     
        public List<Attachment> Attachments { get; set; } 
    }
}
