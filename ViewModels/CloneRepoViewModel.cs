using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using MyApp.Views;

namespace MyApp.ViewModels 
{
    public class CloneRepoViewModel : ViewModelBase
    {
        private string _gitUrl = "";
        public string GitUrl{
            get => _gitUrl;
            set {
                _gitUrl = value;
            }
        }

        private void CloneRepoFromInput()
        {
            
            System.Console.Write(GitUrl);
        }

        // TOdo -> handle clone action
    }

}