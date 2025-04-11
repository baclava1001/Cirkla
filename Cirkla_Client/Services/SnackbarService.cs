using MudBlazor;

namespace Cirkla_Client.Services
{
    public class SnackbarService(ISnackbar snackbar)
    {
        public void Success(string message, int duration = 3000) =>
            snackbar.Add(message, Severity.Success, config => config.VisibleStateDuration = duration);

        public void Error(string message, int duration = 3000) =>
            snackbar.Add(message, Severity.Error, config => config.VisibleStateDuration = duration);

        public void Info(string message, int duration = 3000) =>
            snackbar.Add(message, Severity.Info, config => config.VisibleStateDuration = duration);

        public void Warning(string message, int duration = 3000) =>
            snackbar.Add(message, Severity.Warning, config => config.VisibleStateDuration = duration);
    }
}
