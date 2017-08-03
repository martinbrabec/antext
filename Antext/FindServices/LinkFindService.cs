using System.Collections.Generic;
using Antex.Objects;

namespace Antex.FindServices
{
    public class LinkFindService : IFindService
    {
        public LinkFindService()
        {
            
        }

        public List<AntextStringItem> GetItems(string text)
        {
            var output = new List<AntextStringItem>();

            output.Add(new AntextStringItem(AntextStringItemType.Link, "http://data.com", 20));

            return output;

        }
    }
}