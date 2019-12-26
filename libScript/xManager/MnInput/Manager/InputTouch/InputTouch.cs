#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xInput
{
	public class InputTouch : BaseTickNodeM
	{
		[Header("Output")]
		[SerializeField]private InputFinal inputFinal;
		
		[Header("Axis")]
		[SerializeField]private NodeFloat[] axis;
		
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