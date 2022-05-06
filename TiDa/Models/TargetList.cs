using System;
using System.Collections.Generic;
using System.Text;

namespace TiDa.Models
{
    public class TargetList
    {
        public string MainTitle { get; set; }

        public List<TargetTask> MinorList { get; set; }
    }
}
