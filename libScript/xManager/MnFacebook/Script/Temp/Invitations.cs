#if xLibv2
using System;
using Newtonsoft.Json;

namespace xLib.xFacebook
{
	[Serializable]
	public class Invitations
	{
		[JsonProperty("data")]
		public Data[] data;
		
		[JsonProperty("paging")]
		public Paging Paging;
	}
	
	[Serializable]
	public class Data
	{
		[JsonProperty("id")]
		public string id;
		
		[JsonProperty("from")]
		public Person from;
		
		[JsonProperty("to")]
		public Person to;
		
		[JsonProperty("message")]
		public string message;
		
		[JsonProperty("created_time")]
		public string created_time;
		
		//[JsonConverter(typeof(ParseStringConverter))]
		[JsonProperty("data")]
		public string data;
	}
	
	[Serializable]
	public partial class Person
	{
		[JsonProperty("name")]
		public string name;
		
		[JsonProperty("id")]
		public string id;
	}
	
	[Serializable]
	public partial class Paging
	{
		[JsonProperty("cursors")]
		public Cursors cursors;
	}
	
	[Serializable]
	public partial class Cursors
	{
		[JsonProperty("before")]
		public string before;
		
		[JsonProperty("after")]
		public string after;
	}
}
#endif