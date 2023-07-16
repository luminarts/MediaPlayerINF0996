using System;
using System.IO;
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
using MediaPlayerINF0996.ViewModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Threading;

namespace MediaPlayerINF0996
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {      
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, e) =>
            {
                slider.Value = mediaPlayer.Position.TotalSeconds;
            };
            
            DataContext = new MediaList();

            WeakReferenceMessenger.Default.Register<MediaList.PlayRequestedMessage>(this,(r, m) =>
            {
                timer.Start();
                mediaPlayer.Play();
            });

            WeakReferenceMessenger.Default.Register<MediaList.StopRequestedMessage>(this,(r, m) =>
            {
                mediaPlayer.Stop();
            });

            WeakReferenceMessenger.Default.Register<MediaList.PauseRequestedMessage>(this,(r, m) =>
            {
                mediaPlayer.Pause();
            });

            WeakReferenceMessenger.Default.Register<MediaList.SetNewMediaMessage>(this,(r, m) =>
            {
                titulo.Text = m.Value.Name;
                mediaPlayer.Source = m.Value.MediaPath;
            });

            WeakReferenceMessenger.Default.Register<MediaList.UpdateRequestedMessage>(this,(r, m) =>
            {
                mediaPlayer.Position = TimeSpan.FromSeconds(slider.Value);
            });
            
            mediaPlayer.MediaOpened += (sender, e) =>
            {
                slider.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            };
            mediaPlayer.MediaEnded += (sender, e) =>
            {
                slider.Value = 0;
            };
            slider.ValueChanged += (sender, e) =>
            {
                mediaPlayer.Position = TimeSpan.FromSeconds(slider.Value);
            };
        }

    }
}
