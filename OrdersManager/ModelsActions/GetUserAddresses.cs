using Microsoft.EntityFrameworkCore;
using OrdersManager.Controllers.Models.Addresses;
using OrdersManager.DbContexts;
using OrdersManager.Models;

namespace OrdersManager.ModelsActions
{
    public class GetUserAddresses
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public GetUserAddresses(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public List<AddressWithId> Execute(string userLogin)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<OrdersContext>()!)
                {
                    var user = context.Users.Include(u => u.Addresses)
                                            .First(u => u.Login == userLogin);
                    return user.Addresses
                               .Select(a => new AddressWithId(a))
                               .ToList();
                    
                }
            }
        }

    }
}
