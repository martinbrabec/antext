using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Antex.FindServices;
using Antex.Objects;
using PhoneNumbers;

namespace Antext.FindServices
{
    public class PhoneFindService : IFindService
    {
        public PhoneFindService()
        {
            
        }

        public List<AntextStringItem> GetItems(string text)
        {
            var output = new List<AntextStringItem>();

            PhoneNumberUtil util = PhoneNumberUtil.GetInstance();

            // This should match any sequence of numbers
            string pattern = "(?:(\\d{1}))(\\d| |.){7,30}(\\d)";
            Regex.Match(text, pattern);

            // TODO continue here


            string possible = "777701970";
            var res = util.IsPossibleNumber(possible, null);

            // This phone number can not be found

            List<PhoneNumberMatch> result = util.FindNumbers(text, null, PhoneNumberUtil.Leniency.POSSIBLE, 100).ToList();

            foreach (var phoneNumberMatch in result)
            {
                output.Add(new AntextStringItem(AntextStringItemType.PhoneNumber, phoneNumberMatch.RawString, phoneNumberMatch.Start));
            }
           

            return output;

        }
    }
}