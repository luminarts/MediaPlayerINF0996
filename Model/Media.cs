using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MediaPlayerINF0996.Model
{
    public class Media : ObservableObject
    {
        private string name;
        private string icon;
        private string mediaPath;

        public string Name
        {
            get {return name;}
            set {SetProperty(ref name, value);}
        }

        public string Icon
        {
            get {return icon;}
            set {SetProperty(ref icon, value);}
        }

        public string MediaPath
        {
            get {return mediaPath;}
            set {SetProperty(ref mediaPath, value);}
        }
    }
}