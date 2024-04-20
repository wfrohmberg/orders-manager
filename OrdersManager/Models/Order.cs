namespace OrdersManager.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public bool Cancelled { get; set; } = false;
        public bool Executed { get; set; } = false;
    }
}
