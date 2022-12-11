using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SerializationPlay
{
	//Tell it which class converter to use for this class
	[JsonConverter(typeof(OptInPrivacyClassConverter<PersonOptInPrivacy>))]
	internal class PersonOptInPrivacy
	{
		public PersonOptInPrivacy()
		{
		}

		public PersonOptInPrivacy(string personName, string address, string sSN, 
			string otherId, string[] children)
		{
			PersonName=personName;
			Address=address;
			SSN=sSN;
			OtherId=otherId;
			Children=children;
		}

		public string PersonName { get; set; }
		[JsonInclude]
		public string Address { get; private set; }
		[JsonInclude][PrivacyAttribute]
		internal string SSN { get; set; }
		[JsonInclude]
		private string OtherId { get; set; }
		
		private string[] Children { get; set; }





	}
}
