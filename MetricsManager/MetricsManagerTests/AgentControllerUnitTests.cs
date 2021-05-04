using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentControllerUnitTests
    {
        private AgentController _controller;

        public AgentControllerUnitTests()
        {
            //_controller = new AgentController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange

            //Act
            var result = _controller.GetAllAgents();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
