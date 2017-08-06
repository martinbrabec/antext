using System.Collections.Generic;
using Antext.Objects;

namespace Antext.FindServices
{
    public class LinkAnalyzeService : IAnalyzeService
    {
        public AntextStringItemType Type { get { return AntextStringItemType.Link; } }

        public LinkAnalyzeService()
        {
            
        }

        public List<AntextStringItem> GetAnalyzedItems(string text)
        {
            var output = new List<AntextStringItem>();

            

            return output;

        }
    }
}