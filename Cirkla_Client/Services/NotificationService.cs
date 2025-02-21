using Cirkla.ApiClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Cirkla_Client.Services
{
    public class NotificationService : IAsyncDisposable
    {
        private readonly HubConnection _hubConnection;
        public List<ContractNotification> Notifications { get; private set; } = new();

        public NotificationService(NavigationManager navigationManager)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(navigationManager.ToAbsoluteUri("contractNotifications"))
                .Build();

            _hubConnection.On<ContractNotification>("ReceiveContractUpdate", notification =>
            {
                Notifications.Add(notification);
                NotifyStateChanged();
            });
        }

        public async Task StartAsync()
        {
            await _hubConnection.StartAsync();
            Console.WriteLine("SignalR connection started.");
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
                Console.WriteLine("SignalR connection disposed.");
            }
        }

        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}