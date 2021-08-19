using Portal.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Services
{
    public interface IAuthorService
    {
        List<Author> GetAll();
        void ExprotAuthor();
    }
    public class AuthorService:IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Author> GetAll()
        {
            return _unitOfWork.AuthorRepository.GetAll();
        }

        public void ExprotAuthor()
        {
            var data = _unitOfWork.AuthorRepository.GetDataForExport();
            //process excel
        }
    }
}
