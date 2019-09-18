#if xLibv3
using System;
using System.Collections.Generic;
using xLib.xValueClass;

namespace xLib.xDictionary
{
	[Serializable]
	public class DictFloat
	{
		public ValueFloat[] array;
		public Dictionary<string,ValueFloat> dictionary = new Dictionary<string,ValueFloat>();
		
		private bool isInit;
		public void Init()
		{
			if(isInit) return;
			isInit = true;
			
			if(array == null) return;
			for(int i = 0; i < array.Length; i++)
			{
				dictionary[array[i].Key] = array[i];
			}
		}
		
		public float GetValue(string key,float defaultValue = 0)
		{
			if (!dictionary.ContainsKey(key)) return defaultValue;
			return dictionary[key].Value;
		}
	}
}
#endif