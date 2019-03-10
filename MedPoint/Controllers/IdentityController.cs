using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedPoint.Data.ViewModels;
using MedPoint.Services;
using MedPoint.Templates;

namespace MedPoint.Controllers
{
    [EnableCors("cors")]
    [Route("[controller]")]
    public class IdentityController : Controller
    {
        private IIdentityService IdentityService { get; }
        private IEmailSender EmailSender { get; }

        public IdentityController(IIdentityService identityService, IEmailSender emailSender)
        {
            IdentityService = identityService;
            EmailSender = emailSender;
        }

        [HttpPost(nameof(RegisterByEmail))]
        [Produces("application/json")]
        public async Task<BooleanResponse> RegisterByEmail([FromBody] RegisterByEmailRequest request)
        {
            var token = await IdentityService.RegisterByEmail(request);

            var confirmUrl = Url.Action("ConfirmEmail", "Identity",
                new { Email = request.Email, Token = token }, protocol: HttpContext.Request.Scheme);

            await EmailSender.SendEmail(request.Email, "Confirm your account",
                EmailTemplates.GetConfirmEmailBody(confirmUrl));

            return new BooleanResponse() { Success = true };
        }

        public class ConfirmEmailRequest
        {
            public string Email { get; set; }
            public string Token { get; set; }
        }

        [HttpGet(nameof(ConfirmEmail))]
        public async Task<RedirectResult> ConfirmEmail(ConfirmEmailRequest request)
        {
            var url = await IdentityService.ConfirmEmail(request.Email, request.Token);
            return Redirect(url);
        }
    }
}
