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
        protected bool reviseOriginalText;
        protected string wrapMask;

        /// <summary>
        /// Tells if given original text will be altered by replacing found items with their revised value.
        /// </summary>
        public bool ReviseOriginalText
        {
            get { return reviseOriginalText; }
        }

        /// <summary>
        /// Creates fully functional analyzer. 
        /// If fixInOriginalText is specified, found original values will be replaced with found revised values.
        /// </summary>
        /// <param name="reviseOriginalText">If set to true, found original value will be replaced by revised value.</param>
        /// <param name="wrapMask">If not null, this will be used in string.Format, where {0} will be original or revised value, based on reviseOriginalText settings.</param>
        public AntextAnalyzer(bool reviseOriginalText = false, string wrapMask = null)
        {
            this.wrapMask = wrapMask;
            this.reviseOriginalText = reviseOriginalText;
            this.analyzeService = new T();
        }

        /// <summary>
        /// Creates fully functional analyzer. You can pass custom instance of your analyze plugin. 
        /// If fixInOriginalText is specified, found original values will be replaced with found revised values.
        /// </summary>
        /// <param name="analyzeService">Custm instance of the service.</param>
        /// <param name="reviseOriginalText">If set to true, found original value will be replaced by revised value.</param>
        /// <param name="wrapMask">If not null, this will be used in string.Format, where {0} will be original or revised value, based on fixInOriginalText settings.</param>
        public AntextAnalyzer(T analyzeService, bool reviseOriginalText = false, string wrapMask = null)
        {
            this.wrapMask = wrapMask;

            this.reviseOriginalText = reviseOriginalText;

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

            string revisedText = ReplaceOriginalValuesByRevisedAndWrapValues(foundItems, originalText);

            AntextAnalyzeResult result = new AntextAnalyzeResult(foundItems, revisedText);
            return result;
        }


        protected virtual string ReplaceOriginalValuesByRevisedAndWrapValues(List<AntextStringItem> foundItems, string originalText)
        {
            string revisedText = originalText;
            foreach (var antextStringItem in foundItems)
            {
                if (reviseOriginalText)
                {
                    // Revise and wrap
                    var revisedValue = wrapMask == null ? antextStringItem.RevisedValue : string.Format(wrapMask, antextStringItem.RevisedValue);

                    revisedText = revisedText.Replace(antextStringItem.OriginalValue, revisedValue);
                }
                else if (wrapMask != null)
                {
                    // Only wrap original values
                    revisedText = revisedText.Replace(antextStringItem.OriginalValue, string.Format(wrapMask, antextStringItem.RevisedValue));
                }
            }
            return revisedText;
        }
    }
}