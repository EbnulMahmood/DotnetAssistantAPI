using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssistantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Task_1", "Task_2", "Task_3" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Task: {id}";
        }
    }
}
