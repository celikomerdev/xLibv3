#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/Basic/long")]
	public class MonoLong : MonoValue<long>
	{
		[SerializeField]private ValueLong nodeValue = new ValueLong();
		protected override xValue<long> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif