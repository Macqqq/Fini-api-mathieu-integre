using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace Transcription.Services
{
    internal class MP3ToText
    {
        private readonly string apiKey = "AIzaSyDlzxVyYU2gAhaECs8QVFtlrJnJJzjQY9s";
        private HttpClient client;

        public MP3ToText()
        {
            client = new HttpClient();
        }

        public async Task<string> ConvertAudioToText(string audioFilePath)
        {
            string url = $"https://speech.googleapis.com/v1/speech:recognize?key={apiKey}";

            var requestBody = new
            {
                config = new
                {
                    encoding = "LINEAR16",
                    sampleRateHertz = 44100,
                    languageCode = "fr-FR"
                },
                audio = new
                {
                    content = Convert.ToBase64String(File.ReadAllBytes(audioFilePath))
                }
            };

            string json = JsonConvert.SerializeObject(requestBody);

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await client.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();

            // Utilisez GetTranscriptFromJson pour extraire le transcript du texte
            return GetTranscriptFromJson(responseContent);
        }

        private string GetTranscriptFromJson(string jsonResponse)
        {
            var transcriptionResponse = JsonConvert.DeserializeObject<TranscriptionResponse>(jsonResponse);

            if (transcriptionResponse?.Results != null && transcriptionResponse.Results.Count > 0)
            {
                var firstResult = transcriptionResponse.Results[0];
                if (firstResult?.Alternatives != null && firstResult.Alternatives.Count > 0)
                {
                    return firstResult.Alternatives[0].Transcript;
                }
            }

            return "Aucun transcript trouvé.";
        }
    }

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
}