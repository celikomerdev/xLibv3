#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Profiling;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueSprite : xValueEqual<Sprite>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueSprite global = new ValueSprite();
			global.Globalize();
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(Sprite valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew == Value);
		}
		#endregion
		
		#region Override
		public override string ValueToString
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
				if(Value.IsNull()) return null;
				if(Value.Equals(ValueDefault)) return null;
				
				Profiler.BeginSample($"{nodeSetting.UnityObject.name}:ValueSprite:Get",nodeSetting.UnityObject);
				Texture2D texture2D = Value.texture;
				string stringData = System.Convert.ToBase64String(texture2D.xEncodeToPNG());
				JToken jToken = JToken.FromObject(stringData);
				Profiler.EndSample();
				
				if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:DataLenghtGet:{stringData.Length}",nodeSetting.UnityObject);
				return jToken;
			}
			set
			{
				if(value.IsNull()) return;
				string stringJson = value.ToString();
				if(string.IsNullOrWhiteSpace(stringJson)) return;
				
				Profiler.BeginSample($"{nodeSetting.UnityObject.name}:ValueSprite:Set",nodeSetting.UnityObject);
				string stringData = JToken.FromObject(stringJson).ToObject<string>();
				Texture2D texture2D = new Texture2D(2,2);
				texture2D.Load(System.Convert.FromBase64String(stringData));
				Profiler.EndSample();
				
				if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:DataLenghtSet:{stringData.Length}",nodeSetting.UnityObject);
				Value = texture2D.ToSprite();
			}
		}
		#endregion
	}
}
#endif