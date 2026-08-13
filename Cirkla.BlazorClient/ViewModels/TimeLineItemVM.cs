using Cirkla.BlazorClient.Constants;
using MudBlazor;

namespace Cirkla.BlazorClient.ViewModels;

public class TimeLineItemVM
{
    public string EventName { get; set; }
    public bool IsActive { get; set; }
    public Color Color { get; set; }
    public bool ShowInfoCard { get; set; }
    public InfoCardVM InfoCardData { get; set; }
}