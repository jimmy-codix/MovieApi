using Bogus;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data
{
    public class SeedData
    {
        private static Faker faker = new Faker("sv");

        internal static async Task InitAsync(MovieApiContext context)
        {
            if (await context.Movie.AnyAsync()) return;

            var movies = GenerateMovies(5);
            await context.AddRangeAsync(movies);

            var actors = GenerateAndAssignActors(movies, 20);
            await context.AddRangeAsync(actors);


            await context.SaveChangesAsync();

        }

        private static IEnumerable<Actor> GenerateAndAssignActors(IEnumerable<Movie> movies, int nrOfActors)
        {
            var actors = new List<Actor>(nrOfActors);
            for (int i = 0; i < nrOfActors; i++)
            {
                Actor actor = new Actor()
                {
                    Name = faker.Name.FullName(),
                    BirthYear = faker.Date.Past(50).Year
                };

                //Assign random movies to the actor
                actor.Movies = faker.PickRandom(movies, faker.Random.Int(1, 3)).ToList();
                actors.Add(actor);
            }

            return actors;
        }

        private static ICollection<Review> GenerateReviews(int nrOfReviews)
        {
            var reviews = new List<Review>(nrOfReviews);
            for (int i = 0; i < nrOfReviews; i++)
            {
                Review review = new Review()
                {
                    ReviewerName = faker.Name.FullName(),
                    Comment = faker.Lorem.Sentence(10, 20),
                    Rating = faker.Random.Int(1, 5),
                };

                reviews.Add(review);
            }

            return reviews;
        }

        private static IEnumerable<Movie> GenerateMovies(int nrOfMovies)
        {
            var movies = new List<Movie>(nrOfMovies);

            for (int i = 0; i < nrOfMovies; i++)
            {
                var movie = new Movie
                {
                    Title = faker.Lorem.Sentence(3, 5),
                    Year = faker.Date.Past(20).Year,
                    Genre = faker.Lorem.Word(),
                    Duration = faker.Random.Int(60, 180),
                    MovieDetails = new MovieDetails
                    {
                        Synopsis = faker.Lorem.Paragraph(),
                        Language = faker.Lorem.Word(),
                        Budget = faker.Random.Int(1000000, 1000000000)
                    },
                    Reviews = GenerateReviews(faker.Random.Int(1, 10))
                };

                movies.Add(movie);
            }

            return movies;
        }

    }
}
