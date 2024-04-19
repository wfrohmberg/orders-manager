using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrdersManager.Controllers.Models.Login;
using OrdersManager.DbContexts;
using OrdersManager.Models;
using OrdersManager.ModelsActions;
using System.Runtime.CompilerServices;

namespace OrdersManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly UserCredentialsCheck _userCredentialsCheck;
        public Login(IServiceScopeFactory serviceScopeFactory, UserCredentialsCheck userCredentialsCheck) 
        { 
            _scopeFactory = serviceScopeFactory;
            _userCredentialsCheck = userCredentialsCheck;
        }

        
        [HttpPost]
        public string Post([FromBody] Credentials credentials)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<OrdersContext>()!)
                {
                    _userCredentialsCheck.Login = credentials.login;
                    _userCredentialsCheck.Password = credentials.password;
                    if (!_userCredentialsCheck.Execute())
                    {
                        HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return "";
                    }
                    else
                    {
                        return "SUCCESS";
                    }


                }
            }
        }

    }
}
