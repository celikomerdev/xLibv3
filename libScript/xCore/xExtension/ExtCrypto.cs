#if xLibv2
using Newtonsoft.Json.Linq;

namespace xLib
{
	public static class ExtCrypto
	{
		public static string EncryptSHA256UTF8(this string value,string[] password)
		{
			JObject jObject = new JObject();
			
			jObject.Add("WARNING!!","Your data will be deleted if you edit this file!!!!");
			jObject.Add("Hash",(password[0]+value).HashSHA256UTF8());
			jObject.Add("Content",value);
			
			return jObject.ToString(Newtonsoft.Json.Formatting.Indented);
		}
		
		public static string DecryptSHA256UTF8(this string value,string[] password)
		{
			if(string.IsNullOrWhiteSpace(value)) return "";
			JObject jObject = JObject.Parse(value);
			
			string Hash = jObject.GetValue("Hash").ToString();
			string Content = jObject.GetValue("Content").ToString();
			
			for (int i = 0; i < password.Length; i++)
			{
				if(Hash == (password[i]+Content).HashSHA256UTF8()) return Content;
			}
			
			xDebug.LogExceptionFormat("DecryptSHA256UTF8:HackDetected!!!");
			return "";
		}
	}
}
#endif