using Antex.Objects;

namespace Antext.Objects
{
    public class AntextOptions
    {
        /// <summary>
        /// Libphonenumber's region code. More info here https://stackoverflow.com/questions/16803432/listing-all-country-codes-of-phone-numbers.
        /// </summary>
        public string PhoneNumbersDefaultRegion { get; set; } = "CZ";

        public AntextStringItemType WhatToAnalyse { get; set; } = (AntextStringItemType)int.MaxValue;

        public bool WrapLinksAHrefs { get; set; } = false;
        public string WrapLinksText { get; set; } = "link";
    }
}