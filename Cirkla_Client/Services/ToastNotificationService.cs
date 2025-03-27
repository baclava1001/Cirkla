using MudBlazor;

namespace Cirkla_Client.Services
{
    public class ToastNotificationService
    {
        public event Action<string, Severity, int> OnShow;
        public event Action OnHide;
        private string? _storedMessage;
        private Severity? _storedSeverity;
        private int? _storedDuration;


        public void ShowToast(string message, Severity type, int duration)
        {
            if (OnShow is not null)
            {
                OnShow?.Invoke(message, type, duration);
            }
            else
            {
                _storedMessage = message;
                _storedSeverity = type;
                _storedDuration = duration;
            }
        }

        public void HideToast()
        {
            OnHide?.Invoke();
        }

        // Call ShowToast with specific parameters
        public void ShowSuccess(string message, int dismissAfter = 3) => ShowToast(message, Severity.Success, dismissAfter);
        public void ShowError(string message, int dismissAfter = 3) => ShowToast(message, Severity.Error, dismissAfter);
        public void ShowWarning(string message, int dismissAfter = 3) => ShowToast(message, Severity.Warning, dismissAfter);
        public void ShowInfo(string message, int dismissAfter = 3) => ShowToast(message, Severity.Info, dismissAfter);
        public void ShowDefault(string message, int dismissAfter = 3) => ShowToast(message, Severity.Normal, dismissAfter);

        // "Safety net" method stores the message and severity in case the OnShow event is null (no subscribers).
        public void TryShowPendingMessage()
        {
            if (!string.IsNullOrEmpty(_storedMessage))
            {
                ShowToast(_storedMessage, _storedSeverity!.Value, _storedDuration.Value);
                _storedMessage = null;
                _storedSeverity = null;
                _storedDuration = 3;
            }
        }
    }
}
