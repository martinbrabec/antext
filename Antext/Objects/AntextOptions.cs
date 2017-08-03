namespace Antex.Objects
{
    public class AntextOptions
    {
        public AntextStringItemType WhatToAnalyse { get; set; } = (AntextStringItemType)int.MaxValue;
        public bool WrapLinksAHrefs { get; set; } = false;
        public string WrapLinksText { get; set; } = "link";
    }
}