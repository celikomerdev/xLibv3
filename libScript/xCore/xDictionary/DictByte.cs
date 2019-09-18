#if xLibv3
using System;
using System.Collections.Generic;
using xLib.xValueClass;

namespace xLib.xDictionary
{
	[Serializable]
	public class DictByte
	{
		public ValueByte[] array;
		public Dictionary<string,ValueByte> dictionary = new Dictionary<string,ValueByte>();
		
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
		
		public byte GetValue(string key,byte defaultValue = 0)
		{
			if (!dictionary.ContainsKey(key)) return defaultValue;
			return dictionary[key].Value;
		}
	}
}
#endif