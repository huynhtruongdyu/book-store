using Portal.Infrastructure.EF;
using Portal.Infrastructure.Repositories;
using System;

namespace Portal.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();

        IBookRepository BookRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        ILogRepository LogRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private BookDbContext Context;
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;
        private ILogRepository _logRepository;

        public UnitOfWork(
            BookDbContext context)
        {
            Context = context;
        }

        public IBookRepository BookRepository
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(Context);
                return _bookRepository;
            }
        }

        public IAuthorRepository AuthorRepository
        {
            get
            {
                if (_authorRepository == null)
                    _authorRepository = new AuthorRepository(Context);
                return _authorRepository;
            }
        }

        public ILogRepository LogRepository
        {
            get
            {
                if (_logRepository == null)
                    _logRepository = new LogRepository(Context);
                return _logRepository;
            }
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}