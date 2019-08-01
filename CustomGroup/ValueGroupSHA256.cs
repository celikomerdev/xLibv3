#if xLibv3
using Newtonsoft.Json.Linq;

namespace xLib.xValueClass
{
	[System.Serializable]
	public class ValueGroupSHA256 : ValueGroup
	{
		#region ISerializableBase
		internal override object SerializedObject
		{
			get
			{
				JObject jObject = new JObject();
				JToken Content = (JToken)SerializedObjectRaw;
				string ContentString = Content.ToString().Replace(System.Environment.NewLine,"");
				
				jObject.Add("WARNING!!","Your data will be deleted if you edit this file!!!!");
				jObject.Add("Hash",(KeyEncrypt+ContentString).HashSHA256UTF8());
				jObject.Add("Content",Content);
				
				return jObject;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrEmpty(stringJson)) return;
				JObject jObject = JObject.Parse(stringJson);
				
				string Hash = jObject.GetValue("Hash").ToString();
				string Content = jObject.GetValue("Content").ToString();
				
				if(IsValid(Hash,Content)) SerializedObjectRaw = Content;
			}
		}
		#endregion
		
		
		#region CheckValid
		private bool IsValid(string hash,string content)
		{
			string contentRaw = content.Replace(System.Environment.NewLine,"");
			if(hash == (KeyEncrypt+contentRaw).HashSHA256UTF8()) return true;
			
			xDebug.LogExceptionFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":HackDetected!!!");
			return false;
		}
		#endregion
	}
}
#endif