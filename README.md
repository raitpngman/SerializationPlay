# SerializationPlay
Did you ever struggle with getting Microsoft's new System.Text.Json to do what you wanted or to swap over from NewtonSoft.Json? This repository includes samples for a default OptIn class serializer, OptOut serializer, and also one that supports custom attributes. It also allows **ALL properties** to be included! It doesn't matter if they are private, internal, or public, or if they have private getters or setters.

To use any of the converter classes, you just need to add a line to the start of your class. It's as simple as that!

```
//Tell it which class converter to use for this class
[JsonConverter(typeof(OptInClassConverter<PersonOptIn>))]
internal class PersonOptIn
{
}
```
The three converter classes (OptInClassConverter, OptOutClassConverter, OptInPrivacyClassConverter) are basically the same except for where it gets the properties in the writer override method. 

```
//This allows us to get non public properties.
//The ShouldUse is additional
var props = value.GetType()
				 .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
				 .Where(x => ShouldUse(x))
				 .ToDictionary(x => x.Name, x => x.GetValue(value));
               
```
The BindingFlags.NonPublic allows it to bring in Private or Internal properties. 
The ShouldUse method checks for the propper attribute as to if it should be included or not. Here's the OptIn ShouldUse method. It checks if there is a [JsonInclude] attribute and returns true if there is.

```
private bool ShouldUse(PropertyInfo info)
		{
			return info.GetCustomAttributes(typeof(JsonIncludeAttribute)).Any();

		}
```
For the custom attributes checking, it gets the props and the values through this
```
var props = value.GetType()
		.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
		.Where(x => ShouldUse(x))
		.ToDictionary(x => x.Name, x => GetValue(x,value));

```
And then allows checking and reformatting for custom attributes
 
 ```
 private object GetValue(PropertyInfo pi, object? value)
		{
			if (pi.GetCustomAttributes(typeof(PrivacyAttribute)).Any())
			{
				return "********";
			}
			else
			{
				return pi.GetValue(value);
			}

		}
 
 ```
 The three person classes show examples of the three different types of converters.
 I hope this helps you get started with the System.Text.Json class and work out some of the common beginning frustrations :)
 
