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
using BlockLuster.EntityFramework;
using BlockLuster.Common.SecurityService;

namespace BlockLuster
{
    public class AccountFunctions
    {
        private readonly IUserManager _userManager;
        private readonly ISecurityService _securityService;

        public AccountFunctions(IUserManager userManager, ISecurityService securityService)
        {
            _userManager = userManager;
            _securityService = securityService;
        }

        [FunctionName("SignUp")]
        public async Task<IActionResult> SignUp(
            [HttpTrigger(AuthorizationLevel.Function,"post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("User Sign up");
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string firstName = data?.firstName;
                string lastName = data?.lastName;
                string email = data?.email;
                string password = data?.password;

                var token = await _userManager.SignUpUserAsync(firstName, lastName, email, password);

                return new OkObjectResult(token);
            }
            catch(Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestResult();
            }
        }

        [FunctionName("SignIn")]
        public async Task<IActionResult> SignIn(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string password = data?.password;
                string email = data?.email;

                var token = _securityService.SignInAsync(email, password);

                return new OkObjectResult(token);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestResult();
            }
        }

        [FunctionName("SignOut")]
        public async Task<IActionResult> SignOut(
            [HttpTrigger(AuthorizationLevel.Function, "get" , Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string input = req.Query["input"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            input = input ?? data?.input;

            string responseMessage = $"{_userManager.TestMe(input)}";
            return new OkObjectResult(responseMessage);
        }

        [FunctionName("DeactivateAccount")]
        public IActionResult DeactivateAccount(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            //Logged in check
            throw new NotImplementedException();
        }

        [FunctionName("ReactivateAccount")]
        public IActionResult ReactivateAccount(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            //Logged in check
            throw new NotImplementedException();
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
            input = input ?? data?.input;

            string responseMessage = $"{_userManager.TestMe(input)}";
            return new OkObjectResult(responseMessage);
        }
    }
}
