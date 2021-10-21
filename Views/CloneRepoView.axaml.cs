using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MyApp.Views {

    class CloneRepoView: Window
    {
        private string _dirPath = "";
        public string DirPath
        {
            get => _dirPath;
            set 
            {
                _dirPath = value;
            }
        }
        public CloneRepoView()
        {   
            InitializeComponent();
        }

         private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            // System.Console.Write(dirPath);
        }
    }
}