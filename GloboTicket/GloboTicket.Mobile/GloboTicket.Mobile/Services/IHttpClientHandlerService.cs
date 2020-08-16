using System.Net.Http;

namespace GloboTicket.Mobile.Services
{
    public interface IHttpClientHandlerService
    {
        HttpClientHandler GetInsecureHandler();

    }
}