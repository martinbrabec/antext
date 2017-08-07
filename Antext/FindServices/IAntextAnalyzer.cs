using System.Collections.Generic;
using Antext.Objects;

namespace Antext.FindServices
{
    public interface IAntextAnalyzer
    {
        AntextAnalyzeResult Analyze(string originalText);
    }
}