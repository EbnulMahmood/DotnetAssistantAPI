using AssistantAPI.Context;
using AssistantAPI.Data;
using AssistantAPI.Interfaces;
using Dapper;
using System.Data.SqlClient;
using AssignTask = AssistantAPI.Models.Task;
using Person = AssistantAPI.Models.Person;

namespace AssistantAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;
        private readonly DapperContext _dapperContext;
        public TaskRepository(DataContext context, DapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }

        private Person? GetPerson(string name)
        {
            return _context.Persons.Where(p => p.Name.Trim().ToLower()
            .Equals(name.Trim().ToLower())).FirstOrDefault();
        }

        public bool Add(AssignTask task)
        {
            var requestedPerson = GetPerson(task.RequestedBy.Name);
            var assignedPerson = GetPerson(task.AssignedTo.Name);
            if (requestedPerson != null) { task.RequestedBy = requestedPerson; }
            if (assignedPerson != null) { task.AssignedTo = assignedPerson; }
            _context.Add(task);
            return Save();
        }

        public ICollection<AssignTask> GetAll()
        {
            var sqlQuery = "SELECT * FROM Tasks";
            using (var connection = _dapperContext.CreateConnection())
            {
                var tasks = connection.Query<AssignTask>(sqlQuery);
                return tasks.ToList();
            }
        }

        public ICollection<AssignTask> GetAssignedTasks(string assignedTo)
        {
            int assignedToId = GetPerson(assignedTo).Id;
            var query = "SELECT * FROM Tasks WHERE AssignedToId LIKE @assignedToId;";
            using (var connection = _dapperContext.CreateConnection())
            {
                var tasks = connection.Query<AssignTask>(query, new { assignedToId });
                return tasks.ToList();
            }
        }

        public ICollection<AssignTask> GetRequestedTasks(string requestedBy)
        {
            int requestedById = GetPerson(requestedBy).Id;
            var query = "SELECT * FROM Tasks WHERE RequestedById LIKE @requestedById;";
            using (var connection = _dapperContext.CreateConnection())
            {
                var tasks = connection.Query<AssignTask>(query, new { requestedById });
                return tasks.ToList();
            }
        }

        private AssignTask? GetTask(int id)
        {
            return _context.Tasks.Where(t => t.Id == id).FirstOrDefault();
        }

        public bool Remove(int id)
        {
            AssignTask task = GetTask(id);
            _context.Remove(task);
            return Save();
        }

        public bool Update(AssignTask task)
        {
            _context.Update(task);
            return Save();
        }

        public bool UpdatePerson(Person person)
        {
            _context.Update(person);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public AssignTask GetDetails(string taskName)
        {
            string name = taskName.Trim().ToLower();
            var query = "SELECT * FROM Tasks WHERE LOWER(Name) LIKE @name";
            using (var connection = _dapperContext.CreateConnection())
            {
                var task = connection.QuerySingleOrDefault<AssignTask>(query, new { name });
                return task;
            }
        }

        public ICollection<AssignTask> GetCompletedTasks(string completedBy)
        {
            int completedById = GetPerson(completedBy).Id;
            bool isComplete = true;
            var query = "SELECT * FROM Tasks WHERE AssignedToId LIKE @completedById AND IsComplete LIKE @isComplete;";
            using (var connection = _dapperContext.CreateConnection())
            {
                var tasks = connection.Query<AssignTask>(query, new { completedById, isComplete });
                return tasks.ToList();
            }
        }
    }
}
