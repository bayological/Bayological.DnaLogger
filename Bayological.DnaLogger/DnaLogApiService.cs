using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bayological.DnaLogger
{
  public interface IDnaLogApiService
  {
    Task IngestLog(Line logLine);
  }

  public class DnaLogApiService : IDnaLogApiService
  {
    private readonly IHttpClientHandler _httpClientHandler;

    public DnaLogApiService(IHttpClientHandler httpClientHandler)
    {
      _httpClientHandler = httpClientHandler;
    }

    public async Task IngestLog(Line logLine)
    {
      await Task.Factory.StartNew(async () =>
      {
        var request = GetRequest();
        request.Content = new StringContent(SerializeLogLine(logLine), Encoding.UTF8, "application/json");
        await _httpClientHandler.SendAsync(request);
      });
    }

    private HttpRequestMessage GetRequest()
    {
      var request = new HttpRequestMessage
      {
        Method = HttpMethod.Post,
        RequestUri = new Uri($"https://logs.logdna.com/logs/ingest?hostname={Dns.GetHostName()}&compress=1")
      };

      request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("x:KEYGOESHERE")));
      return request;
    }

    private string SerializeLogLine(Line logLine)
    {
      return JsonConvert.SerializeObject(logLine);
    }

  }
}
