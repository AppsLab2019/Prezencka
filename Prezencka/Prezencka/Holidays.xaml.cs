using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using Xamarin.Essentials;
using System.Reflection;

namespace Prezencka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Holidays : ContentPage
    {
        public Holidays()
        {
            InitializeComponent();
        }

        Var Prezencka = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Prezencka";
        Directory.CreateDirectory(/storage/emulated/0/Android/data/Prezencka);

        public class BrowserTest
        {
            /*public async Task<bool> OpenBrowser(Uri uri)
            {
               return await Browser.OpenAsync("/storage/emulated/0/priepustka.pdf", BrowserLaunchMode.SystemPreferred);
            }*/
        }
        private void Priepustka(object sender, EventArgs e)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Holidays)).Assembly;
            var stream = assembly.GetManifestResourceStream
                ("Prezencka.priepustka.pdf");
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            File.WriteAllBytes("/storage/emulated/0/Android/data/Prezencka/priepustka.pdf", memoryStream.ToArray());

            memoryStream.Close();
            stream.Close();
        }
    }
}