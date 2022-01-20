namespace Assistant.Data.Interfaces
{
    internal interface ITaskRepository
    {
        void Add(Task item);
        IEnumerable<Task> GetAll();
        Task Find(string key);
        Task Remove(string key);
        void Update(Task item);
    }
}
