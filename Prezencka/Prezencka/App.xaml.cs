using Prezencka.Services;

namespace Prezencka
{
    public partial class App
    {
        public static WorkDayStore WorkDayStore { get; private set; }
        public static SettingsService SettingsService { get; private set; }

        public App() => 
            InitializeComponent();

        protected override async void OnStart()
        {
            SettingsService = new SettingsService();

            WorkDayStore = new WorkDayStore();
            await WorkDayStore.InitAsync();

            MainPage = new AppShell();
        }
    }
}
