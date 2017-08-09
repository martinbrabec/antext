using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Antext.Objects;

namespace Antext.Plugins
{
    /// <summary>
    /// Will find html tags in text. Revised value will be always empty as main purpose of this plugin is to remove tags. 
    /// Make sure to run this before any other plugin which adds html tags to the text.
    /// </summary>
    public class HtmlTagAntextPlugin : IAntextPluginable
    {
        /// <summary>
        /// Regular taken from http://haacked.com/archive/2004/10/25/usingregularexpressionstomatchhtml.aspx/
        /// </summary>
        private string htmlRegexPattern = "<\\/?\\w+((\\s+\\w+(\\s*=\\s*(?:\".*?\"|\'.*?\'|[\\^\'\">\\s]+))?)+\\s*|\\s*)\\/?>";

        public AntextStringItemType Type { get { return AntextStringItemType.HtmlTag; } }

        public List<AntextStringItem> Analyze(string text)
        {
            List<AntextStringItem> foundItems = new List<AntextStringItem>();

            // This should match any html tag 
            MatchCollection matches = Regex.Matches(text, htmlRegexPattern);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    foreach (Capture capture in match.Captures)
                    {
                        if (foundItems.FirstOrDefault(f => f.OriginalValue == match.Value) == null) // Prevent duplicate
                            foundItems.Add(new AntextStringItem(AntextStringItemType.HtmlTag, match.Index, match.Value, ""));
                    }
                }
            }

            return foundItems;
        }
    }
}