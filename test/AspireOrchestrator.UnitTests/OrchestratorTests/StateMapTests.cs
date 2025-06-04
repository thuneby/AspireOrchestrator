using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.BusinessLogic;

namespace AspireOrchestrator.UnitTests.OrchestratorTests
{
    public class StateMapTests
    {

        [Fact]
        public void TestAllStates()
        {
            // Arrange
            var entity = new EventEntity() { Id = Guid.NewGuid(), ProcessState = ProcessState.Receive, EventType = EventType.HandleReceipt};

            var result = StateMap.GetNextStep(entity);
            Assert.Equal(ProcessState.Parse, result);

            entity.ProcessState = ProcessState.Parse;
            result = StateMap.GetNextStep(entity);
            Assert.Equal(ProcessState.Convert, result);

            entity.ProcessState = ProcessState.Convert;
            result = StateMap.GetNextStep(entity);
            Assert.Equal(ProcessState.Validate, result);

            entity.ProcessState = ProcessState.Validate;
            result = StateMap.GetNextStep(entity);
            Assert.Equal(ProcessState.ProcessPayment, result);

            entity.ProcessState = ProcessState.ProcessPayment;
            result = StateMap.GetNextStep(entity);
            Assert.Equal(ProcessState.GenerateReceipt, result);

            entity.ProcessState = ProcessState.GenerateReceipt;
            result = StateMap.GetNextStep(entity);
            Assert.Equal(ProcessState.TransferResult, result);

            entity.ProcessState = ProcessState.TransferResult;
            result = StateMap.GetNextStep(entity);
            Assert.Equal(ProcessState.WorkFlowCompleted, result);

            entity.ProcessState = ProcessState.WorkFlowCompleted;
            result = StateMap.GetNextStep(entity);
            Assert.Equal(ProcessState.WorkFlowCompleted, result);
        }
    }
}
