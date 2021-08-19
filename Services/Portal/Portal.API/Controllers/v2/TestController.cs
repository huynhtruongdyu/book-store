using Microsoft.AspNetCore.Mvc;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class TestController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return SuccessResult("ok v2");
        }
    }
}