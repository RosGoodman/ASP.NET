using MetricsManager.Controllers;
using MetricsManager.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController _controller;
        private Mock<ICpuMetricsRepository> _mock;
        private Mock<ILogger<CpuMetricsController>> _logger;

        public CpuMetricsControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            _logger = new Mock<ILogger<CpuMetricsController>>();
            ////_controller = new CpuMetricsController(_mock.Object, _logger.Object);
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
