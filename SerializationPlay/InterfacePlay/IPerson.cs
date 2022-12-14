using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationPlay.InterfacePlay
{
	[JsonInterfaceConverter(typeof(InterfaceConverter<IPerson>))]
	public interface IPerson
    {
        string Address { get; }
        string PersonName { get; }
    }
}
