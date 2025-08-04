using System.Collections.Concurrent;
using AspireOrchestrator.Validation.Business.ValidationRules;

namespace AspireOrchestrator.UnitTests.ValidationTests.ValidationRuleTests
{
    public class ValidateCprTests
    {
        private readonly ValidateCpr _validateCpr = new ValidateCpr();

        [Fact]
        public void ValidateCpr_InvalidCpr()
        {
            // Arrange
            var receiptDetail = new AspireOrchestrator.Domain.Models.ReceiptDetail
            {
                Cpr = "222222-2222" // Invalid CPR 
            };
            var existingErrors = new ConcurrentBag<AspireOrchestrator.Validation.Models.ValidationError>();
            // Act
            var result = _validateCpr.Validate(receiptDetail, existingErrors);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(AspireOrchestrator.Validation.Models.ErrorCode.InvalidCpr, result.ErrorCode);
        }

        [Fact]
        public void ValidateCpr_ValidCpr()
        {
            // Arrange
            var receiptDetail = new AspireOrchestrator.Domain.Models.ReceiptDetail
            {
                Cpr = "200774-3095" // Valid randomly generated CPR 
            };
            var existingErrors = new ConcurrentBag<AspireOrchestrator.Validation.Models.ValidationError>();
            // Act
            var result = _validateCpr.Validate(receiptDetail, existingErrors);
            // Assert
            Assert.Null(result); // No error found, should return null
        }
    }
}
