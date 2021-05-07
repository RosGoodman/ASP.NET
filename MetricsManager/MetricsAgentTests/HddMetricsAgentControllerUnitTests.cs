using MetricsAgent.Controllers;
using MetricsAgent.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddMetricsAgentControllerUnitTests
    {
        private HddMetricsAgentController _controller;
        private Mock<IHddMetricsRepository> _mock;
        private Mock<ILogger<HddMetricsAgentController>> _logger;

        public HddMetricsAgentControllerUnitTests()
        {
            _mock = new Mock<IHddMetricsRepository>();
            _logger = new Mock<ILogger<HddMetricsAgentController>>();
            _controller = new HddMetricsAgentController(_mock.Object, _logger.Object);
        }

        //[Fact]
        //public void GetMetrics_ShouldCall_GetAll_From_Repository()
        //{
        //    //Arrange
        //    _mock.Setup(repository => repository.GetAll()).Verifiable();

        //    //Act
        //    var result = _controller.GetMetrics();

        //    //Assert
        //    _mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());

        //}

        //[Fact]
        //public void GetMetricsFromTimeToTime_ShouldCall_GetMetricsFromeTimeToTime_From_Repository()
        //{
        //    //Arrange
        //    var fromTime = DateTimeOffset.FromUnixTimeSeconds(1575598800);
        //    var toTime = DateTimeOffset.FromUnixTimeSeconds(1576498800);
        //    int id = 1;
        //    _mock.Setup(repository => repository.GetMetricsFromeTimeToTime(id, fromTime, toTime)).Verifiable();

        //    //Act
        //    var result = _controller.GetMetricsFromTimeToTime(id, fromTime, toTime);

        //    //Assert
        //    _mock.Verify(repository => repository.GetMetricsFromeTimeToTime(id, fromTime, toTime), Times.AtMostOnce());
        //}
    }
}
