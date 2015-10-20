using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ILQuickPeek
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {

        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            UserActivityStore.UserActivityManger.PersistHistory();
        }
    }
}
