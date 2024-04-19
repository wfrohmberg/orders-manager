
using OrdersManager.ModelsActions;

namespace OrdersManager
{
    public class Boot : IHostedService
    {
        private readonly RegisterSuperUser _registerSuperUser;
        public Boot(RegisterSuperUser registerSuperUser) 
        { 
            _registerSuperUser = registerSuperUser;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => { _registerSuperUser.Execute(); });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
