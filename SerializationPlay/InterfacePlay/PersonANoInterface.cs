using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SerializationPlay.InterfacePlay
{
	[JsonConverter(typeof(OptInClassConverter<PersonANoInterface>))]
	internal class PersonANoInterface
	{
		public PersonANoInterface()
		{
		}

		public PersonANoInterface(string address, string personName, string additionAInfo)
		{
			Address=address;
			PersonName=personName;
			AdditionAInfo=additionAInfo;
		}
		[JsonInclude]
		public string Address { get; set; }
		[JsonInclude]
		public string PersonName { get; set; }
		private string AdditionAInfo { get; set; }

	}
}
