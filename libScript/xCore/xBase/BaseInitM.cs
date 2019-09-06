#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class BaseInitM : BaseWorkM
	{
		protected bool isInit;
		public void Init(bool value)
		{
			if(isInit == value) return;
			isInit = value;
			if(CanDebug) Debug.LogFormat(this,this.name+":Init:{0}",isInit);
			OnInit(isInit);
			SetDebug();
		}
		protected virtual void OnInit(bool init){}
		
		
		#region Flow
		public BaseInitM()
		{
			MnThread.Register(Awake);
		}
		
		private void Awake()
		{
			Init(true);
		}
		
		private void OnDestroy()
		{
			Init(false);
		}
		
		[ContextMenu ("ReInit")]
		private void ReInit()
		{
			Init(!isInit);
			Init(!isInit);
		}
		#endregion
	}
}
#endif