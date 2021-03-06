﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Syncris
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public event EventHandler<FileListEventArgs> AddFiles;
        public event EventHandler<DataGridEventArgs> RemoveFiles;

        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files == null)
            {
                return;
            }
            AddFiles?.Invoke(this, new FileListEventArgs(files));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(DataContext is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            /*
            var result=MessageBox.Show("終了", "終了しますか？", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
            */
            Hide();
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var selected=WatchList.SelectedItems.Cast<object>().ToArray();
                e.Handled = true;
                var result = MessageBox.Show("削除", "選択された要素をしますか？", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result != MessageBoxResult.Yes)
                {
                    return;
                }
                RemoveFiles?.Invoke(this, new DataGridEventArgs(selected));
            }
        }
        
    }
}
