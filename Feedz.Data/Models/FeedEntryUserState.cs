using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Data.Models;

[Index(nameof(FeedEntryId))]
[Index(nameof(UserId))]
public class FeedEntryUserState
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime FavoriteDate { get; set; }
    public DateTime ViewDate { get; set; }
    public DateTime HiddenDate { get; set; }
    
    // Relations
    public virtual FeedEntry FeedEntry { get; set; }
    public Guid FeedEntryId { get; set; }
    
    public virtual ApplicationUser User { get; set; }
    public Guid UserId { get; set; }
}