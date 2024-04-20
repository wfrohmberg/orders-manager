using Microsoft.EntityFrameworkCore;
using OrdersManager.DbContexts;

namespace OrdersManager.ModelsActions
{
    public class DeleteUserAddress
    {
        private IServiceScopeFactory _scopeFactory;

        public DeleteUserAddress(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public string UserLogin { get; set; }
        public int AddressId { get; set; }

        public bool Execute()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<OrdersContext>()!)
                {
                    var user = context.Users.Include(u => u.Addresses)
                                            .First(u => u.Login == UserLogin);
                    if (!user.Addresses.Any(a => a.Id == AddressId && !a.IsDeleted))
                    {
                        return false;
                    }
                    else
                    {
                        var address = context.Addresses.First(a => a.Id == AddressId);
                        address.IsDeleted = true;
                        context.Addresses.Update(address);
                        context.SaveChanges();
                        return true;
                    }
                }
            }
        }
    }
}
