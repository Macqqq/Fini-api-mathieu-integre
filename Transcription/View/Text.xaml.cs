using System;
using System.Windows;
using System.Windows.Controls;
using Transcription.Services; // Ensure this is the correct namespace

namespace Transcription.View
{
    public partial class Text : UserControl
    {
        private AWSPolly _awsPolly;
        private AWSComprehend _awsComprehend;

        public Text()
        {
            InitializeComponent();
            _awsPolly = new AWSPolly();
            _awsComprehend = new AWSComprehend();
            BTN_Retour.Click += BTN_Retour_Click;
        }

        private async void BTN_Resumer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string textToSynthesize = TB_Entry.Text;
            if (!string.IsNullOrWhiteSpace(textToSynthesize))
            {
                await _awsPolly.SynthesizeSpeechAsync(textToSynthesize);
            }

            string textToAnalyze = TB_Entry.Text;
            if (!string.IsNullOrWhiteSpace(textToAnalyze))
            {
                string analysisResult = await _awsComprehend.AnalyzeAndExplainText(textToAnalyze);
                TB_Exit.Text = analysisResult;
            }
        }
        private void BTN_Retour_Click(object sender, RoutedEventArgs e)
        {
            Window_container.RowDefinitions.Clear();
            Window_container.Children.Clear();
            View.Page1 page1 = new Page1();
            Window_container.Children.Add(page1);
        }

        private void BTN_PDF_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
