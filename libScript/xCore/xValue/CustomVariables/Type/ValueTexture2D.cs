#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueTexture2D : xValueEqual<Texture2D>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueTexture2D global = new ValueTexture2D();
			global.Globalize();
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(Texture2D valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew == Value);
		}
		
		protected override void KeepProperties(Texture2D valueNew)
		{
			if(valueNew == null) return;
			if(ValueDefault != null)
			{
				valueNew.filterMode = ValueDefault.filterMode;
				valueNew.anisoLevel = ValueDefault.anisoLevel;
				valueNew.mipMapBias = ValueDefault.mipMapBias;
				valueNew.wrapMode = ValueDefault.wrapMode;
			}
			valueNew.Compress(true);
			valueNew.Apply();
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
					Texture2D texture2D = Value;
					stringData = System.Convert.ToBase64String(texture2D.xEncodeToPNG());
				}
				if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:DataLenghtGet:{stringData.Length}",nodeSetting.UnityObject);

				JToken jToken = JToken.FromObject(stringData);
				return jToken;
			}
			set
			{
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrWhiteSpace(stringJson)) return;
				string stringData = JToken.FromObject(stringJson).ToObject<string>();
				if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:DataLenghtSet:{stringData.Length}",nodeSetting.UnityObject);
				
				Texture2D texture2D = new Texture2D(2,2);
				texture2D.Load(System.Convert.FromBase64String(stringData));
				
				Value = texture2D;
			}
		}
		#endregion
	}
}
#endif