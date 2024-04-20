using OrdersManager.DbContexts;
using System.ComponentModel.DataAnnotations;

namespace OrdersManager.Controllers.Models.Orders
{
    public class OrderProductForm: IValidatableObject
    {
        public int ProductId { get; set; }
        public int Count { get; set; }

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