using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspireOrchestrator.Orchestrator.BusinessLogic
{
    public static class ServiceInvoker
    {
        public static async Task<T2> InvokeService<T1, T2>(HttpMethod serviceMethod, string appId, string methodName, T1 entity)
        {
            var cancellationToken = new CancellationTokenSource().Token;
            using var client = new HttpClient();
            client.BaseAddress = new Uri($"https://{appId}");
            var request = new HttpRequestMessage(serviceMethod, methodName)
            {
                Content = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<T2>(cancellationToken: cancellationToken);
                return result ?? throw new InvalidOperationException("Response content is null");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Service invocation failed with status code {response.StatusCode}: {errorContent}");
            }
        }
    }
}
