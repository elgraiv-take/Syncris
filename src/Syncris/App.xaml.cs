using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Syncris
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private NotifyIcon m_notifyIcon;

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            m_notifyIcon.Dispose();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            m_notifyIcon = new NotifyIcon();
        }
    }
}
