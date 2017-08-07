using System.Collections.Generic;

namespace Antext.Objects
{
    public class AntextAnalyzeResult
    {
        private List<AntextStringItem> foundItems;
        public List<AntextStringItem> FoundItems
        {
            get { return foundItems; }
        }

        private string fixedText;
        public string FixedText
        {
            get { return fixedText; }
        }

        public AntextAnalyzeResult(List<AntextStringItem> foundItems, string fixedText)
        {
            this.foundItems = foundItems;
            this.fixedText = fixedText;
        }

    }
}