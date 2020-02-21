#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolDate
{
	public class DateNowLocal : BaseTickNodeM
	{
		[SerializeField]private EventLong eventResult = null;
		
		protected override void Tick(float tickTime)
		{
			eventResult.Invoke(SafeTime.NowLocal.Ticks);
		}
	}
}
#endif