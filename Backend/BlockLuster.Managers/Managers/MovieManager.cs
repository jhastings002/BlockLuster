using BlockLuster.Accessors.Interfaces;
using BlockLuster.EntityFramework;
using BlockLuster.Managers.Interfaces;

namespace BlockLuster.Managers.Managers
{
    public class MovieManager : IMovieManager
    {
        private readonly IMovieAccessor _movieAccessor;
        public MovieManager(IMovieAccessor movieAccessor)
        {
            _movieAccessor = movieAccessor;
        }

        public List<Movie> GetCatalog() { throw new NotImplementedException(); }

        public Movie GetMovie() { throw new NotImplementedException(); }

        public bool AddMovie() { throw new NotImplementedException(); }

        public bool RemoveMovie() { throw new NotImplementedException(); }

        public bool RentMovie() { throw new NotImplementedException(); }

        public bool ReturnMovie() { throw new NotImplementedException(); }

        public string TestMe(string input)
        {
            return $"{nameof(MovieManager)} : {_movieAccessor.TestMe(input)}";
        }
    }
}