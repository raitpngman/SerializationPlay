using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using SerializationPlay.InterfacePlay;
using System.Text.Json.Serialization.Metadata;

namespace SerializationPlay
{
	//This class serializes all properties unless they have the [JsonIgnore] attribute
	//This serializes all properties no matter if they are public or private.
	internal class OptOutClassConverter<C> : ConverterBase<C> where C : new()
	{



		/// <summary>
		/// Allows you to exclude any with JsonIgnoreAttributes
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		private protected override bool ShouldUse(PropertyInfo info)
		{
			return !info.GetCustomAttributes(typeof(JsonIgnoreAttribute)).Any();

		}
		/// <summary>
		/// This checks for custom attributes and allows you to do whatever you want
		/// with that value.
		/// </summary>
		/// <param name="pi"></param>
		/// <param name="value"></param>
		/// <returns>object value</returns>
		private protected override object GetValue(PropertyInfo pi, object? value)
		{
			
			//JsonSerializer.Serialize(pi.GetValue(value), pi.GetValue(value).GetType());
			object returnObject = pi.GetValue(value);
			return returnObject;
			
			

		}
	}

}
