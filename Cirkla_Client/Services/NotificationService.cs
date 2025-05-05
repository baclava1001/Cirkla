using System.Text.Json;
using System.Text.Json.Serialization;
using Cirkla.ApiClient;
using Cirkla_Client.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Cirkla_Client.Services
{
    public class NotificationService : IAsyncDisposable
    {
        private readonly HubConnection _hubConnection;
        public List<ContractNotificationForViews> PushNotifications { get; set; } = new();

        public NotificationService(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Frontend service connecting to SignalR hub");

            // Get the base URL from the configuration
            var apiBaseUrl = serviceProvider.GetRequiredService<IConfiguration>().GetValue<string>("ApiBaseUrl");

            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{apiBaseUrl}contractNotifications")
                .WithAutomaticReconnect()
                .AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                })
                .Build();
            
            Console.WriteLine("Hub Connection Initial State: " + _hubConnection.State);
        }

        public async Task StartAsync()
        {
            // Listen to ReceiveContractUpdate method and handle notifications received from it
            _hubConnection.On<ContractNotificationForViews>("ReceiveContractUpdate", notification =>
            {
                Console.WriteLine("Received notification from API: " + notification.NotificationMessage);
                PushNotifications.Add(notification);
                NotifyStateChanged();
            });

            // Start SignalR connection
            await _hubConnection.StartAsync();
            await _hubConnection.InvokeAsync("TestConnection");
            Console.WriteLine("Hub Connection State: " + _hubConnection.State);
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
                Console.WriteLine("SignalR connection disposed.");
            }
        }

        public event Func<Task> OnChange;

        public async Task NotifyStateChanged()
        {
            if (OnChange != null)
            {
                await OnChange.Invoke();
                Console.WriteLine("NotificationService notifying: State changed!");
            }
        }
    }
}