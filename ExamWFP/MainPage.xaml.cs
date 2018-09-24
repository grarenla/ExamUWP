using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ExamWFP
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

        private async void BtnSearch(object sender, RoutedEventArgs e)
        {
            string fileName = TextName.Text;
            string content = TextContent.Text;
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                string contentAsync = await FileIO.ReadTextAsync(file);
                if (contentAsync.Contains(content))
                {
                    DisplayNotify("Success", "File found and text found");
                }
                else
                {
                    DisplayNotify("Success", "File found but text not found");
                }
            }
            catch (Exception exception)
            {
                if (exception.GetType() == typeof(FileNotFoundException))
                {
                    DisplayNotify("Warning", "File not found");
                }
                else
                {
                    DisplayNotify("Error", exception.Message);
                }
            }
        }

        private async void DisplayNotify(string title, string content)
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };

            await noWifiDialog.ShowAsync();
        }
    }
}