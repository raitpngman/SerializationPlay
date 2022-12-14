using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SerializationPlay.InterfacePlay
{
	[JsonConverter(typeof(OptOutClassConverter<PersonTester>))]
	internal class PersonTester
	{
		public int Id { get; set; }
		List<IPerson> Persons { get; set; }
		public PersonTester() { }

		public PersonTester(int id)
		{
			Id = id;
			Persons = new List<IPerson>();
			Persons.Add(new PersonA("1234", "John", "personA"));
			Persons.Add(new PersonB("4321", "Jane", "personB"));
		}

	}
}
