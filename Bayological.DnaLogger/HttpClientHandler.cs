using System.Net.Http;
using System.Threading.Tasks;

namespace Bayological.DnaLogger
{
  public interface IHttpClientHandler
  {
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
  }

  public class HttpClientHandler : IHttpClientHandler
  {
    private HttpClient _client = new HttpClient();

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
      return await _client.SendAsync(request).ConfigureAwait(false);
    }
  }
}
