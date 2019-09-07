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
			eventInit.Invoke(init);
		}
	}
}
#endif