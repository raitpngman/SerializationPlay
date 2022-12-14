// See https://aka.ms/new-console-template for more information
using SerializationPlay;
using SerializationPlay.InterfacePlay;
using System.Text.Json;

Console.WriteLine("Starting now.");

Console.WriteLine("Staring PersonTester (interface usage)");
var personTester = new PersonTester(4);
var personTesterJson = JsonSerializer.Serialize<PersonTester>(personTester);
Console.WriteLine(personTesterJson);
var personTesterDeserialized = JsonSerializer.Deserialize<PersonTester>(personTesterJson);

Console.WriteLine();
Console.WriteLine("Starting personTesterNoInterface");
var personTesterB = new PersonTesterNoInterface(4);
var personTesterJsonB = JsonSerializer.Serialize<PersonTesterNoInterface>(personTesterB);
Console.WriteLine(personTesterJsonB);
var personTesterDeserializedB = JsonSerializer.Deserialize<PersonTesterNoInterface>(personTesterJsonB);

Console.WriteLine();
Console.WriteLine("Starting PersonOptIn");
var personOptIn = new PersonOptIn("John Doe", "anywhere USA", "111-111-1111",
	"Tn359483", new string[3] { "Mandy","James","Vanessa" });
var personOptInJson = JsonSerializer.Serialize<PersonOptIn>(personOptIn);
Console.WriteLine(personOptInJson);
var personOptInDeserialized = JsonSerializer.Deserialize<PersonOptIn>(personOptInJson);

Console.WriteLine();
Console.WriteLine("Starting PersonOptOut");
var personOptOut = new PersonOptOut("John Doe", "anywhere USA", "111-111-1111",
	"Tn359483", new string[3] { "Mandy", "James", "Vanessa" });
var personOptOutJson = JsonSerializer.Serialize<PersonOptOut>(personOptOut);
Console.WriteLine(personOptOutJson);
var personOptOutDeserialized = JsonSerializer.Deserialize<PersonOptOut>(personOptOutJson);

Console.WriteLine();
Console.WriteLine("Starting PersonOptInPrivacy");
var personOptInPrivacy = new PersonOptInPrivacy("John Doe", "anywhere USA", "111-111-1111",
	"Tn359483", new string[3] { "Mandy", "James", "Vanessa" });

var personOptInPrivacyJson = JsonSerializer.Serialize<PersonOptInPrivacy>(personOptInPrivacy);
Console.WriteLine(personOptInPrivacyJson);
var personOptInPrivacyDeserialized = JsonSerializer.Deserialize<PersonOptInPrivacy>(personOptInPrivacyJson);
