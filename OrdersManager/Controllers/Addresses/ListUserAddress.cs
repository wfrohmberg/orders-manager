using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersManager.Controllers.Models.Addresses;
using OrdersManager.ControllersActions;
using OrdersManager.ModelsActions;

namespace OrdersManager.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListUserAddress : ControllerBase
    {
        private readonly GetTokenLogin _getTokenLogin;
        private readonly GetUserAddresses _getUserAddresses;

        public ListUserAddress(GetTokenLogin getTokenLogin, GetUserAddresses getUserAddresses)
        {
            _getTokenLogin = getTokenLogin;
            _getUserAddresses = getUserAddresses;
        }

        [HttpGet]
        public List<AddressWithId> GetAddresses()
        {
            string encryptedToken = HttpContext.Request.Headers["token"]!;
            var userLogin = _getTokenLogin.Execute(encryptedToken)!;
            return _getUserAddresses.Execute(userLogin);
        }
    }
}
