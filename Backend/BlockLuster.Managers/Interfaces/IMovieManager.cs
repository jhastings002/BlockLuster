using BlockLuster.EntityFramework;

namespace BlockLuster.Managers.Interfaces
{
    public interface IMovieManager
    {
        List<Movie> GetCatalog();

        Movie GetMovie(string id);

        bool AddMovie(Movie movie);

        bool RemoveMovie(string id);

        void RentMovie(string movieId, string userId);

        void ReturnMovie(string movieId, string userId);

        string TestMe(string input);
    }
}
