﻿#if xLibv2
using UnityEngine;

namespace xLib.ToolSocial
{
	public class ShowAchievement : BaseM
	{
		public string key;
		
		public void OnClick()
		{
			MnSocial.ins.ShowAchievementUI(key);
		}
	}
}
#endif