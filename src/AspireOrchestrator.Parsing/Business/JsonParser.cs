using System.Text.Json;
using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Parsing.Interfaces;

namespace AspireOrchestrator.Parsing.Business
{
    public class JsonParser: IReceiptDetailParser
    {
        public async Task<IEnumerable<ReceiptDetail>> ParseAsync(Stream payload, DocumentType documentType)
        {
            var receiptDetails = await JsonSerializer.DeserializeAsync<ReceiptDetail[]>(payload);
            if (receiptDetails != null && receiptDetails.Any())
            {
                return receiptDetails.ToList();
            }
            return new List<ReceiptDetail>();
        }
    }
}
