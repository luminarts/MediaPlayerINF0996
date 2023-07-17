using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MediaPlayerINF0996.Model
{
    // classe com as informacoes de uma midia: nome, autor e caminho do arquivo
    public class Media : ObservableObject
    {
        private string name;
        private string author;
        private Uri mediaPath;

        public string Name
        {
            get {return name;}
            set {SetProperty(ref name, value);}
        }

        public string Author
        {
            get {return author;}
            set {SetProperty(ref author, value);}
        }

        public Uri MediaPath
        {
            get {return mediaPath;}
            set {SetProperty(ref mediaPath, value);}
        }
    }
}