using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sammo.Sso.Application.ViewModels.Inputs;
using Sammo.Sso.Infrastructure.Identity.Services;

namespace Sammo.Sso.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IdentityService _identityService;
        public AccountController(IdentityService identityService)
        {
            _identityService = identityService;
        }
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginInput input)
        {
            return Ok(await _identityService.Authenticate(new Infrastructure.Identity.Models.AuthenticateInput { Email = input.Email, Password = input.Password }));
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}