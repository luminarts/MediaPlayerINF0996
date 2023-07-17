using System.Collections.ObjectModel;
using MediaPlayerINF0996.Model;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Globalization;
using System.IO;
using System.Windows.Markup.Primitives;
using System.Windows.Controls;
using System;

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
                Play.NotifyCanExecuteChanged();
                Stop.NotifyCanExecuteChanged();
                Pause.NotifyCanExecuteChanged();
                Previous.NotifyCanExecuteChanged();
                Next.NotifyCanExecuteChanged();
                Mute.NotifyCanExecuteChanged();
            }
        }
        public ObservableCollection<Media> Medias { get; set; }
        public RelayCommand Play { get; set; }
        public RelayCommand Pause {get; set;}
        public RelayCommand Stop {get; set;}
        public RelayCommand Previous {get; set;}
        public RelayCommand Next {get; set;}
        public RelayCommand Mute {get; set;}
        public RelayCommand Video1 {get; set;}
        public RelayCommand Video2 {get; set;}
        public RelayCommand Video3 {get; set;}
        public MediaList()
        {
            Medias = new ObservableCollection<Media>();
            PrepareListCollection();
            Play = new RelayCommand(PlayCommand, CanPlayCommand);
            Stop = new RelayCommand(StopCommand, CanStopCommand);
            Pause = new RelayCommand(PauseCommand, CanPauseCommand);
            Previous = new RelayCommand(PreviousCommand, CanPreviousCommand);
            Next = new RelayCommand(NextCommand, CanNextCommand);
            Mute = new RelayCommand(MuteCommand, CanMuteCommand);
            Video1 = new RelayCommand(Video1Command);
            Video2 = new RelayCommand(Video2Command);
            Video3 = new RelayCommand(Video3Command);
            IsPlaying = false;
        }

        private void PrepareListCollection()
        {
            var media1 = new Media
            {
                Name = "The Pretender",
                Author = "Foo Fighters",
                MediaPath = new Uri(Path.GetFullPath(@"assets\videos\FooFighters-ThePretender.mp4"))
            };

            var media2 = new Media
            {
                Name = "Sinos",
                Author = "Unknown",
                MediaPath = new Uri(Path.GetFullPath(@"assets\videos\teste.mp4"))
            };

            var media3 = new Media
            {
                Name = "Hi",
                Author = "CG5",
                MediaPath = new Uri(Path.GetFullPath(@"assets\videos\videoplayback.mp4"))
            };

            Medias.Add(media1);
            Medias.Add(media2);
            Medias.Add(media3);
        }

        private bool isPlaying;
        public bool IsPlaying
        {
            get { return isPlaying; }
            set { SetProperty(ref isPlaying, value); }
        }

        private void PlayCommand()
        {
            WeakReferenceMessenger.Default.Send(new SetNewMediaMessage(SelectedMedia));
            WeakReferenceMessenger.Default.Send(new PlayRequestedMessage());
            IsPlaying = true;
        }

        public bool CanPlayCommand()
        {
            return SelectedMedia != null;
        }

        private void StopCommand()
        {
            WeakReferenceMessenger.Default.Send(new StopRequestedMessage());
            IsPlaying = false;
        }

        public bool CanStopCommand()
        {
            return SelectedMedia != null;
        }

        private void PauseCommand()
        {
            WeakReferenceMessenger.Default.Send(new PauseRequestedMessage());
            IsPlaying = false;
        }

        public bool CanPauseCommand()
        {
            return SelectedMedia != null;
        }

        public void PreviousCommand()
        {
            StopCommand();
            SelectedMedia = Medias[Medias.IndexOf(SelectedMedia) == 0 ? Medias.Count-1 : Medias.IndexOf(SelectedMedia)-1];
            PlayCommand();
        }

        public bool CanPreviousCommand()
        {
            return SelectedMedia != null;
        }

        public void NextCommand()
        {
            StopCommand();
            SelectedMedia = Medias[Medias.IndexOf(SelectedMedia) == Medias.Count-1 ? 0 : Medias.IndexOf(SelectedMedia)+1];
            PlayCommand();
        }

        public bool CanNextCommand()
        {
            return SelectedMedia != null;
        }

        public void MuteCommand()
        {
           WeakReferenceMessenger.Default.Send(new MuteRequestedMessage()); 
        }

        public bool CanMuteCommand()
        {
            return SelectedMedia != null;
        }
        private void Video1Command()
        {
            SelectedMedia = Medias[0];
            WeakReferenceMessenger.Default.Send(new SetNewMediaMessage(SelectedMedia));
            WeakReferenceMessenger.Default.Send(new PlayRequestedMessage());
        }

        private void Video2Command()
        {
            SelectedMedia = Medias[1];
            WeakReferenceMessenger.Default.Send(new SetNewMediaMessage(SelectedMedia));
            WeakReferenceMessenger.Default.Send(new PlayRequestedMessage());
        }

        private void Video3Command()
        {
            SelectedMedia = Medias[2];
            WeakReferenceMessenger.Default.Send(new SetNewMediaMessage(SelectedMedia));
            WeakReferenceMessenger.Default.Send(new PlayRequestedMessage());
        }

        public class PlayRequestedMessage
        {
            // Pode adicionar propriedades adicionais, se necessário
        }

        public class StopRequestedMessage
        {
            // Pode adicionar propriedades adicionais, se necessário
        }

        public class PauseRequestedMessage
        {
            // Pode adicionar propriedades adicionais, se necessário
        }

        public class SetNewMediaMessage : ValueChangedMessage<Media>
        {
            public SetNewMediaMessage(Media media) : base(media) {}        
        }

        public class MuteRequestedMessage
        {
            // Pode adicionar propriedades adicionais, se necessário
        }
    }
}