using System.Collections.Generic;
using System.Linq;
using Log = my_books.Data.Models.Log;

namespace my_books.Data.Services
{
    public class LogsService
    {
        private readonly AppDbContext _appDbContext;

        public LogsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Log> GetAllLogs() => _appDbContext.Logs.ToList();
    }
}