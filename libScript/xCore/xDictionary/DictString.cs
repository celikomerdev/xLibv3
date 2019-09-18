#if xLibv3
using System;
using System.Collections.Generic;
using xLib.xValueClass;

namespace xLib.xDictionary
{
	[Serializable]
	public class DictString
	{
		public ValueString[] array;
		public Dictionary<string,ValueString> dictionary = new Dictionary<string,ValueString>();
		
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
		
		public string GetValue(string key,string defaultValue)
		{
			if (!dictionary.ContainsKey(key)) return defaultValue;
			return dictionary[key].Value;
		}
	}
}
#endif