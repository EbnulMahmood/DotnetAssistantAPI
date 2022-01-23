using Assistant.Data.Interfaces;
using Assistant.Data.Models;
using Task = Assistant.Data.Models.Task;

namespace Assistant.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public void Add(Task item)
        {
            throw new NotImplementedException();
        }

        public Task Find(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Task> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Task item)
        {
            throw new NotImplementedException();
        }
    }
}
