#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	[CreateAssetMenu(menuName = "xLib/Node/Basic/int")]
	public class NodeInt : NodeValue<int>
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