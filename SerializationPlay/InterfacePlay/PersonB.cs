using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SerializationPlay.InterfacePlay
{
	[JsonConverter(typeof(OptOutClassConverter<PersonB>))]
	internal class PersonB : IPerson
	{
		public PersonB()
		{
		}

		public PersonB(string address, string personName, string additionBInfo)
		{
			Address=address;
			PersonName=personName;
			AdditionBInfo=additionBInfo;
		}

		public string Address { get; set; }

		public string PersonName { get; set; }
		private string AdditionBInfo { get; set; }
	}
}
