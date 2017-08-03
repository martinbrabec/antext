using System.Collections.Generic;
using Antex.Objects;

namespace Antex.FindServices
{
    public interface IFindService
    {
        List<AntextStringItem> GetItems(string text);
    }
}