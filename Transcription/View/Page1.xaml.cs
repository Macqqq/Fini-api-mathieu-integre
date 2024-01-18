using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Transcription.Services;

namespace Transcription.View
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Page1 : UserControl
    {
        public Page1()
        {
            InitializeComponent();
            BTN_Texte.Click += BTN_Texte_Click;
            BTN_MP3.Click += BTN_MP3_Click;
        }

        private void BTN_Texte_Click(object sender, RoutedEventArgs e)
        {
            Windows_container.RowDefinitions.Clear();
            Windows_container.Children.Clear();
            Text textPage = new Text(); 
            Windows_container.Children.Add(textPage);
        }
        private void BTN_MP3_Click(object sender, RoutedEventArgs e)
        {
            Windows_container.RowDefinitions.Clear();
            Windows_container.Children.Clear();
            Button button = sender as Button;
            MP3 mp3Page = new MP3();
            Windows_container.Children.Add(mp3Page);
        }

    }
}
