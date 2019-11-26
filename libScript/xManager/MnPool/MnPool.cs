#if xLibv3
using System.Collections.Generic;
using UnityEngine;
using xLib.ToolPool;

namespace xLib
{
	public class MnPool : SingletonM<MnPool>
	{
		public Transform container;
		
		#region Collection
		private Dictionary<GameObject,Stack<GameObject>> dictionary = new Dictionary<GameObject,Stack<GameObject>>();
		private Stack<GameObject> GetStack(GameObject key)
		{
			Stack<GameObject> stack = null;
			if(dictionary.TryGetValue(key,out stack)) return stack;
			else
			{
				stack = new Stack<GameObject>();
				dictionary.Add(key,stack);
				return stack;
			}
		}
		#endregion
		
		
		#region Main
		public static GameObject Spawn(GameObject original,bool usePool)
		{
			GameObject temp = null;
			
			if(!temp && ins) temp = ins.GetStack(original).Pop();
			if(!temp)
			{
				xDebug.LogTempFormat(original,original.name+":Instantiate:{0}",original.name);
				bool originalState = original.activeSelf;
				original.SetActive(false);
				temp = Instantiate(original);
				original.SetActive(originalState);
			}
			
			temp.SetActive(false);
			if(usePool) temp.AddComponent<PoolKey>().original = original;
			xDebug.LogTempFormat(temp,original.name+":Get:{0}",temp.name);
			return temp;
		}
		
		public static void Pool(GameObject obj)
		{
			if(obj == null) return;
			obj.SetActive(false);
			
			PoolKey poolKey = obj.GetComponent<PoolKey>();
			if(!poolKey || !ins)
			{
				Destroy(obj);
				return;
			}
			
			xDebug.LogTempFormat(obj,obj.name+":Add:{0}",obj.name);
			obj.transform.SetParent(ins.container);
			ins.GetStack(poolKey.original).Push(obj);
		}
		#endregion
	}
}
#endif