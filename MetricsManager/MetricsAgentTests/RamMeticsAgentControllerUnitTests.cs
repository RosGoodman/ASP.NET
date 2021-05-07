using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMeticsAgentControllerUnitTests
    {
        private RamMetricsAgentController _controller;

        public RamMeticsAgentControllerUnitTests()
        {
            //_controller = new RamMeticsAgentController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            int id = 0;

            //Act
            //var result = _controller.GetMetricsFromAgent(id);

            // Assert
            //_ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
