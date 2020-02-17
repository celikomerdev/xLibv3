﻿#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/Basic/float")]
	public class MonoFloat : MonoValue<float>
	{
		[SerializeField]private ValueFloat nodeValue = new ValueFloat();
		protected override xValue<float> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif