namespace Antex.Objects
{
    public class AntextStringItem
    {
        public AntextStringItemType Type { get; private set; }
        public string Value { get; private set; }
        public long StartIndex { get; private set; }

        public AntextStringItem(AntextStringItemType type, string value, long startIndex)
        {
            Type = type;
            Value = value;
            StartIndex = startIndex;
        }
    }
}