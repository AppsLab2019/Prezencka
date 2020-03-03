using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prezencka
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            
            InitializeComponent();

            rootBar.CurrentItem = rootBar.Items[2];

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}