using System;
using System.ComponentModel.DataAnnotations;

namespace Feedz.Data.Models
{
	public class Feed
	{
        // Identity
        [Key]
		public Guid ID { get; set; }
		public Uri URL { get; set; }
		public Uri? WebsiteURL { get; set; }
		public Uri? ImageURL { get; set; }
		public string Title { get; set; }

		// Relations
		public List<FeedEntry> Entries { get; set; }

		// Timestamps
		public DateTime RegistrationDate { get; set; } = DateTime.Now;
		public DateTime? LastFetchDate { get; set; }

	public Feed()
		{
		}
	}
}

