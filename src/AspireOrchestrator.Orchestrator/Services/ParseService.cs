using AspireOrchestrator.Core.OrchestratorModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AspireOrchestrator.Orchestrator.Services
{
    public class ParseService(HttpClient httpClient, ILogger<EventService> logger)
    {
        public async Task<EventEntity> HandleEventAsync(EventEntity eventEntity, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Handling event: {EventId}", eventEntity.Id);

            // for debugging purposes, increase timeout from default 30 seconds to 5 minutes
            httpClient.Timeout = TimeSpan.FromMinutes(5);

            // call orchestrator
            //var test = await httpClient.GetAsync("/api/event/GetAll");

            var response = await httpClient.PostAsJsonAsync("/api/parse/parse", eventEntity, cancellationToken);

            logger.LogInformation("Event handled successfully." + response);

            var result = await response.Content.ReadFromJsonAsync<EventEntity>(cancellationToken: cancellationToken);
            return result;
        }
    }
}
