using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncris
{
    public class DataGridEventArgs:EventArgs
    {
        public object[] TargetItems { get; }
        public DataGridEventArgs(object[] items)
        {
            TargetItems = items;
        }
    }
}
