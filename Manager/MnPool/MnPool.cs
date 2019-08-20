#if xLibv3
using System.Collections.Generic;
using UnityEngine;
using xLib.ToolPool;

namespace xLib
{
	public class MnPool : SingletonM<MnPool>
	{
		public Transform container;
		
		#region Main
		public GameObject Get(GameObject value)
		{
			GameObject key = value;
			
			if(GetStack(key).Count == 0)
			{
				if(CanDebug) Debug.LogWarningFormat(value,this.name+":Instantiate:{0}",value.name);
				GameObject spawn = (GameObject)Instantiate(value,container.position,Quaternion.identity,container);
				spawn.AddComponent<PoolKey>().key = key;
				spawn.name = value.name;
				Add(spawn);
			}
			
			GameObject temp = GetStack(key).Pop();
			temp.SetActive(false);
			temp.transform.parent = null;
			if(CanDebug) Debug.LogFormat(temp,this.name+":Get:{0}",temp.name);
			return temp;
		}
		
		public void Add(GameObject value)
		{
			if(value==null) return;
			
			PoolKey poolKey = value.GetComponent<PoolKey>();
			if(!poolKey)
			{
				Destroy(value);
				return;
			}
			
			if(CanDebug) Debug.LogFormat(value,this.name+":Add:{0}",value.name);
			value.transform.SetParent(container);
			GetStack(poolKey.key).Push(value);
		}
		#endregion
		
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
	}
}
#endif