using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Data.Models;

[Index(nameof(FeedId))]
[Index(nameof(UserId))]
public class FeedSubscription
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsEnabled = true;

    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime UpdateDate { get; set; } = DateTime.Now;

    // Relations
    public virtual Feed Feed { get; set; }
    public Guid FeedId { get; set; }

    public virtual ApplicationUser User { get; set; }
    public Guid UserId { get; set; }
}