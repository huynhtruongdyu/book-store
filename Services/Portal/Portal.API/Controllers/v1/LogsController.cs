using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Domain.Core;
using Portal.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class LogsController : BaseApiController
    {
        private readonly ILogRepository _logRepository;

        public LogsController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return SuccessResult(_logRepository.GetAll());
        }

        [HttpGet("{days}")]
        public IActionResult GetAll([FromRoute] int days)
        {
            var logs = new List<Log>();

            if (days == 0)
            {
                logs = _logRepository.GetAll();
            }
            else
            {
                logs = _logRepository.GetAll();
                var timeNow = DateTime.Now;
                timeNow = timeNow.AddDays(-Math.Abs(days));
                logs = logs.Where(x => x.TimeStamp >= timeNow).ToList();
            }
            return SuccessResult(logs);
        }
    }
}