using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.PersistenceTests.Common;
using AspireOrchestrator.Validation.Business;
using AspireOrchestrator.Validation.DataAccess;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.UnitTests.ValidationTests
{
    public class ValidatorTests : TestBase
    {
        private readonly Validator _validator;

        private readonly ReceiptDetail _receiptDetail = new ReceiptDetail
        {
            Amount = 123.45M,
            Cpr = "110295-7950",
            FromDate = new DateTime(2025, 1, 1),
            ToDate = new DateTime(2025, 1, 31),
            ReceivedDate = new DateTime(2025, 2, 1),
            LaborAgreementNumber = "123456",
            Cvr = "12345678",
            PersonFullName = "Jane Test Peterson",
            PaymentReference = "INFO-OVF",
            PaymentDate = new DateTime(2025, 2, 2),
            ReceiptType = ReceiptType.Payment,
        };

        public ValidatorTests()
        {
            var logger = TestLoggerFactory.CreateLogger<ValidationErrorRepository>();
            var validationErrorRepository = new ValidationErrorRepository(ValidationContext, logger);
            _validator = new Validator(validationErrorRepository);
        }

        [Fact]
        public async Task ValidateAsync_ReceiptDetailIsValid()
        {
            // Arrange

            // Act
            var validationResult = await _validator.ValidateAsync(_receiptDetail);


            // Assert
            Assert.NotNull(validationResult);
            Assert.True(validationResult.valid);
            Assert.Empty(validationResult.validationErrors);

        }

        [Fact]
        public void ValidationRuleFactory_GetsRules()
        {
            // Arrange

            // Act
            var validationRules = ValidationRuleFactory.GetAllValidationRules();

            // Assert
            Assert.NotNull(validationRules);
            Assert.NotEmpty(validationRules);
        }
    }
}
