#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolConvert
{
	public class ConvertToTexture : BaseM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("onConvert")]
		[SerializeField]private EventTexture eventResult = new EventTexture();
		
		public void FromWWW(WWW value)
		{
			eventResult.Invoke(value.texture);
		}
	}
}
#endif