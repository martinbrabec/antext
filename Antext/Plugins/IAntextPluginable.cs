using System.Collections.Generic;
using Antext.Objects;

namespace Antext.Plugins
{
    public interface IAntextPluginable
    {
        AntextStringItemType Type { get; }
        List<AntextStringItem> Analyze(string originalText);
    }
}