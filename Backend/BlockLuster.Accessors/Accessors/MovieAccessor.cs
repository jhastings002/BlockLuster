using BlockLuster.Accessors.Interfaces;
using BlockLuster.EntityFramework;
using Microsoft.Data.SqlClient;

namespace BlockLuster.Accessors.Accessors
{
    public class MovieAccessor : AccessorBase, IMovieAccessor
    {
        public List<Movie> GetAllMovies()
        {
            return UsingDatabaseContext(db => {
                return db.Movies.ToList();
            });
        }

        public Movie GetMovie(string id)
        {
            return UsingDatabaseContext(db => {
                var result = db.Movies.Where(movie => movie.Id == id).First();
                return result;
            });
        }

        public void AddMovie(Movie movie) 
        { 
            UsingDatabaseContext(db => {
                db.Movies.Add(movie);
                db.SaveChanges();
                return true; 
            }); 
        }

        public bool RemoveMovie(string id)
        {
            return UsingDatabaseContext(db => {
                var movie = db.Movies.Where(x => x.Id == id).FirstOrDefault();
                if (movie != null)
                {
                    db.Movies.Remove(movie);
                    db.SaveChanges();
                    return true;
                }

                return false;
                
            });
        }

        public bool UpdateMovie(Movie updateMovie)
        {
            return UsingDatabaseContext(db => {
                var movie = db.Movies.Where(x => x.Id == updateMovie.Id).FirstOrDefault();
                if (movie != null)
                {
                    db.Movies.Update(movie);
                    db.SaveChanges();
                    return true;
                }

                return false;

            });
        }

        public void RentMovie(string movieId, string userId) {

            var rental = new UserRental
            {
                MovieId = movieId,
                UserId = userId,
                RentalDate = DateOnly.FromDateTime(DateTime.Now),
                TotalCost = 0,
                IsReturned = false
            };

            UsingDatabaseContext(db => {
                var movie = db.UserRentals.Add(rental);
                db.SaveChanges();

                return true;

            });
        }

        public bool ReturnMovie(string movieId, string userId)
        {
            return UsingDatabaseContext(db => {
                var rental = db.UserRentals.Where(x => x.UserId == userId && x.MovieId == movieId && x.IsReturned == false).FirstOrDefault();
                if (rental != null)
                {
                    rental.IsReturned = true;
                    db.UserRentals.Update(rental);
                    db.SaveChanges();
                    return true;
                }

                return false;

            });
        }

    }
}