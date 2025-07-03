using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Validation.Interfaces;

namespace AspireOrchestrator.Validation.Business
{
    public class Validator: IValidator
    {
        public async Task<(bool valid, List<string> validationErrors)> ValidateAsync(ReceiptDetail receiptDetail)
        {
            var masterData = await LoadMasterDate(receiptDetail);


            throw new NotImplementedException();
        }

        private async Task<object> LoadMasterDate(ReceiptDetail receiptDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool valid, List<string> validationErrors)> ValidateManyAsync(List<ReceiptDetail> receiptDetails)
        {
            throw new NotImplementedException();
        }
    }
}
