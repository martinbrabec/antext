using System.Collections.Generic;

namespace Antext.Objects
{
    public class AntextString
    {
        /// <summary>
        /// Given original text to be analyzed.
        /// </summary>
        public string OriginalText { get; set; }

        /// <summary>
        /// Contains original text, where found items's original values are replaced with fixed ones.
        /// </summary>
        public string FixedText { get; set; }

        /// <summary>
        /// This collection contains found items in given original text.
        /// </summary>
        public List<AntextStringItem> FoundItems { get; set; }

        public AntextString()
        {
            FoundItems = new List<AntextStringItem>();
        }
    }
}