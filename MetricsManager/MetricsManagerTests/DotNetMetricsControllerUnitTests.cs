using MetricsManager.Controllers;
using MetricsManager.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class DotNetMetricsControllerUnitTests
    {
        private DotNetMetricsController _controller;
        private Mock<IDotNetMetricsRepository> _mock;
        private Mock<ILogger<DotNetMetricsController>> _logger;

        public DotNetMetricsControllerUnitTests()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            _logger = new Mock<ILogger<DotNetMetricsController>>();
            _controller = new DotNetMetricsController(_mock.Object, _logger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ShouldCall_GetById_From_Repository()
        {
            //Arrange
            int id = 1;
            long recordNumb = 2;
            _mock.Setup(repository => repository.GetByRecordNumb(id, recordNumb)).Verifiable();

            //Act
            var result = _controller.GetMetricsFromAgent(id, recordNumb);

            //Assert
            _mock.Verify(repository => repository.GetByRecordNumb(id, recordNumb), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetricsFromAgent_ShouldCall_GetMetricsFromeTimeToTimeFromAgent_From_Repository()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(1575598800);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(1576498800);
            int id = 1;
            _mock.Setup(repository => repository.GetMetricsFromeTimeToTimeFromAgent(id, fromTime, toTime)).Verifiable();

            //Act
            var result = _controller.GetMetricsFromAgent(id, fromTime, toTime);

            //Assert
            _mock.Verify(repository => repository.GetMetricsFromeTimeToTimeFromAgent(id, fromTime, toTime), Times.AtMostOnce());
        }
    }
}
