using TestBlankApp1.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using LogService;
using Unity;
using Prism.Unity;

namespace TestBlankApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
            }
            catch (System.Exception ex)
            {
                /// Change Catching the error 
                ErrorView error = new ErrorView(ex.Show());
                error.ShowDialog();

                throw;
            }
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var con = containerRegistry.GetContainer();
            con.AddExtension(new Diagnostic());
        }
    }
}
