#if xLibv2
using UnityEngine;

namespace xLib.ToolSocial
{
	public class ReportProgress : BaseM
	{
		public string key;
		
		public void Call(float value)
		{
			MnSocial.ins.ReportProgress(key,value);
		}
	}
}
#endif