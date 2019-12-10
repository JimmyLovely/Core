using Microsoft.AspNetCore.Mvc;
using NetCore.Models;

namespace NetCore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DIController : ControllerBase
    {
        public DIController()
        {

        }

        [HttpGet]
        [Route("demo")]
        public IActionResult Demo()
        {
            Person person = new Person();
            person.name = "Jimmy";
            person.Read();
            person.Play();
            person.money();
            return Ok("hi");
        }

        [HttpGet]
        [Route("demo-with-di")]
        public IActionResult DemoWithDI()
        {
            DIPhone diPhone = new DIPhone();
            DIPerson diPerson = new DIPerson(diPhone);
            diPerson.name = "Jimmy";
            diPerson.Read();
            diPerson.Play();
            diPerson.money();
            return Ok("hi");
        }
    }
}