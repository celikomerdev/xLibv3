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
				return JToken.FromObject("");
				Profiler.BeginSample($"{nodeSetting.UnityObject.name}:ValueSprite:Get",nodeSetting.UnityObject);
				string stringData = "";
				if(Value!=null)
				{
					Texture2D texture2D = Value.texture;
					stringData = System.Convert.ToBase64String(texture2D.xEncodeToPNG());
				}
				if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:DataLenghtGet:{stringData.Length}",nodeSetting.UnityObject);
				
				JToken jToken = JToken.FromObject(stringData);
				Profiler.EndSample();
				return jToken;
			}
			set
			{
				return;
				if(value==null) return;
				string stringJson = value.ToString();
				if(string.IsNullOrWhiteSpace(stringJson)) return;
				
				Profiler.BeginSample($"{nodeSetting.UnityObject.name}:ValueSprite:Set",nodeSetting.UnityObject);
				string stringData = JToken.FromObject(stringJson).ToObject<string>();
				if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:DataLenghtSet:{stringData.Length}",nodeSetting.UnityObject);
				
				Texture2D texture2D = new Texture2D(2,2);
				texture2D.Load(System.Convert.FromBase64String(stringData));
				
				Value = texture2D.ToSprite();
				Profiler.EndSample();
			}
		}
		#endregion
	}
}
#endif