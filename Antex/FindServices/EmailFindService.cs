using System.Collections.Generic;
using Antex.Objects;

namespace Antex.FindServices
{
    public class EmailFindService : IFindService
    {
        public EmailFindService()
        {
            
        }

        public List<AntextStringItem> GetItems(string text)
        {
            var output = new List<AntextStringItem>();

            output.Add(new AntextStringItem(AntextStringItemType.Email, "martin@martin.cz", 0));

            return output;

        }
    }
}