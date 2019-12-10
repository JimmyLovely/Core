using Microsoft.AspNetCore.Mvc;

using NetCore.Models;
using NetCore.Interface;

namespace NetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DIWithPersonController : ControllerBase
    {

        private IDIPerson dIPerson;
        public DIWithPersonController(IDIPerson dIPerson)
        {
            this.dIPerson = dIPerson;
        }

        [HttpGet]
        [Route("demo")]
        public IActionResult Demo()
        {
            dIPerson.Read();
            return Ok("Hi");
        }
    }

}