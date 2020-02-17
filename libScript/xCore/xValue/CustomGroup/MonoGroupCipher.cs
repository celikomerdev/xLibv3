﻿#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/Group/Cipher")]
	public class MonoGroupCipher : MonoGroup
	{
		[SerializeField]private ValueGroupCipher nodeValue = new ValueGroupCipher();
		protected override xValue<ObjectGroup> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif