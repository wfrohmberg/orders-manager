using OrdersManager.Common;
using OrdersManager.Controllers.Models.Login;
using OrdersManager.Exceptions;
using System.Text.Json;

namespace OrdersManager.ControllersActions
{
    public class CheckTokenExpiration
    {
        private readonly IConfiguration _configuration;

        public CheckTokenExpiration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValid(string encryptedToken)
        {
            var encryptionPassword = _configuration["Encryption:Password"];
            if (encryptionPassword == null)
            {
                throw new MissingConfigurationEntryException("Encryption:Password is missing");
            }
            try
            {
                string decryptedToken = encryptedToken.Decrypt(encryptionPassword);
                var token = JsonSerializer.Deserialize<Token>(decryptedToken);
                return token!.ExpirationTime > DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }
    }
}
