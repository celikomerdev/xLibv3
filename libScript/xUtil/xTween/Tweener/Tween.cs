#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public abstract class Tween : BaseWorkM
	{
		public virtual void Awake(){}
		
		private void OnEnable()
		{
			BaseRatio = 1;
		}
		
		private void OnDisable()
		{
			BaseRatio = 0;
		}
		
		public bool isClamped = false;
		public float BaseRatio
		{
			set
			{
				if (!CanWork) return;
				if (CanDebug) Debug.LogFormat(this,this.name+":BaseRatio:{0}",value);
				if (isClamped) value = Mathf.Clamp01(value);
				SetRatio(value);
			}
		}
		
		public void SetBaseRatio(float value)
		{
			BaseRatio = value;
		}
		
		protected abstract void SetRatio(float value);
	}
}
#endif