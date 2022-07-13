using BlockLuster.Accessors.Interfaces;
using BlockLuster.Common.Shared.ResponsesAndRequests;
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

        public List<Movie> RentMovie(RentMoviesRequest rentMoviesRequest)
        {
            var rentedMovies = new List<Movie>();

            foreach (var movieId in rentMoviesRequest.MovieIds)
            {
                _movieAccessor.RentMovie(movieId, rentMoviesRequest.UserId);
                var movie = _movieAccessor.GetMovie(movieId);
                movie.IsAvailable = false;
                _movieAccessor.UpdateMovie(movie);
                rentedMovies.Add(movie);
            }
            return rentedMovies;
        }
        public List<Movie> GetRentedMovies(string userId)
        {
            var rentals = _movieAccessor.UserRentals(userId);

            var movies = new List<Movie>();
            foreach (var rental in rentals)
            {                
                var movie = _movieAccessor.GetMovie(rental.MovieId);
                movies.Add(movie);
            }
            return movies;
        }

        public List<Movie> ReturnMovie(string movieId, string userId) 
        {
            _movieAccessor.ReturnMovie(movieId, userId);
            var movie = _movieAccessor.GetMovie(movieId);
            movie.IsAvailable = true;
            _movieAccessor.UpdateMovie(movie);

            var rentals = _movieAccessor.UserRentals(userId);

            var movies = new List<Movie>();
            foreach (var rental in rentals)
            {
                var rentedMovie = _movieAccessor.GetMovie(rental.MovieId);
                movies.Add(rentedMovie);
            }
            return movies; ;
        }

        public string TestMe(string input)
        {
            return $"{nameof(MovieManager)} : {_movieAccessor.TestMe(input)}";
        }
    }
}