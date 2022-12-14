using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace SerializationPlay.InterfacePlay
{
    public class InterfaceConverter<T> : JsonConverter<T>
    where T : class
    {
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

            string typeValue = readerClone.GetString();
            var instance = Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName, typeValue).Unwrap();
            var entityType = instance.GetType();
            //readerClone.Read();
            var deserialized = JsonSerializer.Deserialize(ref reader, entityType, options);
            return (T)deserialized;
           //reader.Read();
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
                        var type = value.GetType();
                        writer.WriteStartObject();
                        writer.WriteString("$type", type.FullName);
                        var stringValue = JsonSerializer.Serialize(value, type, options);
                        var jsonDocument = JsonDocument.Parse(stringValue);
                        //JsonSerializer.Serialize(writer, value, options);
                        if (jsonDocument.RootElement.ValueKind == JsonValueKind.String)
                        {
                            //Remove the starting and ending quote marks so it properly 
                            //parses
                            stringValue = stringValue.Remove(0, stringValue.IndexOf("{") );
                            stringValue = stringValue.Remove(stringValue.LastIndexOf("}" )+1);
							stringValue = stringValue.Replace("\\u0022", @"""");
							jsonDocument = JsonDocument.Parse(stringValue);
						}
                        
						
                        //stringValue = stringValue.Remove(0, stringValue.IndexOf("{") + 1);
                        //stringValue = stringValue.Remove(stringValue.LastIndexOf("}"));
						//stringValue = stringValue.Replace("\\u0022", @"""");
      //                  var jd2 = JsonDocument.Parse(stringValue);


                       // jsonDocument.WriteTo(writer);

                        //var newJsonDoc = JsonDocument.Parse(stringValue);
                        //writer.WriteStringValue(stringValue);
                        //writer.WriteString("$class", JsonSerializer.Serialize(value, type, options));
                        foreach (var element in jsonDocument.RootElement.EnumerateObject())
                        {
                            element.WriteTo(writer);
                        }
                        jsonDocument.Dispose();
                        //writer.WriteStringValue(stringValue);
                        //writer.WriteRawValue(stringValue);

                        writer.WriteEndObject();
                        break;
                    }
            }
        }
    }
}
