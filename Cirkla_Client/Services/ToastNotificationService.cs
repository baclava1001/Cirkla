namespace Cirkla_Client.Services
{
    public class ToastNotificationService
    {
        public event Action<string, string, int> OnShow;
        public event Action OnHide;


        public void ShowToast(string message, string type, int duration)
        {
            OnShow?.Invoke(message, type, duration);
        }

        public void HideToast()
        {
            OnHide?.Invoke();
        }

        // Call ShowToast with specific parameters
        public void ShowSuccess(string message, int dismissAfter = 3) => ShowToast(message, "Success", dismissAfter);
        public void ShowError(string message, int dismissAfter = 3) => ShowToast(message, "Error", dismissAfter);
        public void ShowWarning(string message, int dismissAfter = 3) => ShowToast(message, "Warning", dismissAfter);
        public void ShowInfo(string message, int dismissAfter = 3) => ShowToast(message, "Info", dismissAfter);
        public void ShowDefault(string message, int dismissAfter = 3) => ShowToast(message, "Normal", dismissAfter);
    }
}
