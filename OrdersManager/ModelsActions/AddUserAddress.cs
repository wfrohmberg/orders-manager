using OrdersManager.Controllers.Models.Addresses;
using OrdersManager.DbContexts;
using OrdersManager.Models;

namespace OrdersManager.ModelsActions
{
    public class AddUserAddress
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AddUserAddress(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        } 

        public AddressForm AddressForm { get; set; }
        public string UserLogin { get; set; }
        public bool Execute()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<OrdersContext>()!)
                {
                    try
                    {
                        var user = context.Users.FirstOrDefault(u => u.Login ==  UserLogin);
                        Address address = new Address
                        {
                            City = AddressForm.City,
                            EmailAddress = AddressForm.EmailAddress,
                            FlatNumber = AddressForm.FlatNumber,
                            HouseNumber = AddressForm.HouseNumber,
                            PhoneNumber = AddressForm.PhoneNumber,
                            PostalCode = AddressForm.PostalCode,
                            Street = AddressForm.Street,
                            User = user
                        };
                        context.Addresses.Add(address);
                        context.SaveChanges();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
    }
}
