using Microsoft.EntityFrameworkCore;
using OrdersManager.DbContexts;
using OrdersManager.Exceptions;

namespace OrdersManager.ModelsActions
{
    public class RegisterSuperUser
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private readonly RegisterUser _registerUser;

        public RegisterSuperUser(IServiceScopeFactory scopeFactory, IConfiguration configuration, RegisterUser registerUser)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
            _registerUser = registerUser;
        }

        public void Execute()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<OrdersContext>())
                {
                    if (_configuration["SuperUser:AutoCreate"] != "true")
                    {
                        return;
                    }
                    var user = context.Users.FirstOrDefault(u => u.Login == _configuration["SuperUser:Login"]);
                    if (user != null)
                    {
                        return;
                    }
                    var superUserLogin = _configuration["SuperUser:Login"];
                    if (superUserLogin == null)
                    {
                        throw new MissingConfigurationEntryException("Missing SuperUser:Login entry");
                    }
                    _registerUser.Login = superUserLogin;
                    var initialPassword = _configuration["SuperUser:InitialPassword"];
                    if (initialPassword == null)
                    {
                        throw new MissingConfigurationEntryException("Missing SuperUser:InitialPassword");
                    }
                    _registerUser.Password = initialPassword;
                    _registerUser.Execute();
                }
            }
        }
    }
}
