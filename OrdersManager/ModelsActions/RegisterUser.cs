using Microsoft.AspNetCore.Identity;
using OrdersManager.DbContexts;
using OrdersManager.Models;
using System.Windows.Input;

namespace OrdersManager.ModelsActions
{
    public class RegisterUser
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterUser(IServiceScopeFactory factory, IPasswordHasher<User> passwordHasher)
        {
            _scopeFactory = factory;
            _passwordHasher = passwordHasher;
        }

        public string Login { get; set; }
        public string Password { get; set; }


        public void Execute()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<OrdersContext>())
                {
                    if (Login == null)
                    {
                        throw new ArgumentNullException(nameof(Login));
                    }
                    if (Password == null)
                    {
                        throw new ArgumentNullException(nameof(Password));
                    }

                    var user = new User
                    {
                        Login = Login
                    };
                    user.PasswordHash = _passwordHasher.HashPassword(user, Password);
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        }
    }
}
