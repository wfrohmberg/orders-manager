namespace OrdersManager.Controllers.Models.Login
{
    public class Token
    {
        public DateTime ExpirationTime { get; set; }
        public string Login { get; set; }
    }
}
