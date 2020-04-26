using Prezencka.ViewModels;

namespace Prezencka.Views
{
    public partial class Settings
    {
        public Settings() => 
            InitializeComponent();

        protected override void OnAppearing()
        {
            if (BindingContext is SettingsViewModel vm)
                vm.LoadCommand.Execute(null);
        }
    }
}