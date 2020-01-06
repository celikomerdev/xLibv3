﻿#if xLibv3
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
				if(CanDebug) Debug.LogWarning($"isSdkInit:{isSdkInit}");
			}
		}
	}
}
#else
namespace xLib.libAdvert.xMoPub
{
	public class MnMoPub : BaseWorkM
	{
		public bool IsSdkInit
		{
			get
			{
				return false;
			}
			set{}
		}
	}
}
#endif
#endif