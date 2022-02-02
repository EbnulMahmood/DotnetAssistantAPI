using AssignTask = AssistantAPI.Models.Task;
using AssistantAPI.Models;

namespace AssistantAPI.Interfaces
{
    public interface ITaskRepository
    {
        bool Add(AssignTask task);
        bool Save();
        ICollection<AssignTask> GetAll();
        AssignTask GetDetails(string taskName);
        ICollection<AssignTask> GetAssignedTasks(string assignedTo);
        ICollection<AssignTask> GetRequestedTasks(string requestedBy);
        ICollection<AssignTask> GetCompletedTasks(string completedBy);
        bool Remove(int id);
        bool Update(AssignTask task);
        bool UpdatePerson(Person person);
    }
}
