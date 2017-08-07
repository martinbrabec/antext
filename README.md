# antext
Antext is simple c# text analizer library, which can find phone numbers (by default Czech Republic), emails and links in text. It can also replace them with fixed value. Phone number search uses libphonenumber and regex. Email and link analyzer uses just regex. 

# instalation

You can install it as NUGET package from https://www.nuget.org/packages/Antext/ 

# usage

Usage is really simple

```csharp
// Prepare text to be analysed
string textToAnalyse = "Im selling Ford Focus combi, 1999,. If you want, call 777888999 or SenDmEMail@gmail.com. More photos on https://greatestcars.com/ad/77860320/Ford-FOCUS-1999-85kw.php.";

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
```

The code above will give following result in AntextString object.

````
ORIGINAL TEXT
Im selling Ford Focus combi, 1999,. If you want, call 777888999 or SenDmEMail@gmail.com. More photos on https://greatestcars.com/ad/77860320/Ford-FOCUS-ST-20-ST-250ps.php.

FIXED TEXT
Im selling Ford Focus combi, 1999,. If you want, call +420 777 888 999 or sendmemail@gmail.com. More photos on https://greatestcars.com/ad/77860320/Ford-FOCUS-ST-20-ST-250ps.php.

FOUND ITEMS
Email : sendmemail@gmail.com (SenDmEMail@gmail.com)
PhoneNumber : +420 777 888 999 (777888999)
Link : https://greatestcars.com/ad/77860320/Ford-FOCUS-ST-20-ST-250ps.php
````

If you need anything, you can contact me at info@martinbrabec.cz

