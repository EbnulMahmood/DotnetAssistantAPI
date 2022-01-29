using AssistantAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using AssignTask = AssistantAPI.Models.Task;
using Person = AssistantAPI.Models.Person;
using Microsoft.AspNetCore.Mvc;

namespace AssistantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AssignTask>))]
        public IActionResult GetTasks()
        {
            var tasks = _taskRepository.GetAll();
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(tasks);
        }
    }
}
