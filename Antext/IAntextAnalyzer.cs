using Antext.Objects;
using Antext.Plugins;

namespace Antext
{
    /// <summary>
    /// This interfce allows Antexer to be pluginabe. To implement custom analyzer plugin, implement <see cref="IAntextPluginable"/>
    /// </summary>
    public interface IAntextAnalyzer
    {

        // Runs this specific analyzer.
        AntextAnalyzeResult Analyze(string originalText);
    }
}