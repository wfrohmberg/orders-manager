using Microsoft.EntityFrameworkCore;
using OrdersManager.DbContexts;

namespace OrdersManager.ModelsActions
{
    public class IsUserAddress
    {
        private readonly IServiceScopeFactory _scopeFactory;
        
        public IsUserAddress(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public string UserLogin { get; set; }
        public int AddressId { get; set; }

        public bool Check()
        {
            using (var scope = _scopeFactory.CreateScope())
            using (var context = scope.ServiceProvider.GetService<OrdersContext>()!)
            {
                var address = context.Addresses
                                     .Include(a => a.User)
                                     .First(a => a.Id == AddressId);
                return address.User.Login == UserLogin && !address.IsDeleted;
            }
        }
    }
}
