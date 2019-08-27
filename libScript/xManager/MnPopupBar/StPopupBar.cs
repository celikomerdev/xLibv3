#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class StPopupBar
	{
		public static void QueueMessage(string value,bool isLocalized=true)
		{
			// if(isLocalized) value = MnLocalize.GetValue(value);
			
			if(MnPopupBar.ins)
			{
				MnPopupBar.ins.queueString.QueueMessage(value);
				return;
			}
			
			Debug.LogWarningFormat("StPopupBar:QueueMessage:{0}",value);
		}
	}
}
#endif