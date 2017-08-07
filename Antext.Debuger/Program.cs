using System;
using Antext.Objects;
using Antext.Plugins;

namespace Antext.Debuger
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prepare text to be analysed
            string textToAnalyse = "Im selling Ford Focus combi, 1999,. If you want, call 777888999 or SenDmEMail@gmail.com. More photos on https://greatestcars.com/ad/77860320/Ford-FOCUS-ST-20-ST-250ps.php.";

            // Create new instance of Antexter
            Antexter antexter = new Antexter();

            // Add EmailAntextPlugin for email analysis
            antexter.AddAnalyzer(new AntextAnalyzer<EmailAntextPlugin>(true));

            // Add PhoneAntextPlugin for phonenumebrs analysis (uses libphonenumber)
            antexter.AddAnalyzer(new AntextAnalyzer<PhoneAntextPlugin>(true));

            // Add LinkAntextPlugin for link analysis
            antexter.AddAnalyzer(new AntextAnalyzer<LinkAntextPlugin>(false));


            // Run the analysis
            AntextString result = antexter.Analyze(textToAnalyse);




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
