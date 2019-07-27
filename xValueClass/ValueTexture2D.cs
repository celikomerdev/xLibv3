#if xLibv2
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueTexture2D : xValueEqual<Texture2D>
	{
		#region Compare
		protected override bool IsEqual(Texture2D value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
		}
		
		protected override void KeepProperties(Texture2D value)
		{
			if(value==null) return;
			if(ValueDefault!=null)
			{
				value.filterMode = ValueDefault.filterMode;
				value.anisoLevel = ValueDefault.anisoLevel;
			}
			value.Compress(true);
			value.Apply();
		}
		#endregion
		
		#region Override
		protected override string ValueToString
		{
			get
			{
				return Value.xHashCode().ToString();
			}
		}
		#endregion
		
		#region ISerializableBase
		internal override object SerializedObject
		{
			get
			{
				string stringData = "";
				if(Value!=null)
				{
					Texture2D texture2D = Value;
					stringData = System.Convert.ToBase64String(texture2D.xEncodeToPNG());
				}
				//if(CanDebug) Debug.Log(this.name+": DataLenghtGet: "+stringData.Length,this);
				
				JToken jToken;
				jToken = JToken.FromObject(stringData);
				return jToken;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrEmpty(stringJson)) return;
				string stringData = JToken.FromObject(stringJson).ToObject<string>();
				//if(CanDebug) Debug.Log(this.name+": DataLenghtSet: "+stringData.Length,this);
				
				Texture2D texture2D = new Texture2D(2,2);
				texture2D.LoadImage(System.Convert.FromBase64String(stringData));
				texture2D.Compress(true);
				texture2D.Apply(true);
				
				Value = texture2D;
			}
		}
		#endregion
	}
}
#endif