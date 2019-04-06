using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPoint.Templates
{
    //it may be expand to use HTML template
    public static class EmailTemplates
    {
        public static string GetConfirmEmailBody(string confirmUrl)
        {
            return $"Please confirm your account by clicking this link: {confirmUrl}";
        }
    }
}
