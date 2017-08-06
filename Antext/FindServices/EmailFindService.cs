using System.Collections.Generic;
using System.Text.RegularExpressions;
using Antext.Objects;
using PhoneNumbers;

namespace Antext.FindServices
{
    public class EmailFindService : IFindService
    {
        private string emailRegexPattern = "\\w+([-+.]\\w+)*(@|\\(at\\))(\\w+([-.]\\w+)|\\.\\w+([-.]\\w+))+";

        public EmailFindService()
        {
            
        }

        public List<AntextStringItem> GetItems(string text)
        {
            var output = new List<AntextStringItem>();

            // This should match any email separated by @ or (at)
            var matches = Regex.Match(text, emailRegexPattern);
            if (matches.Success)
            {
                foreach (Capture match in matches.Captures)
                {
                    string email = match.Value.Replace("(at)", "@").ToLower();

                    // If fixed's string length match possible phone number length, try to fix it
                    if (email.Length > 4 && email.Length < 255)
                    {
                        output.Add(new AntextStringItem(AntextStringItemType.Email, match.Index, match.Value, email));
                    }
                }
            }

            return output;

        }
    }
}