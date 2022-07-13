using BlockLuster.Accessors.Accessors;
using BlockLuster.Accessors.EntityFramework;
using BlockLuster.EntityFramework;
using Bogus;
using Shouldly;
using System.Transactions;

namespace BlockLuster.Accessors.Tests
{
    [TestClass]
    public class DatabaseTestSeeder
    {
        private MovieAccessor _movieAccessor;
        private UserAccessor _userAccessor;
        private TransactionScope _transactionScope;

        [TestInitialize]
        public void TestInitialize()
        {

            _movieAccessor = new MovieAccessor();
            _userAccessor = new UserAccessor();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Test Clean up
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

        // Use to insert any number of random Movie records into database
        // These will be as a new movie that was added, that is available
        // Leave 0 to not effect database
        [TestMethod]
        [DataRow(0)]
        public void MovieAccessor_AddMovie_ShouldAddToDatabase(int numberOfRecords)
        {
            // ARRANGE
            var faker = new Faker();
            using var db = new DatabaseContext();

            for (int i = 0; i < numberOfRecords; i++)
            {
                var newMovie = new Movie
                {
                    Title = faker.Lorem.Sentence(),
                    Description = faker.Lorem.Paragraph(),
                    Rating = faker.Random.Int(0, 4),
                    DailyRate = faker.Random.Decimal(0.10m, 2.99m),
                    IsAvailable = true
                };

                // ACT
                _ = _movieAccessor.AddMovie(newMovie);
            }         
        }
    }
}