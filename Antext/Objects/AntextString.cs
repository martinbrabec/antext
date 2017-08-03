using System.Collections.Generic;

namespace Antex.Objects
{
    public class AntextString
    {
        public string OriginalText { get; set; }
        public string AnalyzedText { get; set; }

        public List<AntextStringItem> FoundItems { get; set; }

        public AntextString()
        {
            FoundItems = new List<AntextStringItem>();
        }
    }
}