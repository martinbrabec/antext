using System.Collections.Generic;
using Antext.Objects;

namespace Antext.FindServices
{
    public interface IFindService
    {
        List<AntextStringItem> GetItems(string text);
    }
}