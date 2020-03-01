using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Reflection;
using Xamarin.Essentials;

namespace Prezencka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Holidays : ContentPage
    {
        public Holidays() => 
            InitializeComponent();


        private async void Priepustka(object sender, EventArgs e)
        {
            Directory.CreateDirectory("/storage/emulated/0/Android/data/prezencka");

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Holidays)).Assembly;
            var stream = assembly.GetManifestResourceStream
                ("Prezencka.priepustka.pdf");

            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            File.WriteAllBytes("/storage/emulated/0/Android/data/prezencka/priepustka.pdf", memoryStream.ToArray());

            stream.Close();

            await Browser.OpenAsync("https://mojpracovnycas.sk/download/priepustka.pdf", BrowserLaunchMode.SystemPreferred);

            memoryStream.Close();
        }
    }
} 
