using OrdersManager.Common;
using OrdersManager.Controllers.Models.Login;
using OrdersManager.Exceptions;
using System.Text.Json;

namespace OrdersManager.ControllersActions
{
    public class GenerateEncryptedToken
    {
        private readonly IConfiguration _configuration;

        public GenerateEncryptedToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Execute(string login)
        {
            var token = new Token
            {
                ExpirationTime = DateTime.UtcNow + TimeSpan.FromHours(2),
                Login = login
            };
            string serializedToken = JsonSerializer.Serialize(token);
            var encryptionPassword = _configuration["Encryption:Password"];
            if (encryptionPassword == null)
            {
                throw new MissingConfigurationEntryException("Encryption:Password is missing");
            }
            return serializedToken.Encrypt(encryptionPassword);
        }
        

    }
}
