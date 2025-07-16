using AspireOrchestrator.Orchestrator.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using ValidationContext = AspireOrchestrator.Validation.DataAccess.ValidationContext;

namespace AspireOrchestrator.PersistenceTests.Common
{
    public class TestBase
    {
        protected readonly OrchestratorContext OrchestratorContext = InitializeOrchestratorContext();
        protected readonly ValidationContext ValidationContext = InitializeValidationContect();

        private static ValidationContext InitializeValidationContect()
        {
            var options = new DbContextOptionsBuilder<ValidationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var context = new ValidationContext(options);
            return context;
        }

        protected ILoggerFactory TestLoggerFactory = InitializeLoggerFactory();


        private static OrchestratorContext InitializeOrchestratorContext()
        {
            var options = new DbContextOptionsBuilder<OrchestratorContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var context = new OrchestratorContext(options);
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
