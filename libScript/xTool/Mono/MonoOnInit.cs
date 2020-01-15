#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MonoOnInit : BaseInitM
	{
		[SerializeField]private EventBool eventInit = new EventBool();
		
		protected override void OnInit(bool init)
		{
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat($"{this.name}:OnInit:{init}",this);
			eventInit.Invoke(init);
		}
	}
}
#endif