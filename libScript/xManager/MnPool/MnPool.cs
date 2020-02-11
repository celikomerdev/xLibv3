#if xLibv3
using System.Collections.Generic;
using UnityEngine;
using xLib.ToolPool;

namespace xLib
{
	public class MnPool : SingletonM<MnPool>
	{
		[SerializeField]private Transform container = null;
		
		#region Collection
		private readonly Dictionary<GameObject,Stack<GameObject>> dictionary = new Dictionary<GameObject,Stack<GameObject>>();
		private Stack<GameObject> GetStack(GameObject key)
		{
			if(dictionary.ContainsKey(key)) return dictionary[key];
			Stack<GameObject> stack = new Stack<GameObject>();
			dictionary[key] = stack;
			return stack;
		}
		#endregion
		
		
		#region Main
		public static GameObject Spawn(GameObject original,bool usePool)
		{
			xLogger.Log($"MnPool:Spawn:original:{original.name}",original);
			GameObject clone = null;
			
			if(ins && ins.GetStack(original).Count>0) clone = ins.GetStack(original).Pop();
			if(!clone)
			{
				xLogger.Log($"MnPool:Instantiate:{original.name}",original);
				bool originalState = original.activeSelf;
				original.SetActive(false);
				clone = Instantiate(original);
				original.SetActive(originalState);
			}
			
			if(usePool) clone.AddComponent<PoolKey>().original = original;
			xLogger.Log($"MnPool:Spawn:clone:{clone.name}",clone);
			return clone;
		}
		
		public static void Pool(GameObject obj)
		{
			xLogger.Log($"MnPool:Pool:{obj.name}",obj);
			
			if(obj == null) return;
			obj.SetActive(false);
			
			PoolKey poolKey = obj.GetComponent<PoolKey>();
			if(!poolKey || !ins)
			{
				Destroy(obj);
				return;
			}
			
			obj.transform.SetParent(ins.container);
			ins.GetStack(poolKey.original).Push(obj);
		}
		#endregion
	}
}
#endif