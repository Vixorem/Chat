using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Chat.Hubs
{
    /// <summary>
    /// Хаб
    /// </summary>
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;

        /// <summary>
        /// .ctor
        /// </summary>
        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public async Task Send(string message)
        {
            _logger.LogInformation("Received message: " + message);
            await Clients.All.SendAsync("Receive", "Принято: " + message);
        }
    }
}