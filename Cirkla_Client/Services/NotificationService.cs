using Cirkla.ApiClient;
using Cirkla_Client.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Cirkla_Client.Services
{
    public class NotificationService : IAsyncDisposable
    {
        private readonly HubConnection _hubConnection;
        private readonly IServiceProvider _serviceProvider;
        private IClient _client;
        public List<ContractNotification> Notifications { get; set; } = new();

        public NotificationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Console.WriteLine("Connecting to SignalR hub");
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{ApiAddress.baseAdress}" + "contractNotifications")
                .WithAutomaticReconnect()
                .Build();

            // TODO: Populate Notifications from API here?
            using (var scope = _serviceProvider.CreateScope())
            {
                _client = scope.ServiceProvider.GetRequiredService<IClient>();

                // TODO: Populate Notifications from API here?
                try
                {
                    var notificationsFromAPI = _client.ApiContractNotificationsAsync();
                    Notifications.AddRange<ContractNotification>(notificationsFromAPI);
                }
                catch (ApiException ex)
                {
                    if (ex.StatusCode >= 200 && ex.StatusCode <= 299)
                    {
                        Console.WriteLine("Success!");
                    }
                    else
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        public async Task StartAsync()
        {
            // Listen to ReceiveContractUpdate method and handle notifications received from it
            _hubConnection.On<ContractNotification>("ReceiveContractUpdate", notification =>
            {
                Console.WriteLine("Received notification: " + notification.NotificationMessage);
                Notifications.Add(notification);
                NotifyStateChanged();
            });

            // Start SignalR connection
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