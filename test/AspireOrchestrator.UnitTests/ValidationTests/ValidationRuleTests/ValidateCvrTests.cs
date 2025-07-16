using System.Collections.Concurrent;
using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.PersistenceTests.Common;
using AspireOrchestrator.Validation.Business.ValidationRules;
using AspireOrchestrator.Validation.Models;

namespace AspireOrchestrator.UnitTests.ValidationTests.ValidationRuleTests
{
    public class ValidateCvrTests: TestBase
    {
        private readonly ValidateCvr _validateCvr = new();


        [Fact]
        public void ValidateCvr_InvalidCvr()
        {
            // Arrange
            var receiptDetail = new ReceiptDetail
            {
                Cvr = "12345"
            };
            var existingErrors = new ConcurrentBag<ValidationError>();
            // Act
            var result = _validateCvr.Validate(receiptDetail, existingErrors);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ErrorCode.InvalidCvr, result.ErrorCode);

        }

        [Fact]
        public void ValidateCvr_ValidCvr()
        {
            // Arrange
            var receiptDetail = new ReceiptDetail
            {
                Cvr = "12345678"
            };
            var existingErrors = new ConcurrentBag<ValidationError>();
            // Act
            var result = _validateCvr.Validate(receiptDetail, existingErrors);

            // Assert
            Assert.Null(result);

        }
    }
}
