#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/Unity/Sprite")]
	public class MonoSprite : MonoValue<Sprite>
	{
		[SerializeField]private ValueSprite nodeValue = new ValueSprite();
		protected override xValue<Sprite> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif