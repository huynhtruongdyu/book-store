using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.API.Models;
using System;
using System.Collections.Generic;

namespace Portal.API.Controllers
{
    [ApiController]
    [Authorize]
    public class BaseApiController : ControllerBase
    {
        [NonAction]
        public IActionResult ErrorResult(string errorMessage, List<string> localeParams = null, int? statusCode = null)
        {
            var errorMessages = new List<string> { errorMessage };
            var resp = new ErrorResponseModel
            {
                Status = false,
                StatusCode = statusCode,
                Message = errorMessages,
                LocaleParams = localeParams
            };
            return Ok(resp);
        }

        [NonAction]
        public IActionResult ErrorResult(List<string> errorMessages, List<string> localeParams = null, int? statusCode = null)
        {
            var resp = new ErrorResponseModel
            {
                Status = false,
                StatusCode = statusCode,
                Message = errorMessages,
                LocaleParams = localeParams
            };
            return Ok(resp);
        }

        [NonAction]
        public IActionResult ErrorResult(Exception error, List<string> localeParams = null, int? statusCode = null)
        {
            var errorMessages = new List<string> { error.Message };
            var dataResult = new ErrorResponseModel
            {
                Status = false,
                StatusCode = statusCode,
                Message = errorMessages,
                LocaleParams = localeParams
            };
            return Ok(dataResult);
        }

        [NonAction]
        public IActionResult SuccessResult(string message, List<string> localeParams = null)
        {
            var dataResult = new SuccessResponseModel<object>
            {
                Status = true,
                Message = message,
                LocaleParams = localeParams
            };
            return Ok(dataResult);
        }

        [NonAction]
        public IActionResult SuccessResult(object obj, string message = null, List<string> localeParams = null)
        {
            var dataResult = new SuccessResponseModel<object>
            {
                Status = true,
                Message = message,
                Data = obj,
                LocaleParams = localeParams
            };
            return Ok(dataResult);
        }

        [NonAction]
        public IActionResult FileResult(byte[] file, string fileName, string contentType)
        {
            if (file == null) return ErrorResult("No results were found for your selections.");
            return File(file, contentType, fileName);
        }
    }
}