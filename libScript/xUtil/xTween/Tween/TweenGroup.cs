#if xLibv3
using System;
using System.Collections.Generic;
using UnityEngine;
using xLib.Mathx;

namespace xLib.xTween
{
	public class TweenGroup : Tween
	{
		[SerializeField]private Vector2 remap = new Vector2(0,1);
		[SerializeField]private GameObject[] target = new GameObject[0];
		private List<Tween> tweens = new List<Tween>();
		
		public override void Awake()
		{
			Setup();
		}
		
		[ContextMenu("Setup")]
		private void Setup()
		{
			tweens = new List<Tween>();
			for (int i = 0; i < target.Length; i++)
			{
				tweens.AddRange(target[i].GetGenerics<Tween>());
			}
			tweens.Remove(this);
			tweens.RemoveAll(item => item.CanWork == false);
			
			for (int i = 0; i < tweens.Count; i++)
			{
				tweens[i].Awake();
			}
		}
		
		protected override void SetRatio(float value)
		{
			value = MathFloat.Remap(remap.x,remap.y,value);
			ApplyRatio(value);
		}
		
		[NonSerialized]public float currentRatio = 0f;
		protected virtual void ApplyRatio(float value)
		{
			currentRatio = value;
			for(int i=0; i < tweens.Count; i++)
			{
				tweens[i].SetBaseRatio(value);
			}
		}
		
		#if UNITY_EDITOR
		[ContextMenu("Fill")]
		private void Fill()
		{
			if(target.Length>0) return;
			target = Array.ConvertAll(transform.FindWithComponent<Tween>(true).ToArray(),item=>item.gameObject);
			Setup();
		}
		#endif
	}
	
	public abstract class TweenGroup2 : TweenGroup
	{
		private bool isForward = false;
		private float valueLast = 0;
		protected override void ApplyRatio(float value)
		{
			if(value!=valueLast) isForward = (value>valueLast);
			valueLast = value;
			
			if(isForward)
			{
				base.ApplyRatio(RatioForward(value));
			}
			else
			{
				base.ApplyRatio(RatioBackward(value));
			}
		}
		
		protected abstract float RatioForward(float value);
		protected abstract float RatioBackward(float value);
	}
}
#endif