using Microsoft.AspNetCore.Identity;

namespace BlockLuster.EntityFramework
{
    public partial class UserRental
    {
        public long Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string MovieId { get; set; } = string.Empty;

        public DateTime RentalDate { get; set; }

        public decimal TotalCost { get; set; }

        public bool IsReturned { get; set; }
    }
}
