using Portal.Domain.Core;
using Portal.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories
{
    public interface IAuthorRepository: IBaseRepository<Author>
    {
        List<Author> GetDataForExport();
    }
    public class AuthorRepository:BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookDbContext context) : base(context)
        {
        }

        public List<Author> GetDataForExport()
        {
            //get data from Store Procedure
            return new List<Author>();
        }
    }
}
