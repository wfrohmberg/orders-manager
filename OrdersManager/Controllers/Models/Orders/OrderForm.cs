namespace OrdersManager.Controllers.Models.Orders
{
    public class OrderForm
    {
        public int AddressId { get; set; }
        public List<OrderProductForm> Products { get; set; }
    }
}
