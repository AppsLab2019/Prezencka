using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prezencka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Holidays : ContentPage
    {
        public Holidays() => 
            InitializeComponent();

        private void DateFromPicker(object sender, DateChangedEventArgs e)
        {
            Recalculate();
        }

        private void DateToPicker(object sender, DateChangedEventArgs e)
        {
            Recalculate();
        }

        private void Recalculate()
        {

        }
    }
} 
