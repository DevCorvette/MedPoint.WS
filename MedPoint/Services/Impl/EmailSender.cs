using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MedPoint.Services.Impl
{
    public class EmailSender : IEmailSender
    {
        private EmailSettings EmailSettings { get; }

        public EmailSender(IOptions<AppSettings> options)
        {
            EmailSettings = options.Value.EmailSettings;
        }

        public async Task SendEmail(string toEmailAddress, string subject, string body, bool isBodyHtml = false)
        {
            using (var message = new MailMessage())
            {
                var fromEmail = new MailAddress(EmailSettings.SMTPUserName, EmailSettings.DisplayName);

                message.From = fromEmail;
                message.Sender = fromEmail;
                message.To.Add(toEmailAddress);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isBodyHtml;

                using (var smtpClient = new SmtpClient(EmailSettings.SMTPServer, EmailSettings.SMPTPort)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(EmailSettings.SMTPUserName, EmailSettings.SMPTPassword),
                    EnableSsl = true
                })
                {
                    await smtpClient.SendMailAsync(message);
                }
            }
        }
    }
}
