#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xInput
{
	public class UseLastFloat : BaseTickNodeM
	{
		[Header("Node")]
		[SerializeField]private NodeFloat output = null;
		[SerializeField]private NodeFloat[] input = new NodeFloat[0];
		
		protected override void Tick(float tickTime)
		{
			for (int i = input.Length-1; i >= 0 ; i--)
			{
				NodeFloat tempNode = input[i];
				if(tempNode.Value == tempNode.ValueDefault) continue;
				output.Value = tempNode.Value;
				return;
			}
			output.Value = output.ValueDefault;
		}
	}
}
#endif