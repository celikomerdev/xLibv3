#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolDate
{
	public class DateWeek : BaseTickNodeM
	{
		[SerializeField]private EventLong eventResult = new EventLong();
		
		protected override void Tick(float tickTime)
		{
			eventResult.Invoke(SafeTime.NowUtc.StartOfWeek().Ticks);
		}
	}
}
#endif