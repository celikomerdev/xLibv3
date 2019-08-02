#if xLibv3
using System.Collections.Generic;
using UnityEngine;

namespace xLib
{
	public static class ExtGetGeneric
	{
		public static V GetGeneric<V>(this object target)
		{
			V genericClass;
			if(target.GetType() == typeof(GameObject))
			{
				genericClass = (((GameObject)target).GetComponent<V>());
			}
			else
			{
				genericClass = (V)target;
			}
			return genericClass;
		}
		
		public static V[] GetGenerics<V>(this object target)
		{
			List<V> genericClass = new List<V>();
			if(target==null) return genericClass.ToArray();
			
			if(target.GetType() == typeof(GameObject))
			{
				genericClass.AddRange(((GameObject)target).GetComponents<V>());
			}
			else
			{
				V tempGeneric = (V)target;
				genericClass.Add(tempGeneric);
			}
			return genericClass.ToArray();
		}
		
		public static V[] GetGenericsArray<V>(this object[] target)
		{
			List<V> genericClass = new List<V>();
			for (int i = 0; i < target.Length; i++)
			{
				if(target[i] == null) continue;
				genericClass.AddRange(target[i].GetGenerics<V>());
			}
			return genericClass.ToArray();
		}
		
		public static V[] GetGenericsInChildren<V>(this object target,bool includeInactive=false)
		{
			List<V> genericClass = new List<V>();
			if(target.GetType() == typeof(GameObject))
			{
				genericClass.AddRange(((GameObject)target).GetComponentsInChildren<V>(includeInactive));
			}
			else
			{
				V tempGeneric = (V)target;
				genericClass.Add(tempGeneric);
			}
			return genericClass.ToArray();
		}
		
		public static V[] GetGenericsInChildrenArray<V>(this object[] target,bool includeInactive=false)
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