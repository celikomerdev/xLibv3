#if xLibv3
using System;
using System.Collections.Generic;
using xLib.xValueClass;

namespace xLib.xDictionary
{
	[Serializable]
	public class DictBool
	{
		public ValueBool[] array;
		public Dictionary<string,ValueBool> dictionary = new Dictionary<string,ValueBool>();
		
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
		
		public bool GetValue(string key,bool defaultValue = false)
		{
			if (!dictionary.ContainsKey(key)) return defaultValue;
			return dictionary[key].Value;
		}
	}
}
#endif