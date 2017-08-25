using System.Collections.Generic;
using System.Linq;
using Antext.Objects;

namespace Antext
{
    public class Antexter
    {
        private List<IAntextAnalyzer> analyzeServices;

        public Antexter(params IAntextAnalyzer[] services)
        {
            this.analyzeServices = services?.ToList() ?? new List<IAntextAnalyzer>();
        }

        /// <summary>
        /// Adds another analyzer to the list.
        /// </summary>
        /// <param name="analyzer"></param>
        /// <returns></returns>
        public Antexter AddAnalyzer(IAntextAnalyzer analyzer)
        {
            if(analyzer != null)
                analyzeServices.Add(analyzer);

            return this;
        }

        /// <summary>
        /// Runs the analyzation and fixing (if enabled).
        /// </summary>
        /// <param name="originalText">The text to be analyzed.</param>
        /// <returns></returns>
        public AntextString Analyze(string originalText)
        {
            var outputAntex = new AntextString();
            outputAntex.OriginalText = originalText;
            outputAntex.RevisedText = outputAntex.OriginalText;

            if (originalText == null)
                return outputAntex;

            foreach (var analyzeService in analyzeServices)
            {
                AntextAnalyzeResult analyzedResult = analyzeService.Analyze(outputAntex.RevisedText);

                outputAntex.FoundItems.AddRange(analyzedResult.FoundItems);

                outputAntex.RevisedText = analyzedResult.RevisedText ?? outputAntex.RevisedText;
            }


            return outputAntex;
        }


    }
}
