using Portal.Domain.Core;
using Portal.Infrastructure.EF;

namespace Portal.Infrastructure.Repositories
{
    public interface IBookRepository
    {
    }

    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(BookDbContext context) : base(context)
        {
        }
    }
}