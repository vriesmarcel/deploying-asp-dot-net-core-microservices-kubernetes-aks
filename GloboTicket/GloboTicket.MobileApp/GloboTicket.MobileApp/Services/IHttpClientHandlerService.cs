using System.Net.Http;

namespace GloboTicket.MobileApp.Services
{
    public interface IHttpClientHandlerService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
