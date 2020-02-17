#if xLibv3
using UnityEngine;
using xLib.EventClass;
using xLib.xValueClass;

namespace xLib.xInput
{
	public class UseLastFloat : BaseTickNodeM
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
				if(tempNode.Value == tempNode.ValueDefault) continue;
				tempOutput = tempNode.Value;
				break;
			}
			eventFloat.Invoke(tempOutput);
		}
	}
}
#endif