using AssistantAPI.Interfaces;
using AssignTask = AssistantAPI.Models.Task;
using Person = AssistantAPI.Models.Person;
using ExceptionLog = AssistantAPI.Models.ExceptionLog;
using Microsoft.AspNetCore.Mvc;

namespace AssistantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogRepository _logRepository;
        public TaskController(ITaskRepository taskRepository, ILogRepository logRepository)
        {
            _taskRepository = taskRepository;
            _logRepository = logRepository;
        }

        private void AddExceptionLog(Exception ex)
        {
            ExceptionLog exceptionLog = new()
            {
                Message = ex.Message,
            };
            _logRepository.AddExceptionLog(exceptionLog);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTask([FromBody] AssignTask assignTask)
        {
            try
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
                if (!_taskRepository.Add(assignTask))
                {
                    ModelState.AddModelError("", "Something went wrong while saving!");
                    return StatusCode(500, ModelState);
                }
                return Ok("Successfully created!");
            }
            catch (Exception ex)
            {
                AddExceptionLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTask(int taskId, [FromBody] AssignTask task)
        {
            try
            {
                if (task == null) { return BadRequest(ModelState); }
                if (taskId != task.Id) { return NotFound(); }
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                if (!_taskRepository.Update(task))
                {
                    ModelState.AddModelError("", "Something went wrong updating task!");
                    return StatusCode(500, ModelState);
                }
                return Ok("Successfully updated!");
            }
            catch (Exception ex)
            {
                AddExceptionLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("person/{personId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePerson(int personId, [FromBody] Person person)
        {
            try
            {
                if (person == null) { return BadRequest(ModelState); }
                if (personId != person.Id) { return NotFound(); }
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                if (!_taskRepository.UpdatePerson(person))
                {
                    ModelState.AddModelError("", "Something went wrong updating person!");
                    return StatusCode(500, ModelState);
                }
                return Ok("Successfully updated!");
            }
            catch (Exception ex)
            {
                AddExceptionLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("assigned/{assignedTo}")]
        public IActionResult GetAssignedTasks(string assignedTo)
        {
            try
            {
                var tasks =  _taskRepository.GetAssignedTasks(assignedTo);
                if (tasks == null)
                    return NotFound();

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                AddExceptionLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("requested/{requestedBy}")]
        public IActionResult GetRequestedTasks(string requestedBy)
        {
            try
            {
                var tasks = _taskRepository.GetRequestedTasks(requestedBy);
                if (tasks == null)
                    return NotFound();

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                AddExceptionLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("completed/{completedBy}")]
        public IActionResult GetCompletedTasks(string completedBy)
        {
            try
            {
                var tasks = _taskRepository.GetCompletedTasks(completedBy);
                if (tasks == null)
                    return NotFound();

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                AddExceptionLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AssignTask>))]
        public IActionResult GetTasks()
        {
            try
            {
                var tasks = _taskRepository.GetAll();
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                AddExceptionLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{taskName}")]
        public IActionResult GetTaskDetails(string taskName)
        {
            try
            {
                var tasks = _taskRepository.GetDetails(taskName);
                if (tasks == null)
                    return NotFound();

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                AddExceptionLog(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            try
            {
                if (!_taskRepository.Remove(taskId))
                    ModelState.AddModelError("", "Something went wrong deleting task!");

                return Ok("Successfully deleted!");
            }
            catch (Exception ex)
            {
                AddExceptionLog(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
