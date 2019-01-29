using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestService2.Controllers
{
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("get")]
        public object Get([FromServices]IConfiguration configuration)
        {

            return configuration.AsEnumerable();
        }

    }
}
