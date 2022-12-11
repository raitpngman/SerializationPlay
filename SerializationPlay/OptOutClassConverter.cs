using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SerializationPlay
{
	//This class serializes all properties unless they have the [JsonIgnore] attribute
	//This serializes all properties no matter if they are public or private.
	internal class OptOutClassConverter<C> : JsonConverter<C> where C : new()
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

			

			var name = reader.GetString();

			var source = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(name, options);

			var newClass = new C();

			var classType = newClass.GetType();
			var classProps = classType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

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
			//The ShouldUse is additional
			var props = value.GetType()
							 .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
							 .Where(x => ShouldUse(x))
							 .ToDictionary(x => x.Name, x => x.GetValue(value));
			

				var ser = JsonSerializer.Serialize(props);

			writer.WriteStringValue(ser);
		}

		/// <summary>
		/// Allows you to exclude any with JsonIgnoreAttributes
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		private bool ShouldUse(PropertyInfo info)
		{
			return !info.GetCustomAttributes(typeof(JsonIgnoreAttribute)).Any();

		}

	}

}
