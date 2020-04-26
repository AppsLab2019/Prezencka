using Prezencka.ViewModels;

namespace Prezencka.Views
{    
    public partial class Table
    {
        public Table() => 
            InitializeComponent();

        protected override void OnAppearing()
        {
            if (BindingContext is TableViewModel vm)
                vm.RefreshCommand.Execute(null);
        }
    }
}