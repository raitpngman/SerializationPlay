using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SerializationPlay.InterfacePlay
{
	[JsonConverter(typeof(OptInClassConverter<PersonA>))]
	internal class PersonA :IPerson
	{
		public PersonA()
		{
		}

		public PersonA(string address, string personName, string additionAInfo)
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
