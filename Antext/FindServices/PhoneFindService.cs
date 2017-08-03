using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using Antex.FindServices;
using Antex.Objects;
using PhoneNumbers;

namespace Antext.FindServices
{
    public class PhoneFindService : IFindService
    {
        private string regexPattern = "(?:(\\d{1}))(\\d| |-){7,30}(\\d)";

        public PhoneFindService()
        {
            
        }

        public List<AntextStringItem> GetItems(string text)
        {
            var output = new List<AntextStringItem>();

            PhoneNumberUtil phoneUtils = PhoneNumberUtil.GetInstance();
            List<PhoneNumberMatch> result = phoneUtils.FindNumbers(text, null, PhoneNumberUtil.Leniency.POSSIBLE, 100).ToList();

            foreach (var phoneNumberMatch in result)
            {
                output.Add(new AntextStringItem(AntextStringItemType.PhoneNumber, phoneNumberMatch.Start, phoneNumberMatch.RawString));
            }

            if (!output.Any())
            {
                // Nothing was found, try use regex
                // This should match any sequence of numbers, separated by space or hypen, having 7-30 chars
                string pattern = "(?:(\\d{1}))(\\d| |-){7,30}(\\d)";
                var matches = Regex.Match(text, pattern);
                if (matches.Success)
                {
                    foreach (Capture match in matches.Captures)
                    {
                        // Replace spaces or hypens if any
                        string fix = match.Value.Replace(" ", "").Replace("-", "");

                        // If fixed's string length match possible phone number length, try to fix it
                        if (fix.Length > 8 && fix.Length < 14)
                        {
                            //var fixedPhoneNumber = phoneUtils.Parse(fix, "");
                            output.Add(new AntextStringItem(AntextStringItemType.PhoneNumber, match.Index, match.Value, PhoneNumberUtil.Normalize(fix)));
                        }
                    }
                }
            }

            return output;

        }
    }
}