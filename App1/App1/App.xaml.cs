using Microsoft.UI.Xaml;

namespace App1
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Window window = new MainWindow();
            window.Activate();
        }
    }
}
