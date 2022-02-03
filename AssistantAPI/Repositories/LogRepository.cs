using AssistantAPI.Data;
using AssistantAPI.Interfaces;
using AssistantAPI.Models;

namespace AssistantAPI.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly DataContext _context;
        public LogRepository(DataContext context)
        {
            _context = context;
        }
        public bool AddExceptionLog(ExceptionLog exceptionLog)
        {
            _context.Add(exceptionLog);
            return Save();
        }

        public bool AddRequestLog(RequestLog requestLog)
        {
            _context.Add(requestLog);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
