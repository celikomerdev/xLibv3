#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xInput
{
	public class InputTouch : BaseTickNodeM
	{
		[Header("Output")]
		[SerializeField]private InputFinal inputFinal = null;
		
		[Header("Axis")]
		[SerializeField]private NodeFloat[] axis = new NodeFloat[0];
		
		protected override void Tick(float tickTime)
		{
			for (int i = 0; i < axis.Length; i++)
			{
				inputFinal.Cache(axis[i]);
			}
		}
	}
}
#endif