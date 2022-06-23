using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Data.Models
{
    [Index(nameof(RegistrationDate))]
    public class FeedEntry
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public Uri? Uri { get; set; }
        public string? UniqueI { get; set; }
        public string? Description { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public DateTime PublicationDate { get; set; }

        // Relations
        public Guid FeedId { get; set; }
        public virtual Feed Feed { get; set; }
    }
}

