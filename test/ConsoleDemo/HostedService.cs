using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    public class HostedService : IHostedService
    {
        private readonly ILogger _logger;

        public HostedService(ILogger<HostedService> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("哈哈哈哈");
            _logger.LogError("boooooooooo!");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            return;
        }
    }
}
