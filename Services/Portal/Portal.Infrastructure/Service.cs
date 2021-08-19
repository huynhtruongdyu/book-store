using Portal.Infrastructure.Services;

namespace Portal.Infrastructure
{
    public interface IService
    {
        public IAuthorService AuthorService { get; }
    }

    public class Service : IService
    {
        public IAuthorService AuthorService { get; }

        public Service(
            IAuthorService authorService)
        {
            AuthorService = authorService;
        }
    }
}