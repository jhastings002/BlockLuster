using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BlockLuster.Managers.Interfaces;
using Newtonsoft.Json;
using BlockLuster.Common.Shared.ResponsesAndRequests;
using System.Collections.Generic;
using BlockLuster.EntityFramework;

namespace BlockLuster
{
    public class RentalFunctions
    {
        private readonly IMovieManager _movieManager;

        public RentalFunctions(IMovieManager movieManager)
        {
            _movieManager = movieManager;
        }

        [FunctionName("GetCatalog")]
        public IActionResult GetCatalog(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting Movie Catalog");
            try
            {
                var result = _movieManager.GetCatalog();

                if (result != null && result.Count > 0)
                {
                    return new OkObjectResult(JsonConvert.SerializeObject(result));
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestResult();
            }
        }

        [FunctionName("GetMovie")]
        public IActionResult GetMovie(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting Movie Catalog");
            try
            {
                string movieId = req.Query["movieId"];

                if(movieId == null)
                {
                    return new BadRequestResult();
                }

                var result = _movieManager.GetMovie(movieId);

                if (result != null)
                {
                    return new OkObjectResult(JsonConvert.SerializeObject(result));
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestResult();
            }
        }

        [FunctionName("AddMovie")]
        public async Task<IActionResult> AddMovieAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting Movie Catalog");

            //Admin check
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);

                if(data == null)
                {
                    return new BadRequestResult();
                }
                var movie = new EntityFramework.Movie
                {
                    Title = data.Title,
                    Description = data.Description,
                    Rating = data.Rating,
                    DailyRate = data.DailyRate,
                    IsAvailable = true
                };

                var result = _movieManager.AddMovie(movie);

                if (result)
                {
                    return new OkObjectResult(true);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestResult();
            }
        }

        [FunctionName("RemoveMovie")]
        public async Task<IActionResult> RemoveMovie(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            string movieId = req.Query["movieId"];
            //Admin check

            log.LogInformation($"Removie Movie: {movieId} ");
        
            try
            {
                var result = _movieManager.RemoveMovie(movieId);

                if (result)
                {
                    return new OkResult();
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestResult();
            }
        }

        [FunctionName("RentMovie")]
        public async Task<IActionResult> RentMovie(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Renting Movie");
            // user logged in check
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                var moviesIds = data.MovieIds;
                var userId = data.UserId;

                var request = new RentMoviesRequest { MovieIds = moviesIds.ToObject<string[]>(), UserId = userId };

                var rentedMovies = _movieManager.RentMovie(request);

                return new OkObjectResult(JsonConvert.SerializeObject(rentedMovies));
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new List<Movie>()));
            }
        }

        [FunctionName("ReturnMovie")]
        public async Task<IActionResult> ReturnMovie(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Returning Movie");
            // user logged in check
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string movieId = data.MovieId;
                string userId = data.UserId;

                var rentedMovies = _movieManager.ReturnMovie(movieId, userId);

                return new OkObjectResult(JsonConvert.SerializeObject(rentedMovies));
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new List<Movie>()));
            }
        }

        [FunctionName("Rental_TestMe")]
        public async Task<IActionResult> TestMe(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Rental/TestMe")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string input = req.Query["input"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            input = input ?? data?.input;

            string responseMessage = $"{_movieManager.TestMe(input)}";

            return new OkObjectResult(responseMessage);
        }
    }
}
