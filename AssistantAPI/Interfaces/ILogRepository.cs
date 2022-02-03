using AssistantAPI.Models;

namespace AssistantAPI.Interfaces
{
    public interface ILogRepository
    {
        bool AddRequestLog(RequestLog requestLog);
        bool AddExceptionLog(ExceptionLog exceptionLog);
    }
}
