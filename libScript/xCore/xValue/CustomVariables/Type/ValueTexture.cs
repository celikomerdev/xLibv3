#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueTexture : xValueEqual<Texture>
	{
		#region Compare
		protected override bool IsEqual(Texture valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew == Value);
		}
		
		protected override void KeepProperties(Texture valueNew)
		{
			if(valueNew == null) return;
			if(ValueDefault != null)
			{
				valueNew.filterMode = ValueDefault.filterMode;
				valueNew.anisoLevel = ValueDefault.anisoLevel;
				valueNew.mipMapBias = ValueDefault.mipMapBias;
				valueNew.wrapMode = ValueDefault.wrapMode;
			}
			((Texture2D)valueNew).Compress(true);
			((Texture2D)valueNew).Apply();
		}
		#endregion
		
		#region Override
		public override string ValueToString
		{
			get
			{
				return Value.xHashCode().ToString();
			}
		}
		#endregion
		
		#region ISerializableBase
		public override object SerializedObject
		{
			get
			{
				string stringData = "";
				if(Value!=null)
				{
					Texture2D texture2D = (Texture2D)Value;
					stringData = System.Convert.ToBase64String(texture2D.xEncodeToPNG());
				}
				if(nodeSetting.canDebug) Debug.Log($"{nodeSetting.objDebug.name}:DataLenghtGet:{stringData.Length}",nodeSetting.objDebug);
				
				JToken jToken;
				jToken = JToken.FromObject(stringData);
				return jToken;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrWhiteSpace(stringJson)) return;
				string stringData = JToken.FromObject(stringJson).ToObject<string>();
				if(nodeSetting.canDebug) Debug.Log($"{nodeSetting.objDebug.name}:DataLenghtSet:{stringData.Length}",nodeSetting.objDebug);
				
				Texture2D texture2D = new Texture2D(2,2);
				texture2D.Load(System.Convert.FromBase64String(stringData));
				
				Value = (Texture)texture2D;
			}
		}
		#endregion
	}
}
#endif