using Newtonsoft.Json;

namespace Bayological.DnaLogger
{
  [JsonObject]
  public class Line
  {
    [JsonProperty(PropertyName = "lines")]
    public LogLine[] Lines { get; set; }
  }
  
  [JsonObject]
  public class LogLine
  {
    [JsonProperty(PropertyName = "line")]
    public string Line { get; set; }
    [JsonProperty(PropertyName = "app")]
    public string App { get; set; }
    [JsonProperty(PropertyName = "level")]
    public string Level { get; set; }
    [JsonProperty(PropertyName = "env")]
    public string Env { get; set; }
  }
}
