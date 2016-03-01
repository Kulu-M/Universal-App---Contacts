using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SensorenCamera
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

        private async void abb_camera_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI dialog = new CameraCaptureUI();

            Size aspectRatio = new Size(16, 9);
            dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;

            StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo); 
        }

        private async void abb_mail_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI dialog = new CameraCaptureUI();

            Size aspectRatio = new Size(16, 9);
            dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;

            StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Video);

            sendMail(file);

        }

        private async void sendMail(StorageFile anhang)
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.To.Add(new EmailRecipient("mohr-marco1@gmx.de"));
            string messageBody = "Mailinhalt..... Text";
            emailMessage.Body = messageBody;
            emailMessage.Subject = "Testen als Betreff";

            var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(anhang);
            var attachment = new Windows.ApplicationModel.Email.EmailAttachment
            (
                anhang.Name,
                stream);
            emailMessage.Attachments.Add(attachment);

            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }

        StorageFile attachedFile;

        private async void abb_anhang_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();

            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");

            attachedFile = await picker.PickSingleFileAsync();
        }
    }
}
