﻿#if xLibv3
using UnityEngine;

namespace xLib.ToolGameObject
{
	public class Spawner : BaseActiveM
	{
		[SerializeField]private Transform target = null;
		[SerializeField]private GameObject prefab = null;
		[SerializeField]private bool spawnMulti = false;
		[SerializeField]private bool usePool = false;
		[SerializeField]private bool resetScale = true;
		[SerializeField]private bool destroy = true;
		[SerializeField]private bool destroyImmediate = false;
		
		
		private GameObject temp;
		protected override void OnActive(bool value)
		{
			if(!isMy && !spawnMulti) return;
			if(value)
			{
				Spawn();
			}
			else
			{
				Destroy();
			}
		}
		
		
		public void Spawn(GameObject original)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Spawn:{0}",original);
			if(destroy) Destroy();
			if(temp) return;
			if(!original) return;
			
			temp = MnPool.Spawn(original:original,usePool:usePool);
			temp.transform.SetParent(target);
			temp.transform.ResetTransform(resetScale);
			temp.SetActive(true);
		}
		
		
		[ContextMenu("Spawn")]
		public void Spawn()
		{
			Spawn(prefab);
		}
		
		
		[ContextMenu("Destroy")]
		public void Destroy()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Destroy:{0}",temp);
			if(!temp) return;
			
			if(destroyImmediate || !Application.isPlaying)
			{
				DestroyImmediate(temp);
			}
			else
			{
				MnPool.Pool(temp);
			}
			
			temp = null;
		}
		
		
		public GameObject Prefab
		{
			get
			{
				return prefab;
			}
			set
			{
				if(prefab==value) return;
				if(CanDebug) Debug.LogFormat(this,this.name+":Prefab:{0}:{1}",prefab,value);
				prefab = value;
				Spawn();
			}
		}
	}
}
#endif