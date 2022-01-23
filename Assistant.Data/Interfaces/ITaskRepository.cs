using Task = Assistant.Data.Models.Task;

namespace Assistant.Data.Interfaces
{
    public interface ITaskRepository
    {
        void Add(Task item);
        ICollection<Task> GetAll();
        Task Find(int id);
        Task Remove(int id);
        void Update(Task item);
    }
}
