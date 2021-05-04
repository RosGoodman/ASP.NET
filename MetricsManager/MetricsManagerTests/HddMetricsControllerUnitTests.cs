using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController _controller;

        public HddMetricsControllerUnitTests()
        {
            //_controller = new HddMetricsController();
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
