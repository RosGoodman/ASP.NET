using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddMetricsAgentControllerUnitTests
    {
        private HddMetricsAgentController _controller;

        public HddMetricsAgentControllerUnitTests()
        {
            _controller = new HddMetricsAgentController();
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
