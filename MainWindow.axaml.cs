using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CommanderApp
{
    public partial class MainWindow : Window
    {
        
        private string directoryPath = "";
        
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void getDirectory()
        {
            var directoryPath = this.FindControl<TextBox>("dirPath").Text;


        }

    }
}