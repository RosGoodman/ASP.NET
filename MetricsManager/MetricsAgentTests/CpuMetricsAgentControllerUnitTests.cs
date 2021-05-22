using Xunit;
using Moq;
using MetricsAgent.Repositories;
using Microsoft.Extensions.Logging;
using System;
using MetricsAgent.Controllers;

namespace MetricsAgentTests
{
    public class CpuMetricsAgentControllerUnitTests
    {
        private CpuMetricsAgentController _controller;
        private Mock<ICpuMetricsRepository> _mock;
        private Mock<ILogger<CpuMetricsAgentController>> _logger;

        public CpuMetricsAgentControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            _logger = new Mock<ILogger<CpuMetricsAgentController>>();
            _controller = new CpuMetricsAgentController(_mock.Object, _logger.Object);
        }

        [Fact]
        public void GetMetricsFromTimeToTime_ShouldCall_GetMetricsFromeTimeToTime_From_Repository()     //необходимо ли сокращать наименование данного теста?
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(1575598800);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(1576498800);
            _mock.Setup(repository => repository.GetMetricsFromeTimeToTime(fromTime, toTime)).Verifiable();

            //Act
            //var result = _controller.GetMetricsFromTimeToTime(fromTime, toTime);

            //Assert
            _mock.Verify(repository => repository.GetMetricsFromeTimeToTime(fromTime, toTime), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetricsFromAgent_ShouldCall_GetMetricsFromeTimeToTimeFromAgent_From_Repository()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(1575598800);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(1576498800);
            _mock.Setup(repository => repository.GetMetricsFromeTimeToTime(fromTime, toTime)).Verifiable();

            //Act
            //var result = _controller.GetMetricsFromTimeToTime(fromTime, toTime);

            //Assert
            _mock.Verify(repository => repository.GetMetricsFromeTimeToTime(fromTime, toTime), Times.AtMostOnce());
        }
    }
}
