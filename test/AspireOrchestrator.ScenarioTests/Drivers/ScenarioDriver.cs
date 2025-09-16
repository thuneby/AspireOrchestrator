using System.Text.Json;
using AspireOrchestrator.Accounting.Business;
using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.DataAccess;
using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Orchestrator.BusinessLogic;
using AspireOrchestrator.Orchestrator.DataAccess;
using AspireOrchestrator.Parsing.Business;
using AspireOrchestrator.Parsing.WebApi.Controllers;
using AspireOrchestrator.PaymentProcessing.Business;
using AspireOrchestrator.PaymentProcessing.WebApi.Controllers;
using AspireOrchestrator.PersistenceTests.Common;
using AspireOrchestrator.ScenarioTests.Helpers;
using AspireOrchestrator.ScenarioTests.Processors;
using AspireOrchestrator.Transfer.Business;
using AspireOrchestrator.Transfer.DataAccess;
using AspireOrchestrator.Transfer.Models;
using AspireOrchestrator.Transfer.WebApi.Controllers;
using AspireOrchestrator.Validation.DataAccess;
using AspireOrchestrator.Validation.WebApi.Controllers;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reqnroll;

namespace AspireOrchestrator.ScenarioTests.Drivers
{
    public class ScenarioDriver: TestBase
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly EventRepository _eventRepository;
        private readonly WorkFlowProcessor _workFlowProcessor;
        private readonly ReceiptDetailRepository _receiptDetailRepository;
        private readonly DepositRepository _depositRepository;
        private readonly TestStorageHelper _storageHelper = new();
        private readonly PaymentProcessor _paymentProcessor;
        private readonly PostingRepository _postingRepository;
        private readonly TransferRepository _transferRepository;
        private readonly TransferEngine _transferEngine;
        private readonly TestReplyQueueManager _replyQueueManager;
        private readonly TransferQueueManager _transferQueueManager;


        public ScenarioDriver(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            var logger = TestLoggerFactory.CreateLogger<EventRepository>();
            var flowRepository = new FlowRepository(OrchestratorContext, TestLoggerFactory.CreateLogger<FlowRepository>());
            _eventRepository = new EventRepository(OrchestratorContext, logger);
            _receiptDetailRepository = new ReceiptDetailRepository(DomainContext, TestLoggerFactory.CreateLogger<ReceiptDetailRepository>());
            _depositRepository = new DepositRepository(DomainContext, TestLoggerFactory.CreateLogger<DepositRepository>());
            _postingRepository = new PostingRepository(DomainContext, TestLoggerFactory.CreateLogger<PostingRepository>(), TestLoggerFactory);
            var validationErrorRepository = new ValidationErrorRepository(ValidationContext, TestLoggerFactory.CreateLogger<ValidationErrorRepository>());
            var parseController = new ParseController(_storageHelper, _receiptDetailRepository, _depositRepository, _postingRepository, TestLoggerFactory);
            var validationController = new ValidationController(_receiptDetailRepository, validationErrorRepository, TestLoggerFactory);
            _paymentProcessor = new PaymentProcessor(DomainContext, TestLoggerFactory);
            var paymentController = new PaymentProcessingController(_paymentProcessor, TestLoggerFactory);
            _transferRepository = new TransferRepository(TransferContext, TestLoggerFactory.CreateLogger<TransferRepository>());
            _transferEngine = new TransferEngine(_receiptDetailRepository, _postingRepository, _transferRepository,
                TestLoggerFactory);
            var transferController = new TransferController(_transferEngine, TestLoggerFactory);
            var processorFactory = new TestProcessorFactory(parseController, validationController, paymentController, transferController, TestLoggerFactory);
            _workFlowProcessor = new WorkFlowProcessor(processorFactory, _eventRepository, flowRepository, TestLoggerFactory);
            _replyQueueManager = new TestReplyQueueManager(TestLoggerFactory);
            _transferQueueManager = new TransferQueueManager(TestLoggerFactory);
        }

        private async Task<BlobServiceClient> GetContainerClient()
        {
            var endpoint = new Uri("https://127.0.0.1:64537/devstoreaccount1");
            var credential = new DefaultAzureCredential();
            var blobServiceClient = new BlobServiceClient(endpoint, credential, new BlobClientOptions());
            var properties = await blobServiceClient.GetPropertiesAsync().ConfigureAwait(false);
            return blobServiceClient;
        }

        public void GivenEvent(Table eventTable)
        {
            var entity = eventTable.CreateInstance<EventEntity>();
            _scenarioContext["event"] = entity;
            _eventRepository.AddEvent(entity);
        }

        public async Task GivenTable<T>(Table reqTable)
        where T: GuidModelBase
        {
            var entities = reqTable.CreateSet<T>();
            DomainContext.AddRange(entities);
            await DomainContext.SaveChangesAsync();
        }

        public async Task WhenEventIsProcessed()
        {
            var entity = _eventRepository.GetEventsByTenant(1).First(x => x.EventState == EventState.New);
            _scenarioContext["event"] = await _workFlowProcessor.ProcessEvent(entity); 
        }

        public void ThenEventShouldHaveState(Table eventTable)
        {
            var expected = eventTable.CreateSet<EventEntity>().First();
            var actual = _scenarioContext.Get<EventEntity>("event");
            Assert.Equal(expected.ProcessState, actual.ProcessState);
        }

        internal void ThenEventsShouldBeInTheEventStore(Table eventTable)
        {
            var expected = eventTable.CreateSet<EventEntity>();
            var actual = _eventRepository.GetList();
            Assert.Equal(expected.Count(), actual.Count());


        }

        [Then(@"(EventEntity) table contains")]
        public void ThenTableNameContainsRows(TableName tableName, Table specFlowTable)
        {
            switch (tableName)
            {
                case TableName.EventEntity:
                    ThenGuidContainsRows<EventEntity>(specFlowTable, OrchestratorContext);
                    break;
                case TableName.Tenant:
                    ThenTableContainsRows<Tenant>(specFlowTable, OrchestratorContext);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tableName), tableName, null);
            }
        }

        private static void ThenGuidContainsRows<T>(Table specFlowTable, DbContext context)
            where T : Entity<Guid>
        {
            var entities = context.Set<T>().ToList();
            specFlowTable.CompareToSet(entities);
        }

        private static void ThenTableContainsRows<T>(Table specFlowTable, DbContext context)
            where T : Entity<long>
        {
            var entities = context.Set<T>().ToList();
            specFlowTable.CompareToSet(entities);
        }

        public async Task WhenFlowHasBeenProcessed()
        {
            _scenarioContext.TryGetValue("event", out EventEntity entity);
            var flowId = entity.FlowId.Value;
            _scenarioContext["event"] = await _workFlowProcessor.ProcessFlow(flowId);
        }

        public void ThenTableContains<T>(Table resultTable)
            where T : GuidModelBase
        {
            ThenGuidContainsRows<T>(resultTable, DomainContext);
        }

        public async Task WhenFileUploadedToStorage(DocumentType fileType, string fileName)
        {
            _scenarioContext["fileName"] = fileName;
            _scenarioContext["fileType"] = fileType;
        }

        public async Task WhenFileParsed()
        {
            var documentType = _scenarioContext.Get<DocumentType>("fileType");
            var fileName = _scenarioContext.Get<string>("fileName");
            var stream = await _storageHelper.GetPayload(fileName);
            if (documentType == DocumentType.IpStandard)
            {
                await WhenReceiptDetailParsed(documentType, stream);
            }
            else if (documentType == DocumentType.Camt53 || documentType == DocumentType.PosteringsData)
            {
                await WhenDepositParsed(documentType, stream);

            }
        }

        private async Task WhenReceiptDetailParsed(DocumentType documentType, Stream fileStream)
        {
            var parser = ParserFactory.GetReceiptDetailParser(documentType, TestLoggerFactory);
            var receiptDetails = await parser.ParseAsync(fileStream, documentType);
            await _receiptDetailRepository.AddRange(receiptDetails);
        }

        private async Task WhenDepositParsed(DocumentType documentType, Stream stream)
        {
            // ToDo: change to calling API
            var parser = ParserFactory.GetDepositParser(documentType, TestLoggerFactory);
            var deposits = (await parser.ParseAsync(stream, documentType)).ToList();
            if (deposits.Count > 0)
            {
                foreach (var deposit in deposits)
                {
                    var journal = PostingEngine.PostDeposit(deposit); 
                    await _postingRepository.AddPostingJournal(journal);
                }
                await _depositRepository.AddRange(deposits);
            }
        }

        public async Task WhenDocumentsMatched(DocumentType documentType)
        {
            await _paymentProcessor.MatchDocumentTypeAsync(documentType);
        }

        public async Task TransferAll()
        {
            _ = await _transferEngine.TransferAllAsync();
        }

        public async Task CreateReplies()
        {
            var transfers = await _transferRepository.GetQueryList().Where(x => x.TransferStatus == TransferStatus.Sent)
                .ToListAsync();
            foreach (var reply in transfers.Select(transfer => new BackendReply
                     {
                         TransferId = transfer.Id,
                         Success = true,
                         Message = "OK"
                     }))
            {
                _replyQueueManager.Put(reply, "Replies");
            }
        }

        public async Task HandleReplies()
        {
            _ = await _transferEngine.HandleReplies();
        }

    }
}
