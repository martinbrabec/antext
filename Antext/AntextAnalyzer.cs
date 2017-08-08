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
        protected string wrapMask;

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
        /// <param name="fixInOriginalText">If set to true, found original value will be replaced by fixed value.</param>
        /// <param name="wrapMask">If not null, this will be used in string.Format, where {0} will be original or fixed value, based on fixInOriginalText settings.</param>
        public AntextAnalyzer(bool fixInOriginalText = false, string wrapMask = null)
        {
            this.wrapMask = wrapMask;
            this.fixInOriginalText = fixInOriginalText;
            this.analyzeService = new T();
        }

        /// <summary>
        /// Creates fully functional analyzer. You can pass custom instance of your analyze plugin. 
        /// If fixInOriginalText is specified, found original values will be replaced with found fixed values.
        /// </summary>
        /// <param name="analyzeService">Custm instance of the service.</param>
        /// <param name="fixInOriginalText">If set to true, found original value will be replaced by fixed value.</param>
        /// <param name="wrapMask">If not null, this will be used in string.Format, where {0} will be original or fixed value, based on fixInOriginalText settings.</param>
        public AntextAnalyzer(T analyzeService, bool fixInOriginalText = false, string wrapMask = null)
        {
            this.wrapMask = wrapMask;

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

            string fixedText = ReplaceOriginalValuesByFixedAndWrapValues(foundItems, originalText);

            AntextAnalyzeResult result = new AntextAnalyzeResult(foundItems, fixedText);
            return result;
        }


        protected virtual string ReplaceOriginalValuesByFixedAndWrapValues(List<AntextStringItem> foundItems, string originalText)
        {
            string fixedText = originalText;
            foreach (var antextStringItem in foundItems)
            {
                if (fixInOriginalText)
                {
                    // Fix and wrap
                    var fixedValue = wrapMask == null ? antextStringItem.FixedValue : string.Format(wrapMask, antextStringItem.FixedValue);

                    fixedText = fixedText.Replace(antextStringItem.OriginalValue, fixedValue);
                }
                else if (wrapMask != null)
                {
                    // Only wrap original values
                    fixedText = fixedText.Replace(antextStringItem.OriginalValue, string.Format(wrapMask, antextStringItem.FixedValue));
                }
            }
            return fixedText;
        }
    }
}