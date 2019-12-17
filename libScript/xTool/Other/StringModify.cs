#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolConvert
{
	public class StringModify : BaseMainM
	{
		[SerializeField]private StringModification stringModification = StringModification.DontModify;
		[SerializeField]private EventString eventResult = new EventString();
		
		public void Call(string value)
		{
			eventResult.Invoke(value.ApplyModificationExt(stringModification));
		}
	}
}
#endif