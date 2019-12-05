﻿#if xLibv2
using System.Collections;
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xNode.NodeObject
{
	[CreateAssetMenu(menuName = "xLib/Node/HashTable")]
	public class NodeHashTable : NodeValue<Hashtable>
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