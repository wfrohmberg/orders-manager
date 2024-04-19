using Microsoft.AspNetCore.Identity;
using OrdersManager.DbContexts;
using OrdersManager.Models;

namespace OrdersManager.ModelsActions
{
    public class UserCredentialsCheck
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserCredentialsCheck(IServiceScopeFactory scopeFactory, IPasswordHasher<User> passwordHasher)
        {
            _scopeFactory = scopeFactory;
            _passwordHasher = passwordHasher;
        }

        public string? Login { get; set; }
        public string? Password { get; set; }

        public bool Execute()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<OrdersContext>()!)
                {
                    var user = context.Users.FirstOrDefault(u => u.Login ==  Login);
                    if (user == null)
                    {
                        return false;
                    }
                    if (Password == null)
                    {
                        return false;
                    }
                    var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, Password);
                    return verificationResult == PasswordVerificationResult.Success;
                }
            }
        }
    }
}
