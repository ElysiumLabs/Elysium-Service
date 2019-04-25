using MyCompany.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace MyCompany.Identity.Controllers
{
    [Route("api/[controller]")]
    public class SignInController : ControllerBase
    {
        [HttpGet]
        public IActionResult SignIn([FromServices] IdentityServiceQualquer identityServiceQualquer, [FromServices]IConfiguration configuration)
        {
            return Ok(identityServiceQualquer.Login("balh", "bla") + configuration["SomeInternalConfigInHost"]);
        }
    }
}
