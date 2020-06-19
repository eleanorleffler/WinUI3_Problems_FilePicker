using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FilePickerUWP
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
            picker.FileTypeChoices.Add("Text File", new List<string> { ".txt" });
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            picker.SuggestedFileName = string.Empty;
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
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
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
