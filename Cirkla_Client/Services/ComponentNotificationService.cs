namespace Cirkla_Client.Services
{
    public class ComponentNotificationService
    {            
        // Action to notify components of updates
        public Action OnNotify { get; set; }

        // Method to trigger the notification
        public void NotifyStateChanged() => OnNotify?.Invoke();
    }
}
