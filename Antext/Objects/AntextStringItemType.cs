using System;

namespace Antext.Objects
{
    [Flags]
    public enum AntextStringItemType
    {
        None = 0,
        Email = 1,
        PhoneNumber = 2,
        Link = 4
    }
}