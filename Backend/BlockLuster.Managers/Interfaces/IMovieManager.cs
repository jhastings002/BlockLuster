using BlockLuster.Common.Shared.ResponsesAndRequests;
using BlockLuster.EntityFramework;

namespace BlockLuster.Managers.Interfaces
{
    public interface IMovieManager
    {
        List<Movie> GetCatalog();

        Movie GetMovie(string id);

        bool AddMovie(Movie movie);

        bool RemoveMovie(string id);

        List<Movie> RentMovie(RentMoviesRequest rentMoviesRequest);

        List<Movie> GetRentedMovies(string userId);

        List<Movie> ReturnMovie(string movieId, string userId);

        string TestMe(string input);
    }
}
