using System;
using System.Collections.Generic;
using Antext.Objects;
using Antext.Plugins;

namespace Antext.FindServices
{
    public class AntextAnalyzer<T> : IAntextAnalyzer where T : IAntextPluginable, new()
    {
        protected T analyzeService;
        protected bool fixInOriginalText;
        /// <summary>
        /// Tells if iven original text will be altered by replacing found items with their fixed value.
        /// </summary>
        public bool FixInOriginalText
        {
            get { return fixInOriginalText; }
        }

        public AntextAnalyzer(bool fixInOriginalText = false)
        {
            this.fixInOriginalText = fixInOriginalText;
            this.analyzeService = new T();
        }

        public AntextAnalyzer(T analyzeService, bool fixInOriginalText = false)
        {
            this.fixInOriginalText = fixInOriginalText;

            if (analyzeService == null)
                throw new ArgumentException("Given analyzeService is null!");

            this.analyzeService = analyzeService;
        }

        public AntextAnalyzeResult Analyze(string originalText)
        {
            List<AntextStringItem> foundItems = analyzeService.Analyze(originalText);

            string fixedText = fixInOriginalText
                ? ReplaceOriginalValuesByFixedValues(foundItems, originalText)
                : originalText;

            AntextAnalyzeResult result = new AntextAnalyzeResult(foundItems, fixedText);
            return result;
        }


        protected virtual string ReplaceOriginalValuesByFixedValues(List<AntextStringItem> foundItems, string originalText)
        {
            string fixedText = originalText;
            foreach (var antextStringItem in foundItems)
            {
                fixedText = fixedText.Replace(antextStringItem.OriginalValue, antextStringItem.FixedValue);
            }
            return fixedText;
        }
    }
}