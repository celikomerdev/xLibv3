﻿#if xLibv2
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Basic/bool")]
	public class MonoBool : MonoValue<bool>
	{
		[SerializeField]private ValueBool nodeValue = new ValueBool();
		protected override xValue<bool> Node
		{
			get
			{
				return nodeValue;
			}
		}
		
		public void Toggle()
		{
			Value = !Value;
		}
	}
}
#endif