using System.Collections.Generic;
using Antext.Objects;

namespace Antext.FindServices
{
    public interface IAnalyzeService
    {
        AntextStringItemType Type { get; }
        List<AntextStringItem> GetAnalyzedItems(string text);
    }
}