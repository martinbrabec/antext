namespace Antext.Objects
{
    /// <summary>
    /// Found item in given text.
    /// </summary>
    public class AntextStringItem
    {
        public AntextStringItemType Type { get; private set; }
        public string OriginalValue { get; private set; }
        public long StartIndex { get; private set; }
        public string RevisedValue { get; private set; }

        public AntextStringItem(AntextStringItemType type, long startIndex, string originalValue, string revisedValue = null)
        {
            Type = type;
            OriginalValue = originalValue;
            StartIndex = startIndex;

            RevisedValue = revisedValue ?? originalValue;
        }
    }
}