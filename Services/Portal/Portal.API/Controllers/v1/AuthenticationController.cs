using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portal.Domain.Core.Auth;
using Portal.Domain.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthenticationController: BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly UserManager<User> _userManager;
        public AuthenticationController(
            IUnitOfWork unitOfWork,
            ILogger<AuthenticationController> logger,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserCreateReqModel userCreateReqModel)
        {
            var user = userCreateReqModel.Adapt<User>();
            var result = await _userManager.CreateAsync(user, userCreateReqModel.Password);
            if (!result.Succeeded)
            {
                //foreach (var error in result.Errors)
                //{
                //    ModelState.TryAddModelError(error.Code, error.Description);
                //}
                return ErrorResult(result.Errors.Select(x => $"{x.Code} - {x.Description}").ToList());
            }
            await _userManager.AddToRolesAsync(user, userCreateReqModel.Roles);
            //return SuccessResult("created");
            return StatusCode(201);
        }
    }
}
