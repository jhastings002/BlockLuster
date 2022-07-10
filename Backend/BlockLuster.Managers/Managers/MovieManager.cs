using BlockLuster.Accessors.Interfaces;
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

        public string TestMe(string input)
        {
            return $"{nameof(MovieManager)} : {_movieAccessor.TestMe(input)}";
        }
    }
}