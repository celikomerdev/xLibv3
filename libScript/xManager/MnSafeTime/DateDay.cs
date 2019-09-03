#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolDate
{
	public class DateDay : BaseTickNodeM
	{
		[SerializeField]private EventLong eventResult = new EventLong();
		
		protected override void Tick(float tickTime)
		{
			eventResult.Invoke(SafeTime.UtcNow.StartOfDay().Ticks);
		}
	}
}
#endif