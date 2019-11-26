#if xLibv3
using UnityEngine;

namespace xLib.ToolGameObject
{
	public class Spawner : BaseActiveM
	{
		[SerializeField]private Transform target;
		[SerializeField]private GameObject[] prefab;
		[SerializeField]private bool spawnMulti;
		[SerializeField]private bool usePool;
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
			Spawn(prefab[Random.Range(0,prefab.Length)]);
		}
		
		
		[ContextMenu("Destroy")]
		public void Destroy()
		{
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
	}
}
#endif