using OrdersManager.DbContexts;
using System.ComponentModel.DataAnnotations;

namespace OrdersManager.Models
{
    public class OrderProduct: IValidatableObject
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var scope = validationContext.CreateScope())
            using (var context = scope.ServiceProvider.GetService<OrdersContext>()!)
            {
                var product = context.Products.First(p => p.Id == ProductId);
                if (product.Stock < Count)
                {
                    yield return new ValidationResult($"Ordered product cannot exceed the number of product stock", 
                                                      new[] { nameof(Count) });  
                }
            }
        }
    }
}
