using OrdersManager.DbContexts;
using OrdersManager.Models;
using System.Text.Json.Nodes;

namespace OrdersManager.ModelsActions
{
    public class LoadProductsAndTheirStocks
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;

        public LoadProductsAndTheirStocks(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
        }

        public void Execute()
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                using (var context = scope.ServiceProvider.GetService<OrdersContext>()!)
                using (var httpClient = new HttpClient())
                {
                    string url = _configuration["ProductsProvider:Url"]!;
                    var task = httpClient.GetStringAsync(url);
                    task.Wait();
                    dynamic result = JsonObject.Parse(task.Result);
                    foreach (JsonObject product in result["products"])
                    {
                        int id = product["id"].GetValue<int>();
                        var dbProduct = context.Products.FirstOrDefault(p => p.Id == id);
                        if (dbProduct == null)
                        {
                            dbProduct = new Product
                            {
                                Id = id,
                                Title = product["title"].GetValue<string>(),
                                Description = product["description"].GetValue<string>(),
                                Price = product["price"].GetValue<int>(),
                                Category = product["category"].GetValue<string>(),
                                Stock = product["stock"].GetValue<int>()
                            };
                            context.Products.Add(dbProduct);
                        }
                        else
                        {
                            dbProduct.Stock = product["stock"].GetValue<int>();
                            context.Products.Update(dbProduct);
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
