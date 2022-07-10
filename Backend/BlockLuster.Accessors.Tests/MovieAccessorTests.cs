using BlockLuster.Accessors.Accessors;
using BlockLuster.Accessors.EntityFramework;
using BlockLuster.EntityFramework;
using Bogus;
using Shouldly;
using System.Transactions;

namespace BlockLuster.Accessors.Tests
{
    [TestClass]
    public class MovieAccessorTests
    {
        private MovieAccessor _movieAccessor;
        private TransactionScope _transactionScope;

        [TestInitialize]
        public void TestInitialize()
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            _transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions);
            _movieAccessor = new MovieAccessor();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _transactionScope.Dispose();
        }

        [TestMethod]
        public void TestTemplate()
        {   
            // ARRANGE
            var faker = new Faker();

            // ACT
            var test = "test";

            // ASSERT
            test.ShouldBe("test");
        }

        [TestMethod]
        public void MovieAccessor_AddMovie_ShouldAddToDatabase()
        {
            // ARRANGE
            var faker = new Faker();
            var newMovie = new Movie
            {
                Title = faker.Lorem.Sentence(),
                Description = faker.Lorem.Paragraph(),
                Rating = faker.Random.Int(0, 4),
                DailyRate = faker.Random.Decimal(0.10m, 2.99m),
                IsAvailable = faker.Random.Bool()
            };

            // ACT
            var result = _movieAccessor.AddMovie(newMovie);

            // ASSERT
            using var db = new DatabaseContext();

            var movie = db.Movies.Where(x => x.Id == result.Id).Single();

            movie.ShouldNotBeNull();
            movie.Title.ShouldBe(newMovie.Title);
            movie.Description.ShouldBe(newMovie.Description);
            movie.Rating.ShouldBe(newMovie.Rating);
            movie.DailyRate.ShouldBe(Math.Round(newMovie.DailyRate,2));
            movie.IsAvailable.ShouldBe(newMovie.IsAvailable);

        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void MovieAccessor_AddMovie_Null_ShouldNotAddToDatabase()
        {
            // ARRANGE
            var faker = new Faker();
            var newMovie = new Movie
            {
                Title = faker.Lorem.Sentence(),
                Description = faker.Lorem.Paragraph(),
                Rating = faker.Random.Int(0, 4),
                DailyRate = faker.Random.Decimal(0.10m, 2.99m),
                IsAvailable = faker.Random.Bool()
            };

            // ACT
            var result = _movieAccessor.AddMovie(null);

            // ASSERT

        }
    }
}