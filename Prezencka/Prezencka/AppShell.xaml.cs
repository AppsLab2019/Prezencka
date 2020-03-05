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