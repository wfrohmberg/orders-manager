using OrdersManager.Controllers.Models.Orders;
using OrdersManager.DbContexts;
using OrdersManager.Models;

namespace OrdersManager.ModelsActions
{
    public class AddUserOrder
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AddUserOrder(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public bool Execute(OrderForm orderForm)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                using (var context = scope.ServiceProvider.GetService<OrdersContext>()!)
                {
                    var order = new Order
                    {
                        AddressId = orderForm.AddressId
                    };
                    context.Orders.Add(order);
                    context.SaveChanges();
                    context.Entry(order).Collection(o => o.OrderProducts).Load();
                    foreach (var product in orderForm.Products)
                    {
                        order.OrderProducts.Add(new OrderProduct { ProductId = product.ProductId, Count = product.Count });
                    }
                    context.Update(order);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
