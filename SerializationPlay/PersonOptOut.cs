
using System.Text.Json.Serialization;


namespace SerializationPlay
{
	//Tell it which class converter to use for this class
	[JsonConverter(typeof(OptOutClassConverter<PersonOptOut>))]
	internal class PersonOptOut
	{
		public PersonOptOut()
		{
		}

		public PersonOptOut(string personName, string address, string sSN,
			string otherId, string[] children)
		{
			PersonName=personName;
			Address=address;
			SSN=sSN;
			OtherId=otherId;
			Children=children;
		}
		[JsonIgnore]
		public string PersonName { get; set; }
		
		public string Address { get; private set; }
		internal string SSN { get; set; }
		[JsonIgnore]
		private string OtherId { get; set; }
	
		private string[] Children { get; set; }



	}
}
