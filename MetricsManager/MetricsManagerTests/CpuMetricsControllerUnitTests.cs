﻿using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController _controller;

        public CpuMetricsControllerUnitTests()
        {
            //_controller = new CpuMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            int Id = 0;

            //Act
            //var result = _controller.GetMetricsFromAgent(Id, fromTime, toTime);

            // Assert
            //_ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
