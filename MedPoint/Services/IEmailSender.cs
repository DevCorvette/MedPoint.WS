using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPoint.Services
{
    public interface IEmailSender
    {
        Task SendEmail(string toEmailAddress, string subject, string body, bool isBodyHtml = false);
    }
}
