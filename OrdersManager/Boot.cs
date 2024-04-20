
using OrdersManager.ModelsActions;

namespace OrdersManager
{
    public class Boot : IHostedService
    {
        private readonly RegisterSuperUser _registerSuperUser;
        private readonly LoadProductsAndTheirStocks _loadProducts;
        public Boot(RegisterSuperUser registerSuperUser, LoadProductsAndTheirStocks loadProducts) 
        { 
            _registerSuperUser = registerSuperUser;
            _loadProducts = loadProducts;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => 
            { 
                _registerSuperUser.Execute(); 
                _loadProducts.Execute();
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
