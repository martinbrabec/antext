using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antex.FindServices;
using Antex.Objects;
using Antext.FindServices;
using Antext.Objects;

namespace Antex
{
    public class Antexter
    {
        private AntextOptions options { get; set; }

        public Antexter(AntextOptions options = null)
        {
            this.options = options ?? new AntextOptions();
        }

        public AntextString Analyze(string text)
        {
            var outputAntex = new AntextString();
            outputAntex.OriginalText = text;


            if ((options.WhatToAnalyse & AntextStringItemType.PhoneNumber) == AntextStringItemType.PhoneNumber)
            {
                PhoneFindService phoneFindService = new PhoneFindService(options.PhoneNumbersDefaultRegion);

                outputAntex.FoundItems.AddRange(phoneFindService.GetItems(text));
            }

            if ((options.WhatToAnalyse & AntextStringItemType.Email) == AntextStringItemType.Email)
            {
                EmailFindService emailFindService = new EmailFindService();
                outputAntex.FoundItems.AddRange(emailFindService.GetItems(text));
            }

            if ((options.WhatToAnalyse & AntextStringItemType.Link) == AntextStringItemType.Link)
            {
                LinkFindService linkFindService = new LinkFindService();
                outputAntex.FoundItems.AddRange(linkFindService.GetItems(text));
            }

            return outputAntex;
        }


    }
}
