using System.ComponentModel;
using System.Diagnostics;
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


        // Shows toast immediately if OnShow is not null, otherwise store the message
        public void ShowToast(string message, Severity type, int duration)
        {
            if (OnShow is not null)
            {
                OnShow?.Invoke(message, type, duration);
            }
            else
            {
                SetPendingMessage(message, type, duration);
            }
        }

        // Stores the message and its parameters for later use
        public void SetPendingMessage(string message, Severity type, int duration = 3)
        {
            _storedMessage = message;
            _storedSeverity = type;
            _storedDuration = duration;
        }

        // Shows stored message if there is one
        public void TryShowPendingMessage()
        {
            if (!string.IsNullOrEmpty(_storedMessage))
            {
                ShowToast(_storedMessage, _storedSeverity!.Value, _storedDuration!.Value);

                // Resets message
                _storedMessage = null;
                _storedSeverity = null;
                _storedDuration = 3;
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

        // Call ShowToast with specific parameters
        public void ShowDelayedSuccess(string message, int dismissAfter = 3) => SetPendingMessage(message, Severity.Success, dismissAfter);
        public void ShowDelayedError(string message, int dismissAfter = 3) => SetPendingMessage(message, Severity.Error, dismissAfter);
        public void ShowDelayedWarning(string message, int dismissAfter = 3) => SetPendingMessage(message, Severity.Warning, dismissAfter);
        public void ShowDelayedInfo(string message, int dismissAfter = 3) => SetPendingMessage(message, Severity.Info, dismissAfter);
        public void ShowDelayedDefault(string message, int dismissAfter = 3) => SetPendingMessage(message, Severity.Normal, dismissAfter);
    }
}
