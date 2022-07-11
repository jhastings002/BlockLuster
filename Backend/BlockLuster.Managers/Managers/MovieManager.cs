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

        public List<Movie> GetCatalog() 
        {
            return _movieAccessor.GetAllMovies();
        }

        public Movie GetMovie(string id) 
        { 
            return _movieAccessor.GetMovie(id);
        }

        public bool AddMovie(Movie movie) 
        {
            var result = _movieAccessor.AddMovie(movie);
                
            if(result != null)
            {
                return true;
            }
            return false;
        }

        public bool RemoveMovie(string id) 
        {
            return _movieAccessor.RemoveMovie(id);
        }

        public void RentMovie(string movieId, string userId)
        {
            _movieAccessor.RentMovie(movieId, userId);
        }

        public void ReturnMovie(string movieId, string userId) 
        {
            _movieAccessor.ReturnMovie(movieId, userId);
        }

        public string TestMe(string input)
        {
            return $"{nameof(MovieManager)} : {_movieAccessor.TestMe(input)}";
        }
    }
}