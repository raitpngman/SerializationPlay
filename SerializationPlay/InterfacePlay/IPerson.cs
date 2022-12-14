using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SerializationPlay.InterfacePlay
{
    //[JsonConverter(typeof(InterfaceConverter<IPerson>))]
	[JsonInterfaceConverter(typeof(InterfaceConverter<IPerson>))]
	public interface IPerson
    {
        string Address { get; }
        string PersonName { get; }
    }
}
