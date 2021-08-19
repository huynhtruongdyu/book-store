using Portal.Domain.Core;
using Portal.Infrastructure.EF;

namespace Portal.Infrastructure.Repositories
{
    public interface ILogRepository : IBaseRepository<Log>
    {
    }

    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        public LogRepository(BookDbContext context) : base(context)
        {
        }
    }
}