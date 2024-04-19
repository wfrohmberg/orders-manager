using Microsoft.AspNetCore.Identity;
using OrdersManager.DbContexts;
using OrdersManager.Models;
using System.Windows.Input;

namespace OrdersManager.ModelsActions
{
    public class UserRegister
    {
        private readonly OrdersContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRegister(OrdersContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public string Login { get; set; }
        public string Password { get; set; }


        public void Execute()
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
            _context.Users.Add(user);
        }
    }
}
