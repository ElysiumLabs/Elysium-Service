using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestService2.Controllers
{
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("get")]
        public object Get([FromServices]IMediator mediator)
        {

            return mediator != null;
        }

    }
}
