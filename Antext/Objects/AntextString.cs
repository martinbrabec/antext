﻿using System.Collections.Generic;

namespace Antext.Objects
{
    public class AntextString
    {
        public string OriginalText { get; set; }
        public string FixedText { get; set; }

        public List<AntextStringItem> FoundItems { get; set; }

        public AntextString()
        {
            FoundItems = new List<AntextStringItem>();
        }
    }
}