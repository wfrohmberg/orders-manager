using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersManager.ControllersActions;
using OrdersManager.ModelsActions;

namespace OrdersManager.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteAddress : ControllerBase
    {
        private readonly GetTokenLogin _getTokenLogin;
        private readonly DeleteUserAddress _deleteUserAddress;

        public DeleteAddress(GetTokenLogin getTokenLogin, DeleteUserAddress deleteUserAddress)
        {
            _getTokenLogin = getTokenLogin;
            _deleteUserAddress = deleteUserAddress;
        }

        [HttpDelete]
        [Route("{id}")]
        public string Delete(int id)
        {
            string encryptedToken = HttpContext.Request.Headers["token"]!;
            _deleteUserAddress.UserLogin = _getTokenLogin.Execute(encryptedToken)!;
            _deleteUserAddress.AddressId = id;
            if (!_deleteUserAddress.Execute())
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
