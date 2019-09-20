#if xLibv3
using UnityEngine;

namespace xLib.libAdvert.xEmptyAds
{
	public class AdvertEmpty : AdvertBase
	{
		#region Override
		protected override void Load()
		{
			SetLoadedBase(true);
		}
		
		protected override void Show()
		{
			OnShowBase();
			OnCloseBase();
			OnRewardBase(1);
		}
		#endregion
	}
}
#endif