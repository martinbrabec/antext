using System.Collections.Generic;
using System.Linq;
using Antext.FindServices;
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


        public Antexter AddAnalyzer(IAntextAnalyzer analyzer)
        {
            if(analyzer != null)
                analyzeServices.Add(analyzer);

            return this;
        }

        public AntextString Analyze(string originalText)
        {
            var outputAntex = new AntextString();
            outputAntex.OriginalText = originalText;
            outputAntex.FixedText = outputAntex.OriginalText;


            foreach (var analyzeService in analyzeServices)
            {
                AntextAnalyzeResult analyzedResult = analyzeService.Analyze(outputAntex.FixedText);

                outputAntex.FoundItems.AddRange(analyzedResult.FoundItems);

                outputAntex.FixedText = analyzedResult.FixedText ?? outputAntex.FixedText;
            }


            return outputAntex;
        }


    }
}
