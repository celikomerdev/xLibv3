#if xLibv3
using Newtonsoft.Json.Linq;
using UnityEngine;
using xLib.ToolCrypto;

namespace xLib.xValueClass
{
	[System.Serializable]
	public class ValueGroupCipher : ValueGroupCrypto
	{
		#region ISerializableBase
		public override object SerializedObject
		{
			get
			{
				JObject jObject = new JObject();
				
				string Content = StringCipher.Encrypt(SerializedObjectRaw.ToString(),KeyEncrypt);
				jObject.Add("Content",Content);
				
				return jObject;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrEmpty(stringJson)) return;
				JObject jObject = JObject.Parse(stringJson);
				
				string Content = jObject.GetValue("Content").ToString();
				string stringJObject = "";
				
				if(stringJObject != "") Debug.LogException(new UnityException($"{nodeSetting.UnityObject.name}:Test!!!"),nodeSetting.UnityObject);
				
				SerializedObjectRaw = Decrypt(Content);
			}
		}
		#endregion
		
		
		#region CheckValid
		private string Decrypt(string content)
		{
			for (int i=cryptoVersion.Length-1; i>=0; i--)
			{
				string stringJObject = StringCipher.Decrypt(content,KeyEncryptVersion(i));
				if(!string.IsNullOrEmpty(stringJObject)) return stringJObject;
			}
			
			Debug.LogException(new UnityException($"{nodeSetting.UnityObject.name}:HackDetected!!!"),nodeSetting.UnityObject);
			return "";
		}
		#endregion
	}
}
#endif