#if xLibv3
using System.Collections.Generic;
using UnityEngine;

namespace xLib
{
	public static class ExtGetGeneric
	{
		public static V GetGeneric<V>(this Object target)
		{
			V genericClass;
			switch(target)
			{
				case GameObject gameObject:
					genericClass = gameObject.GetComponent<V>();
					break;
				case Transform transform:
					genericClass = transform.GetComponent<V>();
					break;
				default:
					genericClass = (V)(object)target;
					break;
			}
			return genericClass;
		}
		
		public static V[] GetGenerics<V>(this Object target)
		{
			List<V> genericClass = new List<V>();
			if(target==null) return genericClass.ToArray();
			
			switch(target)
			{
				case GameObject gameObject:
					genericClass.AddRange(gameObject.GetComponents<V>());
					break;
				case Transform transform:
					genericClass.AddRange(transform.GetComponents<V>());
					break;
				default:
				{
					V tempGeneric = (V)(object)target;
					genericClass.Add(tempGeneric);
					break;
				}
			}
			return genericClass.ToArray();
		}
		
		public static V[] GetGenericsArray<V>(this Object[] target)
		{
			List<V> genericClass = new List<V>();
			for (int i = 0; i < target.Length; i++)
			{
				if(target[i] == null) continue;
				genericClass.AddRange(target[i].GetGenerics<V>());
			}
			return genericClass.ToArray();
		}
		
		public static V[] GetGenericsInChildren<V>(this Object target,bool includeInactive=false)
		{
			List<V> genericClass = new List<V>();
			switch(target)
			{
				case GameObject gameObject:
					genericClass.AddRange(gameObject.GetComponentsInChildren<V>(includeInactive));
					break;
				case Transform transform:
					genericClass.AddRange(transform.GetComponentsInChildren<V>(includeInactive));
					break;
				default:
				{
					V tempGeneric = (V)(object)target;
					genericClass.Add(tempGeneric);
					break;
				}
			}
			return genericClass.ToArray();
		}
		
		public static V[] GetGenericsInChildrenArray<V>(this Object[] target,bool includeInactive=false)
		{
			List<V> genericClass = new List<V>();
			for (int i = 0; i < target.Length; i++)
			{
				genericClass.AddRange(target[i].GetGenericsInChildren<V>(includeInactive));
			}
			return genericClass.ToArray();
		}
	}
}
#endif