using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sammo.Sso.Application.ViewModels.Inputs;
using Sammo.Sso.Domain.Core.Bus;
using Sammo.Sso.Domain.Events.Values;
using Sammo.Sso.Infrastructure.Identity.Services;

namespace Sammo.Sso.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ValuesController : ApiBaseController
    {
        private readonly IdentityService _identityService;
        private readonly IMediatorHandler _mediatorHandler;

        public ValuesController(IdentityService identityService, IMediatorHandler mediatorHandler)
        {
            _identityService = identityService;
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginInput input)
        {
            //return Ok("Hello");
            return Succeed(await _identityService.Authenticate(new Infrastructure.Identity.Models.AuthenticateInput { Email = input.Email, Password = input.Password }));
        }

        [HttpGet]
        public Task<IActionResult> Update(int id)
        {
            _mediatorHandler.RaiseEvent(new ValueChangedEvent(id));
            return Task.FromResult(Succeed(result:id));
        }
    }
}