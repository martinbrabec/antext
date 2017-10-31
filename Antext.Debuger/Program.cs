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
            string textToAnalyse = "Im selling Ford Focus combi, 1999,. If you want, call 777888999 or 313313313 or SenDmEMail@gmail.com. Alternatively myMail@google.com. More photos on https://greatestcars.com/ad/77860320/Ford-FOCUS-ST-20-ST-250ps.php. <script>alert('hi');</script>";
            textToAnalyse =
                "Rakovník  Malse Roudne Canlı İzle #Link:  http://www.macekranitv.com/2017/10/rakovnik-malse-roudne-canli-izle/  (Yayın maç saatinde eklenecektir) #maçizle #maçlinkleri";

            // Create new instance of Antexter
            Antexter antexter = new Antexter();


            // Add HtmlTagPlugin for html tags analysis. FOund tags are deduplicated. 
            // Revising in this case means removing. (OriginalValue will be replaced with empty string)
            antexter.AddAnalyzer(new AntextAnalyzer<HtmlTagAntextPlugin>(reviseOriginalText: true));

            // Add EmailAntextPlugin for email analysis
            antexter.AddAnalyzer(new AntextAnalyzer<EmailAntextPlugin>(reviseOriginalText: true));

            // Add PhoneAntextPlugin for phonenumebrs analysis (uses libphonenumber)
            antexter.AddAnalyzer(new AntextAnalyzer<PhoneAntextPlugin>(reviseOriginalText: true));

            // Add LinkAntextPlugin for link analysis
            antexter.AddAnalyzer(new AntextAnalyzer<LinkAntextPlugin>(reviseOriginalText: false, wrapMask: "<a href=\"{0}\">link</a>"));


            // Run the analysis
            AntextString result = antexter.Analyze(textToAnalyse);




            Console.WriteLine("GivenText: " + result.OriginalText);
            Console.WriteLine();
            Console.WriteLine("RevisedText: " + result.RevisedText);
            Console.WriteLine();

            Console.WriteLine("FOUND:");
            foreach (var antextStringItem in result.FoundItems)
            {
                Console.WriteLine($"{antextStringItem.Type} : {antextStringItem.RevisedValue} ({antextStringItem.OriginalValue})");
            }


            var f = PhoneAntextPlugin.GetSupportedRegions();

        }
    }
}
