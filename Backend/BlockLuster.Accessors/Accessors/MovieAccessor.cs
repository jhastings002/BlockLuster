using BlockLuster.Accessors.Interfaces;

namespace BlockLuster.Accessors.Accessors
{
    public class MovieAccessor : AccessorBase, IMovieAccessor
    {
        public List<string> GetAllMovies() { throw new NotImplementedException(); }

        public string GetMovie(string id) { throw new NotImplementedException(); }

        public void AddMovie() { throw new NotImplementedException(); }

        public void RemoveMovie(string id) { throw new NotImplementedException(); }

        public void UpdateMovie(string id) { throw new NotImplementedException(); }

        public bool RentMovie(string id, string userId) { throw new NotImplementedException(); }

    }
}