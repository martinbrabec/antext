using System;
using Antext.FindServices;
using Antext.Objects;
using Antext.Plugins;

namespace Antext.Debuger
{
    class Program
    {
        static void Main(string[] args)
        {
            string textToAnalyse = "Prodám Ford Focus kombi, r.v. 1999, k vidění na náměstí. Kdyžtak 777888999 nebo MAIL@MA-IL.com. Více fotek je na https://auto.bazos.cz/inzerat/77860320/Ford-FOCUS-ST-20-ST-250ps.php";

            Antexter a = new Antexter();
            a.AddAnalyzer(new AntextAnalyzer<EmailAntextPlugin>(true)); 
            a.AddAnalyzer(new AntextAnalyzer<PhoneAntextPlugin>(true));
            a.AddAnalyzer(new AntextAnalyzer<LinkAntextPlugin>(true));


            AntextString result = a.Analyze(textToAnalyse);

            Console.WriteLine("GivenText: " + result.OriginalText);
            Console.WriteLine();
            Console.WriteLine("FixedText: " + result.FixedText);
            Console.WriteLine();

            Console.WriteLine("FOUND:");
            foreach (var antextStringItem in result.FoundItems)
            {
                Console.WriteLine($"{antextStringItem.Type} : {antextStringItem.FixedValue} ({antextStringItem.OriginalValue})");
            }


            var f = PhoneAntextPlugin.GetSupportedRegions();

        }
    }
}
