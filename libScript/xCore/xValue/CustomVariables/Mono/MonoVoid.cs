#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/Basic/void")]
	public class MonoVoid : MonoValue<Void>
	{
		[SerializeField]private ValueVoid nodeValue = new ValueVoid();
		protected override xValue<Void> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif