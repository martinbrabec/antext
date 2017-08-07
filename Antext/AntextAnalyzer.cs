using System;
using System.Collections.Generic;
using Antext.Objects;
using Antext.Plugins;

namespace Antext
{
    /// <summary>
    /// This class extends IAntextPluginable implementation using genericity. It adds some common functionality.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AntextAnalyzer<T> : IAntextAnalyzer where T : IAntextPluginable, new()
    {
        protected T analyzeService;
        protected bool fixInOriginalText;

        /// <summary>
        /// Tells if given original text will be altered by replacing found items with their fixed value.
        /// </summary>
        public bool FixInOriginalText
        {
            get { return fixInOriginalText; }
        }

        /// <summary>
        /// Creates fully functional analyzer. 
        /// If fixInOriginalText is specified, found original values will be replaced with found fixed values.
        /// </summary>
        /// <param name="fixInOriginalText"></param>
        public AntextAnalyzer(bool fixInOriginalText = false)
        {
            this.fixInOriginalText = fixInOriginalText;
            this.analyzeService = new T();
        }

        /// <summary>
        /// Creates fully functional analyzer. You can pass custom instance of your analyze plugin. 
        /// If fixInOriginalText is specified, found original values will be replaced with found fixed values.
        /// </summary>
        /// <param name="fixInOriginalText"></param>
        public AntextAnalyzer(T analyzeService, bool fixInOriginalText = false)
        {
            this.fixInOriginalText = fixInOriginalText;

            if (analyzeService == null)
                throw new ArgumentException("Given analyzeService is null!");

            this.analyzeService = analyzeService;
        }

        /// <summary>
        /// Runs the analyzation.
        /// </summary>
        /// <param name="originalText"></param>
        /// <returns></returns>
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