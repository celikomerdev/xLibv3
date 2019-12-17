#if xLibv3
using System;
using System.Collections.Generic;
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xDictionary
{
	[Serializable]
	public class DictTexture
	{
		public ValueTexture[] array;
		public Dictionary<string,ValueTexture> dictionary = new Dictionary<string,ValueTexture>();
		
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
		
		public Texture GetValue(string key,Texture defaultValue)
		{
			if (!dictionary.ContainsKey(key)) return defaultValue;
			return dictionary[key].Value;
		}
	}
}
#endif