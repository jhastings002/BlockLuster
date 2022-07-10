using Microsoft.AspNetCore.Identity;

namespace BlockLuster.EntityFramework
{
    public partial class Movie
    {
        public string Id { get; set; } = string.Empty;

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int Rating { get; set; }

        public decimal DailyRate { get; set; }

        public string? PictureLocation { get; set; } // Not Implemented

        public bool IsAvailable { get; set; }
    }
}
