#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
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