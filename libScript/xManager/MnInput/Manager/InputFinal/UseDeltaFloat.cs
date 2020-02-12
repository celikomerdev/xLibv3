#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xInput
{
	public class UseDeltaFloat : BaseTickNodeM
	{
		[Header("Node")]
		[SerializeField]private NodeFloat output = null;
		[SerializeField]private NodeFloat[] input = new NodeFloat[0];
		
		protected override void Tick(float tickTime)
		{
			float tempOutput = output.ValueDefault;
			for (int i = input.Length-1; i >= 0 ; i--)
			{
				NodeFloat tempNode = input[i];
				if(Mathf.Abs(tempOutput)<Mathf.Abs(tempNode.Value)) tempOutput = tempNode.Value;
			}
			output.Value = tempOutput;
		}
	}
}
#endif