using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Syncris
{
    public partial class NotifyIcon : Component
    {
        private MainWindow m_mainWindow;

        public NotifyIcon()
        {
            InitializeComponent();

            m_menuItemShow.Click += OnShow;
            m_notifyIcon.DoubleClick += OnShow;
            m_menuItemExit.Click += OnExit;
            m_notifyIcon.Icon = new Icon(Application.GetResourceStream(new Uri("/Resources/SyncrisIcon.ico", UriKind.Relative)).Stream);
            m_mainWindow = new MainWindow();
            m_mainWindow.Show();
        }

        public NotifyIcon(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void OnExit(object sender, EventArgs e)
        {
            var result = MessageBox.Show("終了", "終了しますか？", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            Application.Current.Shutdown();
        }

        private void OnShow(object sender, EventArgs e)
        {
            m_mainWindow.Show();
        }
    }
}
