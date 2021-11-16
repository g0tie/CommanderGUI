using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using MyApp.Views;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MyApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            
        }
        private string _gitUrl = @"ssh://git@example.com/example.git";
        public string GitUrl
        {
            get => _gitUrl;
            set {
                _gitUrl = value;
            }
        }
        private string _gitMsh = @"first commit";
        public string GitMsg
        {
            get => _gitMsh;
            set {
                _gitMsh = value;
            }
        }

        public async void folderOpener(Window window, string title, Action<string, string> callback, string command)
        {
            var folderDialog = new OpenFolderDialog();
            folderDialog.Title = title;

            var folder = await folderDialog.ShowAsync(window);

            if (folder != null)
            {
               callback(folder, command);
            }
        }

        //  public async void fileOpener(Window window, string title, Action<string, string> callback, string command)
        // {
        //     var fileDialog = new OpenFileDialog();
        //     fileDialog.Title = title;

        //     var file = await fileDialog.ShowAsync(window);

        //     if (file != null)
        //     {
        //        callback(file, command);
        //     }
        // }

        private void bashExec(string path, string cmd)
        {
            string command = $"cd {path} && {cmd}";

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{command}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            process.WaitForExit();
        }

        // public void DeleteRepo(Window window) {
        //     folderOpener(window,"Supprimer un dépot local", bashExec, "rm -rf");
        // }

        public void CloneRepo(Window window) {
            folderOpener(window,"Sélectionner une destination", bashExec, $"git clone {GitUrl}");
        }

        public void SyncRepo(Window window) {
            folderOpener(window,"Sélectionner un dépot à synchroniser", bashExec, "git pull ");
        }


        // public void AddFilesToRepo(Window window) {
        //     folderOpener(window,"Sélectionner les fichiers à ajouter", bashExec, "git pull ");
        // }

        public void CommitRepo(Window window) {
            folderOpener(window,"Sélectionner un dépot à synchroniser", bashExec, $"git commit -m {GitMsg}");
        }

        public void PushChangesToRepo(Window window) {
            folderOpener(window,"Sélectionner un dépot à synchroniser", bashExec, $"git push");
        }
       
    }
}
