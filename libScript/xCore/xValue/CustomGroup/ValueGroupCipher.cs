#if xLibv3
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Profiling;

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
				Profiler.BeginSample($"{nodeSetting.UnityObject.name}:ValueGroupCipher:Get",nodeSetting.UnityObject);
				JObject jObject = new JObject();
				JToken Content = (JToken)SerializedObjectRaw;
				
				jObject.Add("WARNING!!","Your data will be deleted if you edit this file!!!!");
				jObject.Add("Hash","");
				jObject.Add("Content",ToolCrypto.StringCipher.Encrypt(Content.ToJsonString(),KeyEncrypt));
				
				Profiler.EndSample();
				return jObject;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrWhiteSpace(stringJson)) return;
				
				Profiler.BeginSample($"{nodeSetting.UnityObject.name}:ValueGroupCipher:Set",nodeSetting.UnityObject);
				JObject jObject = JObject.Parse(stringJson);
				
				string Hash = (string)jObject.GetValue("Hash");
				string Content = (string)jObject.GetValue("Content");
				
				SerializedObjectRaw = Decrypt(Content);
				Profiler.EndSample();
			}
		}
		#endregion
		
		
		#region Validate
		private string Decrypt(string content)
		{
			for (int i = 0; i < cryptoVersion.Length; i++)
			{
				string stringJObject = ToolCrypto.StringCipher.Decrypt(content,KeyEncryptVersion(i));
				if(!string.IsNullOrEmpty(stringJObject)) return stringJObject;
			}
			Debug.LogException(new UnityException($"{nodeSetting.UnityObject.name}:HackDetected!!!"),nodeSetting.UnityObject);
			return "";
		}
		#endregion
	}
}
#endif