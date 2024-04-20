using OrdersManager.Common;
using OrdersManager.Controllers.Models.Login;
using OrdersManager.Exceptions;
using System.Text.Json;

namespace OrdersManager.ControllersActions
{
    public class GetTokenLogin
    {
        private readonly IConfiguration _configuration;

        public GetTokenLogin(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? Execute(string encryptedToken)
        {
            var encryptionPassword = _configuration["Encryption:Password"];
            if (encryptionPassword == null)
            {
                throw new MissingConfigurationEntryException("Encryption:Password is missing");
            }
            try 
            { 
                string decryptedToken = encryptedToken.Decrypt(encryptionPassword);
                var token = JsonSerializer.Deserialize<Token>(decryptedToken)!;
                return token.Login;
            }
            catch
            {
                return null;
            }
        }
    }
}
