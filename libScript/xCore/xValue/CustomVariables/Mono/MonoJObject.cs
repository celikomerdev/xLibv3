#if xLibv3
using Newtonsoft.Json.Linq;
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
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
		
		public void SetTokenSafe(string path,object value)
		{
			if(Node.Value.SetTokenSafe(path,value)) Node.Call();
		}
	}
}
#endif