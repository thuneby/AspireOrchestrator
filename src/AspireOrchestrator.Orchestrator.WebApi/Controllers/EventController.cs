using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Orchestrator.BusinessLogic;
using AspireOrchestrator.Orchestrator.Interfaces;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspireOrchestrator.Orchestrator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly WorkFlowProcessor _workflowProcessor;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ILogger<EventController> _logger;
        private readonly JsonSerializerOptions _options;

        public EventController(IEventRepository eventRepository, IFlowRepository flowRepository, ServiceBusClient client, ILoggerFactory loggerFactory)
        {
            _eventRepository = eventRepository;
            _workflowProcessor = new WorkFlowProcessor(_eventRepository, flowRepository, loggerFactory);
            _serviceBusClient = client;
            _logger = loggerFactory.CreateLogger<EventController>();
            _options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> ExecuteEvent(Guid id)
        {
            var entity = Get(id);
            if (entity == null)
                return NotFound();
            var result = await _workflowProcessor.ProcessEvent(entity);
            return result;
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

        [HttpPost("[action]")]
        public async Task<ActionResult<EventEntity>> SaveAndExecuteEvent([FromBody] EventEntity entity)
        {
            _eventRepository.AddOrUpdateEventEntity(entity);
            var result = await _workflowProcessor.ProcessEvent(entity);
            if (result.EventState == EventState.Error)
            {
                _logger.LogError($"Error processing event {entity.Id}: {result.ErrorMessage}");
                return BadRequest(result);
            }
            if (result.ProcessState != ProcessState.WorkFlowCompleted)
                await PublishEvent(result);
            return result;
        }

        [HttpPost("[action]")]
        public async Task PublishEvent(EventEntity eventEntity, string topicName = "events")
        {
            var sender = _serviceBusClient.CreateSender(topicName);

            var messageJson = JsonSerializer.Serialize(eventEntity, _options);
            // create a message that we can send
            var sbMessage = new ServiceBusMessage(messageJson);

            // send the message
            await sender.SendMessageAsync(sbMessage);
        }

    }
}
