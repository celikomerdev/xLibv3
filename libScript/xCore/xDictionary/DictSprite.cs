#if xLibv3
using System;
using System.Collections.Generic;
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xDictionary
{
	[Serializable]
	public class DictSprite
	{
		public ValueSprite[] array;
		public Dictionary<string,ValueSprite> dictionary = new Dictionary<string,ValueSprite>();
		
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
		
		public Sprite GetValue(string key,Sprite defaultValue)
		{
			if (!dictionary.ContainsKey(key)) return defaultValue;
			return dictionary[key].Value;
		}
	}
}
#endif