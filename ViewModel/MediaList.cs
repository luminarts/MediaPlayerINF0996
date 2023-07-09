using System.Collections.ObjectModel;
using MediaPlayerINF0996.Model;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Globalization;
using System.IO;

namespace MediaPlayerINF0996.ViewModel
{
    public class MediaList : ObservableObject
    {
        private Media selectedMedia;
        public Media SelectedMedia
        {
            get {return selectedMedia;}
            set
            {
                SetProperty(ref selectedMedia, value);
            }
        }
        public ObservableCollection<Media> Medias { get; set; }
        public RelayCommand Play { get; set; }
        public RelayCommand Pause {get; set;}
        public RelayCommand Stop {get; set;}
        public RelayCommand Video1 {get; set;}
        public RelayCommand Video2 {get; set;}
        public RelayCommand Video3 {get; set;}
        public MediaList()
        {
            Medias = new ObservableCollection<Media>();
            PrepareListCollection();
            Play = new RelayCommand(PlayCommand, CanPlayCommand);
            Stop = new RelayCommand(StopCommand, CanStopCommand);
            Video1 = new RelayCommand(Video1Command);
            Video2 = new RelayCommand(Video2Command);
            Video3 = new RelayCommand(Video3Command);
        }

        private void PrepareListCollection()
        {
            var media1 = new Media
            {
                Name = "The Pretender",
                MediaPath = Path.GetFullPath(@"..\assets\videos\FooFighters-ThePretender.mp4")
            };

            var media2 = new Media
            {
                Name = "The Pretender",
                MediaPath = Path.GetFullPath(@"..\assets\videos\teste.mp4")
            };

            var media3 = new Media
            {
                Name = "The Pretender",
                MediaPath = Path.GetFullPath(@"..\assets\videos\videoplayback.mp4")
            };

            Medias.Add(media1);
            Medias.Add(media2);
            Medias.Add(media3);
        }

        private void PlayCommand()
        {
            mediaPlayer.Play();
        }

        public bool CanPlayCommand()
        {
            return mediaPlayer.Source != null;
        }

        private void StopCommand()
        {
            mediaPlayer.Stop();
        }

        public bool CanStopCommand()
        {
            return mediaPlayer.Source != null;
        }

        private void Video1Command()
        {
            SelectedMedia = Medias[0];
            titulo.Text = "Foo Fighters - The Pretender";
            mediaPlayer.Play();
        }

        private void Video2Command()
        {
            SelectedMedia = Medias[1];
            titulo.Text = "Sinos";
            mediaPlayer.Play();
        }

        private void Video3Command()
        {
            SelectedMedia = Medias[2];
            titulo.Text = "CG5 - Hi";
            mediaPlayer.Play();
        }
        
        /*private void Video2(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Source = new Uri("C:\\Users\\sathy\\OneDrive\\Área de Trabalho\\trabalhoUI\\projeto\\assets\\videos\\teste.mp4");
            titulo.Text = "Sinos";
            mediaPlayer.Play();
        }

        private void Video3(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Source = new Uri("C:\\Users\\sathy\\OneDrive\\Área de Trabalho\\trabalhoUI\\projeto\\assets\\videos\\videoplayback.mp4");
            titulo.Text = "CG5 - Hi";
            mediaPlayer.Play();
        }*/

    }
}