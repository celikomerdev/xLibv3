#if xLibv3
using System.Collections.Generic;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xInput
{
	public class InputFinal : BaseTickNodeM
	{
		[Header("Axis")]
		[SerializeField]private NodeFloat[] axis = new NodeFloat[0];
		private readonly Dictionary<string,NodeFloat> dictionary = new Dictionary<string,NodeFloat>();
		
		protected override void Awaked()
		{
			for (int i = 0; i < axis.Length; i++)
			{
				dictionary.Add(axis[i].Key,axis[i]);
			}
		}
		
		protected override void Tick(float tickTime)
		{
			Consume();
		}
		
		private void Consume()
		{
			for (int i = 0; i < axis.Length; i++)
			{
				axis[i].Consume();
			}
		}
		
		internal void Cache(NodeFloat value)
		{
			if(value.Key == "") return;
			if(value.Value == 0) return;
			dictionary[value.Key].ValueCache = value.Value;
		}
	}
}
#endif