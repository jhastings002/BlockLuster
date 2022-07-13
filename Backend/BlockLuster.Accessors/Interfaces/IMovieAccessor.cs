using BlockLuster.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockLuster.Accessors.Interfaces
{
    public interface IMovieAccessor : IAccessorBase 
    {
        List<Movie> GetAllMovies();

        Movie GetMovie(string id);

        Movie AddMovie(Movie movie);

        bool RemoveMovie(string id);

        bool UpdateMovie(Movie updateMovie);

        void RentMovie(string id, string userId);

        bool ReturnMovie(string id, string userId);

        List<UserRental> UserRentals(string userId);
    }
}
