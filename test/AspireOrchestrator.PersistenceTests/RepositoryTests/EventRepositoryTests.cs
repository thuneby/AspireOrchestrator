using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.DataAccess;
using AspireOrchestrator.PersistenceTests.Common;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.PersistenceTests.RepositoryTests
{
    public class EventRepositoryTests : TestBase
    {
        private readonly Tenant _baseTenant = new Tenant { Id = 1, TenantName = "Test Tenant" };
        private readonly EventRepository _eventRepository;

        public EventRepositoryTests()
        {
            var logger = TestLoggerFactory.CreateLogger<EventRepository>();
            _eventRepository = new EventRepository(OrchestratorContext, logger, _baseTenant);
        }

        [Fact]
        public void AddEvent_ShouldAddEvent()
        {
            // Arrange
            var entity = new EventEntity { Id = Guid.NewGuid(), Parameters = "Test" };

            // Act
            _eventRepository.Add(entity);

            // Assert
            var addedEntity = OrchestratorContext.EventEntity.Find(entity.Id);
            Assert.NotNull(addedEntity);
            Assert.Equal("Test", addedEntity.Parameters);
        }

        [Fact]
        public void GetEvent_ShouldReturnEvent()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var entity = new EventEntity { Id = eventId, Parameters = "Test" };
            OrchestratorContext.EventEntity.Add(entity);
            OrchestratorContext.SaveChanges();

            // Act
            var result = _eventRepository.Get(eventId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(eventId, result.Id);
        }

        [Fact]
        public void GetEvent_ShouldReturnNull_WhenEventDoesNotExist()
        {
            // Arrange
            var eventId = Guid.NewGuid();

            // Act
            var result = _eventRepository.Get(eventId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteEvent_ShouldRemoveEvent()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var entity = new EventEntity { Id = eventId, Parameters = "Test" };
            OrchestratorContext.EventEntity.Add(entity);
            OrchestratorContext.SaveChanges();

            // Act
            _eventRepository.Delete(eventId);

            // Assert
            var deletedEntity = OrchestratorContext.EventEntity.Find(eventId);
            Assert.Null(deletedEntity);
        }

        [Fact]
        public void DeleteEvent_ShouldNotThrowException_WhenEventDoesNotExist()
        {
            // Arrange
            var eventId = Guid.NewGuid();

            // Act & Assert
            var exception = Record.Exception(() => _eventRepository.Delete(eventId));
            Assert.Null(exception);
        }

        [Fact]
        public void UpdateEvent_ShouldUpdateEvent()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var entity = new EventEntity { Id = eventId, Parameters = "Test" };
            OrchestratorContext.EventEntity.Add(entity);
            OrchestratorContext.SaveChanges();

            // Act
            entity.Parameters = "Updated Test";
            _eventRepository.Update(entity);

            // Assert
            var updatedEntity = OrchestratorContext.EventEntity.Find(eventId);
            Assert.NotNull(updatedEntity);
            Assert.Equal("Updated Test", updatedEntity.Parameters);
        }

        [Fact]
        public void GetEventsByTenant_ShouldReturnEventsForSpecificTenant()
        {
            // Arrange
            const int tenantId = 1;
            var entity1 = new EventEntity { Id = Guid.NewGuid(), Parameters = "Test1", TenantId = tenantId };
            var entity2 = new EventEntity { Id = Guid.NewGuid(), Parameters = "Test2", TenantId = tenantId };
            var entity3 = new EventEntity { Id = Guid.NewGuid(), Parameters = "Test3", TenantId = 2 };
            OrchestratorContext.EventEntity.AddRange(entity1, entity2, entity3);
            OrchestratorContext.SaveChanges();

            // Act
            var events = _eventRepository.GetEventsByTenant(tenantId).ToList();

            // Assert
            Assert.NotEmpty(events);
            Assert.Equal(2, events.Count);
            Assert.All(events, e => Assert.Equal(tenantId, e.TenantId));
        }
    }
}
