using System.Collections.Generic;
using Antext.FindServices;
using Antext.Objects;

namespace Antext
{
    public class Antexter
    {
        private AntextOptions options { get; set; }

        private List<IAnalyzeService> analyzeServices;

        public Antexter(params IAnalyzeService[] services)
        {
            
        }

        public Antexter(AntextOptions options = null)
        {
            this.options = options ?? new AntextOptions();
        }



        public AntextString Analyze(string text)
        {
            var outputAntex = new AntextString();
            outputAntex.OriginalText = text;

            foreach (var analyzeService in analyzeServices)
            {
                outputAntex.FoundItems.AddRange(analyzeService.GetAnalyzedItems(text));
            }

            return outputAntex;
        }


    }
}
