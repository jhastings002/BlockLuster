using Microsoft.AspNetCore.Identity;

namespace BlockLuster.EntityFramework
{
    public partial class UserRental
    {
        public string UserId { get; set; } = string.Empty;

        public string MovieId { get; set; } = string.Empty;

        public DateOnly RentalDate { get; set; }

        public decimal TotalCost { get; set; }

        public bool IsReturned { get; set; }
    }
}
