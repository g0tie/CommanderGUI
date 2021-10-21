using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using MyApp.Views;

namespace MyApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            
        }
        public string Greeting => "Welcome to Avalonia!";

        private string _directoryPath = @"c:\Example";
        public string DirectoryPath
        {
            get => _directoryPath;
            set {
                _directoryPath = value;
            }
        }
        
        private void OpenCloneRepoView()
        {
            // System.Console.Write(DirectoryPath);
            var cloneRepoWindow = new CloneRepoView();
            cloneRepoWindow.DirPath = _directoryPath;
            cloneRepoWindow.Show();
        }
    }
}
