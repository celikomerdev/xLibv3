#if xLibv3
using UnityEngine;

namespace xLib.xValueClass
{
	[CreateAssetMenu(menuName = "xLib/Node/Basic/bool")]
	public class NodeBool : NodeValue<bool>
	{
		[SerializeField]private ValueBool nodeValue = new ValueBool();
		protected override xValue<bool> Node
		{
			get
			{
				return nodeValue;
			}
		}
		
		public void Toggle()
		{
			Value = !Value;
		}
		
		public int ValueInt
		{
			set
			{
				Value = (value>0);
			}
		}
	}
}
#endif