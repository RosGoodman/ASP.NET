using MetricsManager.Controllers;
using MetricsManager.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController _controller;
        private Mock<IRamMetricsRepository> _mock;
        private Mock<ILogger<RamMetricsController>> _logger;

        public RamMetricsControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();
            _logger = new Mock<ILogger<RamMetricsController>>();
            _controller = new RamMetricsController(_mock.Object, _logger.Object);
        }

        [Fact]
        public void GetMetricsAll_ShouldCall_GetAll_From_Repository()
        {
            //Arrange
            _mock.Setup(repository => repository.GetAll()).Verifiable();

            //Act
            //var result = _controller.GetMetricsAll();

            //Assert
            _mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetricsFromAgent_ShouldCall_GetById_From_Repository()
        {
            //Arrange
            long id = 1;
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
