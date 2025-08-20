using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.ScenarioTests.Processors
{
    public class TestValidationProcessor(ILoggerFactory loggerFactory): IProcessor
    {
        public Task<EventEntity> ProcessEvent(EventEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
