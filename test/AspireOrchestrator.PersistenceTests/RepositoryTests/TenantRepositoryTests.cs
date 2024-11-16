using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Orchestrator.DataAccess;
using AspireOrchestrator.PersistenceTests.Common;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.PersistenceTests.RepositoryTests
{
    public class TenantRepositoryTests : TestBase
    {
        private readonly TenantRepository _tenantRepository;

        public TenantRepositoryTests()
        {
            var logger = TestLoggerFactory.CreateLogger<TenantRepository>();
            _tenantRepository = new TenantRepository(OrchestratorContext, logger);
        }

        [Fact]
        public void AddTenant_ShouldAddTenant()
        {
            // Arrange
            var tenant = new Tenant { Id = 999, TenantName = "Test Tenant" };

            // Act
            _tenantRepository.Add(tenant);

            // Assert
            var addedTenant = OrchestratorContext.Tenant.Find(tenant.Id);
            Assert.NotNull(addedTenant);
            Assert.Equal(tenant.TenantName, addedTenant.TenantName);
        }

        [Fact]
        public void GetTenant_ShouldReturnTenant()
        {
            // Arrange
            const int tenantId = 234;
            var tenant = new Tenant { Id = tenantId, TenantName = "Test Tenant" };
            OrchestratorContext.Tenant.Add(tenant);
            OrchestratorContext.SaveChanges();

            // Act
            var result = _tenantRepository.Get(tenantId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tenantId, result.Id);
        }

        [Fact]
        public void DeleteTenant_ShouldRemoveTenant()
        {
            // Arrange
            const int tenantId = 456;
            var tenant = new Tenant { Id = tenantId, TenantName = "Test Tenant" };
            OrchestratorContext.Tenant.Add(tenant);
            OrchestratorContext.SaveChanges();

            // Act
            _tenantRepository.Delete(tenantId);

            // Assert
            var deletedTenant = _tenantRepository.Get(tenantId);
            Assert.Null(deletedTenant);
        }

        [Fact]
        public void UpdateTenant_ShouldUpdateTenant()
        {
            // Arrange
            const int tenantId = 678;
            var tenant = new Tenant { Id = tenantId, TenantName = "Test Tenant" };
            OrchestratorContext.Tenant.Add(tenant);
            OrchestratorContext.SaveChanges();

            // Act
            tenant.TenantName = "Updated Tenant";
            _tenantRepository.Update(tenant);

            // Assert
            var updatedTenant = _tenantRepository.Get(tenantId);
            Assert.NotNull(updatedTenant);
            Assert.Equal("Updated Tenant", updatedTenant.TenantName);
        }

        [Fact]
        public void ShouldFindAllTenants()
        {
            // Arrange
            var tenant1 = new Tenant { Id = 111, TenantName = "Test Tenant" };
            var tenant2 = new Tenant { Id = 222, TenantName = "Test Tenant" };
            _tenantRepository.Add(tenant1);
            _tenantRepository.Add(tenant2);

            // Act
            var tenants = _tenantRepository.GetAll().ToList();

            // Assert
            Assert.NotEmpty(tenants);
            Assert.Equal(2, tenants.Count());
        }

        [Fact]
        public void GetTenant_ShouldReturnNull_WhenTenantDoesNotExist()
        {
            // Arrange
            const int tenantId = 999;

            // Act
            var result = _tenantRepository.Get(tenantId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteTenant_ShouldNotThrowException_WhenTenantDoesNotExist()
        {
            // Arrange
            const int tenantId = 999;

            // Act & Assert
            var exception = Record.Exception(() => _tenantRepository.Delete(tenantId));
            Assert.Null(exception);
        }

        [Fact]
        public void GetAllTenants_ShouldReturnEmptyList_WhenNoTenantsExist()
        {
            // Act
            var tenants = _tenantRepository.GetAll().ToList();

            // Assert
            Assert.Empty(tenants);
        }
    }
}
