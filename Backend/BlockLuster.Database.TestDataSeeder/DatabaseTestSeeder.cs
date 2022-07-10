using BlockLuster.Accessors.Accessors;

namespace BlockLuster.Database.TestDataSeeder
{
    [TestClass]
    public class DatabaseTestSeeder
    {
        [TestMethod]
        public void SeedMovies()
        {
            var movieAccessor = new MovieAccessor();
        }
    }
}