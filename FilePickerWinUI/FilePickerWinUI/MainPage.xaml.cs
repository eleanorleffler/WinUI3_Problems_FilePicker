using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FilePickerWinUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private async void fileOpenPickerButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();

            //Make file Picker work in Win32
            IntPtr windowHandle = (App.Current as App).WindowHandle;
            MainWindow.InitializeWithWindowWrapper initializeWithWindowWrapper = MainWindow.InitializeWithWindowWrapper.FromAbi(picker.ThisPtr);
            initializeWithWindowWrapper.Initialize(windowHandle);

            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".txt");

            IReadOnlyList<StorageFile> filesToOpen = await picker.PickMultipleFilesAsync();
            string text = string.Empty;
            foreach (StorageFile storageFile in filesToOpen)
            {
                text += storageFile.Path + Environment.NewLine;
            }
            fileOpenPickerTextBlock.Text = text;
        }

        private async void fileSavePickerButton_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker picker = new FileSavePicker();
            //picker.FileTypeChoices.Add("Text File", new List<string> { ".txt" });
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            picker.SuggestedFileName = string.Empty;

            //Make file Picker work in Win32
            IntPtr windowHandle = (App.Current as App).WindowHandle;
            MainWindow.InitializeWithWindowWrapper initializeWithWindowWrapper = MainWindow.InitializeWithWindowWrapper.FromAbi(picker.ThisPtr);
            initializeWithWindowWrapper.Initialize(windowHandle);

            StorageFile file = await picker.PickSaveFileAsync();

            if (file == null)
            {
                fileSavePickerTextBlock.Text = string.Empty;
            }
            else
            {
                fileSavePickerTextBlock.Text = file.Path;
            }
        }

        private async void folderPickerButton_Click(object sender, RoutedEventArgs e)
        {
            FolderPicker picker = new FolderPicker();

            //Make folder Picker work in Win32
            IntPtr windowHandle = (App.Current as App).WindowHandle;
            MainWindow.InitializeWithWindowWrapper initializeWithWindowWrapper = MainWindow.InitializeWithWindowWrapper.FromAbi(picker.ThisPtr);
            initializeWithWindowWrapper.Initialize(windowHandle);

            picker.FileTypeFilter.Add("*");

            StorageFolder folder = await picker.PickSingleFolderAsync();
            if (folder == null)
            {
                folderPickerTextBlock.Text = string.Empty;
            }
            else
            {
                folderPickerTextBlock.Text = folder.Path;
            }
        }
    }
}
