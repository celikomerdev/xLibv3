#if xLibv3
using System;
using System.Collections.Generic;
using UnityEngine;

namespace xLib.xTween
{
	public class TweenGroup : Tween
	{
		[SerializeField]private GameObject[] target = new GameObject[0];
		private List<Tween> tweens = new List<Tween>();
		
		public override void Awake()
		{
			Setup();
		}
		
		private void Setup()
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
		
		[NonSerialized]public float currentRatio = 0f;
		override protected void SetRatio(float value)
		{
			currentRatio = value;
			for(int i=0; i < tweens.Count; i++)
			{
				tweens[i].SetBaseRatio(value);
			}
		}
		
		// #if UNITY_EDITOR
		// [ContextMenu("Fill")]
		// private void Fill()
		// {
		// 	List<Tween> tempList = new List<Tween>();
		// 	tempList.AddRange(target);
		// 	tempList.AddRange(GetComponentsInChildren<Tween>(true));
			
		// 	// Setup();
		// 	// if(!target) target = GetComponent<Graphic>();
		// 	// if(!target) target = GetComponentInParent<Graphic>();
		// }
		// #endif
	}
}
#endif