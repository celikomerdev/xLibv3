#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueSprite : xValueEqual<Sprite>
	{
		#region Compare
		protected override bool IsEqual(Sprite value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
		}
		#endregion
		
		#region Override
		protected override string ValueToString
		{
			get
			{
				return Value.texture.xHashCode().ToString();
			}
		}
		#endregion
		
		#region ISerializableBase
		public override object SerializedObject
		{
			get
			{
				// string stringBase64 = "";
				// if(Value != null) stringBase64 = System.Convert.ToBase64String(((Texture2D)Value).EncodeToPNG());
				
				JToken jToken = null;
				//jToken = JToken.FromObject(stringBase64);
				return jToken;
			}
			set
			{
				// if(value==null) return;
				// string stringJson = value.ToString();
				// if(string.IsNullOrEmpty(stringJson)) return;
				// string stringBase64 = JToken.FromObject(stringJson).ToObject<string>();
				
				// Texture2D returnTexture2d = new Texture2D(2,2);
				// returnTexture2d.LoadImage(System.Convert.FromBase64String(stringBase64));
				// returnTexture2d.Apply();
				
				// Value = (Texture)returnTexture2d;
			}
		}
		#endregion
	}
}
#endif