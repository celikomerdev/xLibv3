#if xLibv2
using UnityEngine;

namespace xLib.ToolSocial
{
	public class ShowLeaderboard : BaseMainM
	{
		public string key;
		
		public void OnClick()
		{
			MnSocial.ins.ShowLeaderBoardUI(key);
		}
	}
}
#endif