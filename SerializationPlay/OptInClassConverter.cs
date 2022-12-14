using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.CompilerServices;
using SerializationPlay.InterfacePlay;

namespace SerializationPlay
{
	//This class only serializes properties that have the [JsonInclude] attribute
	//This serializes all properties no matter if they are public or private.
	internal class OptInClassConverter<C> : ConverterBase<C> where C : new()
	{

		/// <summary>
		/// Allows us to only include those properties with JsonIncludeAttributes
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		private protected override bool ShouldUse(PropertyInfo info)
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
		private protected override object GetValue(PropertyInfo pi, object? value)
		{
			
			object returnObject = pi.GetValue(value);
			
			return returnObject;


		}
	}

}
