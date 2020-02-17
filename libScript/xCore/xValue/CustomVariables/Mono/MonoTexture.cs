﻿#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/Unity/Texture")]
	public class MonoTexture : MonoValue<Texture>
	{
		[SerializeField]private ValueTexture nodeValue = new ValueTexture();
		protected override xValue<Texture> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif