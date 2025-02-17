using Cirkla.ApiClient;

namespace Cirkla_Client.ViewModels;

public class InfoCardVM
{
    public string Title { get; set; }
    public string EventDate { get; set; }
    public string Content { get; set; }
    public Contract Contract { get; set; }
}