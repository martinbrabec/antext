using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antex.Objects;
using Antext.FindServices;
using Antext.Objects;

namespace Antex.Debuger
{
    class Program
    {
        static void Main(string[] args)
        {
            string textToAnalyse = "Prodám Ford Focus kombi, r.v. 1999, k vidění na náměstí. Kdyžtak +420777888999 nebo mail@mail.com. Více fotek je na https://auto.bazos.cz/inzerat/77860320/Ford-FOCUS-ST-20-ST-250ps.php";

            Antexter antexter = new Antexter(new AntextOptions()
            {
                //WhatToAnalyse = AntexStringItemType.Email | AntexStringItemType.Link,
                WrapLinksText = "odkaz",
                WrapLinksAHrefs = true
            });

            AntextString result = antexter.Analyze(textToAnalyse);

            var f = PhoneFindService.GetSupportedRegions();

        }
    }
}
