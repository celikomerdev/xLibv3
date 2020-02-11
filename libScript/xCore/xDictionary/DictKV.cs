#if xLibv3
using System;
using System.Collections.Generic;

namespace xLib.xDictionary
{
	[Serializable]public class DictKV<K,V,T> where T:ObjectKV<K,V>
	{
		public V defaultValue = default;
		public T[] array;
		public Dictionary<K,V> dictionary = new Dictionary<K,V>();
		
		private bool isInit = false;
		public void Init()
		{
			if(isInit) return;
			isInit = true;
			
			for(int i = 0; i < array.Length; i++)
			{
				dictionary[array[i].key] = array[i].value;
			}
		}
		
		public V GetValue(K key,V defaultCustom = default)
		{
			if(key==null) return defaultValue;
			if(dictionary.ContainsKey(key)) return dictionary[key];
			
			// if(!defaultCustom.Equals(default(V))) return defaultCustom; //error
			return defaultValue;
		}
		
		public V GetRandom()
		{
			return array[UnityEngine.Random.Range(0,array.Length)].value;
		}
	}
	
	[Serializable]public class ObjectKV<K,V>
	{
		public K key;
		public V value;
	}
}
#endif