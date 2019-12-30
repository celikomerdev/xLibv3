#if xLibv3
#if AdMoPub
using UnityEngine;

namespace xLib.libAdvert.xMoPub
{
	public class MnMoPub : BaseWorkM
	{
		public static bool isSdkInit = false;
		public bool IsSdkInit
		{
			get
			{
				return isSdkInit;
			}
			set
			{
				isSdkInit = value;
				Debug.LogWarning($"isSdkInit:{isSdkInit}");
			}
		}
	}
}
#endif
#endif