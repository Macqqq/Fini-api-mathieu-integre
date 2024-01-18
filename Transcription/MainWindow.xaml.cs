using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Transcription.Models;
using System.IO;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Transcription.View;

namespace Transcription
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Userdata user;
        public MainWindow()
        {
            InitializeComponent();
            AnimerFond();
            user = JsonSerializer.Deserialize<Userdata>(File.ReadAllText("../../Ressources/Userdata.json"));
        }

        private void AnimerFond()
        {
            ColorAnimation animation = new ColorAnimation();
            animation.From = Colors.Blue;
            animation.To = Colors.Red;
            animation.Duration = TimeSpan.FromSeconds(3);
            animation.AutoReverse = true;
            animation.RepeatBehavior = RepeatBehavior.Forever;

        }
        private void BP_Login_Click(object sender, RoutedEventArgs e)
        {
            Window_container.RowDefinitions.Clear();
            Window_container.Children.Clear();
            View.Page1 page1 = new Page1();
            Window_container.Children.Add(page1);
        }

        private void TB_Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_Login.Text == user.UserName && Pwd_Password.Password.ToString() == user.PassWord)
            {
                BP_Login.IsEnabled = true;
            }
        }
    }
}
