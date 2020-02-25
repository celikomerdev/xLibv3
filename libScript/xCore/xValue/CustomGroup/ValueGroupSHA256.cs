#if xLibv3
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Profiling;

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
				Profiler.BeginSample($"{nodeSetting.UnityObject.name}:ValueGroupSHA256:Get",nodeSetting.UnityObject);
				JObject jObject = new JObject();
				JToken Content = (JToken)SerializedObjectRaw;
				
				jObject.Add("WARNING!!","Your data will be deleted if you edit this file!!!!");
				jObject.Add("Hash",(KeyEncrypt+Content.ToJsonString()).HashSHA256UTF8());
				jObject.Add("Content",Content);
				
				Profiler.EndSample();
				return jObject;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrWhiteSpace(stringJson)) return;
				
				Profiler.BeginSample($"{nodeSetting.UnityObject.name}:ValueGroupSHA256:Set",nodeSetting.UnityObject);
				JObject jObject = JObject.Parse(stringJson);
				
				string Hash = (string)jObject.GetValue("Hash");
				string Content = jObject.GetValue("Content").ToJsonString();
				
				if(IsValid(Hash,Content)) SerializedObjectRaw = Content;
				Profiler.EndSample();
			}
		}
		#endregion
		
		
		#region Validate
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