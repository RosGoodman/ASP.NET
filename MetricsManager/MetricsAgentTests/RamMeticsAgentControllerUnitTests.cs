using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMeticsAgentControllerUnitTests
    {
        private RamMeticsAgentController _controller;

        public RamMeticsAgentControllerUnitTests()
        {
            _controller = new RamMeticsAgentController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            var result = _controller.GetMetricsFromAgent();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
