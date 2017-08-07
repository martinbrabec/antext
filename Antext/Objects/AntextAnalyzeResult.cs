using System.Collections.Generic;

namespace Antext.Objects
{
    public class AntextAnalyzeResult
    {
        private List<AntextStringItem> foundItems;
        
        /// <summary>
        /// This collection contains found items in given original text.
        /// </summary>
        public List<AntextStringItem> FoundItems
        {
            get { return foundItems; }
        }

        private string fixedText;
        /// <summary>
        /// Contains original text, where found items's original values are replaced with fixed ones.
        /// </summary>
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