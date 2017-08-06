namespace Antext.Objects
{
    public class AntextStringItem
    {
        public AntextStringItemType Type { get; private set; }
        public string OriginalValue { get; private set; }
        public long StartIndex { get; private set; }
        public string FixedValue { get; private set; }

        public AntextStringItem(AntextStringItemType type, long startIndex, string originalValue, string fixedValue = null)
        {
            Type = type;
            OriginalValue = originalValue;
            StartIndex = startIndex;

            FixedValue = fixedValue ?? originalValue;
        }
    }
}