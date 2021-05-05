using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsAgentControllerUnitTests
    {
        private CpuMetricsAgentController _controller;

        public CpuMetricsAgentControllerUnitTests()
        {
            //_controller = new CpuMetricsAgentController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            int id = 0;

            //Act
            var result = _controller.GetMetricsFromAgent(id, fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
