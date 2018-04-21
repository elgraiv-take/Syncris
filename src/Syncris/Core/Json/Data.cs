using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncris.Core.Json
{
    [JsonObject]
    public class Data
    {
        public string TargetRootPath { get; set; }
        public List<PathPair> CopyTargets { get; set; } = new List<PathPair>();

    }

    [JsonObject]
    public class PathPair
    {
        public string Src { get; set; }
        public string Dst { get; set; }
    }
}
