using System.Collections.Generic;
using Antex.Objects;

namespace Antex.FindServices
{
    public class PhoneFindService : IFindService
    {
        public PhoneFindService()
        {
            
        }

        public List<AntextStringItem> GetItems(string text)
        {
            var output = new List<AntextStringItem>();

            output.Add(new AntextStringItem(AntextStringItemType.PhoneNumber, "+42067898765", 50));

            

            return output;

        }
    }
}