using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Transcription.Services;

namespace Transcription.View
{
    /// <summary>
    /// Interaction logic for MP3.xaml
    /// </summary>
    public partial class MP3 : UserControl
    {
        private SpeechToMP3 speechToMp3;

        public MP3()
        {
            InitializeComponent();
            speechToMp3 = new SpeechToMP3();
            LoadAudioDevices();

            // Set the delegate to update the TextBox
            speechToMp3.UpdateTextBoxAction = UpdateTranscriptionTextBox;
        }

        private void BTN_Lancer_Click(object sender, RoutedEventArgs e)
        {
            // Check if a device is selected
            int selectedDevice = comboBoxDevices.SelectedIndex;
            if (selectedDevice >= 0)
            {
                // Ensure not to start recording if one is already in progress
                speechToMp3.StartRecording(selectedDevice);
            }
            else
            {
                MessageBox.Show("No device is selected", "Error");
            }
        }

        private void LoadAudioDevices()
        {
            var devices = speechToMp3.GetAudioDevices();
            comboBoxDevices.ItemsSource = devices;
            if (comboBoxDevices.Items.Count > 0)
            {
                comboBoxDevices.SelectedIndex = 0;
            }
        }

        private void BTN_Retour_Click(object sender, RoutedEventArgs e)
        {
            Window_container.RowDefinitions.Clear();
            Window_container.Children.Clear();
            View.Page1 page1 = new Page1();
            Window_container.Children.Add(page1);
        }

        private void BTN_Arreter_Click(object sender, RoutedEventArgs e)
        {
            speechToMp3.StopRecording();
            MessageBox.Show($"Recording stopped.\nFile saved at: {speechToMp3.LastRecordedFilePath}", "MP3 Recording");
        }

        private void UpdateTranscriptionTextBox(string text)
        {
            // Check if the operation needs to be performed on the UI thread
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => TB_texte.Text = text);
            }
            else
            {
                TB_texte.Text = text;
            }
        }

    }
}
