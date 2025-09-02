namespace AspireOrchestrator.Transfer.Interfaces
{
    public interface ITransferEngine
    {
        string TransferQueue { get; set; }
        string ReplyQueue { get; set; }

        Task<int> TransferDocumentAsync(Guid documentId);
        Task<int> TransferAllAsync();
        Task<int> HandleReplies();
        Task<int> Resend(Guid id);
    }
}
