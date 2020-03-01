﻿using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prezencka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Docs : ContentPage
    {
        public Docs()
        {
            InitializeComponent();  
        }

        private async void Priepustka(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://mojpracovnycas.sk/download/priepustka.pdf", BrowserLaunchMode.SystemPreferred);
        }

        private async void Dovolenka(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://mojpracovnycas.sk/download/dovolenka.pdf", BrowserLaunchMode.SystemPreferred);
        }

        private async void Vykaz(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://mojpracovnycas.sk/download/pracovny_vykaz.pdf", BrowserLaunchMode.SystemPreferred);
        }
    }
}