using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antex.Objects;

namespace Antex.Debuger
{
    class Program
    {
        static void Main(string[] args)
        {
            string textToAnalyse = "";

            Antexter antexter = new Antexter(new AntextOptions()
            {
                //WhatToAnalyse = AntexStringItemType.Email | AntexStringItemType.Link,
                WrapLinksText = "odkaz",
                WrapLinksAHrefs = true
            });

            AntextString result = antexter.Analyze(textToAnalyse);

            

        }
    }
}
