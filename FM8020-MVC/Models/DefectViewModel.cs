using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FM8020_MVC.Models
{
    public class DefectViewModel
    {
        public int TotalCount { get; set; }
        public int OpenCount { get; set; }
        public int NewCount { get; set; }
        public int DoneCount { get; set; }
        public DateTime TimeframeStart { get; set; }
        public DateTime TimeframeEnd { get; set; }
        public List<DefectModel> FilteredDefects { get; set; }

        public DefectViewModel(int totalCount, int openCount, int newCount, int doneCount, DateTime timeframeStart, DateTime timeframeEnd)
        {
            TotalCount = totalCount;
            OpenCount = openCount;
            NewCount = newCount;
            DoneCount = doneCount;
            TimeframeStart = timeframeStart;
            TimeframeEnd = timeframeEnd;
        }
    }
}
