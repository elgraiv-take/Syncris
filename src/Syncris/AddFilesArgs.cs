using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncris
{
    public class AddFilesArgs:EventArgs
    {
        public string[] Files { get; }
        public AddFilesArgs(string[] files)
        {
            Files = files;
        }
    }
}
