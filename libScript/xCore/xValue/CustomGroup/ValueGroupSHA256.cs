#if xLibv3
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[System.Serializable]
	public class ValueGroupSHA256 : ValueGroupCrypto
	{
		#region ISerializableBase
		public override object SerializedObject
		{
			get
			{
				JObject jObject = new JObject();
				JToken Content = (JToken)SerializedObjectRaw;
				
				jObject.Add("WARNING!!","Your data will be deleted if you edit this file!!!!");
				jObject.Add("Hash",(KeyEncrypt+Content).HashSHA256UTF8());
				jObject.Add("Content",Content);
				
				return jObject;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrWhiteSpace(stringJson)) return;
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
			for (int i = 0; i < cryptoVersion.Length; i++)
			{
				if(hash == (KeyEncryptVersion(i)+content).HashSHA256UTF8()) return true;
			}
			
			Debug.LogException(new UnityException($"{nodeSetting.UnityObject.name}:HackDetected!!!"),nodeSetting.UnityObject);
			return false;
		}
		#endregion
	}
}
#endif