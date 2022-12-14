
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;

//This class allows for checking for custom attributes. In our case, the privacy attribute
//This class only serializes properties that have the [JsonInclude] attribute
//This serializes all properties no matter if they are public or private.
namespace SerializationPlay
{
	internal class OptInPrivacyClassConverter<C> : ConverterBase<C> where C : new()
	{

		/// <summary>
		/// Use as long as it has a JsonIncludeAttribute
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
