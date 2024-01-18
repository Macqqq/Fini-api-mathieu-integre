using System;
using System.IO;
using System.Threading.Tasks;

using NAudio.Wave;

using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;

namespace Transcription.Services
{
    internal class AWSPolly
    {
        private readonly AmazonPollyClient _pollyClient;

        public AWSPolly()
        {
            string awsAccessKeyId = "AKIA4MTWIJO2ZJJWWU7Q";
            string awsSecretAccessKey = "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";

            var awsCredentials = new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey);
            var awsRegionEndpoint = RegionEndpoint.GetBySystemName("eu-west-2");


            _pollyClient = new AmazonPollyClient(awsCredentials, awsRegionEndpoint);

        }

        public async Task SynthesizeSpeechAsync(string text)
        {
            var synthesizeSpeechRequest = new SynthesizeSpeechRequest
            {
                OutputFormat = OutputFormat.Mp3,
                VoiceId = VoiceId.Joanna,
                Text = text
            };

            try
            {
                var response = await _pollyClient.SynthesizeSpeechAsync(synthesizeSpeechRequest);

                // Obtenez le chemin du dossier du projet
                string projectFolder = AppDomain.CurrentDomain.BaseDirectory;
                string outputPath = Path.Combine(projectFolder, "speech.mp3");

                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    response.AudioStream.CopyTo(fileStream);
                    fileStream.Close();
                }

                // Jouer le fichier audio
                PlayAudio(outputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'appel à Amazon Polly: " + ex.Message);
            }
        }

        private void PlayAudio(string filePath)
        {
            try
            {
                using (var audioFile = new AudioFileReader(filePath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        // Attendez la fin de la lecture
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la lecture du fichier audio: " + ex.Message);
            }
        }
    }
}
