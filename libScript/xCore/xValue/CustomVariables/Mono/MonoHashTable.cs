#if xLibv2
using System.Collections;
using UnityEngine;

namespace xLib.xValueClass
{
	// [CreateAssetMenu(menuName = "xLib/Node/HashTable")]
	public class MonoHashTable : MonoValue<Hashtable>
	{
		[SerializeField]private ValueHashTable nodeValue = new ValueHashTable();
		protected override xValue<Hashtable> Node
		{
			get
			{
				return nodeValue;
			}
		}
	}
}
#endif