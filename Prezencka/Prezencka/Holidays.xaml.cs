using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prezencka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Holidays : ContentPage
    {
        public Holidays() => 
            InitializeComponent();

        private void date_from_picker_DateSelected(object sender, DateChangedEventArgs e)
        {
            Recalculate();
        }

        private void date_to_picker_DateSelected(object sender, DateChangedEventArgs e)
        {
            Recalculate();
        }

        private void Recalculate()
        {

        }
    }
} 
