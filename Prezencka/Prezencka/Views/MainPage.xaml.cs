using Prezencka.ViewModels;

namespace Prezencka.Views
{
    public partial class MainPage
    {
        public MainPage() => 
            InitializeComponent();

        protected override void OnAppearing()
        {
            if (BindingContext is MainViewModel vm)
                vm.InitCommand.Execute(null);
        }
    }
}
