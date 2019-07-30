#if xLibv2
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	// [CreateAssetMenu(menuName = "xLib/Node/Basic/int")]
	public class MonoInt : MonoValue<int>
	{
		[SerializeField]private ValueInt nodeValue = new ValueInt();
		protected override xValue<int> Node
		{
			get
			{
				return nodeValue;
			}
		}
		
		public bool ValueAddBool
		{
			set
			{
				if(value) ValueAdd = 1;
				else ValueAdd = -1;
			}
		}
	}
}
#endif