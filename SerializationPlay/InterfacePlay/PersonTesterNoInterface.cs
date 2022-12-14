using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SerializationPlay.InterfacePlay
{
	[JsonConverter(typeof(OptOutClassConverter<PersonTesterNoInterface>))]
	internal class PersonTesterNoInterface
	{
		public int Id { get; set; }
		List<PersonA> Persons { get; set; }
		public PersonTesterNoInterface() { }

		public PersonTesterNoInterface(int id)
		{
			Id = id;
			Persons = new List<PersonA>();
			Persons.Add(new PersonA("1234", "John", "personA"));
			Persons.Add(new PersonA("4321", "Jane", "personB"));
		}

	}
}
