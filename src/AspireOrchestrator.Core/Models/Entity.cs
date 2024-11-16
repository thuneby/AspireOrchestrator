namespace AspireOrchestrator.Core.Models  
{
    public abstract class Entity<T>
    {
        public Entity()
        {
            CreatedDate = DateTime.Now;
        }

        public required T Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
