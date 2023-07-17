using System.Collections.ObjectModel;
using MediaPlayerINF0996.Model;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.IO;
using System;

//Definição da classe MediaList, da ViewModel.
namespace MediaPlayerINF0996.ViewModel
{
    public class MediaList : ObservableObject
    {
        //Define a propriedade SelectedMedia, que representa a mídia atualmente selecionada. 
        //Quando o valor é alterado, ele notifica as mudanças utilizando o método SetProperty da classe base ObservableObject. 
        //Além disso, ele chama NotifyCanExecuteChanged em cada comando relacionado para atualizar o estado de execução do comando.
        private Media selectedMedia;
        public Media SelectedMedia
        {
            get { return selectedMedia; }
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
        //Essa propriedade é uma coleção observável de objetos Media. Ela é usada para armazenar e exibir a lista 
        //de mídias disponíveis.
        public ObservableCollection<Media> Medias { get; set; }
        //Essas propriedades representam os comandos que podem ser executados na ViewModel. 
        //Os comandos são implementados utilizando a classe RelayCommand do Community Toolkit MVVM.
        public RelayCommand Play { get; set; }
        public RelayCommand Pause { get; set; }
        public RelayCommand Stop { get; set; }
        public RelayCommand Previous { get; set; }
        public RelayCommand Next { get; set; }
        public RelayCommand Mute { get; set; }
        //O construtor da classe MediaList inicializa a coleção Medias, chama o método PrepareListCollection() 
        //para adicionar mídias à coleção e inicializa os comandos com os respectivos métodos de execução e 
        //verificação de execução.
        public MediaList()
        {
            Medias = new ObservableCollection<Media>();
            PrepareListCollection();
            //Comandos que podem ser executados na ViewModel. Os comandos são implementados utilizando a classe 
            //RelayCommand do Community Toolkit MVVM.
            Play = new RelayCommand(PlayCommand, CanPlayCommand);
            Stop = new RelayCommand(StopCommand, CanStopCommand);
            Pause = new RelayCommand(PauseCommand, CanPauseCommand);
            Previous = new RelayCommand(PreviousCommand, CanPreviousCommand);
            Next = new RelayCommand(NextCommand, CanNextCommand);
            Mute = new RelayCommand(MuteCommand, CanMuteCommand);
            IsPlaying = false;
        }

        //O método PrepareListCollection() é responsável por adicionar as instâncias de mídia à coleção de Mídia. 
        //Cada instância é criada com um nome, autor e caminho de mídia.
        private void PrepareListCollection()
        {
            //Elementos da mídia 1
            var media1 = new Media
            {
                Name = "The Pretender",
                Author = "Foo Fighters",
                MediaPath = new Uri(Path.GetFullPath(@"assets\videos\FooFighters-ThePretender.mp4"))
            };

            //Elementos da mídia 2
            var media2 = new Media
            {
                Name = "Sinos",
                Author = "Unknown",
                MediaPath = new Uri(Path.GetFullPath(@"assets\videos\teste.mp4"))
            };

            //Elementos da mídia 3
            var media3 = new Media
            {
                Name = "Hi",
                Author = "CG5",
                MediaPath = new Uri(Path.GetFullPath(@"assets\videos\videoplayback.mp4"))
            };

            //Adição das instâncias à coleção de mídia
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

        //O método PlayCommand() é chamado quando o comando Play é executado. Ele envia uma mensagem através do 
        //mecanismo de mensagens para definir uma nova mídia selecionada e solicita a reprodução. Também define a 
        //propriedade IsPlaying como true.
        private void PlayCommand()
        {
            WeakReferenceMessenger.Default.Send(new SetNewMediaMessage(SelectedMedia));
            WeakReferenceMessenger.Default.Send(new PlayRequestedMessage());
            IsPlaying = true;
        }

        //Os métodos booleanos Can...Command() são utilizados para verificar se o comando correspondente pode ser executado. 
        //Eles retornam true se SelectedMedia não for nulo, ou seja, se houver uma mídia selecionada.
        public bool CanPlayCommand()
        {
            return SelectedMedia != null;
        }

        //O método StopCommand() é chamado quando o comando Stop é executado. Ele envia uma mensagem através do 
        //mecanismo de mensagens para pausar e retornar a mídia ao início de sua reprodução. Também define a 
        //propriedade IsPlaying como false.
        private void StopCommand()
        {
            WeakReferenceMessenger.Default.Send(new StopRequestedMessage());
            IsPlaying = false;
        }

        public bool CanStopCommand()
        {
            return SelectedMedia != null;
        }

        //O método PauseCommand() é chamado quando o comando Pause é executado. Ele envia uma mensagem através do 
        //mecanismo de mensagens para pausar a mídia no momento de sua reprodução. Também define a 
        //propriedade IsPlaying como false.
        private void PauseCommand()
        {
            WeakReferenceMessenger.Default.Send(new PauseRequestedMessage());
            IsPlaying = false;
        }

        public bool CanPauseCommand()
        {
            return SelectedMedia != null;
        }

        //O método PreviousCommand() é chamado quando o comando Previous é executado. Ele para a reprodução atual, 
        //seleciona a mídia anterior na lista Medias e inicia a reprodução dessa mídia.
        public void PreviousCommand()
        {
            StopCommand();
            SelectedMedia = Medias[Medias.IndexOf(SelectedMedia) == 0 ? Medias.Count - 1 : Medias.IndexOf(SelectedMedia) - 1];
            PlayCommand();
        }

        public bool CanPreviousCommand()
        {
            return SelectedMedia != null;
        }

        //O método NextCommand() é chamado quando o comando Next é executado. Ele envia uma mensagem através do 
        //mecanismo de mensagens para pausar e retornar a próxima mídia da lista.
        public void NextCommand()
        {
            StopCommand();
            SelectedMedia = Medias[Medias.IndexOf(SelectedMedia) == Medias.Count - 1 ? 0 : Medias.IndexOf(SelectedMedia) + 1];
            PlayCommand();
        }

        public bool CanNextCommand()
        {
            return SelectedMedia != null;
        }
        //O método MuteCommand() é chamado quando o comando Mute é executado. Ele envia uma mensagem através do 
        //mecanismo de mensagens para que o valor do volume mude para 0 enquanto selecionado.
        public void MuteCommand()
        {
            WeakReferenceMessenger.Default.Send(new MuteRequestedMessage());
        }

        public bool CanMuteCommand()
        {
            return SelectedMedia != null;
        }

        public class PlayRequestedMessage { }

        public class StopRequestedMessage { }

        public class PauseRequestedMessage { }

        //Essa classe define uma mensagem que é usada para enviar informações sobre uma nova mídia selecionada. 
        //Ela herda da classe ValueChangedMessage<T> do Community Toolkit MVVM.
        public class SetNewMediaMessage : ValueChangedMessage<Media>
        {
            public SetNewMediaMessage(Media media) : base(media) { }
        }

        public class MuteRequestedMessage { }
    }
}