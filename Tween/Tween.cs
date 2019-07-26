#if xLibv3
using UnityEngine;

namespace xLib.ToolTween
{
	public abstract class Tween : BaseWorkM
	{
		public virtual void Awake(){}
		
		private void OnEnable()
		{
			SetBaseRatio(1);
		}
		
		private void OnDisable()
		{
			SetBaseRatio(0);
		}
		
		public bool isClamped = false;
		public void SetBaseRatio(float value)
		{
			if (!CanWork) return;
			if (isClamped) value = Mathf.Clamp01(value);
			SetRatio(value);
		}
		
		protected abstract void SetRatio(float value);
	}
}
#endif