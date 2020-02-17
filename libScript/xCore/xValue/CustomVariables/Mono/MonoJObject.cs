#if xLibv3
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/JObject")]
	public class MonoJObject : MonoValue<JObject>
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