using System;
using System.Windows;
using MediaPlayerINF0996.ViewModel;
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

            // Configura o timer para atualizar a interface do usuário
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, e) =>
            {
                slider_seek.Value = mediaPlayer.Position.TotalSeconds;
            };

            mediaPlayer.MediaOpened += (sender, e) =>
            {
                slider_seek.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            };
            mediaPlayer.MediaEnded += (sender, e) =>
            {
                slider_seek.Value = 0;
            };
            slider_seek.ValueChanged += (sender, e) =>
            {
                mediaPlayer.Position = TimeSpan.FromSeconds(slider_seek.Value);
            };

            // Define o contexto de dados para a janela principal
            DataContext = new MediaList();

            // Registra os eventos para controlar a reprodução de mídia
            WeakReferenceMessenger.Default.Register<MediaList.PlayRequestedMessage>(this, (r, m) =>
            {
                // Inicia a reprodução da mídia
                timer.Start();
                mediaPlayer.Play();
            });

            WeakReferenceMessenger.Default.Register<MediaList.StopRequestedMessage>(this, (r, m) =>
            {
                // Para a reprodução da mídia
                mediaPlayer.Stop();
            });

            WeakReferenceMessenger.Default.Register<MediaList.PauseRequestedMessage>(this, (r, m) =>
            {
                // Pausa a reprodução da mídia
                mediaPlayer.Pause();
            });

            WeakReferenceMessenger.Default.Register<MediaList.SetNewMediaMessage>(this, (r, m) =>
            {
                // Atualiza a mídia atual
                titulo.Text = m.Value.Name;
                mediaPlayer.Source = m.Value.MediaPath;
            });

            WeakReferenceMessenger.Default.Register<MediaList.MuteRequestedMessage>(this, (r, m) =>
            {
                // Muta ou desmuta o media player
                if(mediaPlayer.Volume > 0)
                    mediaPlayer.Volume = 0;
                else
                    mediaPlayer.Volume = slider_vol.Value;
            });
        }
        // Atualiza o volume do media element
        private void Slider_vol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = slider_vol.Value;
        }
    }
}
