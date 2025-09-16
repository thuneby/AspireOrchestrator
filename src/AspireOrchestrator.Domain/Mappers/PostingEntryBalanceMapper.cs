using AspireOrchestrator.Core.Mappers;
using AspireOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.Domain.Mappers
{
    public class PostingEntryBalanceMapper(ILoggerFactory loggerFactory)
        : GuidMapperBase<PostingEntry, PostingEntryBalance>(loggerFactory);
}
