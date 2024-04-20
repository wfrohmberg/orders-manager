namespace OrdersManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}
