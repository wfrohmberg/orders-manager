﻿namespace OrdersManager.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
