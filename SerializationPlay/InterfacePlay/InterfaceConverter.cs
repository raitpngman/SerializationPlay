using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace SerializationPlay.InterfacePlay
{
    public class InterfaceConverter<T> : JsonConverter<T>
    where T : class
    {
        public override bool CanConvert(Type typeToConvert)
        {
            //We have to override the CanConvert, because the interface converter
            //works okay for List<IPerson>, but it doesn't trigger for a singular
            //IPerson
            
            var r = base.CanConvert(typeToConvert);
			var interfaces = typeToConvert.GetInterfaces().FirstOrDefault(x => x.GetCustomAttribute(typeof(JsonInterfaceConverterAttribute)) != null);
			if (interfaces != null)
			{
                r= true;
			}
			return r;

        }
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerClone = reader;
            if (readerClone.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = readerClone.GetString();
            if (propertyName != "$type")
            {
                throw new JsonException();
            }

            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }
			//We need to remove this converter from the options or it makes
			//an endless loop.
			var options2 = new JsonSerializerOptions(options);
			JsonConverter toRemove = null;
			foreach (var item in options2.Converters.ToList())
			{
				if (item.GetType() == this.GetType())
				{
					options2.Converters.Remove(item);
				}
			}

			string typeValue = readerClone.GetString();
            var instance = Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName, typeValue).Unwrap();
            var entityType = instance.GetType();
            //This is where it makes an endless loop if we don't remove "this" converter
            var deserialized = JsonSerializer.Deserialize(ref reader, entityType, options2);
            return (T)deserialized;

        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case null:
                    JsonSerializer.Serialize(writer, (T)null, options);
                    break;
                default:
                    {
                        //We need to remove this converter from the options or it makes
                        //an endless loop.
                        var options2 = new JsonSerializerOptions(options);
                        JsonConverter toRemove = null;
                        foreach (var item in options2.Converters.ToList())
                        {
                            if (item.GetType() == this.GetType())
                            {
                                options2.Converters.Remove(item);
                            }
                        }
                        var type = value.GetType();
                        writer.WriteStartObject();
                        writer.WriteString("$type", type.FullName);
						//This is where it makes an endless loop if we don't remove "this" converter
						var stringValue = JsonSerializer.Serialize(value, type, options2);
                        var jsonDocument = JsonDocument.Parse(stringValue);
                    
                        if (jsonDocument.RootElement.ValueKind == JsonValueKind.String)
                        {
                            //Remove the starting and ending quote marks so it properly 
                            //parses
                            stringValue = stringValue.Remove(0, stringValue.IndexOf("{") );
                            stringValue = stringValue.Remove(stringValue.LastIndexOf("}" )+1);
							stringValue = stringValue.Replace("\\u0022", @"""");
							jsonDocument = JsonDocument.Parse(stringValue);
						}
                        
						
                        foreach (var element in jsonDocument.RootElement.EnumerateObject())
                        {
                            element.WriteTo(writer);
                        }
                        jsonDocument.Dispose();
                        

                        writer.WriteEndObject();
                        break;
                    }
            }
        }
    }
}
