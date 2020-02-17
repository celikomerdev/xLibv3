#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/Basic/string")]
	public class MonoString : MonoValue<string>
	{
		[SerializeField]private ValueString nodeValue = new ValueString();
		protected override xValue<string> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif