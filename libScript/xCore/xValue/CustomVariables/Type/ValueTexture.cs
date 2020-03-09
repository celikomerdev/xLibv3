#if xLibv3
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Profiling;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueTexture : xValueEqual<Texture>
	{
		#region Global
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void SetDefaultGlobal()
		{
			ValueTexture global = new ValueTexture();
			global.Globalize();
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(Texture valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew == Value);
		}
		
		protected override void KeepProperties(Texture valueNew)
		{
			if(valueNew.IsNull()) return;
			if(!ValueDefault.IsNull())
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
				if(Value.IsNull()) return null;
				if(Value.Equals(ValueDefault)) return null;
				
				Profiler.BeginSample($"{nodeSetting.UnityObject.name}:ValueTexture:Get",nodeSetting.UnityObject);
				Texture2D texture2D = (Texture2D)Value;
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
				
				Profiler.BeginSample($"{nodeSetting.UnityObject.name}:ValueTexture:Set",nodeSetting.UnityObject);
				string stringData = JToken.FromObject(stringJson).ToObject<string>();
				Texture2D texture2D = new Texture2D(2,2);
				texture2D.Load(System.Convert.FromBase64String(stringData));
				Profiler.EndSample();
				
				if(nodeSetting.CanDebug) Debug.Log($"{nodeSetting.UnityObject.name}:DataLenghtSet:{stringData.Length}",nodeSetting.UnityObject);
				Value = texture2D;
			}
		}
		#endregion
	}
}
#endif