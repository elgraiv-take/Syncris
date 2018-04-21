using Syncris.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Syncris.Core
{
    public class UpdateWatcher: BindableBase,IDisposable
    {
        private MainViewModel m_main;
        private Task m_pollingTask;
        private CancellationTokenSource m_cancel;

        private static readonly int s_minSleepTimeMs = 500;

        private int m_sleepTimeMs = Properties.Settings.Default.DefaultPollingInterval;

        public int SleepTimeMs {
            get
            {
                return m_sleepTimeMs;
            }
            set
            {
                if (value < s_minSleepTimeMs)
                {
                    value = s_minSleepTimeMs;
                }
                SetProperty(ref m_sleepTimeMs, value);
            }
        }

        public bool IsRunning
        {
            get
            {
                return m_pollingTask != null;
            }
        }

        public UpdateWatcher(MainViewModel mainViewModel)
        {
            m_main = mainViewModel;
        }

        public void Start()
        {
            if (IsRunning)
            {
                return;
            }
            m_cancel = new CancellationTokenSource();
            var token = m_cancel.Token;
            m_pollingTask=new Task(()=>Polling(token),TaskCreationOptions.LongRunning);
            m_pollingTask.Start();
            RaisePropertyChanged(nameof(IsRunning));

        }

        private async void Polling(CancellationToken cancel)
        {
            while (!cancel.IsCancellationRequested)
            {
                m_main.SyncAll();
                try
                {
                    await Task.Delay(SleepTimeMs, cancel);
                }
                catch (TaskCanceledException) { }
            }
        }

        public void Stop()
        {
            if (!IsRunning)
            {
                return;
            }
            m_cancel.Cancel();
            m_pollingTask.Wait();
            m_pollingTask.Dispose();
            m_pollingTask = null;
            m_cancel.Dispose();
            m_cancel = null;
            RaisePropertyChanged(nameof(IsRunning));
        }
        

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Stop();
                    // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~Polling() {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
