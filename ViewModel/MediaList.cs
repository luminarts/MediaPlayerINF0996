using System.Collections.ObjectModel;
using MediaPlayerINF0996.Model;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.IO;
using System;

namespace MediaPlayerINF0996.ViewModel
{
    public class MediaList : ObservableObject
    {
        // atributo e propriedade da midia que está selecionada para executar no app
        private Media selectedMedia;
        public Media SelectedMedia
        {
            get {return selectedMedia;}
            set
            {   
                // notifica os botoes que dependem de uma midia selecionada para funcionar
                SetProperty(ref selectedMedia, value);
                Play.NotifyCanExecuteChanged();
                Stop.NotifyCanExecuteChanged();
                Pause.NotifyCanExecuteChanged();
                Previous.NotifyCanExecuteChanged();
                Next.NotifyCanExecuteChanged();
                Mute.NotifyCanExecuteChanged();
            }
        }
        //lista de midias disponiveis
        public ObservableCollection<Media> Medias { get; set; }
        // comandos criados para executar nos botoes
        public RelayCommand Play { get; set; }
        public RelayCommand Pause {get; set;}
        public RelayCommand Stop {get; set;}
        public RelayCommand Previous {get; set;}
        public RelayCommand Next {get; set;}
        public RelayCommand Mute {get; set;}
        public MediaList()
        {
            // cria uma lista com todas as midias disponiveis no app e cria o eventos de botoes
            Medias = new ObservableCollection<Media>();
            PrepareListCollection();
            Play = new RelayCommand(PlayCommand, CanPlayCommand);
            Stop = new RelayCommand(StopCommand, CanStopCommand);
            Pause = new RelayCommand(PauseCommand, CanPauseCommand);
            Previous = new RelayCommand(PreviousCommand, CanPreviousCommand);
            Next = new RelayCommand(NextCommand, CanNextCommand);
            Mute = new RelayCommand(MuteCommand, CanMuteCommand);
            IsPlaying = false;
        }
        // prepara a lista de midias com os videos que adicionamos
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

        // propriedade que identifica se tem uma midia tocando
        private bool isPlaying;
        public bool IsPlaying
        {
            get { return isPlaying; }
            set { SetProperty(ref isPlaying, value); }
        }
        // comando de play na midia
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
        // comando de stop da midia
        private void StopCommand()
        {
            WeakReferenceMessenger.Default.Send(new StopRequestedMessage());
            IsPlaying = false;
        }

        public bool CanStopCommand()
        {
            return SelectedMedia != null;
        }
        //comando de pause da midia
        private void PauseCommand()
        {
            WeakReferenceMessenger.Default.Send(new PauseRequestedMessage());
            IsPlaying = false;
        }

        public bool CanPauseCommand()
        {
            return SelectedMedia != null;
        }
        // comando de retornar para midia anterior
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
        // comando para ir para a proxima midia da lista
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
        // comando para mutar o volume
        public void MuteCommand()
        {
           WeakReferenceMessenger.Default.Send(new MuteRequestedMessage()); 
        }

        public bool CanMuteCommand()
        {
            return SelectedMedia != null;
        }
        // conjunto de mensagens enviadas para o view, a fim de executar comandos ou trocas dados
        public class PlayRequestedMessage{}

        public class StopRequestedMessage{}

        public class PauseRequestedMessage {}

        public class SetNewMediaMessage : ValueChangedMessage<Media>
        {
            public SetNewMediaMessage(Media media) : base(media) {}        
        }

        public class MuteRequestedMessage{}
    }
}