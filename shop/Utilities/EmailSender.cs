using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace shop.Utilities
{
    public class EmailSender : IEmailSender
    {
        private MailOptions _mailOptions;
        private SmtpClient _smtpClient;
        
        public EmailSender(IOptions<MailOptions> mailOptions)
        {
            _mailOptions = mailOptions.Value;
            _smtpClient = new SmtpClient(_mailOptions.Host, _mailOptions.Port);
            _smtpClient.Credentials = new NetworkCredential(_mailOptions.UserName, _mailOptions.Password);
            _smtpClient.EnableSsl = true;
        }
        
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _smtpClient.Send(_mailOptions.UserName, email, subject, htmlMessage);
            
            return Task.CompletedTask;
        }
    }
}