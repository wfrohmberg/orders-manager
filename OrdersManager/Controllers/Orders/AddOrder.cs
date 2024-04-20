using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersManager.Middleware;
using OrdersManager.Controllers.Models.Orders;
using OrdersManager.ControllersActions;
using OrdersManager.ModelsActions;

namespace OrdersManager.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    [MiddlewareFilter(typeof(TokenMiddleware))]
    public class AddOrder : ControllerBase
    {
        private readonly GetTokenLogin _getTokenLogin;
        private readonly IsUserAddress _isUserAddress;
        private readonly AddUserOrder _addUserOrder;

        public AddOrder(GetTokenLogin getTokenLogin, 
                        IsUserAddress isUserAddress,
                        AddUserOrder addUserOrder)
        {
            _getTokenLogin = getTokenLogin;
            _isUserAddress = isUserAddress;
            _addUserOrder = addUserOrder;
        }

        [HttpPost]
        public string Post([FromBody]OrderForm orderForm)
        {
            string encryptedToken = HttpContext.Request.Headers["token"]!;
            _isUserAddress.UserLogin = _getTokenLogin.Execute(encryptedToken)!;
            _isUserAddress.AddressId = orderForm.AddressId;
            if (!_isUserAddress.Check())
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return "FAILED";
            }
            if (!_addUserOrder.Execute(orderForm))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return "FAILED";
            }
            return "SUCCESS";
        }
    }
}
