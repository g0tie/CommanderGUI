using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MyApp.Views {

    class CloneRepoView: Window 
    {
        public CloneRepoView()
        {
            InitializeComponent();
        }

         private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}