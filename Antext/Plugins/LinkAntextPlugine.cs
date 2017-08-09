using System.Collections.Generic;
using System.Text.RegularExpressions;
using Antext.Objects;

namespace Antext.Plugins
{
    public class LinkAntextPlugin : IAntextPluginable
    {
        // Taken from https://stackoverflow.com/questions/6038061/regular-expression-to-find-urls-within-a-string
        /// <summary>
        /// 
        /// </summary>
        private string linkRegexPattern = "(((https?|ftp):\\/\\/)|www.)([\\w_-]+(?:(?:\\.[\\w_-]+)+))([\\w.,@?^=%&:/~+#-]*[\\w@?^=%&/~+#-])?";

        public AntextStringItemType Type { get { return AntextStringItemType.Link; } }

        public List<AntextStringItem> Analyze(string text)
        {
            List<AntextStringItem> foundItems = new List<AntextStringItem>();

            // This should match link 
            var matches = Regex.Matches(text, linkRegexPattern);
            foreach (Match match in matches)
            {
                if (match.Value.Length > 5 && match.Value.Length <= 2000)
                {
                    foundItems.Add(new AntextStringItem(AntextStringItemType.Link, match.Index, match.Value, match.Value));
                }

                //if (match.Success)
                //{
                //    foreach (Capture capture in match.Captures)
                //    {
                //        if (match.Value.Length > 5 && match.Value.Length <= 2000)
                //        {
                //            foundItems.Add(new AntextStringItem(AntextStringItemType.Link, match.Index, match.Value, match.Value));
                //        }
                //    }
                //}
            }

            return foundItems;
        }
    }
}