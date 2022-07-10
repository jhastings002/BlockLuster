using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockLuster.Accessors.Interfaces
{
    public interface IMovieAccessor : IAccessorBase 
    {
        List<string> GetAllMovies();

        string GetMovie(string id);

        void AddMovie();

        void RemoveMovie(string id);

        void UpdateMovie(string id);

        bool RentMovie(string id, string userId);
    }
}
