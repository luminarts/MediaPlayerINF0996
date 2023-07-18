# Media Player
Projeto final do módulo de User Interface do curso de Tecnologias Microsoft - Unicamp

### Objetivo
Criar um Media Player (tocador de vídeos) utilizando o WPF com XAML e C# que permita reproduzir e controlar arquivos de mídia.

## Como executar o programa
*pré-requisito para executar o programa: 
- Sistema operacional Microsoft Windows 10 ou superior
- .NET Framework 4.7.2 ou superior
- Clonar o repositório em uma pasta local: ```git clone https://github.com/luminarts/MediaPlayerINF0996.git```
- Abra o projeto com o VS Code ou Visual Studio
- Abra o terminal e verifique se o mesmo encontra-se na pasta ```MediaPlayerINF0996/```
- Execute o projeto pelo terminal com o comando ```dotnet run```

## Arquitetura da aplicação
A aplicação foi desenvolvida no padrão de arquitetura Model-View-ViewModel(MVVM).
Aqui estão alguns trechos de código que demonstram como usar determinadas funcionalidades do media player:

## Descrição das classes principais
- Classe MainWindow (View):

Esta classe define a janela principal do media player.
Ela contém os elementos de interface do usuário e lida com eventos e lógica da interface.
A estrutura da interface de usuário é definida no arquivo XAML associado.
<br>


- Classe MainWindow.xaml.cs:

Esta classe contém o código por trás da janela principal do media player.
Ela controla a lógica do aplicativo e interage com os elementos da interface do usuário.
No construtor da classe, são configurados os eventos para atualizar a interface do usuário e registrar os eventos para controlar a reprodução de mídia.
O método Slider_vol_ValueChanged é chamado quando o valor do slider de volume é alterado, atualizando o volume do elemento de mídia.
<br>


- Classe MediaList (ViewModel):

Esta classe é responsável por gerenciar os dados e a lógica da interface do usuário relacionada à lista de mídias.
Ela herda da classe ObservableObject, que permite a notificação de alterações de propriedades.
Possui propriedades como SelectedMedia (representa a mídia atualmente selecionada) e Medias (uma coleção observável de objetos Media).
Também possui comandos como Play, Pause, Stop, Previous, Next e Mute, implementados usando a classe RelayCommand do Community Toolkit MVVM.
No construtor da classe, a coleção de mídias é inicializada, o método PrepareListCollection é chamado para adicionar mídias à coleção e os comandos são inicializados com seus respectivos métodos de execução e verificação de execução.
<br>


- Classe Media (Model):

Esta classe representa uma mídia.
Ela herda da classe ObservableObject, permitindo a notificação de alterações de propriedades.
Possui propriedades como Name (nome da mídia), Author (autor da mídia), Icon (ícone associado à mídia) e MediaPath (caminho da mídia).
Cada propriedade utiliza o método SetProperty para notificar as alterações e atualizar o valor da propriedade.
Essas classes são responsáveis por definir a lógica e a estrutura do media player, incluindo a interface do usuário, a manipulação de eventos e a reprodução de mídia.

## Funcionamento do sistema
- <img width="960" alt="image" src="https://github.com/luminarts/MediaPlayerINF0996/assets/100354416/027931be-e56f-4e07-a151-5f4de6217267">

<br>
No aplicativo Songfy é possível assistir vídeos a partir de uma lista de mídias pré-selecionadas.
O usuário é capaz de pausar, parar e retormar um vídeo, bem como controlar ou mutar o volume do som. Ademais, pode-se selecionar um ponto especifico do vídeo para asisitir a partir da barra de reprodução logo abaixo do vídeo.




