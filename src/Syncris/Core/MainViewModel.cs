using Syncris.Core.Json;
using Syncris.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Syncris.Core
{
    public class MainViewModel:IDisposable
    {
        public TargetCollection Targets { get; } = new TargetCollection();

        public string TargetRootPath { get; set; } = Properties.Settings.Default.DefaultTargetRootPath;

        public string LoadedDataPath { get; set; }

        public ICommand AutoAssignCommand { get; private set; }
        public ICommand SyncAllCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        private UpdateWatcher m_polling;
        public UpdateWatcher PollingThread { get { return m_polling; } }

        public MainViewModel()
        {
            AutoAssignCommand = new DelegateCommand(SearchAndAssignByName);
            SyncAllCommand = new DelegateCommand(SyncAll);
            RefreshCommand = new DelegateCommand(() => RefreshTimestamp());
            BindingOperations.EnableCollectionSynchronization(Targets, new object());

            m_polling = new UpdateWatcher(this);
            StartCommand = new DelegateCommand(() => PollingThread.Start());
            StopCommand = new DelegateCommand(() => PollingThread.Stop());
            //m_polling.Start();
        }

        public void OnAddFiles(object sender,AddFilesArgs e)
        {
            foreach(var file in e.Files)
            {
                var newTarget = new CopyTarget() { SrcFilePath = file };
                newTarget.RefreshTimestamp();
                Targets.Add(newTarget);
            }
        }

        public async void SyncAll()
        {
            await Task.Run(() =>
            {
                foreach(var target in Targets)
                {
                    target.Sync();
                }
            }
            );
        }

        public bool RefreshTimestamp()
        {
            var allSync = true;
            foreach (var target in Targets)
            {
                target.RefreshTimestamp();
                allSync &= target.IsSync;
            }
            return allSync;
        }

        private void SearchAndAssignByName()
        {
            var dict = new Dictionary<string, CopyTarget>();
            foreach(var target in Targets)
            {
                if (string.IsNullOrEmpty(target.DstFilePath))
                {
                    dict.Add(target.SrcFileName, target);
                }
            }

            SearchRecurcive(TargetRootPath,dict);
        }

        private void SearchRecurcive(string path, Dictionary<string, CopyTarget> dict)
        {
            if (dict.Count < 1)
            {
                return;
            }
            foreach(var file in Directory.EnumerateFiles(path))
            {
                var name = Path.GetFileName(file);
                if (dict.ContainsKey(name))
                {
                    dict[name].DstFilePath = file;
                    dict[name].RefreshTimestamp();
                    dict.Remove(name);
                }
            }
            foreach(var dir in Directory.EnumerateDirectories(path))
            {
                SearchRecurcive(dir, dict);
            }
        }

        public bool SaveToFile(string path)
        {
            var setting=new JsonSerializerSettings();
            setting.Formatting = Formatting.Indented;
            var data=new Data();
            data.TargetRootPath = TargetRootPath;
            foreach(var target in Targets)
            {
                data.CopyTargets.Add(new PathPair() { Src = target.SrcFilePath, Dst= target.DstFilePath });
            }
            try
            {
                using (var writer = new StreamWriter(path))
                {
                    writer.Write(JsonConvert.SerializeObject(data, setting));
                }
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }
        public bool LoadFromFile(string path)
        {
            var polling = m_polling.IsRunning;
            m_polling.Stop();
            Data data = null ;
            try
            {
                using (var reader = new StreamReader(path))
                {
                    var json = reader.ReadToEnd();

                    data = JsonConvert.DeserializeObject<Data>(json);
                }
            }
            catch (Exception)
            {
                return false;
            }
            if (data == null)
            {
                return false;
            }
            TargetRootPath=data.TargetRootPath;
            Targets.Clear();
            foreach(var target in data.CopyTargets)
            {
                var newTarget = new CopyTarget() { SrcFilePath = target.Src,DstFilePath=target.Dst };
                newTarget.RefreshTimestamp();
                Targets.Add(newTarget);
            }
            if (polling)
            {
                m_polling.Start();
            }
            return true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    m_polling.Dispose();
                    // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~MainViewModel() {
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
