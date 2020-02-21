#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolDate
{
	public class DateNowUtc : BaseTickNodeM
	{
		[SerializeField]private EventLong eventResult = null;
		
		protected override void Tick(float tickTime)
		{
			eventResult.Invoke(SafeTime.NowUtc.Ticks);
		}
	}
}
#endif