using MetricsManager.Controllers;
using MetricsManager.Models;
using MetricsManager.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentControllerUnitTests
    {
        private AgentController _controller;
        private Mock<IAgentRepository> _mock;
        private Mock<ILogger<AgentController>> _logger;

        public AgentControllerUnitTests()
        {
            _mock = new Mock<IAgentRepository>();
            _logger = new Mock<ILogger<AgentController>>();
            _controller = new AgentController(_mock.Object, _logger.Object);
        }

        [Fact]
        public void GetMetrics_ShouldCall_GetAll_From_Repository()
        {
            //Arrange
            _mock.Setup(repository => repository.GetAll()).Verifiable();

            //Act
            var result = _controller.GetAllAgents();

            //Assert
            _mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }

        [Fact]
        public void GetAgentById_ShouldCall_GetById_From_Repository()
        {
            //Arrange
            int id = 1;
            _mock.Setup(repository => repository.GetById(id)).Verifiable();

            //Act
            var result = _controller.GetAgentById(id);

            //Assert
            _mock.Verify(repository => repository.GetById(id), Times.AtMostOnce());
        }

        [Fact]
        public void CreateAgent_ShouldCall_Create_From_Repository()
        {
            //Arrange
            string name = "agent";
            AgentModel agent = new AgentModel();
            agent.Name = name;

            _mock.Setup(repository => repository.Create(agent)).Verifiable();

            //Act
            var result = _controller.CreateAgent(name);

            //Assert
            _mock.Verify(repository => repository.Create(agent), Times.AtMostOnce());
        }
    }
}
