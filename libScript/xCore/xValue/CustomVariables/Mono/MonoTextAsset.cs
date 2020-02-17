#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/Unity/TextAsset")]
	public class MonoTextAsset : MonoValue<TextAsset>
	{
		[SerializeField]private ValueTextAsset nodeValue = new ValueTextAsset();
		protected override xValue<TextAsset> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif