using Elysium;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Host.Playground.Controllers
{
    [Route("api/[controller]")]
    public class DevTestController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public object Get([FromServices]IService hostService)
        {
            return new { ServiceInfo = (ServiceInfo)hostService.Options, hostService.Status };
        }

       
    }
}