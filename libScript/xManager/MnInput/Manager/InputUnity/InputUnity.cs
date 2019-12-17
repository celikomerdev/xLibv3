#if xLibv2
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xInput
{
	public class InputUnity : BaseTickM
	{
		[Header("Output")]
		[SerializeField]private InputFinal inputFinal;
		
		[Header("Axis")]
		[SerializeField]private NodeFloat[] axis;
		
		protected override void Tick(float tickTime)
		{
			for (int i = 0; i < axis.Length; i++)
			{
				axis[i].Value = Input.GetAxis(axis[i].Key);
				inputFinal.Cache(axis[i]);
			}
		}
	}
}
#endif