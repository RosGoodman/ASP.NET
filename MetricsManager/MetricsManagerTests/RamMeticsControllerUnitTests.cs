using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class RamMeticsControllerUnitTests
    {
        private RamMetricsController _controller;

        public RamMeticsControllerUnitTests()
        {
            //_controller = new RamMeticsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            int Id = 0;

            //Act
            var result = _controller.GetMetricsFromAgent(Id);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
