using Cirkla.ApiClient;

namespace Cirkla.BlazorClient.ViewModels;

public class InfoCardVM
{
    public string Title { get; set; }
    public string EventDate { get; set; }
    public string Content { get; set; }
    public Contract Contract { get; set; }
}