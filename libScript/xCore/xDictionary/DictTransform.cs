#if xLibv3
using System;
using System.Collections.Generic;
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xDictionary
{
	[Serializable]
	public class DictTransform
	{
		public ValueTransform[] array;
		public Dictionary<string,ValueTransform> dictionary = new Dictionary<string,ValueTransform>();
		
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
		
		public Transform GetValue(string key,Transform defaultValue)
		{
			if (!dictionary.ContainsKey(key)) return defaultValue;
			return dictionary[key].Value;
		}
	}
}
#endif