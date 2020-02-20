using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Reflection;

namespace Prezencka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Holidays : ContentPage
    {
        public Holidays() => 
            InitializeComponent();


        private void Priepustka(object sender, EventArgs e)
        {
            Directory.CreateDirectory("/storage/emulated/0/Android/data/prezencka");

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Holidays)).Assembly;
            var stream = assembly.GetManifestResourceStream
                ("Prezencka.priepustka.pdf");

            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            File.WriteAllBytes("/storage/emulated/0/Android/data/prezencka/priepustka.pdf", memoryStream.ToArray());

            stream.Close();

            //await Browser.OpenAsync("https://docs.microsoft.com/en-us/xamarin/essentials/open-browser?tabs=android", BrowserLaunchMode.SystemPreferred);

            PdfViewer.InputFileStream = memoryStream;
            memoryStream.Close();
        }
    }
} 
