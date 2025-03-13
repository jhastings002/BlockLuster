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
using BlockLuster.Common.Shared.ResponsesAndRequests;

namespace BlockLuster
{
    public class AccountFunctions
    {
        private readonly IUserManager _userManager;
        private readonly IMovieManager _movieManager;
        private readonly ISecurityService _securityService;

        public AccountFunctions(IUserManager userManager, IMovieManager movieManager, ISecurityService securityService)
        {
            _userManager = userManager;
            _movieManager = movieManager;
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

                var result = await _userManager.SignUpUserAsync(firstName, lastName, email, password);

                return new OkObjectResult(JsonConvert.SerializeObject(result));
            }
            catch(Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new SigninResponse()
                {
                   Success = false,
                   Token = null,
                }));
            }
        }

        [FunctionName("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Update User");
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string userId = data.userId;
                string firstName = data?.firstName;
                string lastName = data?.lastName;

               _userManager.UpdateProfile(userId, firstName, lastName);

                return new OkObjectResult(JsonConvert.SerializeObject(true));
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestObjectResult(JsonConvert.SerializeObject(false));
            }
        }

        [FunctionName("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Update Password");
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string userId = data.userId;
                string oldPassword = data?.oldPassword;
                string newPassword = data?.newPassword;

                await _userManager.UpdatePassword(userId, oldPassword, newPassword);

                return new OkObjectResult(JsonConvert.SerializeObject(true));
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new SigninResponse()
                {
                    Success = false,
                    Token = null,
                }));
            }
        }

        [FunctionName("SignIn")]
        public async Task<IActionResult> SignIn(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string password = data?.password;
                string email = data?.email;

                var result = await _securityService.SignInAsync(email, password);

                result.RentedMovies = _movieManager.GetRentedMovies(result.User.Id).ToArray();

                return new OkObjectResult(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new SigninResponse
                {
                    Success = false,
                    Token = null,
                }));
            }
        }

        [FunctionName("SignOut")]
        public async Task<IActionResult> SignOut(
            [HttpTrigger(AuthorizationLevel.Function, "get" , Route = null)] HttpRequest req,
            ILogger log)
        {

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
            throw new NotImplementedException();
        }

        [FunctionName("ReactivateAccount")]
        public IActionResult ReactivateAccount(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
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

            string responseMessage = $"{_userManager.TestMe(input)} ${_movieManager.TestMe(input)}";
            return new OkObjectResult(responseMessage);
        }
    }
}
