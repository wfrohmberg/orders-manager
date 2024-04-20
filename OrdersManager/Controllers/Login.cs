using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrdersManager.Controllers.Models.Login;
using OrdersManager.ControllersActions;
using OrdersManager.DbContexts;
using OrdersManager.Models;
using OrdersManager.ModelsActions;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace OrdersManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly UserCredentialsCheck _userCredentialsCheck;
        private readonly GenerateEncryptedToken _generateEncryptedToken;
        public Login(IServiceScopeFactory serviceScopeFactory, 
                     UserCredentialsCheck userCredentialsCheck,
                     GenerateEncryptedToken generateEncryptedToken) 
        { 
            _scopeFactory = serviceScopeFactory;
            _userCredentialsCheck = userCredentialsCheck;
            _generateEncryptedToken = generateEncryptedToken;
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
                        var result = new LoginResult { token = _generateEncryptedToken.Execute(credentials.login!) };

                        var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
                        return JsonSerializer.Serialize(result, options);
                    }
                }
            }
        }

    }
}
