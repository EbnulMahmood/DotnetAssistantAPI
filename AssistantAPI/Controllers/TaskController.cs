using AssistantAPI.Interfaces;
using AssignTask = AssistantAPI.Models.Task;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTask([FromBody] AssignTask assignTask)
        {
            if (assignTask == null) { return BadRequest(ModelState); }
            var task = _taskRepository.GetAll()
                .Where(t => t.Name.Trim().ToLower().Equals(assignTask.Name.Trim().ToLower()))
                .FirstOrDefault();
            if (task != null)
            {
                ModelState.AddModelError("", "Task already exists!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if(!_taskRepository.Add(assignTask))
            {
                ModelState.AddModelError("", "Something went wrong while saving!");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created!");
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
