using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BlockLuster.Managers.Interfaces;

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
        public static async Task<IActionResult> GetCatalog (
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This is the get Catalog endpoint.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("RentMovie")]
        public static async Task<IActionResult> RentMovie(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["movieId"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}.  This is the Rent Movie endpoint.";

            return new OkObjectResult(responseMessage);
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
