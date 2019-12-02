#if xLibv3
using Newtonsoft.Json.Linq;
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/JObject")]
	public class NodeJObject : NodeValue<JObject>
	{
		[SerializeField]private ValueJObject nodeValue = new ValueJObject();
		protected override xValue<JObject> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif