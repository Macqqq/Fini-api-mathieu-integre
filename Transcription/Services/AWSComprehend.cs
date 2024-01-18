using System;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using Amazon.Runtime;

namespace Transcription.Services
{
    internal class AWSComprehend
    {
        private readonly AmazonComprehendClient _comprehendClient;

        public AWSComprehend()
        {
            string awsAccessKeyId = "AKIA4MTWIJO2ZJJWWU7Q";
            string awsSecretAccessKey = "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";

            var awsCredentials = new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey);
            var awsRegionEndpoint = RegionEndpoint.GetBySystemName("eu-west-2");

            _comprehendClient = new AmazonComprehendClient(awsCredentials, awsRegionEndpoint);
        }

        public async Task<string> AnalyzeAndExplainText(string text)
        {
            var sentimentRequest = new DetectSentimentRequest
            {
                Text = text,
                LanguageCode = "en" 
            };

            var entitiesRequest = new DetectEntitiesRequest
            {
                Text = text,
                LanguageCode = "en" 
            };

            var keyPhrasesRequest = new DetectKeyPhrasesRequest
            {
                Text = text,
                LanguageCode = "en" 
            };

            try
            {
                var sentimentResponse = await _comprehendClient.DetectSentimentAsync(sentimentRequest);
                var entitiesResponse = await _comprehendClient.DetectEntitiesAsync(entitiesRequest);
                var keyPhrasesResponse = await _comprehendClient.DetectKeyPhrasesAsync(keyPhrasesRequest);

                // Construction de l'explication basée sur les réponses
                var explanation = new StringBuilder();
                explanation.AppendLine("Analyse du Texte:");
                explanation.AppendLine($"Sentiment: {sentimentResponse.Sentiment}");
                explanation.AppendLine("Entités Détectées:");

                foreach (var entity in entitiesResponse.Entities)
                {
                    explanation.AppendLine($"- {entity.Text} ({entity.Type})");
                }

                explanation.AppendLine("Phrases Clés:");
                foreach (var phrase in keyPhrasesResponse.KeyPhrases)
                {
                    explanation.AppendLine($"- {phrase.Text}");
                }

                return explanation.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'appel à Amazon Comprehend: " + ex.Message);
                return null;
            }
        }
    }
}
