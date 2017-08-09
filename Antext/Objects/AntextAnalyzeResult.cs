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

        private string revisedText;
        /// <summary>
        /// Contains original text, where found items's original values are replaced with revised ones.
        /// </summary>
        public string RevisedText
        {
            get { return revisedText; }
        }

        public AntextAnalyzeResult(List<AntextStringItem> foundItems, string revisedText)
        {
            this.foundItems = foundItems;
            this.revisedText = revisedText;
        }

    }
}