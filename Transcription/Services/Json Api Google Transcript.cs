using Newtonsoft.Json;
using System.Collections.Generic;

public class TranscriptionResponse
{
    [JsonProperty("results")]
    public List<Result> Results { get; set; }
}

public class Result
{
    [JsonProperty("alternatives")]
    public List<Alternative> Alternatives { get; set; }
}

public class Alternative
{
    [JsonProperty("transcript")]
    public string Transcript { get; set; }
}