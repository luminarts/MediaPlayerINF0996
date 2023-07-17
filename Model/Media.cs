using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MediaPlayerINF0996.Model
{
    //A classe ObservableObject, fornecida pelo Community Toolkit MVVM, permite a notificação de alterações de propriedades.
    public class Media : ObservableObject
    {
        private string name;
        private string author;
        private Uri mediaPath;

        //Propriedade Name, que representa o nome da mídia.
        public string Name
        {
            get { return name; }
            //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
            set { SetProperty(ref name, value); }
        }

        //Propriedade Author, que representa o autor da mídia. Segue a mesma estrutura da propriedade Name, notificando 
        //as alterações e atualizando o valor usando o método SetProperty.
        public string Author
        {
            get { return author; }
            set { SetProperty(ref author, value); }
        }
        
        //propriedade MediaPath, que representa o caminho da mídia. É do tipo Uri, o que sugere que representa o 
        //local onde o arquivo de mídia está armazenado. O getter retorna o valor atual da propriedade e o setter 
        //utiliza o método SetProperty para notificar as alterações e atualizar o valor.
        public Uri MediaPath
        {
            get { return mediaPath; }
            set { SetProperty(ref mediaPath, value); }
        }
    }
}