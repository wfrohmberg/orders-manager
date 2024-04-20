using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersManager.ControllersActions;
using OrdersManager.Middleware;
using OrdersManager.ModelsActions;

namespace OrdersManager.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    [MiddlewareFilter(typeof(TokenMiddleware))]
    public class RestoreAddress : ControllerBase
    {
        private readonly GetTokenLogin _getTokenLogin;
        private readonly RestoreUserAddress _restoreUserAddress;

        public RestoreAddress(GetTokenLogin getTokenLogin, RestoreUserAddress restoreUserAddress)
        {
            _getTokenLogin = getTokenLogin;
            _restoreUserAddress = restoreUserAddress;
        }

        [HttpPatch]
        [Route("{id}")]
        public string Patch(int id)
        {
            string encryptedToken = HttpContext.Request.Headers["token"]!;
            _restoreUserAddress.UserLogin = _getTokenLogin.Execute(encryptedToken)!;
            _restoreUserAddress.AddressId = id;
            if (!_restoreUserAddress.Execute())
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return "FAILED";
            }
            else
            {
                return "SUCCESS";
            }
        }
    }
}
