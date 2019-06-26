using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sammo.Sso.Application.ViewModels.Inputs;
using Sammo.Sso.Infrastructure.Identity.Services;

namespace Sammo.Sso.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ValuesController : ApiController
    {
        private readonly IdentityService _identityService;
        public ValuesController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginInput input)
        {
            //return Ok("Hello");
            return Succeed(await _identityService.Authenticate(new Infrastructure.Identity.Models.AuthenticateInput { Email = input.Email, Password = input.Password }));
        }
    }
}