using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portal.Domain.Core.Auth;
using Portal.Domain.Models;
using Portal.Infrastructure;
using Portal.Infrastructure.Services;
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
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(
            IUnitOfWork unitOfWork,
            ILogger<AuthenticationController> logger,
            UserManager<User> userManager,
            IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
            _authenticationService = authenticationService;
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginModel user)
        {
            if (!await _authenticationService.ValidateUser(user))
            {
                _logger.LogWarning($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password.");
                return ErrorResult("wrong.user.name.or.password");
            }
            var response = new { Token = await _authenticationService.CreateToken() };
            return SuccessResult(response);
        }
    }
}
