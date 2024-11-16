using AspireOrchestrator.Orchestrator.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.PersistenceTests.Common
{
    public class TestBase
    {
        protected OrchestratorContext OrchestratorContext = InitializeOrchestratorContext();
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
