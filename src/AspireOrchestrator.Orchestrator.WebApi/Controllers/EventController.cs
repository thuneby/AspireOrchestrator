using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.BusinessLogic;
using AspireOrchestrator.Orchestrator.DataAccess;
using AspireOrchestrator.Orchestrator.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspireOrchestrator.Orchestrator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly WorkFlowProcessor _workflowProcessor;
        private readonly ILogger<EventController> _logger;

        public EventController(IEventRepository eventRepository, ILoggerFactory loggerFactory)
        {
            _eventRepository = eventRepository;
            _workflowProcessor = new WorkFlowProcessor(_eventRepository, loggerFactory);
            _logger = loggerFactory.CreateLogger<EventController>();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> ExecuteEvent(Guid id)
        {
            var entity = Get(id);
            if (entity == null)
                return NotFound();
            await _workflowProcessor.ProcessEvent(entity);
            return entity;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> ExecuteFlow(long flowId)
        {
            var entity = await _workflowProcessor.ProcessFlow(flowId);
            return entity;
        }

        //[HttpPost("[action]")]
        //public async Task<ActionResult> GenerateReceiveEvents(long tenantId, EventType eventType) 
        //{
        //    var fileList = await GetFileList(tenantId, eventType);
        //    if (fileList.Count == 0)
        //        return new ObjectResult("No files to download!");
        //    var eventCount = 0;
        //    var result = new List<string>();
        //    foreach (var fileName in fileList)
        //    {
        //        var entity = _eventRepository.AddOrGetEventFromFileName(tenantId, fileName, eventType);
        //        if (entity == null)
        //            continue;
        //        eventCount++;
        //        result.Add(entity.Id.ToString());
        //    }
        //    result.Add(eventCount + " events in event store...");
        //    return new ObjectResult(result);
        //}

        //// ToDo: Implement this method
        //private async Task<List<string>> GetFileList(long tenantId, EventType eventType)
        //{
        //    var source = new CancellationTokenSource();
        //    var cancellationToken = source.Token;
        //    var documentType = ProcessHelper.GetDocumentType(eventType);
        //    var result = new List<string>();
        //    try
        //    {
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.Message, e);
        //    }
        //    return result;
        //}


        // GET: api/<EventController>
        [HttpGet("[action]")]
        public IEnumerable<EventEntity> GetAll(int take = 100)
        {
            return _eventRepository.GetAll(take);
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        public EventEntity? Get(Guid id)
        {
            return _eventRepository.Get(id);
        }

        // POST api/<EventController>
        [HttpPost("[action]")]
        public void Add([FromBody] EventEntity entity)
        {
            _eventRepository.AddEvent(entity);
        }

    }
}
