using System.Collections.ObjectModel;
using MediaPlayerINF0996.Model;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MediaPlayerINF0996.ViewModel
{
    public class MediaList : ObservableObject
    {
        public ObservableCollection<Media> mediaList { get; set; }

        public MediaList()
        {
            mediaList = new ObservableCollection<Media>();
            PrepareListCollection();
        }

        private void PrepareListCollection()
        {
            // ainda precisa implementar esse método, a lista de medias disponíveis está vazia
        }
    }
}