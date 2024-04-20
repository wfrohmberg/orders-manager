using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersManager.Controllers.Models.Addresses;
using OrdersManager.ControllersActions;
using OrdersManager.Middleware;
using OrdersManager.Models;
using OrdersManager.ModelsActions;

namespace OrdersManager.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    [MiddlewareFilter(typeof(TokenMiddleware))]
    public class AddAddress : ControllerBase
    {
        private readonly AddUserAddress _addUserAddress;
        private readonly GetTokenLogin _getTokenLogin;

        public AddAddress(AddUserAddress addUserAddress, GetTokenLogin getTokenLogin)
        {
            _addUserAddress = addUserAddress;
            _getTokenLogin = getTokenLogin;
        }

        [HttpPost]
        public string Post([FromBody]AddressForm address)
        {
            var encryptedToken = HttpContext.Request.Headers["token"];

            _addUserAddress.AddressForm = address;
            _addUserAddress.UserLogin = _getTokenLogin.Execute(encryptedToken);

            if (_addUserAddress.Execute())
            {
                return "SUCCESS";
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return "FAILED";
            }
        }
    }
}
