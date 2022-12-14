// See https://aka.ms/new-console-template for more information
using SerializationPlay;
using SerializationPlay.InterfacePlay;
using System.Text.Json;

Console.WriteLine("Starting now.");
//We have to have the converter in options to handle non list interface properties
//such as "IPerson p {get;set;}"
JsonSerializerOptions options = new JsonSerializerOptions();
options.Converters.Add(new InterfaceConverter<IPerson>());


Console.WriteLine("Staring PersonTester (interface usage)");
var personTester = new PersonTester(4);
var personTesterJson = JsonSerializer.Serialize<PersonTester>(personTester, options);
Console.WriteLine(personTesterJson);
var personTesterDeserialized = JsonSerializer.Deserialize<PersonTester>(personTesterJson,options);

//Because the class has List<PersonA> rather than List<IPerson>, of the converter is 
//passed in in the options, it will cause an error.
Console.WriteLine();
Console.WriteLine("Starting personTesterNoInterface");
var personTesterB = new PersonTesterNoInterface(4);
var personTesterJsonB = JsonSerializer.Serialize<PersonTesterNoInterface>(personTesterB);
Console.WriteLine(personTesterJsonB);
var personTesterDeserializedB = JsonSerializer.Deserialize<PersonTesterNoInterface>(personTesterJsonB);


//The rest work fine with or without the converter in options.
Console.WriteLine();
Console.WriteLine("Starting PersonOptIn");
var personOptIn = new PersonOptIn("John Doe", "anywhere USA", "111-111-1111",
	"Tn359483", new string[3] { "Mandy","James","Vanessa" });
var personOptInJson = JsonSerializer.Serialize<PersonOptIn>(personOptIn, options);
Console.WriteLine(personOptInJson);
var personOptInDeserialized = JsonSerializer.Deserialize<PersonOptIn>(personOptInJson, options);

Console.WriteLine();
Console.WriteLine("Starting PersonOptOut");
var personOptOut = new PersonOptOut("John Doe", "anywhere USA", "111-111-1111",
	"Tn359483", new string[3] { "Mandy", "James", "Vanessa" });
var personOptOutJson = JsonSerializer.Serialize<PersonOptOut>(personOptOut, options);
Console.WriteLine(personOptOutJson);
var personOptOutDeserialized = JsonSerializer.Deserialize<PersonOptOut>(personOptOutJson, options);

Console.WriteLine();
Console.WriteLine("Starting PersonOptInPrivacy");
var personOptInPrivacy = new PersonOptInPrivacy("John Doe", "anywhere USA", "111-111-1111",
	"Tn359483", new string[3] { "Mandy", "James", "Vanessa" });

var personOptInPrivacyJson = JsonSerializer.Serialize<PersonOptInPrivacy>(personOptInPrivacy, options);
Console.WriteLine(personOptInPrivacyJson);
var personOptInPrivacyDeserialized = JsonSerializer.Deserialize<PersonOptInPrivacy>(personOptInPrivacyJson, options);
