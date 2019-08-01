#if xLibv3
using Newtonsoft.Json.Linq;
using xLib.ToolCrypto;

namespace xLib.xValueClass
{
	[System.Serializable]
	public class ValueGroupCipher : ValueGroupCrypto
	{
		#region ISerializableBase
		internal override object SerializedObject
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
				
				if(stringJObject != "") xDebug.LogExceptionFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":Test!!!");
				else if(string.IsNullOrEmpty(stringJObject)) stringJObject = StringCipher.Decrypt(Content,KeyEncrypt);
				
				if(string.IsNullOrEmpty(stringJObject))
				{
					xDebug.LogExceptionFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":HackDetected!!!");
					return;
				}
				
				SerializedObjectRaw = stringJObject;
			}
		}
		#endregion
	}
}
#endif