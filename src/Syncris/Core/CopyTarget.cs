using Syncris.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncris.Core
{
    
    public class CopyTarget: BindableBase
    {
        private string m_srcFilePath;
        public string SrcFilePath
        {
            get
            {
                return m_srcFilePath;
            }
            set
            {
                var result=SetProperty(ref m_srcFilePath, value, nameof(SrcFilePath), nameof(SrcFileName));
                if (result)
                {
                    RefleshSrcTimestamp();
                    RaisePropertyChanged(nameof(IsSync));
                }
            }
        }
        public string SrcFileName { get { return Path.GetFileName(m_srcFilePath); } }
        private string m_dstFilePath;
        public string DstFilePath
        {
            get
            {
                return m_dstFilePath;
            }
            set
            {
                var result=SetProperty(ref m_dstFilePath, value, nameof(DstFilePath), nameof(DstFileName));
                if (result)
                {
                    RefleshDstTimestamp();
                    RaisePropertyChanged(nameof(IsSync));
                }
            }
        }


        public string DstFileName { get { return Path.GetFileName(m_dstFilePath); } }


        private DateTime m_srcTimestamp=DateTime.MinValue;
        public DateTime SrcTimestamp
        {
            get
            {
                return m_srcTimestamp;
            }
            private set
            {
                SetProperty(ref m_srcTimestamp, value);
            }

        }
        private DateTime m_dstTimestamp = DateTime.MinValue;
        public DateTime DstTimestamp
        {
            get
            {
                return m_dstTimestamp;
            }
            private set
            {
                SetProperty(ref m_dstTimestamp, value);
            }

        }
        public DateTime LastSync { get; set; }

        public bool IsSync
        {
            get
            {
                return m_srcTimestamp == m_dstTimestamp;
            }
        }


        private void RefleshSrcTimestamp()
        {
            if (File.Exists(m_srcFilePath))
            {
                SrcTimestamp = File.GetLastWriteTime(m_srcFilePath);
            }
            else
            {
                SrcTimestamp = DateTime.MinValue;
            }
        }
        private void RefleshDstTimestamp()
        {
            if (File.Exists(m_dstFilePath))
            {
                DstTimestamp = File.GetLastWriteTime(m_dstFilePath);
            }
            else
            {
                DstTimestamp = DateTime.MinValue;
            }
        }

        public void RefreshTimestamp()
        {
            RefleshSrcTimestamp();
            RefleshDstTimestamp();
            RaisePropertyChanged(nameof(IsSync));
        }


        public void Sync()
        {
            try
            {
                if((!File.Exists(m_srcFilePath))|| (!File.Exists(m_dstFilePath)))
                {
                    return;
                }

                RefleshSrcTimestamp();
                RefleshDstTimestamp();
                //コピー先が古かった場合のみ
                if (!(DstTimestamp<SrcTimestamp))
                {
                    RaisePropertyChanged(nameof(IsSync));
                    return;
                }

                File.Copy(m_srcFilePath, DstFilePath, true);
                RefreshTimestamp();
            }
            catch(Exception)
            {

            }
        }
    }
}
