﻿#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnSingleton : SingletonM<MnSingleton>
	{
		[SerializeField]private NodeBool nodeDebug = null;
		[SerializeField]private bool isTesting = false;
		
		protected override void Awaked()
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
			xDebug.CanDebug = CanDebug;
			nodeDebug.Value = CanDebug;
			xDebug.isTesting = isTesting;
		}
	}
}
#endif