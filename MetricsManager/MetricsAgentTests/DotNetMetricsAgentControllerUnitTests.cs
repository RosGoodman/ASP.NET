using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetMetricsAgentControllerUnitTests
    {
        private DotNetMetricsAgentController _controller;

        public DotNetMetricsAgentControllerUnitTests()
        {
            //_controller = new DotNetMetricsAgentController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            int id = 0;

            //Act
            //var result = _controller.GetMetricsFromAgent(id, fromTime, toTime);

            // Assert
            //_ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
