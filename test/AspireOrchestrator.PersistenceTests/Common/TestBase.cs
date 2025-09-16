using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Orchestrator.DataAccess;
using AspireOrchestrator.Transfer.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using ValidationContext = AspireOrchestrator.Validation.DataAccess.ValidationContext;

namespace AspireOrchestrator.PersistenceTests.Common
{
    public class TestBase
    {
        protected readonly OrchestratorContext OrchestratorContext = InitializeOrchestratorContext();
        protected readonly DomainContext DomainContext = InitializeDomainContext();
        protected readonly ValidationContext ValidationContext = InitializeValidationContect();
        protected readonly TransferContext TransferContext = InitializeTransferContext();

        protected readonly ILoggerFactory TestLoggerFactory = InitializeLoggerFactory();

        private static OrchestratorContext InitializeOrchestratorContext()
        {
            var options = new DbContextOptionsBuilder<OrchestratorContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var context = new OrchestratorContext(options);
            return context;
        }

        private static DomainContext InitializeDomainContext()
        {
            var options = new DbContextOptionsBuilder<DomainContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var context = new DomainContext(options);
            return context;
        }

        private static ValidationContext InitializeValidationContect()
        {
            var options = new DbContextOptionsBuilder<ValidationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var context = new ValidationContext(options);
            return context;
        }

        private static TransferContext InitializeTransferContext()
        {
            var options = new DbContextOptionsBuilder<TransferContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var context = new TransferContext(options);
            return context;
        }


        private static ILoggerFactory InitializeLoggerFactory()
        {
            var loggerFactory = new LoggerFactory();
            return loggerFactory;
        }

        public static void TestCleanup()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
