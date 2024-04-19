
namespace OrdersManager
{
    public class Boot : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => { });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
