using MetricsAgent.Controllers;
using Xunit;
using Moq;
using MetricsAgent.Repositories;
using Microsoft.Extensions.Logging;
using System;

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
        public void GetMetrics_ShouldCall_GetAll_From_Repository()
        {
            //Arrange

            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mock.Setup(repository => repository.GetAll()).Verifiable();

            //Act

            // выполняем действие на контроллере
            var result = _controller.GetMetrics();

            //Assert

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
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
