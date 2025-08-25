using System.ComponentModel.DataAnnotations;

namespace AspireOrchestrator.Core.Models  
{
    public abstract class Entity<T>
    {
        public T Id { get; set; }
        [Display(Name = "Oprettet")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
