namespace AspireOrchestrator.Core.Models  
{
    public abstract class Entity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
