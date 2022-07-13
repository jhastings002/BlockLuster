using BlockLuster.EntityFramework;

namespace BlockLuster.Common.Shared.ResponsesAndRequests
{
    public class SigninResponse
    {
        public bool Success { get; set; } = false;
        public User User { get; set; }
        public bool isAdmin { get; set; } = false;

        public Movie[] RentedMovies { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
