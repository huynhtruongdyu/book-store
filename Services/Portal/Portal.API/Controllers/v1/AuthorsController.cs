using Mapster;
using Microsoft.AspNetCore.Mvc;
using Portal.Domain.Models;
using Portal.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Portal.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthorsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var authors = _unitOfWork.AuthorRepository.GetAll();
            if (authors.Any())
            {
                var authorsModel = authors.Adapt<List<AuthorModel>>();
                return SuccessResult(authorsModel);
            }
            return ErrorResult("no.author.data");
        }
    }
}