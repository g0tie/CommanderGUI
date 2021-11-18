# commanderGUI

CommanderGUI is a .NET App made with AvaloniaUI in C# lang to manage git repositories faster.
MessageBox Library is used to display info dialogs.

![image](https://user-images.githubusercontent.com/56622131/142376299-6e3fa309-e682-4d19-8854-49a43176834b.png)

<h2>Usage</h2>

<h3>Create new repo</h3>
Name you repo in default field, then click on create, next choose a folder where to put yout local repo

<h3>Clone a repo</h3>
Enter ssh or https address from a distant repo then click on clone button, next, choose a folder where to clone yout project

<h3>add files to a repo (git add)</h3>
First, select a repo with select button, next click on add button and select files to stage to your repo

<h3>Commit changes (git commit)</h3>
write a message in the field, then click on commit button, select your repo, that's it

<h3>Commit changes (git commit)</h3>
write a message in the field, then click on commit button, select your repo, that's it

<h3>Push changes or Sync distant repo with local</h3>
To push new commit on distant repo, click on push and select your repo
To pull changes to yout local repo, click on Sync button then select your local repo to sync with

<h2>Code</h2>

All code refering to template is in <b>./Views/MainWindow.axaml</b>
This project is using MVVM concept. Actions are binding to controls.

<h3>Button action binding</h3>

here <code>Command</code> indicate what function to use when button clicked
here <code>CommandParameter</code> let us pass an argument to the previous function (We're passing the mainwindow element needed to execute some actions)

```xaml
 <Button 
    Grid.Row="0" 
    Grid.Column="1" 
    Command="{Binding CreateRepo}" 
    CommandParameter="{Binding ElementName=MainWindow}"
>Create</Button>
```

<h3>Text field binding</h3>
Here <code>Text</code> attribute is binded to a property from the ViewModel 

```xaml
<TextBox Margin="10,0"  Grid.Row="0" Grid.Column="0" Name="repoName" Text="{Binding RepoName, Mode=TwoWay}"></TextBox>
```

<h3>Functions of ViewModel</h3>
All code refering to the functions triggered when clicking on buttons are in <b>./ViewModels/MainWindowViewModel.cs</b>

<h3>Getters/Setters</h3>
For text field getters and setters are defined

```csharp
private string _repoName = @"default";
public string RepoName
{
    get => _repoName;
    set {
        _repoName = value;
    }
}
```

<h3>Functions</h3>

```csharp

//Change folder selected for commit actions. window passed from button binding parameter is used to open file explorer from system
public async void SelectRepo(Window window) {}

// generic function to handle file explorer opening and execute a callback after folder selected
public async void folderOpener(Window window, string title, Action<string, string> callback, string command) {}

// generic function to handle file explorer opening and execute a callback after files are selected
public async void fileOpener(Window window, string title, Action<string, string> callback, string command) {}

// Set & Execute bash commands
private void bashExec(string path, string cmd) {}

//wrappers for button actions
public void CreateRepo(Window window) {
    folderOpener(window,"Sélectionner une destination", bashExec, $"mkdir {RepoName} && cd {RepoName} && git init");
}

// same as previous wrapper execpt handle no file selected with dialog error box
public void AddFilesToRepo(Window window) {}
```
