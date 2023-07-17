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

            // Configura o timer para atualizar a interface do usuário
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, e) =>
            {
                //slider.Value = mediaPlayer.Position.TotalSeconds;
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
               // titulo2.Text = m.Value.Name;
               // autor2.Text = m.Value.Author;
                mediaPlayer.Source = m.Value.MediaPath;
            });

            /*mediaPlayer.MediaOpened += (sender, e) =>
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
            };*/
        }
    }
}
