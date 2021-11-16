using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using MyApp.Views;
using System.Threading.Tasks;
using System.Diagnostics;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Models;
using MessageBoxAvaloniaEnums = MessageBox.Avalonia.Enums;
using System.Threading;

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
        private string _gitMsg = @"first commit";
        public string GitMsg
        {
            get => _gitMsg;
            set {
                _gitMsg = value;
            }
        }

        private string _repoName = @"default";
        public string RepoName
        {
            get => _repoName;
            set {
                _repoName = value;
            }
        }

        private string _currentRepo = @"choose repo...";
        public string CurrentRepo
        {
            get => _currentRepo;
            set {
                _currentRepo = value;
            }
        }

        private string _filesToAdd = "";
        public string FilesToAdd
        {
            get => _filesToAdd;
            set {
                _filesToAdd = value;
            }
        }
        public async void SelectRepo(Window window)
        {
            var folderDialog = new OpenFolderDialog();
            folderDialog.Title = "Sélectionner un dépot";

            var folder = await folderDialog.ShowAsync(window);

            
            if (folder != null)
            {
                CurrentRepo = folder;
                window.FindControl<TextBlock>("currentRepo").Text = folder;
            }
        }
        public async void folderOpener(Window window, string title, Action<string, string> callback, string command)
        {
            var folderDialog = new OpenFolderDialog();
            folderDialog.Title = title;
            folderDialog.Directory = CurrentRepo;

            var folder = await folderDialog.ShowAsync(window);

            if (folder != null)
            {
               callback(folder, command);
            }
        }

         public async void fileOpener(Window window, string title, Action<string, string> callback, string command)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Title = title;
            fileDialog.Directory = CurrentRepo;

            var files = await fileDialog.ShowAsync(window);

            if (files != null)
            {
                FilesToAdd = string.Join(" ", files);  
                command += FilesToAdd;

                callback(CurrentRepo, command);
            }
        }

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
                    RedirectStandardError=true,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                }
            };

            // await msgBox.Show();
            Thread.Sleep(50);
            process.Start();

            var errors = process.StandardError.ReadToEnd(); 
            var output = process.StandardOutput.ReadToEnd(); 

            process.WaitForExit();

            if (errors.Length > 0) {
                
                var msgBox = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams{
                    ContentTitle = "Infos",
                    ContentMessage = $"{errors}",
                    Icon = Icon.Error,
                    Style = Style.UbuntuLinux
                });

                msgBox.Show();

            }

            if (output.Length > 0) {
                    
                var msgBox = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams{
                    ContentTitle = "Infos",
                    ContentMessage = $"Opération réussie: {output}",
                    Icon = Icon.Success,
                    Style = Style.UbuntuLinux
                });

                msgBox.Show();
            }

        }

        public void CreateRepo(Window window) {
            folderOpener(window,"Sélectionner une destination", bashExec, $"mkdir {RepoName} && git init");
        }

        public void CloneRepo(Window window) {
            folderOpener(window,"Sélectionner une destination", bashExec, $"git clone {GitUrl}");
        }

        public void SyncRepo(Window window) {
            folderOpener(window,"Sélectionner un dépot à synchroniser", bashExec, "git pull ");
        }

        public void AddFilesToRepo(Window window) {
            if (CurrentRepo == "choose repo...") {
                 var msgBox = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams{
                    ContentTitle = "Infos",
                    ContentMessage = "Veuillez séléctionner un dépot.",
                    Icon = Icon.Error,
                    Style = Style.UbuntuLinux
                });
            } else {
                fileOpener(window,"Sélectionner les fichiers à ajouter", bashExec, "git add ");
            }
        }

        public void CommitRepo(Window window) {
            folderOpener(window,"Sélectionner un dépot à synchroniser", bashExec, $"git commit -m \"{GitMsg}\"");
        }

        public void PushChangesToRepo(Window window) {
            folderOpener(window,"Sélectionner un dépot à synchroniser", bashExec, $"git push");
        }
       
    }
}
