using System.Collections.Generic;
using System.Text.RegularExpressions;
using Antext.Objects;

namespace Antext.Plugins
{
    public class EmailAntextPlugin : IAntextPluginable
    {
        public AntextStringItemType Type { get { return AntextStringItemType.Email;} }

        private string emailRegexPattern = "((?:[-+.\\w])+)(@|\\(at\\))(((?:\\w|[.-])+)(\\.\\w{2,10}))";

        public List<AntextStringItem> Analyze(string originalText)
        {
            List<AntextStringItem> foundItems = new List<AntextStringItem>();

            // This should match any email separated by @ or (at)
            var matches = Regex.Matches(originalText, emailRegexPattern);
            foreach (Match match in matches)
            {
                string email = match.Value.Replace("(at)", "@").ToLower();

                // If revised's string length match possible phone number length, try to fix it
                if (email.Length > 4 && email.Length < 255)
                {
                    foundItems.Add(new AntextStringItem(AntextStringItemType.Email, match.Index, match.Value, email));
                }
            }

            //if (matches.Success)
            //{
            //    foreach (Capture match in matches.Captures)
            //    {
            //        string email = match.Value.Replace("(at)", "@").ToLower();

            //        // If revised's string length match possible phone number length, try to fix it
            //        if (email.Length > 4 && email.Length < 255)
            //        {
            //            foundItems.Add(new AntextStringItem(AntextStringItemType.Email, match.Index, match.Value, email));
            //        }
            //    }
            //}

            return foundItems;
        }
    }
}