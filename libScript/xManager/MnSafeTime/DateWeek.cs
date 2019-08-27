#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolDate
{
	public class DateWeek : BaseTickNodeM
	{
		[SerializeField]private EventLong eventResult;
		
		protected override void Tick(float tickTime)
		{
			eventResult.Invoke(SafeTime.UtcNow.StartOfWeek().Ticks);
		}
	}
}
#endif