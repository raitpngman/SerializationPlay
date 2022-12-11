// See https://aka.ms/new-console-template for more information
using SerializationPlay;
using System.Text.Json;

Console.WriteLine("Hello, World!");


var personOptIn = new PersonOptIn("John Doe", "anywhere USA", "111-111-1111",
	"Tn359483", new string[3] { "Mandy","James","Vanessa" });

var personOptInJson = JsonSerializer.Serialize<PersonOptIn>(personOptIn);
Console.WriteLine(personOptInJson);
var personOptInDeserialized = JsonSerializer.Deserialize<PersonOptIn>(personOptInJson);

var personOptOut = new PersonOptOut("John Doe", "anywhere USA", "111-111-1111",
	"Tn359483", new string[3] { "Mandy", "James", "Vanessa" });

var personOptOutJson = JsonSerializer.Serialize<PersonOptOut>(personOptOut);
Console.WriteLine(personOptOutJson);
var personOptOutDeserialized = JsonSerializer.Deserialize<PersonOptOut>(personOptOutJson);

var personOptInPrivacy = new PersonOptInPrivacy("John Doe", "anywhere USA", "111-111-1111",
	"Tn359483", new string[3] { "Mandy", "James", "Vanessa" });

var personOptInPrivacyJson = JsonSerializer.Serialize<PersonOptInPrivacy>(personOptInPrivacy);
Console.WriteLine(personOptInPrivacyJson);
var personOptInPrivacyDeserialized = JsonSerializer.Deserialize<PersonOptInPrivacy>(personOptInPrivacyJson);
