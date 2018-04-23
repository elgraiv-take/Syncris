using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncris
{
    public class FileListEventArgs:EventArgs
    {
        public string[] Files { get; }
        public FileListEventArgs(string[] files)
        {
            Files = files;
        }
    }
}
