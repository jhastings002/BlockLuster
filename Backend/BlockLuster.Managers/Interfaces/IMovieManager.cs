using BlockLuster.EntityFramework;

namespace BlockLuster.Managers.Interfaces
{
    public interface IMovieManager
    {
        List<Movie> GetCatalog();

        Movie GetMovie();

        bool AddMovie();

        bool RemoveMovie();

        bool RentMovie();

        bool ReturnMovie();

        string TestMe(string input);
    }
}
