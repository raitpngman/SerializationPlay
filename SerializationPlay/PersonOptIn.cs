using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

//Because this class is using the OptIn class converter, you must put the [JsonInclude]
//attribute on any property that you want included.
//It doesn't matter if it is private, internal, or public
namespace SerializationPlay
{
	//Tell it which class converter to use for this class
	[JsonConverter(typeof(OptInClassConverter<PersonOptIn>))]
	internal class PersonOptIn
	{
		public PersonOptIn()
		{
		}

		public PersonOptIn(string personName, string address, string sSN, 
			string otherId, string[] children)
		{
			PersonName=personName;
			Address=address;
			SSN=sSN;
			OtherId=otherId;
			Children=children;
		}

		//PersonName will not be included
		public string PersonName { get; set; }
		//Address will be included
		[JsonInclude]
		public string Address { get; private set; }
		//SSN will not be included
		internal string SSN { get; set; }
		//OtherId will be included
		[JsonInclude]
		private string OtherId { get; set; }
		//Children will be included
		[JsonInclude]
		private string[] Children { get; set; }





	}
}
