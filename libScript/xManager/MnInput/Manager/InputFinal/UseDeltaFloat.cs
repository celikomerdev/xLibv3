#if xLibv3
using UnityEngine;
using xLib.EventClass;
using xLib.xNode.NodeObject;

namespace xLib.xInput
{
	public class UseDeltaFloat : BaseTickNodeM
	{
		[Header("Node")]
		[SerializeField]private NodeFloat[] input = new NodeFloat[0];
		[SerializeField]private EventFloat eventFloat = new EventFloat();
		
		protected override void Tick(float tickTime)
		{
			float tempOutput = 0;
			for (int i = input.Length-1; i >= 0 ; i--)
			{
				NodeFloat tempNode = input[i];
				if(Mathf.Abs(tempOutput)<Mathf.Abs(tempNode.Value)) tempOutput = tempNode.Value;
			}
			eventFloat.Invoke(tempOutput);
		}
	}
}
#endif