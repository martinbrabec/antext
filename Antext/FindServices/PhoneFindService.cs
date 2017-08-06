using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using Antext.Objects;
using PhoneNumbers;

namespace Antext.FindServices
{
    public class PhoneFindService : IFindService
    {
        private string phoneRegexPattern = "(?:(\\d{1}))(\\d| |-){7,30}(\\d)";
        private string defaultRegion;

        /// <summary>
        /// Creates new instance of PhoneFindService.
        /// </summary>
        /// <param name="defaultRegion">Default region for phone number search. To get all supported region codes, use <see cref="GetSupportedRegions"/>.</param>
        public PhoneFindService(string defaultRegion = "CZ")
        {
            this.defaultRegion = defaultRegion;
        }

        public List<AntextStringItem> GetItems(string text)
        {
            var output = new List<AntextStringItem>();

            // Try to use libphonenumber
            PhoneNumberUtil phoneUtils = PhoneNumberUtil.GetInstance();
            List<PhoneNumberMatch> result = phoneUtils.FindNumbers(text, null, PhoneNumberUtil.Leniency.POSSIBLE, 100).ToList();

            foreach (PhoneNumberMatch phoneNumberMatch in result)
            {
                output.Add(new AntextStringItem(AntextStringItemType.PhoneNumber, phoneNumberMatch.Start, phoneUtils.Format(phoneNumberMatch.Number, PhoneNumberFormat.INTERNATIONAL)));
            }

            // If nothing was found, try to use custom regex
            if (!output.Any())
            {
                // Nothing was found, try use regex
                // This should match any sequence of numbers, separated by space or hypen, having 7-30 chars
                var matches = Regex.Match(text, phoneRegexPattern);
                if (matches.Success)
                {
                    foreach (Capture match in matches.Captures)
                    {
                        // Replace spaces or hypens if any
                        string fix = match.Value.Replace(" ", "").Replace("-", "");

                        // If fixed's string length match possible phone number length, try to fix it
                        if (fix.Length > 8 && fix.Length < 30)
                        {
                            PhoneNumber parsedPhone = phoneUtils.Parse(fix, defaultRegion);
                            output.Add(new AntextStringItem(AntextStringItemType.PhoneNumber, match.Index, match.Value, (phoneUtils.Format(parsedPhone, PhoneNumberFormat.INTERNATIONAL))));
                        }
                    }
                }
            }

            return output;

        }

        /// <summary>
        /// Gets all supported regionCodes. More informations here https://stackoverflow.com/questions/16803432/listing-all-country-codes-of-phone-numbers.
        /// </summary>
        /// <returns></returns>
        public static HashSet<string> GetSupportedRegions()
        {
            return PhoneNumberUtil.GetInstance().GetSupportedRegions();
        }
    }
}