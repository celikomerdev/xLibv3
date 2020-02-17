#if xLibv3
using UnityEngine;
using xLib.EventClass;
using xLib.xValueClass;

namespace xLib.xInput
{
	public class UseGreatFloat : BaseTickNodeM
	{
		[Header("Node")]
		[SerializeField]public float multiplier = 1;
		[SerializeField]private NodeFloat[] input = new NodeFloat[0];
		[SerializeField]private EventFloat eventFloat = new EventFloat();
		
		protected override void Tick(float tickTime)
		{
			float tempOutput = 0;
			for (int i = input.Length-1; i >= 0 ; i--)
			{
				NodeFloat tempNode = input[i];
				if(tempOutput*multiplier<tempNode.Value*multiplier) tempOutput = tempNode.Value;
			}
			eventFloat.Invoke(tempOutput);
		}
	}
}
#endif