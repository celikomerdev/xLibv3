#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnVisible : BaseWorkM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("eventBool")]
		[SerializeField]private EventBool eventVisible = new EventBool();
		
		private void OnVisible(bool value)
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat($"{this.name}:MonoOnVisible:{value}",this);
			eventVisible.Invoke(value);
		}
		
		private void OnBecameVisible()
		{
			OnVisible(true);
		}
		
		private void OnBecameInvisible()
		{
			OnVisible(false);
		}
	}
}
#endif