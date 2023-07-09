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
using MediaPlayerINF0996.ViewModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

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
            DataContext = new MediaList();

            WeakReferenceMessenger.Default.Register<MediaList.PlayRequestedMessage>(this,(r, m) =>
            {
                mediaPlayer.Play();
            });

            WeakReferenceMessenger.Default.Register<MediaList.StopRequestedMessage>(this,(r, m) =>
            {
                mediaPlayer.Stop();
            });

            WeakReferenceMessenger.Default.Register<MediaList.SetNewMediaMessage>(this,(r, m) =>
            {
                Console.WriteLine(m.Value.MediaPath.ToString());
                mediaPlayer.Source = m.Value.MediaPath;
            });
        }
    }
}
