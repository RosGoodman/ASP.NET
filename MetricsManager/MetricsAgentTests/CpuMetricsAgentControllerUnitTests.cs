using MetricsAgent.Controllers;
using Xunit;
using Moq;
using MetricsAgent.Repositories;
using Microsoft.Extensions.Logging;
using System;
using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Responses;

namespace MetricsAgentTests
{
    public class CpuMetricsAgentControllerUnitTests
    {
        private CpuMetricsAgentController _controller;
        private Mock<ICpuMetricsRepository> _mock;
        private Mock<ILogger<CpuMetricsAgentController>> _logger;
        private MapperConfiguration _config;
        private IMapper _mapper;

        public CpuMetricsAgentControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            _logger = new Mock<ILogger<CpuMetricsAgentController>>();
            _config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetricsModel, CpuMetricsDto>());
            _mapper = _config.CreateMapper();

            _controller = new CpuMetricsAgentController(_mapper, _mock.Object, _logger.Object);
        }

        [Fact]
        public void GetMetrics_ShouldCall_GetAll_From_Repository()
        {
            //Arrange
            _mock.Setup(repository => repository.GetAll()).Verifiable();

            //Act
            var result = _controller.GetMetrics();

            //Assert
            _mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetricsFromTimeToTime_ShouldCall_GetMetricsFromeTimeToTime_From_Repository()     //необходимо ли сокращать наименование данного теста?
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(1575598800);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(1576498800);
            int id = 1;
            _mock.Setup(repository => repository.GetMetricsFromeTimeToTime(id, fromTime, toTime)).Verifiable();

            //Act
            var result = _controller.GetMetricsFromTimeToTime(id, fromTime, toTime);

            //Assert
            _mock.Verify(repository => repository.GetMetricsFromeTimeToTime(id, fromTime, toTime), Times.AtMostOnce());
        }
    }
}
