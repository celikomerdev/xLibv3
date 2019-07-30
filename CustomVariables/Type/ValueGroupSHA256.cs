#if xLibv2
using Newtonsoft.Json.Linq;
using UnityEngine;

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
				jObject.Add("Hash",(KeyEncryptv2Key+ContentString).HashSHA256UTF8());
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
			string contentv2 = content.Replace(System.Environment.NewLine,"");
			if(hash == (KeyEncryptv2Key+contentv2).HashSHA256UTF8())	return true;
			
			string contentv1 = content;
			if(hash == (KeyEncryptv2Key+contentv1).HashSHA256UTF8())	return true;
			if(hash == (KeyEncryptv2Name+contentv1).HashSHA256UTF8())	return true;
			if(hash == (KeyEncryptv1Key+contentv1).HashSHA256UTF8())	return true;
			if(hash == (KeyEncryptv1Name+contentv1).HashSHA256UTF8())	return true;
			if(hash == (contentv1+KeyEncryptv1Key).HashSHA256UTF8())	return true;
			if(hash == (contentv1+KeyEncryptv1Name).HashSHA256UTF8())	return true;
			if(hash == (contentv1+keyEncrypt).HashSHA256UTF8())			return true;
			
			
			string contentv0 = content.Replace(System.Environment.NewLine,"\n");
			if(hash == (KeyEncryptv2Key+contentv0).HashSHA256UTF8())	return true;
			if(hash == (KeyEncryptv2Name+contentv0).HashSHA256UTF8())	return true;
			if(hash == (KeyEncryptv1Key+contentv0).HashSHA256UTF8())	return true;
			if(hash == (KeyEncryptv1Name+contentv0).HashSHA256UTF8())	return true;
			if(hash == (contentv0+KeyEncryptv1Key).HashSHA256UTF8())	return true;
			if(hash == (contentv0+KeyEncryptv1Name).HashSHA256UTF8())	return true;
			if(hash == (contentv0+keyEncrypt).HashSHA256UTF8())			return true;
			
			xDebug.LogExceptionFormat(nodeSetting.objDebug,nodeSetting.objDebug.name+":HackDetected!!!");
			return false;
		}
		#endregion
	}
}
#endif