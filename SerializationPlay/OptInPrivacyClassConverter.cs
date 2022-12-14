﻿
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;

//This class allows for checking for custom attributes. In our case, the privacy attribute
//This class only serializes properties that have the [JsonInclude] attribute
//This serializes all properties no matter if they are public or private.
namespace SerializationPlay
{
	internal class OptInPrivacyClassConverter<C> : JsonConverter<C> where C : new()
	{
		/// <summary>
		/// Standard read override
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="typeToConvert"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public override C Read(ref Utf8JsonReader reader,
									  Type typeToConvert,
									  JsonSerializerOptions options)
		{

			var newClass = new C();

			var classType = newClass.GetType();

			var classProps = classType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);


			//handle interface type.
			JsonDocument jsonDoc;
			if (reader.TokenType != JsonTokenType.String && JsonDocument.TryParseValue(ref reader, out jsonDoc))
			{

				foreach (var item in jsonDoc.RootElement.EnumerateObject())
				{
					if (item.Name != "$type")
					{
						var classProp = classProps.FirstOrDefault(x => x.Name == item.Name);

						if (classProp != null)
						{
							string text = item.Value.GetRawText();
							var t = classProp.PropertyType;
							var value = JsonSerializer.Deserialize(text, t, options);


							classType.InvokeMember(classProp.Name,
								BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
								null,
								newClass,
								new object[] { value });
						}

					}

				}
				jsonDoc.Dispose();

			}
			else //Normal usage
			{
				var name = reader.GetString();

				var source = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(name, options);


				foreach (var s in source.Keys)
				{
					var classProp = classProps.FirstOrDefault(x => x.Name == s);

					if (classProp != null)
					{
						string text = source[s].GetRawText();
						var t = classProp.PropertyType;
						var value = JsonSerializer.Deserialize(text, t, options);


						classType.InvokeMember(classProp.Name,
							BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
							null,
							newClass,
							new object[] { value });
					}
				}
			}





			return newClass;
		}

	
		/// <summary>
		/// Standard write override except for prop selection
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="value"></param>
		/// <param name="options"></param>
		public override void Write(Utf8JsonWriter writer,
								   C value,
								   JsonSerializerOptions options)
		{
			
			//This allows us to get non public properties.
			//The ShouldUse and GetValue are additional
			var props = value.GetType()
							 .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
							 .Where(x => ShouldUse(x))
							 .ToDictionary(x => x.Name, x => GetValue(x,value));
			

				var ser = JsonSerializer.Serialize(props, options);

			writer.WriteStringValue(ser);
		}

		/// <summary>
		/// Use as long as it has a JsonIncludeAttribute
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		private bool ShouldUse(PropertyInfo info)
		{
			return info.GetCustomAttributes(typeof(JsonIncludeAttribute)).Any();

		}
		/// <summary>
		/// This checks for custom attributes and allows you to do whatever you want
		/// with that value.
		/// </summary>
		/// <param name="pi"></param>
		/// <param name="value"></param>
		/// <returns>object value</returns>
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

	}

}
