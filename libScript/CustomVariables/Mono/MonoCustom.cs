#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Complex/Custom")]
	public class MonoCustom : MonoValue<ValueCustom.Token>
	{
		[SerializeField]private ValueCustom nodeValue = new ValueCustom();
		protected override xValue<ValueCustom.Token> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif