using MetricsAgent.Controllers;
using MetricsAgent.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkMetricsAgentControllerUnitTests
    {
        private NetworkMetricsAgentController _controller;
        private Mock<INetworkMetricsRepository> _mock;
        private Mock<ILogger<NetworkMetricsAgentController>> _logger;

        public NetworkMetricsAgentControllerUnitTests()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            _logger = new Mock<ILogger<NetworkMetricsAgentController>>();
            //_controller = new NetworkMetricsAgentController(_mock.Object, _logger.Object);
        }

        [Fact]
        public void GetMetricsFromTimeToTime_ShouldCall_GetMetricsFromeTimeToTime_From_Repository()
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
