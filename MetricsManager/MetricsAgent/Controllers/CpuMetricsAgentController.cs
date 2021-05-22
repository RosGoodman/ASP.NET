﻿using AutoMapper;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.Models;
using MetricsAgent.Repositories;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsAgentController : ControllerBase
    {
        private readonly ILogger<CpuMetricsAgentController> _logger;
        private readonly ICpuMetricsRepository _repository;

        public CpuMetricsAgentController(ICpuMetricsRepository repository, ILogger<CpuMetricsAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("greeting")]
        public IActionResult GreetingMethod()
        {
            return Ok("Программа запущена!");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromTimeToTime([FromRoute] CpuMetricsRequest request)
        {

            _logger.LogInformation($"Запрос на получение метрик CPU (" +
                $"fromTime = {request.FromTime:yyyy-M-d}," +
                $" toTime = {request.ToTime:yyyy-M-d})");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetricsModel, CpuMetricsDto>());
            var m = config.CreateMapper();
            IList<CpuMetricsModel> metrics = _repository.GetMetricsFromeTimeToTime(request.FromTime, request.ToTime);

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<CpuMetricsDto>(metric));
            }

            return Ok(metrics);
        }
    }
}

