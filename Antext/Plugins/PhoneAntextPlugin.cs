using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Antext.Objects;
using PhoneNumbers;

namespace Antext.Plugins
{
    public class PhoneAntextPlugin : IAntextPluginable
    {

        public AntextStringItemType Type { get { return AntextStringItemType.PhoneNumber; } }

        private string phoneRegexPattern = "(?:(\\d{1}))(\\d| |-){7,30}(\\d)";
        private string defaultRegion;

        public PhoneAntextPlugin()
        {
            this.defaultRegion = "CZ";
        }

        /// <summary>
        /// Creates new instance of PhoneFindService.
        /// </summary>
        /// <param name="defaultRegion">Default region for phone number search. To get all supported region codes, use <see cref="GetSupportedRegions"/>.</param>
        public PhoneAntextPlugin(string defaultRegion = "CZ")
        {
            this.defaultRegion = defaultRegion;
        }

        public List<AntextStringItem> Analyze(string text)
        {
            List<AntextStringItem> foundItems = new List<AntextStringItem>();

            // Try to use libphonenumber
            PhoneNumberUtil phoneUtils = PhoneNumberUtil.GetInstance();
            List<PhoneNumberMatch> findNumbersResult = phoneUtils.FindNumbers(text, null, PhoneNumberUtil.Leniency.POSSIBLE, 100).ToList();

            foreach (PhoneNumberMatch phoneNumberMatch in findNumbersResult)
            {
                foundItems.Add(new AntextStringItem(AntextStringItemType.PhoneNumber, phoneNumberMatch.Start, phoneUtils.Format(phoneNumberMatch.Number, PhoneNumberFormat.INTERNATIONAL)));
            }

            // If nothing was found, try to use custom regex
            if (!foundItems.Any())
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

                        // If revised's string length match possible phone number length, try to fix it
                        if (fix.Length > 8 && fix.Length < 30)
                        {
                            PhoneNumber parsedPhone = phoneUtils.Parse(fix, defaultRegion);
                            foundItems.Add(new AntextStringItem(AntextStringItemType.PhoneNumber, match.Index, match.Value, (phoneUtils.Format(parsedPhone, PhoneNumberFormat.INTERNATIONAL))));
                        }
                    }
                }
            }


            return foundItems;

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