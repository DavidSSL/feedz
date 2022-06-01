using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Data.Models
{
    [Index(nameof(RegistrationDate))]
	public class FeedEntry
	{
        [Key]
		public Guid ID { get; set; }
		public string Title { get; set; }
		public Uri Uri { get; set; }
		public string UniqueID { get; set; }
		public string? Description { get; set; }

		public DateTime RegistrationDate { get; set; } = DateTime.Now;
		public DateTime PublicationDate { get; set; }

		// Relations
		public Guid FeedID { get; set; }
		public Feed Feed { get; set; }

		public FeedEntry()
		{
		}
	}
}

