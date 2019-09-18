#if xLibv3
using System;
using System.Collections.Generic;

namespace xLib.xDictionary
{
	[Serializable]public class DictKV<K,V,T> where T:ObjectKV<K,V>
	{
		public T[] array;
		public Dictionary<K,V> dictionary = new Dictionary<K,V>();
		
		private bool isInit;
		public void Init()
		{
			if(isInit) return;
			isInit = true;
			
			for(int i = 0; i < array.Length; i++)
			{
				dictionary[array[i].key] = array[i].value;
			}
		}
		
		public V GetValue(K key,V defaultValue = default)
		{
			if (!dictionary.ContainsKey(key)) return defaultValue;
			return dictionary[key];
		}
	}
	
	[Serializable]public class ObjectKV<K,V>
	{
		public K key;
		public V value;
	}
}
#endif