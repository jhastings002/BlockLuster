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
    public class AccountFunctions
    {
        private readonly IUserManager _userManager;

        public AccountFunctions(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [FunctionName("Account_TestMe")]
        public async Task<IActionResult> TestMe(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Account/TestMe")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string input = req.Query["input"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            input = input ?? data?.name;

            string responseMessage = $"{_userManager.TestMe(input)}";
            return new OkObjectResult(responseMessage);
        }
    }
}
