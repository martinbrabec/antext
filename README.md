# antext
Antext is simple c# text analizer library, which can find phone numbers (by default Czech Republic), emails and links in text. It can also replace them with fixed value. Phone number search uses libphonenumber and regex. Email and link analyzer uses just regex. 

# instalation

You can install it as NUGET package from https://www.nuget.org/packages/Antext/ 

# usage

Usage is really simple

```csharp
// Prepare text to be analysed
string textToAnalyse = "Im selling Ford Focus combi, 1999,. If you want, call 777888999 or 313313313 or SenDmEMail@gmail.com. Alternatively myMail@google.com. More photos on https://greatestcars.com/ad/77860320/Ford-FOCUS-ST-20-ST-250ps.php. <script>alert('hi');</script>";

// Create new instance of Antexter
Antexter antexter = new Antexter();


// Add HtmlTagPlugin for html tags analysis. Found tags are deduplicated. 
// Revising in this case means removing. (OriginalValue will be replaced with empty string)
antexter.AddAnalyzer(new AntextAnalyzer<HtmlTagAntextPlugin>(reviseOriginalText: true));

// Add EmailAntextPlugin for email analysis
antexter.AddAnalyzer(new AntextAnalyzer<EmailAntextPlugin>(reviseOriginalText: true)); 

// Add PhoneAntextPlugin for phonenumebrs analysis (uses libphonenumber)
antexter.AddAnalyzer(new AntextAnalyzer<PhoneAntextPlugin>(reviseOriginalText: true));

// Add LinkAntextPlugin for link analysis
// Make sure to add this plugin AFTER HtmlTagPlugin, so your wrapped links are not replaced
antexter.AddAnalyzer(new AntextAnalyzer<LinkAntextPlugin>(reviseOriginalText: false, wrapMask: "<a href=\"{0}\">link</a>"));


// Run the analysis
AntextString result = antexter.Analyze(textToAnalyse);
```

The code above will give following result in AntextString object.

````
ORIGINAL TEXT
Im selling Ford Focus combi, 1999,. If you want, call 777888999 or 313313313 or SenDmEMail@gmail.com. Alternatively myMail@google.com. More photos on https://greatestcars.com/ad/77860320/Ford-FOCUS-ST-20-ST-250ps.php. <script>alert('hi');</script>hp.

REVISED TEXT
Im selling Ford Focus combi, 1999,. If you want, call +420 777 888 999 or +420 313 313 313 or sendmemail@gmail.com. Alternatively mymail@google.com. More photos on <a href="https://greatestcars.com/ad/77860320/Ford-FOCUS-ST-20-ST-250ps.php">link</a>. alert('hi');


FOUND ITEMS
HtmlTag :  (<script>)
HtmlTag :  (</script>)
Email : sendmemail@gmail.com (SenDmEMail@gmail.com)
Email : mymail@google.com (myMail@google.com)
PhoneNumber : +420 777 888 999 (777888999)
PhoneNumber : +420 313 313 313 (313313313)
Link : https://greatestcars.com/ad/77860320/Ford-FOCUS-ST-20-ST-250ps.php (https://greatestcars.com/ad/77860320/Ford-FOCUS-ST-20-ST-250ps.php)
````

If you need anything, you can contact me at info@martinbrabec.cz

