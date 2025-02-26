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
        //private readonly IServiceProvider _serviceProvider;
        //private IClient _client;
        public List<ContractNotification> Notifications { get; set; } = new();

        public NotificationService(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Connecting to SignalR hub");
            
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{ApiAddress.baseAdress}" + "contractNotifications")
                .WithAutomaticReconnect()
                .AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                })
                .Build();
            
            Console.WriteLine("Hub Connection Initial State: " + _hubConnection.State);
            
            // TODO: Populate Notifications from API - not here - move to ContractNotifications component

            //_serviceProvider = serviceProvider;
            //using (var scope = _serviceProvider.CreateScope())
            //{
            //    _client = scope.ServiceProvider.GetRequiredService<IClient>();
            //    try
            //    {
            //        //var notificationsFromAPI = _client.ApiContractNotificationsAsync();
            //        //Notifications.AddRange(notificationsFromAPI);
            //    }
            //    catch (ApiException ex)
            //    {
            //        if (ex.StatusCode >= 200 && ex.StatusCode <= 299)
            //        {
            //            Console.WriteLine("Success!");
            //        }
            //        else
            //        {
            //            Console.WriteLine(ex);
            //        }
            //    }
            //}
        }

        public async Task StartAsync()
        {
            // Listen to ReceiveContractUpdate method and handle notifications received from it
            _hubConnection.On<ContractNotification>("ReceiveContractUpdate", notification =>
            {
                Console.WriteLine("Received notification from API: " + notification.NotificationMessage);
                Notifications.Add(notification);
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