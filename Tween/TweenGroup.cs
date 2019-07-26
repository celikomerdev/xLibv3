#if xLibv2
using System.Collections.Generic;
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenGroup : Tween
	{
		public GameObject[] target;
		private List<Tween> tweens = new List<Tween>();
		
		public override void Awake()
		{
			tweens = new List<Tween>();
			for (int i = 0; i < target.Length; i++)
			{
				tweens.AddRange(target[i].GetComponents<Tween>());
			}
			tweens.Remove(this);
			tweens.RemoveAll(item => item.CanWork == false);
			
			for (int i = 0; i < tweens.Count; i++)
			{
				tweens[i].Awake();
			}
		}
		
		override protected void SetRatio(float value)
		{
			for(int i=0; i < tweens.Count; i++)
			{
				tweens[i].SetBaseRatio(value);
			}
		}
	}
}
#endif